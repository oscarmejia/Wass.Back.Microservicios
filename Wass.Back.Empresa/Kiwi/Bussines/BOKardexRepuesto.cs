using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.KardexRepuesto;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOKardexRepuesto
    {
        private readonly DALCKardexRepuesto _dalc;
        private readonly DALCRecepcionRepuestos _dalcRecepcionRepuestos;
        private readonly DALCAjustesAlmacenes _dalcAjustesAlmacenes;
        private readonly DALCTransferenciasInternasAlmacenes _dalcTransferenciasInternasAlmacenes;
        private readonly DALCOrdenEntregaAlmacen _dalcOrdenEntregaAlmacen;
        private readonly DALCAlmacen _dalcAlmacen;
        private readonly DALCDañosRepuestos _dalcDañosRepuestos;


        public BOKardexRepuesto(EmpresaContext context)
        {
            _dalc = new DALCKardexRepuesto(context);
            _dalcRecepcionRepuestos = new DALCRecepcionRepuestos(context);
            _dalcAjustesAlmacenes = new DALCAjustesAlmacenes(context);
            _dalcTransferenciasInternasAlmacenes = new DALCTransferenciasInternasAlmacenes(context);
            _dalcOrdenEntregaAlmacen = new DALCOrdenEntregaAlmacen(context);
            _dalcAlmacen = new DALCAlmacen(context);
            _dalcDañosRepuestos = new DALCDañosRepuestos(context);
        }


        public async Task<ResponseBase<Object>> trazabilidadRepuestoAlmacenFisico(long idAlmacen, long idRepuesto, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var fechaInicial = (DateTime.Now.Date - fechaInicio).TotalDays;
                var fechaFinal = (DateTime.Now.Date - fechaFin).TotalDays;

                var almacen = await _dalcAlmacen.Get(idAlmacen);
                var consulta = new List<Object>();

                var dias = new List<double>();

                if (almacen != null && almacen.tipo == 1)
                {
                    var transferenciaInterna = await _dalcTransferenciasInternasAlmacenes.GetTodasPorAlmacenRepuesto(idAlmacen, idRepuesto);
                    var recepcionAlmacen = await _dalcRecepcionRepuestos.GetTodasPorAlmacenRepuesto(idAlmacen, idRepuesto);
                    var ajusteAlmacen = await _dalcAjustesAlmacenes.GetTodasPorAlmacenRepuesto(idAlmacen, idRepuesto);
                    var dañosRepuestos = await _dalcDañosRepuestos.GetTodasPorAlmacenRepuesto(idAlmacen, idRepuesto);

                    double diasFechaTransferencia = 0;
                    double diasFechaRecepcion = 0;
                    double diasFechaAjustes = 0;
                    double diasFechaDaños = 0;

                    if (transferenciaInterna != null)
                    {

                        foreach (var item in transferenciaInterna)
                        {
                            if (item.estado == 180 || item.estado == 179)
                            {
                                diasFechaTransferencia = (DateTime.Now.Date - item.fechaHora).TotalDays;

                                dias.Add(diasFechaTransferencia);
                            }
                        }
                    }
                    if (recepcionAlmacen != null)
                    {
                        foreach (var item in recepcionAlmacen)
                        {
                            diasFechaRecepcion = (DateTime.Now.Date - item.fechaHora).TotalDays;

                            dias.Add(diasFechaRecepcion);
                        }
                    }
                    if (ajusteAlmacen != null)
                    {
                        foreach (var item in ajusteAlmacen)
                        {
                            diasFechaAjustes = (DateTime.Now.Date - item.fechaHora).TotalDays;

                            dias.Add(diasFechaAjustes);
                        }
                    }
                    if (dañosRepuestos != null)
                    {
                        foreach (var item in dañosRepuestos)
                        {
                            diasFechaDaños = (DateTime.Now.Date - item.fechaHora).TotalDays;

                            dias.Add(diasFechaDaños);
                        }
                    }

                    //TipoOperacion
                    //ajustesAlmacen = 1
                    //DañosRepuestos = 2
                    //RecepcionRepuestos = 3
                    //TransferenciaInterna = 4
                    //OrdenEntrega = 5

                    if (dias.Count > 0)
                    {
                        dias.Sort();

                        foreach (double i in dias)
                        {
                            if (i <= fechaInicial && i >= fechaFinal)
                            {
                                foreach (var item in ajusteAlmacen)
                                {
                                    diasFechaAjustes = (DateTime.Now.Date - item.fechaHora).TotalDays;
                                    if (i == diasFechaAjustes)
                                    {
                                        var ajusteAlmacenRequest = new AjustesRequest()
                                        {
                                            idAjustesAlmacenes = item.idAjustesAlmacenes,
                                            idRepuesto = item.idRepuesto,
                                            cantidadAnterior = item.cantidadAnterior,
                                            cantidadNueva = item.cantidadNueva,
                                            fechaHora = item.fechaHora,
                                            idAlmacen = item.idAlmacen,
                                            motivo = item.motivo,
                                            idUsuario = item.idUsuario,
                                            tipoOperacion = 1,
                                            existenciaActual = item.existenciaActual
                                        };
                                        consulta.Add(ajusteAlmacenRequest);
                                    }
                                }
                                foreach (var item in dañosRepuestos)
                                {
                                    diasFechaDaños = (DateTime.Now.Date - item.fechaHora).TotalDays;
                                    if (i == diasFechaDaños)
                                    {
                                        var dañosRepuestosRequest = new DañosRequest()
                                        {
                                            idDañosRepuestos = item.idDañosRepuestos,
                                            idRepuesto = item.idRepuesto,
                                            fechaHora = item.fechaHora,
                                            cantidad = item.cantidad,
                                            motivo = item.motivo,
                                            idAlmacen = item.idAlmacen,
                                            idUsuario = item.idUsuario,
                                            tipoOperacion = 2,
                                            existenciaActual = item.existenciaActual
                                        };
                                        consulta.Add(dañosRepuestosRequest);
                                    }
                                }

                                foreach (var item in recepcionAlmacen)
                                {
                                    diasFechaRecepcion = (DateTime.Now.Date - item.fechaHora).TotalDays;
                                    if (i == diasFechaRecepcion)
                                    {
                                        var recepcionAlmacenRequest = new RecepcionRequest()
                                        {
                                            idRecepcionRepuestos = item.idRecepcionRepuestos,
                                            fechaHora = item.fechaHora,
                                            repuestos = item.repuestos,
                                            idAlmacen = item.idAlmacen,
                                            idUsuario = item.idUsuario,
                                            tipoOperacion = 3
                                        };
                                        consulta.Add(recepcionAlmacenRequest);
                                    }
                                }
                                foreach (var item in transferenciaInterna)
                                {
                                    if (item.estado == 180 || item.estado == 179)
                                    {
                                        diasFechaTransferencia = (DateTime.Now.Date - item.fechaHora).TotalDays;
                                        if (i == diasFechaTransferencia)
                                        {

                                            var transferenciaInternaRequest = new TransferenciasRequest()
                                            {
                                                idTransferenciasInternasAlmacenes = item.idTransferenciasInternasAlmacenes,
                                                idAlmacenEmisor = item.idAlmacenEmisor,
                                                idAlmacenReceptor = item.idAlmacenReceptor,
                                                repuestos = item.repuestos,
                                                fechaHora = item.fechaHora,
                                                motivoTransferencia = item.motivoTransferencia,
                                                ordenTrabajo = item.ordenTrabajo,
                                                tipoOperacion = 4,
                                                estado = item.estado
                                            };
                                            consulta.Add(transferenciaInternaRequest);
                                        }
                                    }

                                }
                            }

                        }
                    }


                }



                if (consulta != null)
                {
                    return new ResponseBase<Object>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = consulta
                    };
                }
                else
                {
                    return new ResponseBase<Object>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<Object>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Object>> trazabilidadRepuestoAlmacenVirtual(long idAlmacen, long idRepuesto, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var fechaInicial = (DateTime.Now.Date - fechaInicio).TotalDays;
                var fechaFinal = (DateTime.Now.Date - fechaFin).TotalDays;

                var almacen = await _dalcAlmacen.Get(idAlmacen);
                var consulta = new List<Object>();
                var dias = new List<double>();

                if (almacen != null && almacen.tipo == 2)
                {
                    var ordenEntrega = await _dalcOrdenEntregaAlmacen.GetTodasPorAlmacenRepuesto(idAlmacen, idRepuesto);
                    var transferenciaInterna = await _dalcTransferenciasInternasAlmacenes.GetTodasPorAlmacenRepuesto(idAlmacen, idRepuesto);
                    var dañosRepuestos = await _dalcDañosRepuestos.GetTodasPorAlmacenRepuesto(idAlmacen, idRepuesto);

                    double diasFechaOrden = 0;
                    double diasFechaTransferencia = 0;
                    double diasFechaDaños = 0;

                    if (transferenciaInterna != null)
                    {

                        foreach (var item in transferenciaInterna)
                        {
                            if (item.estado == 180 || item.estado == 179)
                            {
                                diasFechaTransferencia = (DateTime.Now.Date - item.fechaHora).TotalDays;

                                dias.Add(diasFechaTransferencia);
                            }

                        }
                    }
                    if (ordenEntrega != null)
                    {
                        foreach (var item in ordenEntrega)
                        {
                            diasFechaOrden = (DateTime.Now.Date - item.fechaHora).TotalDays;

                            dias.Add(diasFechaOrden);
                        }
                    }
                    if (dañosRepuestos != null)
                    {
                        foreach (var item in dañosRepuestos)
                        {
                            diasFechaDaños = (DateTime.Now.Date - item.fechaHora).TotalDays;

                            dias.Add(diasFechaDaños);
                        }
                    }

                    //TipoOperacion
                    //ajustesAlmacen = 1
                    //DañosRepuestos = 2
                    //RecepcionRepuestos = 3
                    //TransferenciaInterna = 4
                    //OrdenEntrega = 5

                    if (dias.Count > 0)
                    {
                        dias.Sort();

                        foreach (double i in dias)
                        {
                            if (i <= fechaInicial && i >= fechaFinal)
                            {
                                foreach (var item in dañosRepuestos)
                                {
                                    diasFechaDaños = (DateTime.Now.Date - item.fechaHora).TotalDays;
                                    if (i == diasFechaDaños)
                                    {
                                        var dañosRepuestosRequest = new DañosRequest()
                                        {
                                            idDañosRepuestos = item.idDañosRepuestos,
                                            idRepuesto = item.idRepuesto,
                                            fechaHora = item.fechaHora,
                                            cantidad = item.cantidad,
                                            motivo = item.motivo,
                                            idAlmacen = item.idAlmacen,
                                            idUsuario = item.idUsuario,
                                            tipoOperacion = 2,
                                            existenciaActual = item.existenciaActual
                                        };
                                        consulta.Add(dañosRepuestosRequest);
                                    }
                                }
                                foreach (var item in ordenEntrega)
                                {
                                    diasFechaOrden = (DateTime.Now.Date - item.fechaHora).TotalDays;
                                    if (i == diasFechaOrden)
                                    {
                                        var ordenEntregaRequest = new EntregaRequest()
                                        {
                                            idOrdenEntregaAlmacen = item.idOrdenEntregaAlmacen,
                                            repuestos = item.repuestos,
                                            idOrdenTrabajo = item.idOrdenTrabajo,
                                            fechaHora = item.fechaHora,
                                            idAlmacen = item.idAlmacen,
                                            idCuadrilla = item.idCuadrilla,
                                            idSede = item.idSede,
                                            tipoOperacion = 5
                                        };
                                        consulta.Add(ordenEntregaRequest);
                                    }
                                }
                                foreach (var item in transferenciaInterna)
                                {
                                    if (item.estado == 180 || item.estado == 179)
                                    {
                                        diasFechaTransferencia = (DateTime.Now.Date - item.fechaHora).TotalDays;
                                        if (i == diasFechaTransferencia)
                                        {

                                            var transferenciaInternaRequest = new TransferenciasRequest()
                                            {
                                                idTransferenciasInternasAlmacenes = item.idTransferenciasInternasAlmacenes,
                                                idAlmacenEmisor = item.idAlmacenEmisor,
                                                idAlmacenReceptor = item.idAlmacenReceptor,
                                                repuestos = item.repuestos,
                                                fechaHora = item.fechaHora,
                                                motivoTransferencia = item.motivoTransferencia,
                                                ordenTrabajo = item.ordenTrabajo,
                                                tipoOperacion = 4,
                                                estado = item.estado
                                            };
                                            consulta.Add(transferenciaInternaRequest);
                                        }
                                    }

                                }
                            }

                        }
                    }


                }
                if (consulta != null)
                {
                    return new ResponseBase<Object>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = consulta
                    };
                }
                else
                {
                    return new ResponseBase<Object>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<Object>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Object>> trazabilidadRepuestoAlmacenVirtualMobile(long idAlmacen, DateTime fechaActual)
        {
            try
            {

                DateTime fecha = Convert.ToDateTime(fechaActual);


                fecha = fecha.AddDays(-30);

                Console.WriteLine(fecha);

                var fechaInicial = (DateTime.Now.Date - fechaActual.AddDays(-30)).TotalDays;
                var fechaFinal = (DateTime.Now.Date - fechaActual).TotalDays;

                var almacen = await _dalcAlmacen.Get(idAlmacen);
                var consulta = new List<Object>();
                var dias = new List<double>();

                var listaTransferencias = new List<TransferenciasInternasAlmacenes>();
                var listasOrdenesTrabajo = new List<OrdenEntregaAlmacen>();

                if (almacen != null)
                {

                    if (almacen.RepuestosAlmacen.Count > 0)
                    {
                        var ordenEntrega = await _dalcOrdenEntregaAlmacen.GetPorFechaAlmacen(idAlmacen);
                        if (ordenEntrega != null)
                        {
                            foreach (var ordenTra in ordenEntrega)
                            {
                                listasOrdenesTrabajo.Add(ordenTra);
                            }
                        };
                        foreach (var item in almacen.RepuestosAlmacen)
                        {

                            var transferenciaInterna = await _dalcTransferenciasInternasAlmacenes.GetTodasPorAlmacenRepuestoMobile(idAlmacen, item.idRepuestos);
                            if (transferenciaInterna != null)
                            {
                                foreach (var trans in transferenciaInterna)
                                {
                                    if (trans.estado == 180 || trans.estado == 179)
                                    {
                                        if (!listaTransferencias.Contains(trans))
                                        {
                                            listaTransferencias.Add(trans);
                                        }
                                    }

                                }
                            }

                        }

                    }
                    //var ordenEntrega = await _dalcOrdenEntregaAlmacen.GetTodasPorAlmacenRepuesto(idAlmacen, 46);
                    //var transferenciaInterna = await _dalcTransferenciasInternasAlmacenes.GetTodasPorAlmacenRepuestoMobile(idAlmacen, 46);


                    double diasFechaOrden = 0;
                    double diasFechaTransferencia = 0;

                    if (listaTransferencias != null)
                    {

                        foreach (var item in listaTransferencias)
                        {
                            if (item.estado == 180 || item.estado == 179)
                            {
                                diasFechaTransferencia = (DateTime.Now.Date - item.fechaHora).TotalDays;

                                dias.Add(diasFechaTransferencia);
                            }

                        }
                    }
                    if (listasOrdenesTrabajo != null)
                    {
                        foreach (var item in listasOrdenesTrabajo)
                        {
                            diasFechaOrden = (DateTime.Now.Date - item.fechaHora).TotalDays;

                            dias.Add(diasFechaOrden);
                        }
                    }


                    //TipoOperacion
                    //ajustesAlmacen = 1
                    //DañosRepuestos = 2
                    //RecepcionRepuestos = 3
                    //TransferenciaInterna = 4
                    //OrdenEntrega = 5

                    if (dias.Count > 0)
                    {
                        dias.Sort();

                        foreach (double i in dias)
                        {
                            if (i <= fechaInicial && i >= fechaFinal)
                            {

                                foreach (var item in listasOrdenesTrabajo)
                                {
                                    diasFechaOrden = (DateTime.Now.Date - item.fechaHora).TotalDays;
                                    if (i == diasFechaOrden)
                                    {
                                        var ordenEntregaRequest = new EntregaRequest()
                                        {
                                            idOrdenEntregaAlmacen = item.idOrdenEntregaAlmacen,
                                            repuestos = item.repuestos,
                                            idOrdenTrabajo = item.idOrdenTrabajo,
                                            fechaHora = item.fechaHora,
                                            idAlmacen = item.idAlmacen,
                                            idCuadrilla = item.idCuadrilla,
                                            idSede = item.idSede,
                                            tipoOperacion = 5
                                        };
                                        consulta.Add(ordenEntregaRequest);
                                    }
                                }
                                foreach (var item in listaTransferencias)
                                {
                                    if (item.estado == 180 || item.estado == 179)
                                    {
                                        diasFechaTransferencia = (DateTime.Now.Date - item.fechaHora).TotalDays;
                                        if (i == diasFechaTransferencia)
                                        {

                                            var transferenciaInternaRequest = new TransferenciasRequest()
                                            {
                                                idTransferenciasInternasAlmacenes = item.idTransferenciasInternasAlmacenes,
                                                idAlmacenEmisor = item.idAlmacenEmisor,
                                                idAlmacenReceptor = item.idAlmacenReceptor,
                                                repuestos = item.repuestos,
                                                fechaHora = item.fechaHora,
                                                motivoTransferencia = item.motivoTransferencia,
                                                ordenTrabajo = item.ordenTrabajo,
                                                tipoOperacion = 4,
                                                estado = item.estado
                                            };
                                            consulta.Add(transferenciaInternaRequest);
                                        }
                                    }

                                }
                            }

                        }
                    }


                }
                if (consulta != null)
                {
                    return new ResponseBase<Object>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = consulta
                    };
                }
                else
                {
                    return new ResponseBase<Object>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<Object>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
    }
}
