using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.RespuestaActivosVariables;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BORespuestaActivosVariables
    {
        private readonly DALCRespuestaActivosVariables _dalc;

        public BORespuestaActivosVariables(EmpresaContext context)
        {
            _dalc = new DALCRespuestaActivosVariables(context);
        }

        public async Task<ResponseBase<RespuestaActivosVariablesResponse>> Get(long idRespuestaActivosVariables)
        {
            try
            {
                var respuesta = await _dalc.Get(idRespuestaActivosVariables);
                if (respuesta != null)
                {
                    var data = new RespuestaActivosVariablesResponse()
                    {
                        idRespuestaActivosVariables = respuesta.idRespuestaActivosVariables,
                        idActivoVariable = respuesta.idActivoVariable,
                        idClasificacion = respuesta.idClasificacion,
                        idCategorizacion = respuesta.idCategorizacion,
                        idActivoFlota = respuesta.idActivoFlota,
                        idActivoEquipo = respuesta.idActivoEquipo,
                        respuesta = !String.IsNullOrEmpty(respuesta.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaResponse>>(respuesta.respuesta) : new List<RespuestaResponse>(),

                    };
                    return new ResponseBase<RespuestaActivosVariablesResponse>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<RespuestaActivosVariablesResponse>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo datos",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<RespuestaActivosVariablesResponse>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaActivosVariablesResponse>>> GetTodas()
        {
            try
            {
                var respuesta = await _dalc.GetTodas();
                var datos = new List<RespuestaActivosVariablesResponse>();

                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new RespuestaActivosVariablesResponse()
                        {
                            idRespuestaActivosVariables = item.idRespuestaActivosVariables,
                            idActivoVariable = item.idActivoVariable,
                            idClasificacion = item.idClasificacion,
                            idCategorizacion = item.idCategorizacion,
                            idActivoFlota = item.idActivoFlota,
                            idActivoEquipo = item.idActivoEquipo,
                            respuesta = !String.IsNullOrEmpty(item.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaResponse>>(item.respuesta) : new List<RespuestaResponse>(),

                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<RespuestaActivosVariablesResponse>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaActivosVariablesResponse>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo datos",
                        datos = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseBase<List<RespuestaActivosVariablesResponse>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RespuestaActivosVariablesResponse>> GuardarRespuesta(RespuestaActivosVariablesResponse respuesta, Transaction transaction)
        {
            try
            {
                var dataTransformada = new RespuestaActivosVariables()
                {
                    idRespuestaActivosVariables = respuesta.idRespuestaActivosVariables,
                    idActivoVariable = respuesta.idActivoVariable,
                    idClasificacion = respuesta.idClasificacion,
                    idCategorizacion = respuesta.idCategorizacion,
                    idActivoFlota = respuesta.idActivoFlota,
                    idActivoEquipo = respuesta.idActivoEquipo,
                    respuesta = JsonConvert.SerializeObject(respuesta.respuesta)

                };

                var data = await _dalc.Set(dataTransformada, transaction);

                var dataRespuesta = new RespuestaActivosVariablesResponse()
                {
                    idRespuestaActivosVariables = data.idRespuestaActivosVariables,
                    idActivoVariable = data.idActivoVariable,
                    idClasificacion = data.idClasificacion,
                    idCategorizacion = data.idCategorizacion,
                    idActivoFlota = data.idActivoFlota,
                    idActivoEquipo = data.idActivoEquipo,
                    respuesta = !String.IsNullOrEmpty(data.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaResponse>>(data.respuesta) : new List<RespuestaResponse>(),

                };

                if (dataRespuesta != null)
                {
                    return new ResponseBase<RespuestaActivosVariablesResponse>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = dataRespuesta
                    };
                }
                else
                {
                    return new ResponseBase<RespuestaActivosVariablesResponse>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se a completado",
                        datos = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseBase<RespuestaActivosVariablesResponse>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaActivosVariablesResponse>>> GetPorCategoriaClasificacionActivo(long idClasificacion, long idCategorizacion, Guid idActivo)
        {
            try
            {
                var respuesta = await _dalc.GetPorCategoriaClasificacionActivo(idClasificacion, idCategorizacion, idActivo);
                var datos = new List<RespuestaActivosVariablesResponse>();
                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new RespuestaActivosVariablesResponse()
                        {
                            idRespuestaActivosVariables = item.idRespuestaActivosVariables,
                            idActivoVariable = item.idActivoVariable,
                            idClasificacion = item.idClasificacion,
                            idCategorizacion = item.idCategorizacion,
                            idActivoFlota = item.idActivoFlota,
                            idActivoEquipo = item.idActivoEquipo,
                            respuesta = !String.IsNullOrEmpty(item.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaResponse>>(item.respuesta) : new List<RespuestaResponse>(),

                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<RespuestaActivosVariablesResponse>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaActivosVariablesResponse>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo datos",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<RespuestaActivosVariablesResponse>>()
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
