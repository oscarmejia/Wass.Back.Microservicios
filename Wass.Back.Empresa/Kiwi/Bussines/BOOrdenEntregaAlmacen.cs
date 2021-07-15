using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.OrdenEntregaAlmacen;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOOrdenEntregaAlmacen
    {
        private readonly DALCOrdenEntregaAlmacen _dalc;
        private readonly DALCAlmacen _dalcAlmacen;
        private readonly DALCRepuestosAlmacen _dalcRepuestosAlmacen;
        private readonly DALCRepuestos _dalcRepuestos;

        public BOOrdenEntregaAlmacen(EmpresaContext context)
        {
            _dalc = new DALCOrdenEntregaAlmacen(context);
            _dalcAlmacen = new DALCAlmacen(context);
            _dalcRepuestosAlmacen = new DALCRepuestosAlmacen(context);
            _dalcRepuestos = new DALCRepuestos(context);
        }

        public async Task<ResponseBase<List<OrdenEntregaAlmacenRequest>>> GetTodas()
        {
            try
            {
                var ordenEntregaAlmacen = await _dalc.GetTodas();
                var datos = new List<OrdenEntregaAlmacenRequest>();

                if (ordenEntregaAlmacen != null && ordenEntregaAlmacen.Count > 0)
                {
                    foreach (var item in ordenEntregaAlmacen)
                    {
                        var data = new OrdenEntregaAlmacenRequest()
                        {
                            idOrdenEntregaAlmacen = item.idOrdenEntregaAlmacen,
                            repuestos = !String.IsNullOrEmpty(item.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosOrdenEntregaRequest>>(item.repuestos) : new List<RepuestosOrdenEntregaRequest>(),
                            idOrdenTrabajo = item.idOrdenTrabajo,
                            fechaHora = item.fechaHora,
                            idAlmacen = item.idAlmacen,
                            idCuadrilla = item.idCuadrilla,
                            idSede = item.idSede
                        };

                        datos.Add(data);
                    }
                    return new ResponseBase<List<OrdenEntregaAlmacenRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<OrdenEntregaAlmacenRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo ningun resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<OrdenEntregaAlmacenRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<OrdenEntregaAlmacenPromedioRequest>> GetPromedioUltimosSeisMeses(long idRepuesto, DateTime fechaActual, long idSede)
        {
            try
            {
                var ordenEntregaFecha = await _dalc.GetPorFecha(idSede);
                var repuestoID = await _dalcRepuestos.GetID(idRepuesto);

                var primerDiaMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);

                DateTime ultimosSeisMeses = primerDiaMes.AddMonths(-6);


                var contadorCantidadRepuestos = 0;
                float suma = 0;
                if (ordenEntregaFecha != null && ordenEntregaFecha.Count > 0 && repuestoID != null)
                {
                    foreach (var item in ordenEntregaFecha)
                    {
                        var fechaInicial = DateTime.Compare(item.fechaHora, primerDiaMes);
                        var fechaFinal = DateTime.Compare(item.fechaHora, ultimosSeisMeses);
                        if (fechaInicial < 0 && fechaFinal >= 0)
                        {
                            var count = 0;
                            var contadorRepuestos = 0;

                            dynamic jsonObjIdRepuesto = JsonConvert.DeserializeObject(item.repuestos);
                            foreach (var repuesto in jsonObjIdRepuesto)
                            {
                                var buscarIdRepuesto = jsonObjIdRepuesto[count]["idRepuesto"].ToString();
                                if (Convert.ToInt64(buscarIdRepuesto) == idRepuesto)
                                {
                                    dynamic jsonObj = JsonConvert.DeserializeObject(item.repuestos);
                                    var cantidad = jsonObj[count]["cantidad"].ToString();

                                    contadorRepuestos++;
                                    suma += Convert.ToDouble(cantidad);
                                }
                                count++;
                            }
                            contadorCantidadRepuestos += contadorRepuestos;

                        }

                    }
                    var data = new OrdenEntregaAlmacenPromedioRequest()
                    {
                        idSede = idSede,
                        promedio = suma / 6,
                        repuesto = repuestoID
                    };
                    return new ResponseBase<OrdenEntregaAlmacenPromedioRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<OrdenEntregaAlmacenPromedioRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo ningun resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<OrdenEntregaAlmacenPromedioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<OrdenEntregaAlmacenPromedioPorAlmacenRequest>> GetPromedioUltimosSeisMesesPorAlmacen(long idRepuesto, DateTime fechaActual, long idAlmacen)
        {
            try
            {
                var ordenEntregaFecha = await _dalc.GetPorFechaAlmacen(idAlmacen);
                var repuestoID = await _dalcRepuestos.GetID(idRepuesto);
                var primerDiaMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);

                DateTime ultimosSeisMeses = primerDiaMes.AddMonths(-6);


                var contadorCantidadRepuestos = 0;
                float suma = 0;
                if (ordenEntregaFecha != null && ordenEntregaFecha.Count > 0 && repuestoID != null)
                {
                    foreach (var item in ordenEntregaFecha)
                    {
                        var fechaInicial = DateTime.Compare(item.fechaHora, primerDiaMes);
                        var fechaFinal = DateTime.Compare(item.fechaHora, ultimosSeisMeses);
                        if (fechaInicial < 0 && fechaFinal >= 0)
                        {
                            var count = 0;
                            var contadorRepuestos = 0;

                            dynamic jsonObjIdRepuesto = JsonConvert.DeserializeObject(item.repuestos);
                            foreach (var repuesto in jsonObjIdRepuesto)
                            {
                                var buscarIdRepuesto = jsonObjIdRepuesto[count]["idRepuesto"].ToString();
                                if (Convert.ToInt64(buscarIdRepuesto) == idRepuesto)
                                {
                                    dynamic jsonObj = JsonConvert.DeserializeObject(item.repuestos);
                                    var cantidad = jsonObj[count]["cantidad"].ToString();

                                    contadorRepuestos++;
                                    suma += Convert.ToDouble(cantidad);
                                }
                                count++;
                            }
                            contadorCantidadRepuestos += contadorRepuestos;

                        }

                    }

                    var data = new OrdenEntregaAlmacenPromedioPorAlmacenRequest()
                    {
                        idAlmacen = idAlmacen,
                        promedio = suma / 6,
                        repuesto = repuestoID
                    };
                    return new ResponseBase<OrdenEntregaAlmacenPromedioPorAlmacenRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<OrdenEntregaAlmacenPromedioPorAlmacenRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo ningun resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<OrdenEntregaAlmacenPromedioPorAlmacenRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<OrdenEntregaAlmacenRequest>> GetPorId(long idOrdenEntregaAlmacen)
        {
            try
            {
                var ordenEntregaAlmacenRequest = await _dalc.Get(idOrdenEntregaAlmacen);
                if (ordenEntregaAlmacenRequest != null)
                {
                    var data = new OrdenEntregaAlmacenRequest()
                    {
                        idOrdenEntregaAlmacen = ordenEntregaAlmacenRequest.idOrdenEntregaAlmacen,
                        repuestos = !String.IsNullOrEmpty(ordenEntregaAlmacenRequest.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosOrdenEntregaRequest>>(ordenEntregaAlmacenRequest.repuestos) : new List<RepuestosOrdenEntregaRequest>(),
                        idOrdenTrabajo = ordenEntregaAlmacenRequest.idOrdenTrabajo,
                        fechaHora = ordenEntregaAlmacenRequest.fechaHora,
                        idAlmacen = ordenEntregaAlmacenRequest.idAlmacen,
                        idCuadrilla = ordenEntregaAlmacenRequest.idCuadrilla,
                        idSede = ordenEntregaAlmacenRequest.idSede
                    };
                    return new ResponseBase<OrdenEntregaAlmacenRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<OrdenEntregaAlmacenRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<OrdenEntregaAlmacenRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<OrdenEntregaAlmacenRequest>> GetPorIdOrdenTrabajo(long idOrden)
        {
            try
            {
                var ordenEntregaAlmacenRequest = await _dalc.GetPorIdOrdenTrabajo(idOrden);
                if (ordenEntregaAlmacenRequest != null)
                {
                    var data = new OrdenEntregaAlmacenRequest()
                    {
                        idOrdenEntregaAlmacen = ordenEntregaAlmacenRequest.idOrdenEntregaAlmacen,
                        repuestos = !String.IsNullOrEmpty(ordenEntregaAlmacenRequest.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosOrdenEntregaRequest>>(ordenEntregaAlmacenRequest.repuestos) : new List<RepuestosOrdenEntregaRequest>(),
                        idOrdenTrabajo = ordenEntregaAlmacenRequest.idOrdenTrabajo,
                        fechaHora = ordenEntregaAlmacenRequest.fechaHora,
                        idAlmacen = ordenEntregaAlmacenRequest.idAlmacen,
                        idCuadrilla = ordenEntregaAlmacenRequest.idCuadrilla,
                        idSede = ordenEntregaAlmacenRequest.idSede
                    };
                    return new ResponseBase<OrdenEntregaAlmacenRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<OrdenEntregaAlmacenRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<OrdenEntregaAlmacenRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<OrdenEntregaAlmacenPromedioPorAlmacenRequest>> GetPromedioUsoDiario(long idRepuesto, DateTime fechaActual, long idAlmacen)
        {
            try
            {
                var promedioSeisMeses = await GetPromedioUltimosSeisMesesPorAlmacen(idRepuesto, fechaActual, idAlmacen);
                var repuestoID = await _dalcRepuestos.GetID(idRepuesto);
                if (promedioSeisMeses.datos != null && repuestoID != null)
                {

                    var data = new OrdenEntregaAlmacenPromedioPorAlmacenRequest()
                    {
                        idAlmacen = idAlmacen,
                        promedio = promedioSeisMeses.datos.promedio / DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month),
                        repuesto = repuestoID
                    };
                    return new ResponseBase<OrdenEntregaAlmacenPromedioPorAlmacenRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<OrdenEntregaAlmacenPromedioPorAlmacenRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<OrdenEntregaAlmacenPromedioPorAlmacenRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<OrdenEntregaAlmacenRequest>> guardarOrdenEntregaAlmacen(OrdenEntregaAlmacenRequest ordenEntregaAlmacenRequest, Transaction transaction)
        {
            try
            {
                var almacen = await _dalcAlmacen.Get(ordenEntregaAlmacenRequest.idAlmacen);

                // almacen.tipo == 2 Virtual
                List<RepuestosOrdenEntregaRequest> ob = new List<RepuestosOrdenEntregaRequest>();

                if (almacen != null && almacen.tipo == 2)
                {
                    foreach (var item in ordenEntregaAlmacenRequest.repuestos)
                    {
                        var repuesto = await _dalcRepuestosAlmacen.GetPorIdRepuestoAlmacen(Convert.ToInt64(item.idRepuesto), almacen.idAlmacen);
                        if (repuesto != null)
                        {
                            var repuestosAlmacenEmisor = await _dalcRepuestosAlmacen.Get(repuesto.idRepuestosAlmacen);
                            if (repuestosAlmacenEmisor.cantidadActual >= Convert.ToInt64(item.cantidad))
                            {
                                var cantidadRepuesto = await _dalcRepuestosAlmacen.ActualizarCantidadRepuestosAlmacen(almacen.idAlmacen, Convert.ToInt64(item.idRepuesto), Convert.ToInt64(item.cantidad), almacen.tipo);
                                ob.Add(item);
                            }
                        }

                        var repuestosAlmacen = await _dalcRepuestosAlmacen.GetPorIdRepuestoAlmacen(Convert.ToInt64(item.idRepuesto), ordenEntregaAlmacenRequest.idAlmacen);
                        if (repuestosAlmacen != null)
                        {
                            item.existenciaActual = repuestosAlmacen.cantidadActual;
                        }
                    }

                    var dataTransformada = new OrdenEntregaAlmacen()
                    {
                        idOrdenEntregaAlmacen = ordenEntregaAlmacenRequest.idOrdenEntregaAlmacen,
                        repuestos = JsonConvert.SerializeObject(ordenEntregaAlmacenRequest.repuestos),
                        idOrdenTrabajo = ordenEntregaAlmacenRequest.idOrdenTrabajo,
                        fechaHora = ordenEntregaAlmacenRequest.fechaHora,
                        idAlmacen = ordenEntregaAlmacenRequest.idAlmacen,
                        idCuadrilla = ordenEntregaAlmacenRequest.idCuadrilla,
                        idSede = ordenEntregaAlmacenRequest.idSede
                    };

                    if (ob.Count > 0)
                    {
                        dataTransformada.repuestos = JsonConvert.SerializeObject(ob);
                        var dataOrdenEntregaAlmacenRequest = await _dalc.Set(dataTransformada, transaction);

                        var dataRespuesta = new OrdenEntregaAlmacenRequest()
                        {
                            idOrdenEntregaAlmacen = dataOrdenEntregaAlmacenRequest.idOrdenEntregaAlmacen,
                            repuestos = !String.IsNullOrEmpty(dataOrdenEntregaAlmacenRequest.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosOrdenEntregaRequest>>(dataOrdenEntregaAlmacenRequest.repuestos) : new List<RepuestosOrdenEntregaRequest>(),
                            idOrdenTrabajo = dataOrdenEntregaAlmacenRequest.idOrdenTrabajo,
                            fechaHora = dataOrdenEntregaAlmacenRequest.fechaHora,
                            idAlmacen = dataOrdenEntregaAlmacenRequest.idAlmacen,
                            idCuadrilla = dataOrdenEntregaAlmacenRequest.idCuadrilla,
                            idSede = dataOrdenEntregaAlmacenRequest.idSede
                        };
                        return new ResponseBase<OrdenEntregaAlmacenRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "Operacion realizada con exito",
                            datos = dataRespuesta
                        };
                    }
                    else
                    {
                        return new ResponseBase<OrdenEntregaAlmacenRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = false,
                            mensaje = "La operacion no se ha podido completar",
                            datos = null
                        };
                    }

                }
                else
                {
                    return new ResponseBase<OrdenEntregaAlmacenRequest>()
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
                return new ResponseBase<OrdenEntregaAlmacenRequest>()
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
