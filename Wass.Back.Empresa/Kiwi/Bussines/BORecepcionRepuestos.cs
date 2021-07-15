using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.RecepcionRepuestos;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BORecepcionRepuestos
    {
        private readonly DALCRecepcionRepuestos _dalc;
        private readonly DALCAlmacen _dalcAlmacen;
        private readonly DALCRepuestosAlmacen _dalcRepuestosAlmacen;
        private readonly DALCRepuestos _dalcRepuestos;

        public BORecepcionRepuestos(EmpresaContext context)
        {
            _dalc = new DALCRecepcionRepuestos(context);
            _dalcAlmacen = new DALCAlmacen(context);
            _dalcRepuestosAlmacen = new DALCRepuestosAlmacen(context);
            _dalcRepuestos = new DALCRepuestos(context);
        }

        public async Task<ResponseBase<List<RecepcionRepuestosRequest>>> GetTodas()
        {
            try
            {
                var recepcionRepuestosRequest = await _dalc.GetTodas();
                var datos = new List<RecepcionRepuestosRequest>();

                if (recepcionRepuestosRequest != null && recepcionRepuestosRequest.Count > 0)
                {
                    foreach (var item in recepcionRepuestosRequest)
                    {
                        var data = new RecepcionRepuestosRequest()
                        {
                            idRecepcionRepuestos = item.idRecepcionRepuestos,
                            fechaHora = item.fechaHora,
                            repuestos = !String.IsNullOrEmpty(item.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRecepcionRequest>>(item.repuestos) : new List<RepuestosRecepcionRequest>(),
                            idAlmacen = item.idAlmacen,
                            idUsuario = item.idUsuario
                        };

                        datos.Add(data);
                    }
                    return new ResponseBase<List<RecepcionRepuestosRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<RecepcionRepuestosRequest>>()
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
                return new ResponseBase<List<RecepcionRepuestosRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RecepcionRepuestosRequest>> GetPorId(long idRecepcionRepuestosRequest)
        {
            try
            {
                var recepcionRepuestosRequest = await _dalc.Get(idRecepcionRepuestosRequest);
                if (recepcionRepuestosRequest != null)
                {
                    var data = new RecepcionRepuestosRequest()
                    {
                        idRecepcionRepuestos = recepcionRepuestosRequest.idRecepcionRepuestos,
                        fechaHora = recepcionRepuestosRequest.fechaHora,
                        repuestos = !String.IsNullOrEmpty(recepcionRepuestosRequest.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRecepcionRequest>>(recepcionRepuestosRequest.repuestos) : new List<RepuestosRecepcionRequest>(),
                        idAlmacen = recepcionRepuestosRequest.idAlmacen,
                        idUsuario = recepcionRepuestosRequest.idUsuario
                    };
                    return new ResponseBase<RecepcionRepuestosRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<RecepcionRepuestosRequest>()
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
                return new ResponseBase<RecepcionRepuestosRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<promedioCostoUnitarioRepuestoRequest>> GetPromedioUltimosSeisMesesCostoUnitario(long idRepuesto)
        {
            try
            {
                var recepcionRepuestosRequest = await _dalc.GetTodasPorRepuesto(idRepuesto);
                var repuestoID = await _dalcRepuestos.GetID(idRepuesto);
                long countRecepciones = 0;
                decimal suma = 0;

                if (recepcionRepuestosRequest != null && recepcionRepuestosRequest.Count > 0 && repuestoID != null)
                {
                    var ultimaRecepcion = recepcionRepuestosRequest[0];
                    var ultimosSeisMeses = ultimaRecepcion.fechaHora.AddMonths(-6);
                    foreach (var item in recepcionRepuestosRequest)
                    {

                        var fechaInicial = DateTime.Compare(item.fechaHora, ultimaRecepcion.fechaHora);
                        var fechaFinal = DateTime.Compare(item.fechaHora, ultimosSeisMeses);
                        if (fechaInicial <= 0 && fechaFinal >= 0)
                        {
                            var count = 0;
                            dynamic jsonObjIdRepuesto = JsonConvert.DeserializeObject(item.repuestos);
                            foreach (var repuesto in jsonObjIdRepuesto)
                            {
                                var buscarIdRepuesto = jsonObjIdRepuesto[count]["idRepuesto"].ToString();
                                if (Convert.ToInt64(buscarIdRepuesto) == idRepuesto)
                                {
                                    dynamic jsonObj = JsonConvert.DeserializeObject(item.repuestos);
                                    var costo = jsonObj[count]["costo"].ToString();

                                    suma += Convert.ToDecimal(costo);
                                    countRecepciones++;
                                }
                                count++;

                            }
                        }

                    }

                    var promedioCostoUnitario = suma / countRecepciones;
                    //repuestoID.costoUnitario = promedioCostoUnitario;
                    //await _dalcRepuestos.Set(repuestoID,Transaction.Update);

                    var data = new promedioCostoUnitarioRepuestoRequest()
                    {
                        promedioCostoUnitario = suma / countRecepciones,
                        detalleRepuesto = repuestoID
                    };
                    return new ResponseBase<promedioCostoUnitarioRepuestoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<promedioCostoUnitarioRepuestoRequest>()
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
                return new ResponseBase<promedioCostoUnitarioRepuestoRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RecepcionRepuestosRequest>> GetPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            try
            {
                var recepcionRepuestosRequest = await _dalc.GetPorAlmacenRepuesto(idAlmacen, idRepuesto);
                if (recepcionRepuestosRequest != null)
                {
                    var data = new RecepcionRepuestosRequest()
                    {
                        idRecepcionRepuestos = recepcionRepuestosRequest.idRecepcionRepuestos,
                        fechaHora = recepcionRepuestosRequest.fechaHora,
                        repuestos = !String.IsNullOrEmpty(recepcionRepuestosRequest.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRecepcionRequest>>(recepcionRepuestosRequest.repuestos) : new List<RepuestosRecepcionRequest>(),
                        idAlmacen = recepcionRepuestosRequest.idAlmacen,
                        idUsuario = recepcionRepuestosRequest.idUsuario
                    };
                    return new ResponseBase<RecepcionRepuestosRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<RecepcionRepuestosRequest>()
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
                return new ResponseBase<RecepcionRepuestosRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<RecepcionRepuestosRequest>> guardarRecepcionRepuestos(RecepcionRepuestosRequest recepcionRepuestosRequest, Transaction transaction)
        {
            try
            {
                var almacen = await _dalcAlmacen.Get(recepcionRepuestosRequest.idAlmacen);
                // almacen.tipo == 1 Fisico
                //Estado = true Activo, Estado = false Desactivo
                if (almacen != null && almacen.tipo == 1 && almacen.estado == true)
                {
                    foreach (var item in recepcionRepuestosRequest.repuestos)
                    {
                        var repuestosAlmacen = await _dalcRepuestosAlmacen.GetPorIdRepuestoAlmacen(Convert.ToInt64(item.idRepuesto), recepcionRepuestosRequest.idAlmacen);

                        if (repuestosAlmacen != null)
                        {
                            var cantidadRepuesto = await _dalcRepuestosAlmacen.ActualizarCantidadRepuestosAlmacen(almacen.idAlmacen, Convert.ToInt64(item.idRepuesto), Convert.ToInt64(item.cantidad), almacen.tipo);

                            item.existenciaActual = repuestosAlmacen.cantidadActual;
                        }

                        if (repuestosAlmacen == null)
                        {

                            var nuevoRepuestoAlmacen = new RepuestosAlmacen()
                            {
                                cantidadActual = Convert.ToInt64(item.cantidad),
                                idAlmacen = recepcionRepuestosRequest.idAlmacen,
                                idRepuestos = Convert.ToInt64(item.idRepuesto),

                            };
                            item.existenciaActual = nuevoRepuestoAlmacen.cantidadActual;
                            var dataNuevoRepuestoAlmacen = await _dalcRepuestosAlmacen.Set(nuevoRepuestoAlmacen, Transaction.Insert);

                        }
                    }

                    var dataTransformada = new RecepcionRepuestos()
                    {
                        idRecepcionRepuestos = recepcionRepuestosRequest.idRecepcionRepuestos,
                        fechaHora = recepcionRepuestosRequest.fechaHora,
                        repuestos = JsonConvert.SerializeObject(recepcionRepuestosRequest.repuestos),
                        idAlmacen = recepcionRepuestosRequest.idAlmacen,
                        idUsuario = recepcionRepuestosRequest.idUsuario
                    };

                    var dataRecepcionRepuestosRequest = await _dalc.Set(dataTransformada, transaction);

                    foreach (var item in recepcionRepuestosRequest.repuestos)
                    {
                        var repuestoID = await _dalcRepuestos.Get(Convert.ToInt64(item.idRepuesto));
                        var promedioCostoUnitario = await GetPromedioUltimosSeisMesesCostoUnitario(Convert.ToInt64(item.idRepuesto));
                        if (promedioCostoUnitario.datos != null)
                        {
                            repuestoID.costoUnitario = promedioCostoUnitario.datos.promedioCostoUnitario;
                        }
                        await _dalcRepuestos.Set(repuestoID, Transaction.Update);
                    }

                    var dataRespuesta = new RecepcionRepuestosRequest()
                    {
                        idRecepcionRepuestos = dataRecepcionRepuestosRequest.idRecepcionRepuestos,
                        fechaHora = dataRecepcionRepuestosRequest.fechaHora,
                        repuestos = !String.IsNullOrEmpty(dataRecepcionRepuestosRequest.repuestos) ? JsonConvert.DeserializeObject<List<RepuestosRecepcionRequest>>(dataRecepcionRepuestosRequest.repuestos) : new List<RepuestosRecepcionRequest>(),
                        idAlmacen = dataRecepcionRepuestosRequest.idAlmacen,
                        idUsuario = dataRecepcionRepuestosRequest.idUsuario
                    };
                    return new ResponseBase<RecepcionRepuestosRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataRespuesta
                    };
                }
                else
                {
                    return new ResponseBase<RecepcionRepuestosRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar. El almacen debe ser Fisico o el almacen no existe. El almacen debe estar Activo",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<RecepcionRepuestosRequest>()
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
