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
    public class BOActivosVariables : IBOCrud<ActivosVariables>
    {
        public Dictionary<string, string> EndPointsDictinoDictionary { get; set; }
        private readonly DALCActivosVariables _dalc;
        private readonly string _msg_base;

        public BOActivosVariables(EmpresaContext context)
        {
            _dalc = new DALCActivosVariables(context);
            _msg_base = "variables valores";
        }

        public async Task<ResponseBase<ActivosVariables>> GetAsync(long id)
        {
            try
            {
                var datos = await _dalc.GetAsync(id);

                if (datos != null)
                {
                    return new ResponseBase<ActivosVariables>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<ActivosVariables>()
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
                return new ResponseBase<ActivosVariables>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosVariables>> GetPorCalsificacionActivoAsync(long idActivoClasificacionVariable)
        {
            try
            {
                var datos = await _dalc.GetPorCalsificacionActivoAsync(idActivoClasificacionVariable);

                if (datos != null)
                {
                    return new ResponseBase<ActivosVariables>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<ActivosVariables>()
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
                return new ResponseBase<ActivosVariables>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosVariables>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosVariables>>()
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
                return new ResponseBase<List<ActivosVariables>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosVariables>>> GetPorFlotaAsync(Guid idActivoFlota)
        {
            try
            {
                var obj = await _dalc.GetPorFlotaAsync(idActivoFlota);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosVariables>>()
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
                return new ResponseBase<List<ActivosVariables>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosVariables>>> GetPorEquipoAsync(Guid idActivoEquipo)
        {
            try
            {
                var obj = await _dalc.GetPorEquipoAsync(idActivoEquipo);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosVariables>>()
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
                return new ResponseBase<List<ActivosVariables>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosVariables>>> GetPorOrdenFechaEquipoAsync(Guid idActivoEquipo)
        {
            try
            {
                var obj = await _dalc.GetPorOrdenFechaEquipoAsync(idActivoEquipo);
                var result = new List<ActivosVariables>();
                long count = 0;
                DateTime fechaMeses = DateTime.Now;

                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        foreach (var item in obj)
                        {
                            if (count == 0)
                            {
                                fechaMeses = item.fechaCreacion.AddMonths(-3);
                            }
                            //Console.WriteLine(fechaMeses.ToString("d"));
                            var compararfechas = DateTime.Compare(item.fechaCreacion, fechaMeses);
                            if (compararfechas > 0)
                            {
                                result.Add(item);
                            }
                            count++;
                            //Console.WriteLine("i: "+item+ "compararfechas: " + compararfechas);
                        }
                        if (result.Count > 0)
                        {
                            return new ResponseBase<List<ActivosVariables>>()
                            {
                                codigo = (int)HttpStatusCode.OK,
                                estado = true,
                                mensaje = string.Empty,
                                datos = result
                            };
                        }
                        else
                        {
                            return new ResponseBase<List<ActivosVariables>>()
                            {
                                codigo = (int)HttpStatusCode.NotFound,
                                estado = true,
                                mensaje = $"La consulta de {_msg_base} no retornó resultados.",
                                datos = null
                            };
                        }
                    }
                    else
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosVariables>>()
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
                return new ResponseBase<List<ActivosVariables>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosVariables>>> GetPorUltimoEquipoAsync(Guid idActivoEquipo)
        {
            try
            {
                var obj = await _dalc.GetPorOrdenFechaEquipoAsync(idActivoEquipo);
                var result = new List<ActivosVariables>();

                if (obj != null)
                {
                    if (obj.Count > 0)
                    {

                        foreach (var item in obj)
                        {
                            var ultimo = await _dalc.GetPorUltimoEquipoAsync(idActivoEquipo, item.idActivoClasificacionVariable);
                            if (!result.Contains(ultimo))
                            {
                                result.Add(ultimo);
                            }

                        }
                        if (result.Count > 0)
                        {
                            return new ResponseBase<List<ActivosVariables>>()
                            {
                                codigo = (int)HttpStatusCode.OK,
                                estado = true,
                                mensaje = string.Empty,
                                datos = result
                            };
                        }
                        else
                        {
                            return new ResponseBase<List<ActivosVariables>>()
                            {
                                codigo = (int)HttpStatusCode.NotFound,
                                estado = true,
                                mensaje = $"La consulta de {_msg_base} no retornó resultados.",
                                datos = null
                            };
                        }
                    }
                    else
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosVariables>>()
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
                return new ResponseBase<List<ActivosVariables>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosVariables>>> GetPorUltimoFlotaAsync(Guid idActivoEquipo)
        {
            try
            {
                var obj = await _dalc.GetPorOrdenFechaFlotaAsync(idActivoEquipo);
                var result = new List<ActivosVariables>();

                if (obj != null)
                {
                    if (obj.Count > 0)
                    {

                        foreach (var item in obj)
                        {
                            var ultimo = await _dalc.GetPorUltimoFlotaAsync(idActivoEquipo, item.idActivoClasificacionVariable);
                            if (!result.Contains(ultimo))
                            {
                                result.Add(ultimo);
                            }

                        }
                        if (result.Count > 0)
                        {
                            return new ResponseBase<List<ActivosVariables>>()
                            {
                                codigo = (int)HttpStatusCode.OK,
                                estado = true,
                                mensaje = string.Empty,
                                datos = result
                            };
                        }
                        else
                        {
                            return new ResponseBase<List<ActivosVariables>>()
                            {
                                codigo = (int)HttpStatusCode.NotFound,
                                estado = true,
                                mensaje = $"La consulta de {_msg_base} no retornó resultados.",
                                datos = null
                            };
                        }
                    }
                    else
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosVariables>>()
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
                return new ResponseBase<List<ActivosVariables>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosVariables>>> GetPorOrdenFechaFlotaAsync(Guid idActivoFlota)
        {
            try
            {
                var obj = await _dalc.GetPorOrdenFechaFlotaAsync(idActivoFlota);
                var result = new List<ActivosVariables>();
                long count = 0;
                DateTime fechaMeses = DateTime.Now;

                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        foreach (var item in obj)
                        {
                            if (count == 0)
                            {
                                fechaMeses = item.fechaCreacion.AddMonths(-3);
                            }
                            //Console.WriteLine(fechaMeses.ToString("d"));
                            var compararfechas = DateTime.Compare(item.fechaCreacion, fechaMeses);
                            if (compararfechas > 0)
                            {
                                result.Add(item);
                            }
                            count++;
                            //Console.WriteLine("i: "+item+ "compararfechas: " + compararfechas);
                        }
                        if (result.Count > 0)
                        {
                            return new ResponseBase<List<ActivosVariables>>()
                            {
                                codigo = (int)HttpStatusCode.OK,
                                estado = true,
                                mensaje = string.Empty,
                                datos = result
                            };
                        }
                        else
                        {
                            return new ResponseBase<List<ActivosVariables>>()
                            {
                                codigo = (int)HttpStatusCode.NotFound,
                                estado = true,
                                mensaje = $"La consulta de {_msg_base} no retornó resultados.",
                                datos = null
                            };
                        }
                    }
                    else
                        return new ResponseBase<List<ActivosVariables>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosVariables>>()
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
                return new ResponseBase<List<ActivosVariables>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosVariables>> SetAsync(ActivosVariables objeto, Transaction transaccion)
        {
            try
            {
                var data = await _dalc.SetAsync(objeto, transaccion);
                if (data != null)
                {
                    return new ResponseBase<ActivosVariables>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación sobre {_msg_base} realizada con exito",
                        datos = data
                    };
                }
                else
                    return new ResponseBase<ActivosVariables>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
                        datos = data
                    };

            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosVariables>()
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
