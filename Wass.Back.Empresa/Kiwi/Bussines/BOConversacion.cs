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
    public class BOConversacion
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCConversacion _dalc;

        public BOConversacion(EmpresaContext context)
        {
            _dalc = new DALCConversacion(context);
        }

        public async Task<ResponseBase<List<Conversacion>>> GetTodas()
        {
            try
            {
                var conversacion = await _dalc.GetTodas();

                if (conversacion != null)
                {
                    return new ResponseBase<List<Conversacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = conversacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Conversacion>>()
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
                return new ResponseBase<List<Conversacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Conversacion>>> GetTodasPorEmpleado(long idEmpleado)
        {
            try
            {
                var conversacion = await _dalc.GetTodasporEmpleados(idEmpleado);

                if (conversacion != null)
                {
                    return new ResponseBase<List<Conversacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = conversacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Conversacion>>()
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
                return new ResponseBase<List<Conversacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Conversacion>> GetPorId(long idConversacion)
        {
            try
            {
                var comentario = await _dalc.Get(idConversacion);
                if (comentario != null)
                {
                    return new ResponseBase<Conversacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = comentario
                    };
                }
                else
                {
                    return new ResponseBase<Conversacion>()
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
                return new ResponseBase<Conversacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Conversacion>> EliminarConversacion(long idConversacion)
        {
            try
            {
                var data = await _dalc.EliminarConversacion(idConversacion);


                return new ResponseBase<Conversacion>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre el Comentario realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Conversacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Conversacion>> guardarConversacion(Conversacion conversacion, Transaction transaction)
        {
            try
            {
                var dataConversacion = await _dalc.Set(conversacion, transaction);

                var getConversacion = await _dalc.Get(dataConversacion.idConversacion);

                if (dataConversacion != null)
                {
                    return new ResponseBase<Conversacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = getConversacion
                    };
                }
                else
                {
                    return new ResponseBase<Conversacion>()
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
                return new ResponseBase<Conversacion>()
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
