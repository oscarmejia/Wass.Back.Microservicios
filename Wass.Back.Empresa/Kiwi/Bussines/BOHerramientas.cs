using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOHerramientas
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCHerramientas _dalc;

        public BOHerramientas(EmpresaContext context)
        {
            _dalc = new DALCHerramientas(context);
        }

        public async Task<ResponseBase<List<Herramientas>>> GetTodas()
        {
            try
            {
                var herramienta = await _dalc.GetTodas();

                if (herramienta != null)
                {
                    return new ResponseBase<List<Herramientas>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = herramienta
                    };
                }
                else
                {
                    return new ResponseBase<List<Herramientas>>()
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
                return new ResponseBase<List<Herramientas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Herramientas>>> GetTodasPorSede(long idSede)
        {
            try
            {
                var herramienta = await _dalc.GetPorSede(idSede);

                if (herramienta != null)
                {
                    return new ResponseBase<List<Herramientas>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = herramienta
                    };
                }
                else
                {
                    return new ResponseBase<List<Herramientas>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultados",
                        datos = herramienta
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Herramientas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Herramientas>> GetPorId(long idHerramienta)
        {
            try
            {
                var herramienta = await _dalc.GetPorId(idHerramienta);
                if (herramienta != null)
                {
                    return new ResponseBase<Herramientas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = herramienta
                    };
                }
                else
                {
                    return new ResponseBase<Herramientas>()
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
                return new ResponseBase<Herramientas>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Herramientas>>> GetPorEmpresaAsync(long idEmpresa)
        {
            try
            {
                var obj = await _dalc.GetPorEmpresaAsync(idEmpresa);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<Herramientas>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<Herramientas>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay Herramientas disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<Herramientas>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de Herramientas no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Herramientas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Herramientas>> SaveHerramienta(Herramientas herramienta, Transaction transaction)
        {
            var ob = JsonConvert.DeserializeObject<Herramientas>(JsonConvert.SerializeObject(herramienta));

            if (transaction == Transaction.Insert)
            {
                var herramientas = await _dalc.Set(ob, transaction);

                if (herramientas != null)
                {
                    return new ResponseBase<Herramientas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "La Herramienta se creo correctamente",
                        datos = herramientas
                    };
                }
                else
                {
                    return new ResponseBase<Herramientas>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La Herramienta no se creo",
                        datos = null
                    };
                }
            }
            else
            {
                var herramientas = await _dalc.Set(ob, transaction);

                if (herramientas != null)
                {
                    return new ResponseBase<Herramientas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "La Herramienta se actualizo correctamente",
                        datos = herramientas
                    };
                }
                else
                {
                    return new ResponseBase<Herramientas>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La Herramienta no se actualizo",
                        datos = null
                    };
                }
            }
        }


        public async Task<ResponseBase<Herramientas>> EliminarHerramienta(Herramientas _data)
        {
            try
            {

                var result = await _dalc.Eliminar(_data.idHerramienta);
                if (result.estado)
                {
                    return new ResponseBase<Herramientas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación realizada con exito",
                        datos = null
                    };
                }
                else
                {
                    return new ResponseBase<Herramientas>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación solicitada no se pudo realizar.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Herramientas>()
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
