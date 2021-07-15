using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BORespuestaMantenimientoCorrectivo
    {
        private readonly DALCRespuestaMantenimientoCorrectivo _dalc;

        public BORespuestaMantenimientoCorrectivo(ProgramadorContext context)
        {
            _dalc = new DALCRespuestaMantenimientoCorrectivo(context);
        }

        public async Task<ResponseBase<CorrectivoRequest>> Get(long idRespuestaMantenimientoCorrectivo)
        {
            try
            {
                var respuesta = await _dalc.Get(idRespuestaMantenimientoCorrectivo);
                if (respuesta != null)
                {
                    var data = new CorrectivoRequest()
                    {
                        idRespuestaMantenimientoCorrectivo = respuesta.idRespuestaMantenimientoCorrectivo,
                        idDiagnostico = respuesta.idDiagnostico,
                        idMantenimientoCorrectivo = respuesta.idMantenimientoCorrectivo,
                        respuesta = !String.IsNullOrEmpty(respuesta.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaCorrectivoRequest>>(respuesta.respuesta) : new List<RespuestaCorrectivoRequest>(),
                        
                    };
                    return new ResponseBase<CorrectivoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<CorrectivoRequest>()
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
                return new ResponseBase<CorrectivoRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CorrectivoRequest>>> GetTodas()
        {
            try
            {
                var respuesta = await _dalc.GetTodas();
                var datos = new List<CorrectivoRequest>();

                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new CorrectivoRequest()
                        {
                            idRespuestaMantenimientoCorrectivo = item.idRespuestaMantenimientoCorrectivo,
                            idDiagnostico = item.idDiagnostico,
                            idMantenimientoCorrectivo = item.idMantenimientoCorrectivo,
                            respuesta = !String.IsNullOrEmpty(item.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaCorrectivoRequest>>(item.respuesta) : new List<RespuestaCorrectivoRequest>(),
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<CorrectivoRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<CorrectivoRequest>>()
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
                return new ResponseBase<List<CorrectivoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CorrectivoRequest>> GuardarRespuestaMantenimientoCorrectivo(CorrectivoRequest respuesta, Transaction transaction)
        {
            try
            {
                var dataTransformada = new RespuestaMantenimientoCorrectivo()
                {
                    idRespuestaMantenimientoCorrectivo = respuesta.idRespuestaMantenimientoCorrectivo,
                    idDiagnostico = respuesta.idDiagnostico,
                    idMantenimientoCorrectivo = respuesta.idMantenimientoCorrectivo,
                    respuesta = JsonConvert.SerializeObject(respuesta.respuesta),
                };

                var data = await _dalc.Set(dataTransformada, transaction);

                var dataRespuesta = new CorrectivoRequest()
                {
                    idRespuestaMantenimientoCorrectivo = data.idRespuestaMantenimientoCorrectivo,
                    idDiagnostico = data.idDiagnostico,
                    idMantenimientoCorrectivo = data.idMantenimientoCorrectivo,
                    respuesta = !String.IsNullOrEmpty(data.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaCorrectivoRequest>>(data.respuesta) : new List<RespuestaCorrectivoRequest>(),
                   
                };

                if (dataRespuesta != null)
                {
                    return new ResponseBase<CorrectivoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = dataRespuesta
                    };
                }
                else
                {
                    return new ResponseBase<CorrectivoRequest>()
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
                return new ResponseBase<CorrectivoRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        

        public async Task<ResponseBase<List<CorrectivoRequest>>> GetPorDiagnostico(long idDiagnostico)
        {
            try
            {
                var respuesta = await _dalc.GetPorDiagnostico(idDiagnostico);
                var datos = new List<CorrectivoRequest>();
                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new CorrectivoRequest()
                        {
                            idRespuestaMantenimientoCorrectivo = item.idRespuestaMantenimientoCorrectivo,
                            idDiagnostico = item.idDiagnostico,
                            idMantenimientoCorrectivo = item.idMantenimientoCorrectivo,
                            respuesta = !String.IsNullOrEmpty(item.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaCorrectivoRequest>>(item.respuesta) : new List<RespuestaCorrectivoRequest>(),
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<CorrectivoRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<CorrectivoRequest>>()
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
                return new ResponseBase<List<CorrectivoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CorrectivoRequest>>> GetPorMantenimientoCorrectivo(long idMantenimientoCorrectivo)
        {
            try
            {
                var respuesta = await _dalc.GetPorMantenimientoCorrectivo(idMantenimientoCorrectivo);
                var datos = new List<CorrectivoRequest>();
                if (respuesta != null && respuesta.Count > 0)
                {
                    foreach (var item in respuesta)
                    {
                        var data = new CorrectivoRequest()
                        {
                            idRespuestaMantenimientoCorrectivo = item.idRespuestaMantenimientoCorrectivo,
                            idDiagnostico = item.idDiagnostico,
                            idMantenimientoCorrectivo = item.idMantenimientoCorrectivo,
                            respuesta = !String.IsNullOrEmpty(item.respuesta) ? JsonConvert.DeserializeObject<List<RespuestaCorrectivoRequest>>(item.respuesta) : new List<RespuestaCorrectivoRequest>(),
                        };

                        datos.Add(data);
                    }

                    return new ResponseBase<List<CorrectivoRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<CorrectivoRequest>>()
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
                return new ResponseBase<List<CorrectivoRequest>>()
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
