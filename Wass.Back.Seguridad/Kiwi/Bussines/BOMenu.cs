using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Seguridad.Models.Entity;
using Wass.Back.Seguridad.Models.Enum;
using Wass.Back.Seguridad.Models.Peticiones.Base;
using Wass.Back.Seguridad.Models.Peticiones.Usuario;
using Wass.Back.Seguridad.Rabbit.Context;
using Wass.Back.Seguridad.Rabbit.DALC;

namespace Wass.Back.Seguridad.Kiwi.Bussines
{
    public class BOMenu
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCMenus _dalc;

        public BOMenu(SeguridadContext context)
        {
            _dalc = new DALCMenus(context);
        }

        public async Task<ResponseBase<Menus>> GetAsync(long id)
        {
            try
            {
                var menus = await _dalc.Get(id);

                if (menus != null)
                {
                    return new ResponseBase<Menus>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = menus
                    };
                }
                else
                {
                    return new ResponseBase<Menus>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "La menús consultada no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Menus>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Menus>>> GetTodoAsync()
        {
            try
            {
                var menúss = await _dalc.GetAll();
                if (menúss != null)
                {
                    if (menúss.Count > 0)
                        return new ResponseBase<List<Menus>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = menúss
                        };
                    else
                    {
                        return new ResponseBase<List<Menus>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay menús disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<Menus>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de menús no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Menus>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Menus>>> GetPadresAsync()
        {
            try
            {
                var menúss = await _dalc.GetPadres();
                if (menúss != null)
                {
                    if (menúss.Count > 0)
                        return new ResponseBase<List<Menus>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = menúss
                        };
                    else
                    {
                        return new ResponseBase<List<Menus>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay menús disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<Menus>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de menus no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Menus>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Menus>>> GetHijosAsync(long idPadre)
        {
            try
            {
                var menúss = await _dalc.GetHijos(idPadre);
                if (menúss != null)
                {
                    if (menúss.Count > 0)
                        return new ResponseBase<List<Menus>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = menúss
                        };
                    else
                    {
                        return new ResponseBase<List<Menus>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay menús disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<Menus>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de menus no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Menus>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Menus>> save(RequestMenus _data, Transaction trans)
        {
            var ob = JsonConvert.DeserializeObject<Menus>(JsonConvert.SerializeObject(_data));
            if (trans == Transaction.Delete)
            {
                return new ResponseBase<Menus>()
                {
                    codigo = (int)HttpStatusCode.NotFound,
                    estado = false,
                    mensaje = $"La operación de eliminar menús no ha sido implementada.",
                    datos = null
                };
            }
            else
            {
                var data = await _dalc.Set(ob, trans);
                if (data != null)
                {
                    return new ResponseBase<Menus>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación realizada con exito",
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<Menus>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación solicitada no se pudo realizar.",
                        datos = data
                    };
                }
            }
        }
    }
}
