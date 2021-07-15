using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Preguntas;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOPreguntas
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCPreguntas _dalc;

        public BOPreguntas(EmpresaContext context)
        {
            _dalc = new DALCPreguntas(context);
        }

        public async Task<ResponseBase<PreguntasRequest>> Get(long idPreguntas)
        {
            try
            {
                var pregunta = await _dalc.Get(idPreguntas);
                if (pregunta != null)
                {
                    var data = new PreguntasRequest()
                    {
                        idPregunta = pregunta.idPregunta,
                        idEmpresa = pregunta.idEmpresa,
                        nombre = pregunta.nombre,
                        tipoRespuesta = pregunta.tipoRespuesta,
                        activo = pregunta.activo,
                        opciones = !String.IsNullOrEmpty(pregunta.opciones) ? JsonConvert.DeserializeObject<List<OpcionesRequest>>(pregunta.opciones) : new List<OpcionesRequest>()
                    };
                    return new ResponseBase<PreguntasRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<PreguntasRequest>()
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
                return new ResponseBase<PreguntasRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<PreguntasRequest>>> GetTodas()
        {
            try
            {
                var pregunta = await _dalc.GetTodas();
                var datos = new List<PreguntasRequest>();

                if (pregunta != null && pregunta.Count > 0)
                {
                    foreach (var item in pregunta)
                    {
                        var data = new PreguntasRequest()
                        {
                            idPregunta = item.idPregunta,
                            idEmpresa = item.idEmpresa,
                            nombre = item.nombre,
                            tipoRespuesta = item.tipoRespuesta,
                            activo = item.activo,
                            opciones = !String.IsNullOrEmpty(item.opciones) ? JsonConvert.DeserializeObject<List<OpcionesRequest>>(item.opciones) : new List<OpcionesRequest>()
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<PreguntasRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<PreguntasRequest>>()
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
                return new ResponseBase<List<PreguntasRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<List<PreguntasRequest>>> GetPorEmpresa(long idEmpresa)
        {
            try
            {
                var preguntas = await _dalc.GetPorEmpresa(idEmpresa);
                var datos = new List<PreguntasRequest>();

                if (preguntas != null && preguntas.Count > 0)
                {
                    foreach (var item in preguntas)
                    {
                        var data = new PreguntasRequest()
                        {
                            idPregunta = item.idPregunta,
                            idEmpresa = item.idEmpresa,
                            nombre = item.nombre,
                            tipoRespuesta = item.tipoRespuesta,
                            activo = item.activo,
                            opciones = !String.IsNullOrEmpty(item.opciones) ? JsonConvert.DeserializeObject<List<OpcionesRequest>>(item.opciones) : new List<OpcionesRequest>()
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<PreguntasRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<PreguntasRequest>>()
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
                return new ResponseBase<List<PreguntasRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<PreguntasRequest>> guardarPregunta(PreguntasRequest preguntas, Transaction transaction)
        {
            try
            {
                var dataTransformda = new Preguntas()
                {
                    idPregunta = preguntas.idPregunta,
                    idEmpresa = preguntas.idEmpresa,
                    nombre = preguntas.nombre,
                    tipoRespuesta = preguntas.tipoRespuesta,
                    activo = preguntas.activo,
                    opciones = JsonConvert.SerializeObject(preguntas.opciones)
                };

                var data = await _dalc.Set(dataTransformda, transaction);

                var dataRespuesta = new PreguntasRequest()
                {
                    idPregunta = data.idPregunta,
                    idEmpresa = data.idEmpresa,
                    nombre = data.nombre,
                    tipoRespuesta = data.tipoRespuesta,
                    activo = data.activo,
                    opciones = !String.IsNullOrEmpty(data.opciones) ? JsonConvert.DeserializeObject<List<OpcionesRequest>>(data.opciones) : new List<OpcionesRequest>()
                };

                if (dataRespuesta != null)
                {
                    return new ResponseBase<PreguntasRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = dataRespuesta
                    };
                }
                else
                {
                    return new ResponseBase<PreguntasRequest>()
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
                return new ResponseBase<PreguntasRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<Preguntas>> Eliminar(Preguntas pregunta)
        {
            try
            {
                var result = await _dalc.Eliminar(pregunta);
                if (result.estado)
                {
                    return new ResponseBase<Preguntas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = result.mensaje,
                        datos = null
                    };
                }
                else
                {
                    return new ResponseBase<Preguntas>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = result.mensaje,
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Preguntas>()
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
