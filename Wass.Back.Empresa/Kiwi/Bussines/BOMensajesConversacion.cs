using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOMensajesConversacion
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCMensajesConversacion _dalc;

        public BOMensajesConversacion(EmpresaContext context)
        {
            _dalc = new DALCMensajesConversacion(context);
        }

        public async Task<ResponseBase<List<MensajesConversacion>>> GetTodas()
        {
            try
            {
                var mensajesConversacion = await _dalc.GetTodas();

                if (mensajesConversacion != null)
                {
                    return new ResponseBase<List<MensajesConversacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = mensajesConversacion
                    };
                }
                else
                {
                    return new ResponseBase<List<MensajesConversacion>>()
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
                return new ResponseBase<List<MensajesConversacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<MensajesConversacion>> GetPorId(long idMensajesConversacion)
        {
            try
            {
                var mensajesConversacion = await _dalc.Get(idMensajesConversacion);
                if (mensajesConversacion != null)
                {
                    return new ResponseBase<MensajesConversacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = mensajesConversacion
                    };
                }
                else
                {
                    return new ResponseBase<MensajesConversacion>()
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
                return new ResponseBase<MensajesConversacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<MensajesConversacion>> EliminarMensajesConversacion(long idMensajesConversacion)
        {
            try
            {
                var data = await _dalc.EliminarMensajesConversacion(idMensajesConversacion);


                return new ResponseBase<MensajesConversacion>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre el Comentario realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<MensajesConversacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<MensajesConversacion>> guardarMensajesConversacion(MensajesConversacion mensajesConversacion, Transaction transaction)
        {
            try
            {
                var dataMensajesConversacion = await _dalc.Set(mensajesConversacion, transaction);

                if (dataMensajesConversacion != null)
                {
                    return new ResponseBase<MensajesConversacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = mensajesConversacion
                    };
                }
                else
                {
                    return new ResponseBase<MensajesConversacion>()
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
                return new ResponseBase<MensajesConversacion>()
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
