using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BORespuestaVariableRonda
    {

        private readonly DALCRespuestaVariableRonda _dalc;

        public BORespuestaVariableRonda(ProgramadorContext context)
        {
            _dalc = new DALCRespuestaVariableRonda(context);
        }

        public async Task<ResponseBase<RespuestaVariableRonda>> Get (long idRespuestaVariableRonda)
        {
            try
            {
                var respuesta = await _dalc.Get(idRespuestaVariableRonda);
                if (respuesta != null)
                {
                    return new ResponseBase<RespuestaVariableRonda>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta

                    };
                }
                else
                {
                    return new ResponseBase<RespuestaVariableRonda>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "no hay registros para este id",
                        datos = null

                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<RespuestaVariableRonda>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaVariableRonda>>> getTodas()
        {
            try
            {
                var respuesta = await _dalc.GetTodas();

                if (respuesta != null)
                {
                    return new ResponseBase<List<RespuestaVariableRonda>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaVariableRonda>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "no hay registros para esta consulta",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<RespuestaVariableRonda>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaVariableRonda>>> getTodasPorRonda(long idRonda)
        {
            try
            {
                var respuesta = await _dalc.GetTodasPorRonda(idRonda);

                if (respuesta != null)
                {
                    return new ResponseBase<List<RespuestaVariableRonda>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaVariableRonda>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "no hay respuestas para esta ronda",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<RespuestaVariableRonda>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaVariableRonda>>> getTodasPorVariable(long idVariable)
        {
            try
            {
                var respuesta = await _dalc.GetTodasPorVariable(idVariable);

                if (respuesta != null)
                {
                    return new ResponseBase<List<RespuestaVariableRonda>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaVariableRonda>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "no hay respuestas para esta variable",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<RespuestaVariableRonda>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RespuestaVariableRonda>> guardarRespuestaVariableRonda(RespuestaVariableRonda respuesta, Transaction transaction)
        {
            try
            {
                var nuevaRespuesta = await _dalc.Set(respuesta, transaction);

                if (nuevaRespuesta != null)
                {
                    return new ResponseBase<RespuestaVariableRonda>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = nuevaRespuesta

                    };
                }
                else
                {
                    return new ResponseBase<RespuestaVariableRonda>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "no se realizo la operacion",
                        datos = null

                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseBase<RespuestaVariableRonda>()
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
