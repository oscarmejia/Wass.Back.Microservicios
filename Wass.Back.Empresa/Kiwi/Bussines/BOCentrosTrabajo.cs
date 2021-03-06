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
    public class BOCentrosTrabajo : IBOCrud<CentrosTrabajo>
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCCentrosTrabajo _dalc;
        private readonly string _namespace;

        public BOCentrosTrabajo(EmpresaContext context)
        {
            _dalc = new DALCCentrosTrabajo(context);
            _namespace = "Centros de trabajo";
        }

        public async Task<ResponseBase<CentrosTrabajo>> GetAsync(long id)
        {
            try
            {
                var obj = await _dalc.GetAsync(id);

                if (obj != null)
                {
                    return new ResponseBase<CentrosTrabajo>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = obj
                    };
                }
                else
                {
                    return new ResponseBase<CentrosTrabajo>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = $"El {_namespace} consultado no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CentrosTrabajo>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CentrosTrabajo>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<CentrosTrabajo>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<CentrosTrabajo>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_namespace} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<CentrosTrabajo>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta de {_namespace} no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CentrosTrabajo>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<List<CentrosTrabajo>>> GetPorSedeAsync(long id)
        {
            try
            {
                var obj = await _dalc.GetPorSedeAsync(id);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<CentrosTrabajo>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<CentrosTrabajo>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_namespace} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<CentrosTrabajo>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta de {_namespace} no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CentrosTrabajo>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CentrosTrabajo>>> GetPorSedeActivaAsync(long id)
        {
            try
            {
                var obj = await _dalc.GetPorSedeActivaAsync(id);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<CentrosTrabajo>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<CentrosTrabajo>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_namespace} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<CentrosTrabajo>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta de {_namespace} no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CentrosTrabajo>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CentrosTrabajo>> SetAsync(CentrosTrabajo objeto, Transaction transaccion)
        {
            try
            {
                var data = await _dalc.SetAsync(objeto, transaccion);
                if (data != null)
                {
                    return new ResponseBase<CentrosTrabajo>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación sobre {_namespace} realizada con exito",
                        datos = data
                    };
                }
                else
                    return new ResponseBase<CentrosTrabajo>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación sobre {_namespace} solicitada no se pudo realizar.",
                        datos = data
                    };

            }
            catch (Exception ex)
            {
                return new ResponseBase<CentrosTrabajo>()
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
