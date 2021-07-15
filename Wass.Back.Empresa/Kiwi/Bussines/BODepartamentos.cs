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
    public class BODepartamentos : IBOLectura<Departamentos>
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCDepartamentos _dalc;

        public BODepartamentos(EmpresaContext context)
        {
            _dalc = new DALCDepartamentos(context);
        }

        public async Task<ResponseBase<Departamentos>> GetAsync(long id)
        {
            try
            {
                var obj = await _dalc.GetAsync(id);

                if (obj != null)
                {
                    return new ResponseBase<Departamentos>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = obj
                    };
                }
                else
                {
                    return new ResponseBase<Departamentos>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "El pais consultado no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Departamentos>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Departamentos>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Departamentos>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Departamentos>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay departamentos disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Departamentos>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de departamentos no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Departamentos>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Departamentos>>> GetPorPaisAsync(long idPais)
        {
            try
            {
                var obj = await _dalc.GetPorPaisAsync(idPais);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Departamentos>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Departamentos>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay departamentos disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Departamentos>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de departamentos no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Departamentos>>()
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
