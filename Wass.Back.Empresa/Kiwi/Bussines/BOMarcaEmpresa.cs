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
    public class BOMarcaEmpresa
    {
        private readonly DALCMarcaEmpresa _dalc;
        public BOMarcaEmpresa(EmpresaContext context)
        {
            _dalc = new DALCMarcaEmpresa(context);
        }

        public async Task<ResponseBase<MarcaEmpresa>> get(long idMarcaEmpresa)
        {
            try
            {
                var marcaEmpresa = await _dalc.get(idMarcaEmpresa);

                if (marcaEmpresa != null)
                {

                    return new ResponseBase<MarcaEmpresa>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = marcaEmpresa
                    };
                }
                else
                {
                    return new ResponseBase<MarcaEmpresa>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "la consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<MarcaEmpresa>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<MarcaEmpresa>>> getTodas()
        {
            try
            {
                var marcaEmpresa = await _dalc.GetTodas();

                if (marcaEmpresa != null)
                {

                    return new ResponseBase<List<MarcaEmpresa>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = marcaEmpresa
                    };
                }
                else
                {
                    return new ResponseBase<List<MarcaEmpresa>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "la consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<MarcaEmpresa>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<MarcaEmpresa>>> getTodasPorEmpresa(long idEmpresa)
        {
            try
            {
                var marcaEmpresa = await _dalc.GetTodasPorEmpresa(idEmpresa);

                if (marcaEmpresa != null)
                {

                    return new ResponseBase<List<MarcaEmpresa>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = marcaEmpresa
                    };
                }
                else
                {
                    return new ResponseBase<List<MarcaEmpresa>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "la consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<MarcaEmpresa>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<MarcaEmpresa>>> getTodasPorMarca(long idMarca)
        {
            try
            {
                var marcaEmpresa = await _dalc.GetTodasPorMarca(idMarca);

                if (marcaEmpresa != null)
                {

                    return new ResponseBase<List<MarcaEmpresa>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = marcaEmpresa
                    };
                }
                else
                {
                    return new ResponseBase<List<MarcaEmpresa>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "la consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<MarcaEmpresa>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<MarcaEmpresa>> set(MarcaEmpresa marcaEmpresa, Transaction transaction)
        {
            try
            {
                var newMarcaEmpresa = await _dalc.Set(marcaEmpresa, transaction);

                if (newMarcaEmpresa != null)
                {

                    return new ResponseBase<MarcaEmpresa>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = newMarcaEmpresa
                    };
                }
                else
                {
                    return new ResponseBase<MarcaEmpresa>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "La operacion fallo",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<MarcaEmpresa>()
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
