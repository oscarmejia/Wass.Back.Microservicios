using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.ComentarioActivosClasificacionDiagnosticoAcciones;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BOComentarioActivosClasificacionDiagnosticoAcciones
    {
        private readonly DALCComentarioActivosClasificacionDiagnosticoAcciones _dalc;

        public BOComentarioActivosClasificacionDiagnosticoAcciones(ProgramadorContext context)
        {
            _dalc = new DALCComentarioActivosClasificacionDiagnosticoAcciones(context);
        }

        public async Task<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>> Get(long idComentarioDiagnosticoAccionesRequest)
        {
            try
            {
                var respuesta = await _dalc.Get(idComentarioDiagnosticoAccionesRequest);
                if (respuesta != null)
                {
                    var data = new ComentarioActivosClasificacionDiagnosticoAccionesRequest()
                    {
                        idComentarioDiagnosticosAcciones = respuesta.idComentarioDiagnosticosAcciones,
                        idClasificacion = respuesta.idClasificacion,
                        idDiagnostico = respuesta.idDiagnostico,
                        idMantenimientoCorrectivo = (long)respuesta.idMantenimientoCorrectivo,
                        idAviso = (long)respuesta.idAviso,
                        comentario = !String.IsNullOrEmpty(respuesta.comentario) ? JsonConvert.DeserializeObject<List<ComentarioAccionRequest>>(respuesta.comentario) : new List<ComentarioAccionRequest>(),
                        
                    };
                    return new ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>()
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
                return new ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>> GetPorDiagnosticoMantenimientoCorrectivoClasificacion(long idDiagnostico, long idMantenimientoCorrectivo, long idClasificacion)
        {
            try
            {
                var respuesta = await _dalc.GetPorDiagnosticoMantenimientoCorrectivoClasificacion(idDiagnostico, idMantenimientoCorrectivo, idClasificacion);
                if (respuesta != null)
                {
                    var data = new ComentarioActivosClasificacionDiagnosticoAccionesRequest()
                    {
                        idComentarioDiagnosticosAcciones = respuesta.idComentarioDiagnosticosAcciones,
                        idClasificacion = respuesta.idClasificacion,
                        idDiagnostico = respuesta.idDiagnostico,
                        idMantenimientoCorrectivo = (long)respuesta.idMantenimientoCorrectivo,
                        idAviso = (long)respuesta.idAviso,
                        comentario = !String.IsNullOrEmpty(respuesta.comentario) ? JsonConvert.DeserializeObject<List<ComentarioAccionRequest>>(respuesta.comentario) : new List<ComentarioAccionRequest>(),

                    };
                    return new ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>()
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
                return new ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>> GetTodas()
        {
            try
            {
                var respuesta = await _dalc.GetTodas();
                var datos = new List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>();

                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new ComentarioActivosClasificacionDiagnosticoAccionesRequest()
                        {
                            idComentarioDiagnosticosAcciones = item.idComentarioDiagnosticosAcciones,
                            idClasificacion = item.idClasificacion,
                            idDiagnostico = item.idDiagnostico,
                            idMantenimientoCorrectivo = (long)item.idMantenimientoCorrectivo,
                            idAviso = (long)item.idAviso,
                            comentario = !String.IsNullOrEmpty(item.comentario) ? JsonConvert.DeserializeObject<List<ComentarioAccionRequest>>(item.comentario) : new List<ComentarioAccionRequest>(),
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>()
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
                return new ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>> GuardarComentario(ComentarioActivosClasificacionDiagnosticoAccionesRequest respuesta, Transaction transaction)
        {
            try
            {
                var dataTransformada = new ComentarioActivosClasificacionDiagnosticoAcciones()
                {
                    idComentarioDiagnosticosAcciones = respuesta.idComentarioDiagnosticosAcciones,
                    idClasificacion = respuesta.idClasificacion,
                    idDiagnostico = respuesta.idDiagnostico,
                    idMantenimientoCorrectivo = respuesta.idMantenimientoCorrectivo,
                    idAviso = respuesta.idAviso,
                    comentario = JsonConvert.SerializeObject(respuesta.comentario)


                };

                var data = await _dalc.Set(dataTransformada, transaction);

                var dataRespuesta = new ComentarioActivosClasificacionDiagnosticoAccionesRequest()
                {
                    idComentarioDiagnosticosAcciones = data.idComentarioDiagnosticosAcciones,
                    idClasificacion = data.idClasificacion,
                    idDiagnostico = data.idDiagnostico,
                    idMantenimientoCorrectivo = (long)data.idMantenimientoCorrectivo,
                    idAviso = (long)data.idAviso,
                    comentario = !String.IsNullOrEmpty(data.comentario) ? JsonConvert.DeserializeObject<List<ComentarioAccionRequest>>(data.comentario) : new List<ComentarioAccionRequest>(),
                };

                if (dataRespuesta != null)
                {
                    return new ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = dataRespuesta
                    };
                }
                else
                {
                    return new ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>()
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
                return new ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>> GetPorClasificacion(long idClasificacion)
        {
            try
            {
                var respuesta = await _dalc.GetPorClasificacion(idClasificacion);
                var datos = new List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>();
                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new ComentarioActivosClasificacionDiagnosticoAccionesRequest()
                        {
                            idComentarioDiagnosticosAcciones = item.idComentarioDiagnosticosAcciones,
                            idClasificacion = item.idClasificacion,
                            idDiagnostico = item.idDiagnostico,
                            idMantenimientoCorrectivo = (long)item.idMantenimientoCorrectivo,
                            idAviso = (long)item.idAviso,
                            comentario = !String.IsNullOrEmpty(item.comentario) ? JsonConvert.DeserializeObject<List<ComentarioAccionRequest>>(item.comentario) : new List<ComentarioAccionRequest>(),
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>()
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
                return new ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>()
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
