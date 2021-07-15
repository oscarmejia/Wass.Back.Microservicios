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
    public class BORespuestaAccionesPlanMantenimientoPreventivo
    {
        private readonly DALCRespuestaAccionesPlanMantenimientoPreventivo _dalc;

        public BORespuestaAccionesPlanMantenimientoPreventivo(ProgramadorContext context)
        {
            _dalc = new DALCRespuestaAccionesPlanMantenimientoPreventivo(context);
        }

        public async Task<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>> Get(long idRespuesta)
        {
            try
            {
                var respuesta = await _dalc.Get(idRespuesta);
                if (respuesta != null)
                {
                    
                    return new ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>()
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
                return new ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>> GetPorMantenimiento(long idMantenimientoPreventivo)
        {
            try
            {
                var respuesta = await _dalc.GetPorMantenimientoPreventivo(idMantenimientoPreventivo);
                if (respuesta != null)
                {

                    return new ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>()
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
                return new ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>> GetPorMantenimientoyParte(long idMantenimientoPreventivo, Guid idParte)
        {
            try
            {
                var respuesta = await _dalc.GetPorMantenimientoPreventivoyParte(idMantenimientoPreventivo, idParte);
                if (respuesta != null)
                {

                    return new ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>()
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
                return new ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>> GetTodas()
        {
            try
            {
                var respuesta = await _dalc.GetTodas();
                

                if (respuesta != null && respuesta.Count > 0)
                {
                    

                    return new ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>()
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
                return new ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>> GuardarRespuesta(RespuestaAccionesPlanMantenimientoPreventivo respuesta, Transaction transaction)
        {
            try
            {
                

                var data = await _dalc.Set(respuesta, transaction);

                if (data != null)
                {
                    return new ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>()
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
                return new ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>()
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
