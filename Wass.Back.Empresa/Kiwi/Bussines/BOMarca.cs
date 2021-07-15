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
    public class BOMarca
    {
        private readonly DALCMarca _dalc;
        public BOMarca(EmpresaContext context)
        {
            _dalc = new DALCMarca(context);
        }

        public async Task<ResponseBase<Marca>> getMarca(long idMarca)
        {
            try
            {
                var marca = await _dalc.get(idMarca);

                if (marca != null)
                {
                    return new ResponseBase<Marca>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = marca
                    };
                }
                else
                {
                    return new ResponseBase<Marca>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "la consulta no arrojo resultado",
                        datos = { }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Marca>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Marca>> getSubMarca(long idSubMarca)
        {
            try
            {
                var marca = await _dalc.getSub(idSubMarca);

                if (marca != null)
                {
                    return new ResponseBase<Marca>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = marca
                    };
                }
                else
                {
                    return new ResponseBase<Marca>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "la consulta no arrojo resultado",
                        datos = { }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Marca>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Marca>>> getMarcas()
        {
            try
            {
                var marcas = await _dalc.getTodas();

                if (marcas != null)
                {
                    return new ResponseBase<List<Marca>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = marcas
                    };
                }
                else
                {
                    return new ResponseBase<List<Marca>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "la consulta no arrojo resultado",
                        datos = new List<Marca>()
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Marca>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Marca>> Guardar(Marca marca, Transaction transaction)
        {
            try
            {
                var newMarca = await _dalc.Set(marca, transaction);

                if (marca != null)
                {
                    return new ResponseBase<Marca>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "La operacion tuvo exito",
                        datos = newMarca
                    };
                }
                else
                {
                    return new ResponseBase<Marca>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "La operacion no tuvo exito",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Marca>()
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
