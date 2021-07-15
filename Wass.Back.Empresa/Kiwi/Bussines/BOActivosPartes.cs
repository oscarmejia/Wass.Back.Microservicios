using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Kiwi.Interface;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;

namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOActivosPartes : IBOCrudGuid<ActivosPartes>
    {

        public Dictionary<string, string> EndPointsDictinoDictionary { get; set; }
        private readonly DALCActivosPartes _dalc;
        private readonly string _msg_base;
        public BOActivosPartes(EmpresaContext context)
        {
            _dalc = new DALCActivosPartes(context);
            _msg_base = "partes";
        }

        public async Task<ResponseBase<ActivosPartes>> GetAsync(Guid id)
        {
            try
            {
                var datos = await _dalc.GetAsync(id);

                if (datos != null)
                {
                    return new ResponseBase<ActivosPartes>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<ActivosPartes>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = $"La {_msg_base} no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosPartes>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosPartes>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosPartes>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosPartes>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosPartes>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta de {_msg_base} no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ActivosPartes>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosPartes>>> GetPorClasificacionAsync(long idClasificacion)
        {
            try
            {
                var obj = await _dalc.GetPorClasificacionAsync(idClasificacion);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosPartes>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosPartes>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosPartes>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta de {_msg_base} no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ActivosPartes>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosPartes>>> GetPorSubParteAsync(Guid idParte)
        {
            try
            {
                var obj = await _dalc.GetPorSubParteAsync(idParte);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosPartes>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosPartes>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosPartes>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta de {_msg_base} no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ActivosPartes>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosPartes>> SetAsync(ActivosPartes objeto, Transaction transaccion)
        {
            try
            {
                var data = await _dalc.SetAsync(objeto, transaccion);
                if (data != null)
                {
                    return new ResponseBase<ActivosPartes>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación sobre {_msg_base} realizada con exito",
                        datos = data
                    };
                }
                else
                    return new ResponseBase<ActivosPartes>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
                        datos = data
                    };

            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosPartes>()
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
