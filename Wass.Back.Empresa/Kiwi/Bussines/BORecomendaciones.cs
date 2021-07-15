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
    public class BORecomendaciones
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCRecomendaciones _dalc;
        public BORecomendaciones(EmpresaContext context)
        {
            _dalc = new DALCRecomendaciones(context);
        }

        private async Task<ResponseBase<Recomendaciones>> ValidarReglasNegocio(Recomendaciones recomendaciones)
        {
            var result = await GetRecomendacionPorEmisorYReceptor(recomendaciones);

            if (!result.codigo.Equals((int)HttpStatusCode.OK))
            {
                return new ResponseBase<Recomendaciones>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = String.Empty,
                    datos = null
                };
            }
            else
            {
                return new ResponseBase<Recomendaciones>()
                {
                    codigo = (int)HttpStatusCode.Conflict,
                    estado = true,
                    mensaje = "Este proveedor ya tiene una recomendacion tuya",
                    datos = null
                };
            }

        }

        public async Task<ResponseBase<Recomendaciones>> GetRecomendacionPorEmisorYReceptor(Recomendaciones recomendaciones)
        {


            try
            {
                var recomendacion = await _dalc.GetRecomendacionPorEmisorYReceptor(recomendaciones.idEmpresaRecomienda, recomendaciones.idEmpresaRecomendada);
                if (recomendacion != null)
                {
                    return new ResponseBase<Recomendaciones>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = recomendacion
                    };
                }
                else
                {
                    return new ResponseBase<Recomendaciones>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "No se tiene recomendaciones hechas",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Recomendaciones>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Recomendaciones>> GetRecomendacionAsyn(int idRecomendacion)
        {
            try
            {
                var recomendacion = await _dalc.Get(idRecomendacion);

                if (recomendacion != null)
                {
                    return new ResponseBase<Recomendaciones>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = recomendacion
                    };

                }
                else
                {
                    return new ResponseBase<Recomendaciones>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "La recomedacion consultada no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Recomendaciones>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Recomendaciones>>> GetRecomendacionesAsync()
        {
            try
            {
                var recomendacion = await _dalc.GetTodas();

                if (recomendacion != null)
                {
                    if (recomendacion.Count > 0)
                    {
                        return new ResponseBase<List<Recomendaciones>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = String.Empty,
                            datos = recomendacion
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<Recomendaciones>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "No hay recomendaciones disponibles",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<Recomendaciones>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta de recomendaciones no retorno resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Recomendaciones>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Recomendaciones>>> GetTodasPorEmpresaRecomenda(long idEmpresaRecomendada)
        {
            try
            {
                var recomendacion = await _dalc.GetTodasPorIdRecomendado(idEmpresaRecomendada);

                if (recomendacion != null)
                {
                    if (recomendacion.Count > 0)
                    {
                        return new ResponseBase<List<Recomendaciones>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = String.Empty,
                            datos = recomendacion
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<Recomendaciones>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "este proveedor no tiene recomendaciones",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<Recomendaciones>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = "este proveedor no tiene recomendaciones",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Recomendaciones>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<Recomendaciones>> SaveRecomendacionAync(Recomendaciones recomendaciones, Transaction transaction)
        {
            try
            {
                var ob = JsonConvert.DeserializeObject<Recomendaciones>(JsonConvert.SerializeObject(recomendaciones));
                var reglasNegocio = await ValidarReglasNegocio(ob);

                if (reglasNegocio.codigo.Equals((int)HttpStatusCode.OK))
                {
                    var dataRecomendacion = await _dalc.Set(ob, transaction);

                    if (dataRecomendacion != null)
                    {
                        return new ResponseBase<Recomendaciones>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = String.Empty,
                            datos = dataRecomendacion
                        };
                    }
                    else
                    {
                        return new ResponseBase<Recomendaciones>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = "Operacion no tuvo exito",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<Recomendaciones>()
                    {
                        codigo = (int)HttpStatusCode.Conflict,
                        estado = false,
                        mensaje = reglasNegocio.mensaje,
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Recomendaciones>()
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
