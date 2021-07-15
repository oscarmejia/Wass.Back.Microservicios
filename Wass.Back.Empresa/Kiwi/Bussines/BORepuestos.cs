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
    public class BORepuestos
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCRepuestos _dalc;

        public BORepuestos(EmpresaContext context)
        {
            _dalc = new DALCRepuestos(context);
        }

        public async Task<ResponseBase<List<Repuestos>>> GetTodas()
        {
            try
            {
                var repuestos = await _dalc.GetTodas();

                if (repuestos != null)
                {
                    return new ResponseBase<List<Repuestos>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestos
                    };
                }
                else
                {
                    return new ResponseBase<List<Repuestos>>()
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
                return new ResponseBase<List<Repuestos>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Repuestos>>> GetTodasPorCategoria(long idCategoria)
        {
            try
            {
                var repuestos = await _dalc.GetTodasPorCategoria(idCategoria);

                if (repuestos != null)
                {
                    return new ResponseBase<List<Repuestos>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestos
                    };
                }
                else
                {
                    return new ResponseBase<List<Repuestos>>()
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
                return new ResponseBase<List<Repuestos>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Repuestos>>> GetTodasPorClasificacion(long idClasificacion)
        {
            try
            {
                var repuestos = await _dalc.GetTodasPorClasificacion(idClasificacion);

                if (repuestos != null)
                {
                    return new ResponseBase<List<Repuestos>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestos
                    };
                }
                else
                {
                    return new ResponseBase<List<Repuestos>>()
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
                return new ResponseBase<List<Repuestos>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Repuestos>> GetPorId(long idRepuestos)
        {
            try
            {
                var repuestos = await _dalc.Get(idRepuestos);
                if (repuestos != null)
                {
                    return new ResponseBase<Repuestos>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = repuestos
                    };
                }
                else
                {
                    return new ResponseBase<Repuestos>()
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
                return new ResponseBase<Repuestos>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<Repuestos>> guardarRepuestos(Repuestos repuestos, Transaction transaction)
        {
            try
            {
                // Clasificacion ABC: (A - Alto valor, B - Valor medio, C - Bajo valor)
                // Criticidad. Grado 1 (baja criticidad), Grado 2 (criticidad media), Grado 3 (alta criticidad).
                var dataRepuestos = await _dalc.Set(repuestos, transaction);


                if (dataRepuestos != null)
                {
                    return new ResponseBase<Repuestos>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = repuestos
                    };
                }
                else
                {
                    return new ResponseBase<Repuestos>()
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
                return new ResponseBase<Repuestos>()
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
