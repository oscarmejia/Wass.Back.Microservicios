using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.Cotizaciones;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BOCotizaciones 
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly ProgramadorContext _context;

        private readonly DLACCotizaciones _dalc;

        private readonly DALCLicitacion _dalcLicitacion;

        private readonly string _msg_base;

        public BOCotizaciones(ProgramadorContext context)
        {
            _context = context;
            _dalc = new DLACCotizaciones(_context);
            _msg_base = " Cotizaciones";
            _dalcLicitacion = new DALCLicitacion(_context);
        }

        public async Task<ResponseBase<Cotizaciones>> getAsync(long idCotizacion)
        {
            try
            {
                var cotizacion = await _dalc.getAsync(idCotizacion);
                
                if (cotizacion != null)
                {
                    return new ResponseBase<Cotizaciones>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cotizacion
                    };
                }
                else
                {
                    return new ResponseBase<Cotizaciones>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro esta Cotizacion",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Cotizaciones>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Cotizaciones>>> getTodasAsync()
        {
            try
            {
               
                var cotizacion = await _dalc.getTodasAsync();

                if (cotizacion != null)
                {
                    return new ResponseBase<List<Cotizaciones>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cotizacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Cotizaciones>>()
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
                return new ResponseBase<List<Cotizaciones>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Cotizaciones>>> getTodasPorSedeAsync(long idSede)
        {
            try
            {

                var cotizacion = await _dalc.getTodasPorSedeAsync(idSede);
                if (cotizacion != null)
                {
                    return new ResponseBase<List<Cotizaciones>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cotizacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Cotizaciones>>()
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
                return new ResponseBase<List<Cotizaciones>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<List<Cotizaciones>>> getTodasPorEmpresaAsync(long idEmpresa)
        {
            try
            {

                var cotizacion = await _dalc.getTodasPorEmpresaAsync(idEmpresa);
                
                if (cotizacion != null)
                {
                    return new ResponseBase<List<Cotizaciones>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cotizacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Cotizaciones>>()
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
                return new ResponseBase<List<Cotizaciones>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Cotizaciones>>> getByInterpriseAndState(long idEmpresa, long estadoCotizacion, long estadoLicitacion)
        {
            try
            {

                var cotizacion = await _dalc.getCotizacionesByState(idEmpresa, estadoCotizacion, estadoLicitacion);

                if (cotizacion != null)
                {
                    return new ResponseBase<List<Cotizaciones>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cotizacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Cotizaciones>>()
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
                return new ResponseBase<List<Cotizaciones>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Cotizaciones>>> getTodasPorEmpresaPago(long idEmpresa)
        {
            try
            {

                var cotizacion = await _dalc.GetIdEmpresaPago(idEmpresa);

                if (cotizacion != null)
                {
                    return new ResponseBase<List<Cotizaciones>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cotizacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Cotizaciones>>()
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
                return new ResponseBase<List<Cotizaciones>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Cotizaciones>>> getTodasPago(long idOrdenPago)
        {
            try
            {

                var cotizacion = await _dalc.GetIdOrdenPago(idOrdenPago);

                if (cotizacion != null)
                {
                    return new ResponseBase<List<Cotizaciones>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cotizacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Cotizaciones>>()
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
                return new ResponseBase<List<Cotizaciones>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Cotizaciones>>> GetPorLicitacion(long idLicitacion)
        {
            try
            {
                var cotizacion = await _dalc.GetPorLicitacion(idLicitacion);

                if (cotizacion != null && cotizacion.Count > 0)
                {
                    return new ResponseBase<List<Cotizaciones>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cotizacion
                    };
                }
                else
                {
                    return new ResponseBase<List<Cotizaciones>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Cotizaciones>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CotizacionesResponse>>> GetSumaMesAMesPorAnio(long idLicitacion, long idEmpresa)
        {
            try
            {
                var cotizaciones = await _dalc.GetPorLicitacionOrdenada(idLicitacion, idEmpresa);
                HashSet<long> listaAnios = new HashSet<long>();
                var listaSumas = new List<decimal>();
                var respuesta = new List<CotizacionesResponse>();

                if (cotizaciones != null && cotizaciones.Count > 0)
                {
                    foreach(var item in cotizaciones)
                    {
                        listaAnios.Add(item.fechaPropuestaServicioCotizacion.Year);
                    }

                    foreach (int anio in listaAnios)
                    {
                        var sumasMeses = new List<SumaCotizacionesPorMes>();
                        for (int mes = 1; mes <= 12; mes++)
                        {
                            decimal suma = 0;
                            foreach (var item in cotizaciones)
                            {
                                if (item.fechaPropuestaServicioCotizacion.Year == anio && item.fechaPropuestaServicioCotizacion.Month == mes)
                                {
                                    var primerDiaMes = new DateTime(anio, mes, 1);
                                    var ultimoDiaMes = primerDiaMes.AddMonths(1).AddDays(-1);

                                    int fechaInicial = DateTime.Compare(primerDiaMes, item.fechaPropuestaServicioCotizacion);
                                    int fechaFinal = DateTime.Compare(item.fechaPropuestaServicioCotizacion, ultimoDiaMes);
                                    if (fechaInicial <= 0 && fechaFinal <= 0)
                                    {
                                        suma += item.Costo;
                                    }
                                }
                            }
                            //listaSumas.Add(suma);
                            sumasMeses.Add(new SumaCotizacionesPorMes()
                            {
                                mes = mes,
                                suma = suma
                            });
                        }
                        respuesta.Add(new CotizacionesResponse()
                        {
                            anio = anio,
                            meses = sumasMeses
                        });
                    }
                    
                    return new ResponseBase<List<CotizacionesResponse>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<CotizacionesResponse>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CotizacionesResponse>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CotizacionesUltimoAnioResponse>>> GetSumaUltimoAnio(long idLicitacion)
        {
            try
            {
                var cotizaciones = await _dalc.GetPorLicitacionUltimoAnio(idLicitacion);
                var listaSumas = new List<decimal>();
                var respuesta = new List<CotizacionesUltimoAnioResponse>();
                var anioActual = DateTime.Now.Year;
                decimal sumaCostosCotizaciones = 0;

                if (cotizaciones != null && cotizaciones.Count > 0)
                {
                    foreach(var cotizacion in cotizaciones)
                    {
                        sumaCostosCotizaciones += cotizacion.Costo;
                    }
                    respuesta.Add(new CotizacionesUltimoAnioResponse()
                    {
                        anio = anioActual,
                        suma = sumaCostosCotizaciones
                    });

                    return new ResponseBase<List<CotizacionesUltimoAnioResponse>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<CotizacionesUltimoAnioResponse>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CotizacionesUltimoAnioResponse>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Cotizaciones>> setAsync(Cotizaciones objeto, Transaction transaccion)
        {
            try
            {
                //Estados cotizacion: Creada = 1, Enviada = 2, Ganada Total = 3, Rechazada = 4, Cancelada = 5, Ganada Parcial = 6
                
                var licitacion = await _dalcLicitacion.Get(objeto.idLicitacion);

                if(licitacion != null)
                {
                    int fechaInicio = DateTime.Compare(objeto.fechaPropuestaServicioCotizacion, licitacion.fechaLimiteRepCotizacion);
                    int fechaInicioFinalizacion = DateTime.Compare(objeto.fechaPropuestaServicioCotizacion, objeto.fechafinalizacionPropuestaServicioCotizacion);
                    int fechaFinalizacion = DateTime.Compare(objeto.fechafinalizacionPropuestaServicioCotizacion, licitacion.fechaLimiteRepCotizacion);

                    if (fechaInicio <= 0 && fechaInicioFinalizacion < 0 && fechaFinalizacion < 0)
                    {

                        var data = await _dalc.setAsync(objeto, transaccion);

                        if (data != null)
                        {
                            return new ResponseBase<Cotizaciones>()
                            {
                                codigo = (int)HttpStatusCode.OK,
                                estado = true,
                                mensaje = $"Operación sobre {_msg_base} realizada con exito",
                                datos = data
                            };
                        }
                        else
                            return new ResponseBase<Cotizaciones>()
                            {
                                codigo = (int)HttpStatusCode.NotFound,
                                estado = false,
                                mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
                                datos = null
                            };
                    }
                    else
                        return new ResponseBase<Cotizaciones>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = false,
                            mensaje = $"Fecha Propuesta para el Servicio de Cotizacion incorrecta. La fecha de inicio y de finalizacion para el Servicio de Cotizacion " +
                            $"debe ser antes de la fecha límite para recepción de Cotizaciones en la Licitación.",
                            datos = null
                        };
                }
                else
                    return new ResponseBase<Cotizaciones>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
                        datos = null
                    };

            }
            catch (Exception ex)
            {
                return new ResponseBase<Cotizaciones>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        

        public async Task<ResponseBase<Cotizaciones>> EliminarCotizacion(long idCotizacion)
        {
            try
            {
                var data = await _dalc.EliminarCotizacion(idCotizacion);
                

                return new ResponseBase<Cotizaciones>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre Cotizacion realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Cotizaciones>()
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
