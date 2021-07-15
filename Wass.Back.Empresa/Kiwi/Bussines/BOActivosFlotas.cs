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
    public class BOActivosFlotas : IBOCrudGuid<ActivosFlotas>
    {

        public Dictionary<string, string> EndPointsDictinoDictionary { get; set; }
        private readonly DALCActivosFlotas _dalc;
        private readonly string _msg_base;
        private readonly object _namespace;
        public BOActivosFlotas(EmpresaContext context)
        {
            _dalc = new DALCActivosFlotas(context);
            _msg_base = "Activos de flotas";
            _namespace = "ActivosFlota";
        }

        public async Task<ResponseBase<ActivosFlotas>> GetAsync(Guid id)
        {
            try
            {
                var datos = await _dalc.GetAsync(id);

                if (datos != null)
                {
                    return new ResponseBase<ActivosFlotas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<ActivosFlotas>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = $"La entidad {_msg_base} no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosFlotas>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosFlotas>>> ObtenerFlotasCategoriaClasificacionSubClasificacionSedeMarca(long idCategoria, long idClasificacion1, long idSedeResponsable, string marca, long? idClasificacion2 = null)
        {
            try
            {
                var obj = await _dalc.ObtenerFlotasCategoriaClasificacionSubClasificacionSedeMarca(idCategoria, idClasificacion1, idSedeResponsable, marca, idClasificacion2);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosFlotas>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosFlotas>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosFlotas>>()
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
                return new ResponseBase<List<ActivosFlotas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosFlotas>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosFlotas>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosFlotas>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosFlotas>>()
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
                return new ResponseBase<List<ActivosFlotas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosFlotas>>> GetPorSedeAsync(long idSede)
        {
            try
            {
                var obj = await _dalc.GetPorSedeAsync(idSede);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosFlotas>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosFlotas>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosFlotas>>()
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
                return new ResponseBase<List<ActivosFlotas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosFlotas>>> GetPorEmpresaAsync(long idEmpresa)
        {
            try
            {
                var obj = await _dalc.GetPorEmpresaAsync(idEmpresa);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosFlotas>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosFlotas>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay FLotas disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosFlotas>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de Flotas no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ActivosFlotas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosFlotas>> SetAsync(ActivosFlotas objeto, Transaction transaccion)
        {
            try
            {
                var data = await _dalc.SetAsync(objeto, transaccion);
                if (data != null)
                {
                    return new ResponseBase<ActivosFlotas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación sobre {_namespace} realizada con exito",
                        datos = data
                    };
                }
                else
                    return new ResponseBase<ActivosFlotas>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación sobre {_namespace} solicitada no se pudo realizar.",
                        datos = data
                    };

            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosFlotas>()
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
