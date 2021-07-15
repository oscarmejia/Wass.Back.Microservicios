using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.RespuestaCuestionario;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BORespuestaCuestionario
    {

        private readonly DALCRespuestaCuestionario _dalc;

        public BORespuestaCuestionario(ProgramadorContext context)
        {
            _dalc = new DALCRespuestaCuestionario(context);
        }

        public async Task<ResponseBase<RespuestaCuestionarioRequest>> Get(long idRespuestaCuestionario)
        {
            try
            {
                var respuesta = await _dalc.Get(idRespuestaCuestionario);
                if (respuesta != null)
                {
                    var data = new RespuestaCuestionarioRequest()
                    {
                        idRespuestaCuestionario = respuesta.idRespuestaCuestionario,
                        idCuestionario = respuesta.idCuestionario,
                        idCotizacion = respuesta.idCotizacion,
                        idLicitacion = respuesta.idLicitacion,
                        respuesta = !String.IsNullOrEmpty(respuesta.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaRequest>>(respuesta.respuesta) : new List<RespuestaRequest>(),
                        activo = respuesta.activo,
                        opciones = !String.IsNullOrEmpty(respuesta.opciones) ? JsonConvert.DeserializeObject<List<OpcionesRequest>>(respuesta.opciones) : new List<OpcionesRequest>()
                    };
                    return new ResponseBase<RespuestaCuestionarioRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<RespuestaCuestionarioRequest>()
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
                return new ResponseBase<RespuestaCuestionarioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaCuestionarioRequest>>> GetTodas()
        {
            try
            {
                var respuesta = await _dalc.GetTodas();
                var datos = new List<RespuestaCuestionarioRequest>();

                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new RespuestaCuestionarioRequest()
                        {
                            idRespuestaCuestionario = item.idRespuestaCuestionario,
                            idCuestionario = item.idCuestionario,
                            idCotizacion = item.idCotizacion,
                            idLicitacion = item.idLicitacion,
                            respuesta = !String.IsNullOrEmpty(item.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaRequest>>(item.respuesta) : new List<RespuestaRequest>(),
                            activo = item.activo,
                            opciones = !String.IsNullOrEmpty(item.opciones) ? JsonConvert.DeserializeObject<List<OpcionesRequest>>(item.opciones) : new List<OpcionesRequest>()
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<RespuestaCuestionarioRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaCuestionarioRequest>>()
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
                return new ResponseBase<List<RespuestaCuestionarioRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RespuestaCuestionarioRequest>> GuardarRespuestaCuestionario(RespuestaCuestionarioRequest respuesta, Transaction transaction)
        {
            try
            {
                var dataTransformada = new RespuestaCuestionario()
                {
                    idRespuestaCuestionario = respuesta.idRespuestaCuestionario,
                    idCuestionario = respuesta.idCuestionario,
                    idCotizacion = respuesta.idCotizacion,
                    idLicitacion = respuesta.idLicitacion,
                    respuesta = JsonConvert.SerializeObject(respuesta.respuesta),
                    activo = respuesta.activo,
                    opciones = JsonConvert.SerializeObject(respuesta.opciones)
                };

                var data = await _dalc.Set(dataTransformada, transaction);

                var dataRespuesta = new RespuestaCuestionarioRequest()
                {
                    idRespuestaCuestionario = data.idRespuestaCuestionario,
                    idCuestionario = data.idCuestionario,
                    idCotizacion = data.idCotizacion,
                    idLicitacion = data.idLicitacion,
                    respuesta = !String.IsNullOrEmpty(data.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaRequest>>(data.respuesta) : new List<RespuestaRequest>(),
                    activo = data.activo,
                    opciones = !String.IsNullOrEmpty(data.opciones) ? JsonConvert.DeserializeObject<List<OpcionesRequest>>(data.opciones) : new List<OpcionesRequest>()
                };

                if (dataRespuesta != null)
                {
                    return new ResponseBase<RespuestaCuestionarioRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = dataRespuesta
                    };
                }
                else
                {
                    return new ResponseBase<RespuestaCuestionarioRequest>()
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
                return new ResponseBase<RespuestaCuestionarioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaCuestionarioRequest>>> GetPorCuestionarioCotizacionLicitacion(long idCuestionario, long idCotizacion, long idLicitacion)
        {
            try
            {
                var respuesta = await _dalc.GetPorCuestionarioCotizacionLicitacion(idCuestionario, idCotizacion, idLicitacion);
                var datos = new List<RespuestaCuestionarioRequest>();
                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new RespuestaCuestionarioRequest()
                        {
                            idRespuestaCuestionario = item.idRespuestaCuestionario,
                            idCuestionario = item.idCuestionario,
                            idCotizacion = item.idCotizacion,
                            idLicitacion = item.idLicitacion,
                            respuesta = !String.IsNullOrEmpty(item.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaRequest>>(item.respuesta) : new List<RespuestaRequest>(),
                            activo = item.activo,
                            opciones = !String.IsNullOrEmpty(item.opciones) ? JsonConvert.DeserializeObject<List<OpcionesRequest>>(item.opciones) : new List<OpcionesRequest>()
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<RespuestaCuestionarioRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaCuestionarioRequest>>()
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
                return new ResponseBase<List<RespuestaCuestionarioRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaCuestionarioRequest>>> GetPorCotizacion(long idCotizacion)
        {
            try
            {
                var respuesta = await _dalc.GetPorCotizacion(idCotizacion);
                var datos = new List<RespuestaCuestionarioRequest>();
                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new RespuestaCuestionarioRequest()
                        {
                            idRespuestaCuestionario = item.idRespuestaCuestionario,
                            idCuestionario = item.idCuestionario,
                            idCotizacion = item.idCotizacion,
                            idLicitacion = item.idLicitacion,
                            respuesta = !String.IsNullOrEmpty(item.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaRequest>>(item.respuesta) : new List<RespuestaRequest>(),
                            activo = item.activo,
                            opciones = !String.IsNullOrEmpty(item.opciones) ? JsonConvert.DeserializeObject<List<OpcionesRequest>>(item.opciones) : new List<OpcionesRequest>()
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<RespuestaCuestionarioRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaCuestionarioRequest>>()
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
                return new ResponseBase<List<RespuestaCuestionarioRequest>>()
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

