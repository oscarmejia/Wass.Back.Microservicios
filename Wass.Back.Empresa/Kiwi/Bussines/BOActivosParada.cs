using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.ActivoParada;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;

namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOActivosParada
    {
        public Dictionary<string, string> EndPointsDictinoDictionary { get; set; }
        private readonly DALCActivosParada _dalc;
        private readonly string _msg_base;
        private readonly DALCActivosEquipos _dALCActivosEquipos;
        private readonly DALCActivosFlotas _dALCActivosFlotas;
        public BOActivosParada(EmpresaContext context)
        {
            _dalc = new DALCActivosParada(context);
            _msg_base = "Parada de Activos";
            _dALCActivosEquipos = new DALCActivosEquipos(context);
            _dALCActivosFlotas = new DALCActivosFlotas(context);
        }

        public async Task<ResponseBase<ActivosParadaRequest>> GetAsync(long idActivosParada)
        {
            try
            {
                var datos = await _dalc.GetAsync(idActivosParada);

                if (datos != null)
                {
                    var activoEquipo = await _dALCActivosEquipos.GetAsync(datos.idActivo);
                    var activoFlota = await _dALCActivosFlotas.GetAsync(datos.idActivo);
                    if (activoEquipo != null)
                    {
                        var respuesta = new ActivosParadaRequest()
                        {
                            idActivosParada = datos.idActivosParada,
                            idActivo = datos.idActivo,
                            idOrden = datos.idOrden,
                            fechaHoraParada = datos.fechaHoraParada,
                            fechaHoraReactivacion = datos.fechaHoraReactivacion,
                            tipoOrden = datos.tipoOrden,
                            idCuadrilla = datos.idCuadrilla,
                            idSede = datos.idSede,
                            idEmpresa = datos.idEmpresa,
                            horasParada = datos.horasParada,
                            Activo = activoEquipo
                        };
                        return new ResponseBase<ActivosParadaRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else if (activoFlota != null)
                    {
                        var respuesta = new ActivosParadaRequest()
                        {
                            idActivosParada = datos.idActivosParada,
                            idActivo = datos.idActivo,
                            idOrden = datos.idOrden,
                            fechaHoraParada = datos.fechaHoraParada,
                            fechaHoraReactivacion = datos.fechaHoraReactivacion,
                            tipoOrden = datos.tipoOrden,
                            idCuadrilla = datos.idCuadrilla,
                            idSede = datos.idSede,
                            idEmpresa = datos.idEmpresa,
                            horasParada = datos.horasParada,
                            Activo = activoFlota
                        };
                        return new ResponseBase<ActivosParadaRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                    {
                        return new ResponseBase<ActivosParadaRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"La {_msg_base} no esta disponible.",
                            datos = null
                        };
                    }


                }
                else
                {
                    return new ResponseBase<ActivosParadaRequest>()
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
                return new ResponseBase<ActivosParadaRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<List<ActivosParadaRequest>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();
                var respuesta = new List<ActivosParadaRequest>();

                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        foreach (var item in obj)
                        {
                            var activoEquipo = await _dALCActivosEquipos.GetAsync(item.idActivo);
                            var activoFlota = await _dALCActivosFlotas.GetAsync(item.idActivo);

                            if (activoEquipo != null)
                            {
                                respuesta.Add(new ActivosParadaRequest()
                                {
                                    idActivosParada = item.idActivosParada,
                                    idActivo = item.idActivo,
                                    idOrden = item.idOrden,
                                    fechaHoraParada = item.fechaHoraParada,
                                    fechaHoraReactivacion = item.fechaHoraReactivacion,
                                    tipoOrden = item.tipoOrden,
                                    idCuadrilla = item.idCuadrilla,
                                    idSede = item.idSede,
                                    idEmpresa = item.idEmpresa,
                                    horasParada = item.horasParada,
                                    Activo = activoEquipo
                                });
                            }

                            if (activoFlota != null)
                            {
                                respuesta.Add(new ActivosParadaRequest()
                                {
                                    idActivosParada = item.idActivosParada,
                                    idActivo = item.idActivo,
                                    idOrden = item.idOrden,
                                    fechaHoraParada = item.fechaHoraParada,
                                    fechaHoraReactivacion = item.fechaHoraReactivacion,
                                    tipoOrden = item.tipoOrden,
                                    idCuadrilla = item.idCuadrilla,
                                    idSede = item.idSede,
                                    idEmpresa = item.idEmpresa,
                                    horasParada = item.horasParada,
                                    Activo = activoFlota
                                });

                            }
                        }
                        return new ResponseBase<List<ActivosParadaRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };

                    }
                    else
                        return new ResponseBase<List<ActivosParadaRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosParadaRequest>>()
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
                return new ResponseBase<List<ActivosParadaRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosParada>>> GetAllParadosAsync(DateTime fechaActual)
        {
            try
            {
                var obj = await _dalc.GetAllParadosAsync(fechaActual);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosParada>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosParada>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosParada>>()
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
                return new ResponseBase<List<ActivosParada>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<List<ActivosParadaSedeRequest>>> GetPorSedeAsync(long idSede, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var obj = await _dalc.GetPorSedeAsync(idSede);
                double countHours = 0;
                var obtenerPorActivo = new List<ActivosParada>();
                var respuesta = new List<ActivosParadaSedeRequest>();

                HashSet<Guid> idActivosPorSede = new HashSet<Guid>();

                if (obj != null)
                {
                    foreach (var item in obj)
                    {
                        idActivosPorSede.Add(item.idActivo);

                    }

                    if (idActivosPorSede != null)
                    {
                        foreach (var idActivo in idActivosPorSede)
                        {
                            obtenerPorActivo = await _dalc.GetPorActivoAsync(idActivo);
                            countHours = 0;
                            foreach (var activo in obtenerPorActivo)
                            {

                                if (activo.fechaHoraReactivacion.HasValue)
                                {
                                    var inicial = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaInicio);
                                    var final = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaFinal);
                                    if (inicial >= 0 && final <= 0)
                                    {
                                        countHours += activo.horasParada;

                                    }
                                }

                            }

                            respuesta.Add(new ActivosParadaSedeRequest()
                            {
                                idSede = idSede,
                                idActivo = idActivo,
                                horasParada = countHours
                            });
                        }

                    }

                    if (respuesta != null)
                    {
                        return new ResponseBase<List<ActivosParadaSedeRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                        return new ResponseBase<List<ActivosParadaSedeRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };

                }
                else
                {
                    return new ResponseBase<List<ActivosParadaSedeRequest>>()
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
                return new ResponseBase<List<ActivosParadaSedeRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosParadaSedePromedioRequest>>> GetPromedioPorSedeAsync(long idSede, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var obj = await _dalc.GetPorSedeAsync(idSede);
                double countHours = 0;
                double count = 0;
                double promedioHoras = 0;
                var obtenerPorActivo = new List<ActivosParada>();
                var respuesta = new List<ActivosParadaSedePromedioRequest>();

                HashSet<Guid> idActivosPorSede = new HashSet<Guid>();

                if (obj != null)
                {
                    foreach (var item in obj)
                    {
                        idActivosPorSede.Add(item.idActivo);

                    }

                    if (idActivosPorSede != null)
                    {
                        foreach (var idActivo in idActivosPorSede)
                        {
                            obtenerPorActivo = await _dalc.GetPorActivoAsync(idActivo);
                            countHours = 0;
                            foreach (var activo in obtenerPorActivo)
                            {

                                if (activo.fechaHoraReactivacion.HasValue)
                                {
                                    var inicial = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaInicio);
                                    var final = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaFinal);
                                    if (inicial >= 0 && final <= 0)
                                    {
                                        countHours += activo.horasParada;
                                        count++;
                                    }
                                }

                            }
                            if (count > 0)
                            {
                                promedioHoras = countHours / count;
                            }
                            respuesta.Add(new ActivosParadaSedePromedioRequest()
                            {
                                idSede = idSede,
                                idActivo = idActivo,
                                promedioHorasParada = promedioHoras
                            });
                        }

                    }

                    if (respuesta != null)
                    {
                        return new ResponseBase<List<ActivosParadaSedePromedioRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                        return new ResponseBase<List<ActivosParadaSedePromedioRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };

                }
                else
                {
                    return new ResponseBase<List<ActivosParadaSedePromedioRequest>>()
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
                return new ResponseBase<List<ActivosParadaSedePromedioRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosParadaSedeDisponibleRequest>>> GetTiempoDisponiblePorSedeAsync(long idSede, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var obj = await _dalc.GetPorSedeAsync(idSede);
                double countHours = 0;
                double count = 0;
                double promedioHoras = 0;
                var obtenerPorActivo = new List<ActivosParada>();
                var respuesta = new List<ActivosParadaSedeDisponibleRequest>();

                TimeSpan countTotalHours = fechaFinal.Subtract(fechaInicio);
                double horasTotales = countTotalHours.TotalHours;
                double procentajeHorasDisponible = 0;
                double porcentajeHorasParada = 0;

                HashSet<Guid> idActivosPorSede = new HashSet<Guid>();

                if (obj != null)
                {
                    foreach (var item in obj)
                    {
                        idActivosPorSede.Add(item.idActivo);

                    }

                    if (idActivosPorSede != null)
                    {
                        foreach (var idActivo in idActivosPorSede)
                        {
                            obtenerPorActivo = await _dalc.GetPorActivoAsync(idActivo);
                            countHours = 0;
                            foreach (var activo in obtenerPorActivo)
                            {

                                if (activo.fechaHoraReactivacion.HasValue)
                                {
                                    var inicial = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaInicio);
                                    var final = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaFinal);
                                    if (inicial >= 0 && final <= 0)
                                    {
                                        countHours += activo.horasParada;
                                        count++;
                                    }
                                }

                            }
                            if (count > 0)
                            {
                                promedioHoras = countHours / count;
                            }

                            porcentajeHorasParada = (promedioHoras * 100) / horasTotales;

                            procentajeHorasDisponible = 100 - porcentajeHorasParada;
                            respuesta.Add(new ActivosParadaSedeDisponibleRequest()
                            {
                                idSede = idSede,
                                idActivo = idActivo,
                                porcentajeHorasDisponible = procentajeHorasDisponible
                            });
                        }

                    }

                    if (respuesta != null)
                    {
                        return new ResponseBase<List<ActivosParadaSedeDisponibleRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                        return new ResponseBase<List<ActivosParadaSedeDisponibleRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };

                }
                else
                {
                    return new ResponseBase<List<ActivosParadaSedeDisponibleRequest>>()
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
                return new ResponseBase<List<ActivosParadaSedeDisponibleRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosParadaActivoRequest>> GetPorActivoAsync(Guid idActivo, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var obj = await _dalc.GetPorActivoAsync(idActivo);
                double countHours = 0;


                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        foreach (var item in obj)
                        {
                            if (item.fechaHoraReactivacion.HasValue)
                            {
                                var inicial = DateTime.Compare(item.fechaHoraReactivacion.Value.Date, fechaInicio);
                                var final = DateTime.Compare(item.fechaHoraReactivacion.Value.Date, fechaFinal);
                                if (inicial >= 0 && final <= 0)
                                {
                                    countHours += item.horasParada;
                                }
                            }
                        }

                        var activosParada = new ActivosParadaActivoRequest()
                        {
                            idActivo = idActivo,
                            horasParada = countHours
                        };
                        return new ResponseBase<ActivosParadaActivoRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = activosParada
                        };
                    }
                    else
                        return new ResponseBase<ActivosParadaActivoRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<ActivosParadaActivoRequest>()
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
                return new ResponseBase<ActivosParadaActivoRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosParadaActivoDisponibleRequest>> GetTiempoDisponiblePorActivoAsync(Guid idActivo, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var obj = await _dalc.GetPorActivoAsync(idActivo);
                double countHours = 0;
                double count = 0;
                double promedioHoras = 0;

                TimeSpan countTotalHours = fechaFinal.Subtract(fechaInicio);
                double horasTotales = countTotalHours.TotalHours;
                double procentajeHorasDisponible = 0;
                double porcentajeHorasParada = 0;


                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        foreach (var item in obj)
                        {
                            if (item.fechaHoraReactivacion.HasValue)
                            {
                                var inicial = DateTime.Compare(item.fechaHoraReactivacion.Value.Date, fechaInicio);
                                var final = DateTime.Compare(item.fechaHoraReactivacion.Value.Date, fechaFinal);
                                if (inicial >= 0 && final <= 0)
                                {
                                    countHours += item.horasParada;
                                    count++;
                                }
                            }
                        }
                        if (count > 0)
                        {
                            promedioHoras = countHours / count;
                        }

                        porcentajeHorasParada = (promedioHoras * 100) / horasTotales;

                        procentajeHorasDisponible = 100 - porcentajeHorasParada;

                        var activosParada = new ActivosParadaActivoDisponibleRequest()
                        {
                            idActivo = idActivo,
                            porcentajeHorasDisponible = procentajeHorasDisponible
                        };
                        return new ResponseBase<ActivosParadaActivoDisponibleRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = activosParada
                        };
                    }
                    else
                        return new ResponseBase<ActivosParadaActivoDisponibleRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<ActivosParadaActivoDisponibleRequest>()
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
                return new ResponseBase<ActivosParadaActivoDisponibleRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosParadaActivoPromedioRequest>> GetPromedioPorActivoAsync(Guid idActivo, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var obj = await _dalc.GetPorActivoAsync(idActivo);
                double countHours = 0;
                double count = 0;
                double promedioHoras = 0;


                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        foreach (var item in obj)
                        {
                            if (item.fechaHoraReactivacion.HasValue)
                            {
                                var inicial = DateTime.Compare(item.fechaHoraReactivacion.Value.Date, fechaInicio);
                                var final = DateTime.Compare(item.fechaHoraReactivacion.Value.Date, fechaFinal);
                                if (inicial >= 0 && final <= 0)
                                {
                                    countHours += item.horasParada;
                                    count++;
                                }
                            }
                        }
                        if (count > 0)
                        {
                            promedioHoras = countHours / count;
                        }
                        var activosParada = new ActivosParadaActivoPromedioRequest()
                        {
                            idActivo = idActivo,
                            promedioHorasParada = promedioHoras
                        };
                        return new ResponseBase<ActivosParadaActivoPromedioRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = activosParada
                        };
                    }
                    else
                        return new ResponseBase<ActivosParadaActivoPromedioRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<ActivosParadaActivoPromedioRequest>()
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
                return new ResponseBase<ActivosParadaActivoPromedioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosParadaEmpresaPromedioRequest>>> GetPromedioPorEmpresaAsync(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var obj = await _dalc.GetPorEmpresaAsync(idEmpresa);
                double countHours = 0;
                var obtenerPorActivo = new List<ActivosParada>();
                var respuesta = new List<ActivosParadaEmpresaPromedioRequest>();
                double count = 0;
                double promedioHoras = 0;

                HashSet<Guid> idActivosPorEmpresa = new HashSet<Guid>();

                if (obj != null)
                {
                    foreach (var item in obj)
                    {
                        idActivosPorEmpresa.Add(item.idActivo);
                    }

                    if (idActivosPorEmpresa != null)
                    {
                        foreach (var idActivo in idActivosPorEmpresa)
                        {
                            obtenerPorActivo = await _dalc.GetPorActivoAsync(idActivo);
                            countHours = 0;
                            foreach (var activo in obtenerPorActivo)
                            {

                                if (activo.fechaHoraReactivacion.HasValue)
                                {
                                    var inicial = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaInicio);
                                    var final = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaFinal);
                                    if (inicial >= 0 && final <= 0)
                                    {
                                        countHours += activo.horasParada;
                                        count++;

                                    }
                                }

                            }

                            if (count > 0)
                            {
                                promedioHoras = countHours / count;
                            }

                            respuesta.Add(new ActivosParadaEmpresaPromedioRequest()
                            {
                                idEmpresa = idEmpresa,
                                idActivo = idActivo,
                                promedioHorasParada = promedioHoras
                            });
                        }

                    }

                    if (respuesta != null)
                    {
                        return new ResponseBase<List<ActivosParadaEmpresaPromedioRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                        return new ResponseBase<List<ActivosParadaEmpresaPromedioRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };

                }
                else
                {
                    return new ResponseBase<List<ActivosParadaEmpresaPromedioRequest>>()
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
                return new ResponseBase<List<ActivosParadaEmpresaPromedioRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosParadaEmpresaDisponibleRequest>>> GetTiempoDisponiblePorEmpresaAsync(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var obj = await _dalc.GetPorEmpresaAsync(idEmpresa);
                double countHours = 0;
                var obtenerPorActivo = new List<ActivosParada>();
                var respuesta = new List<ActivosParadaEmpresaDisponibleRequest>();
                double count = 0;
                double promedioHoras = 0;

                HashSet<Guid> idActivosPorEmpresa = new HashSet<Guid>();

                TimeSpan countTotalHours = fechaFinal.Subtract(fechaInicio);
                double horasTotales = countTotalHours.TotalHours;
                double procentajeHorasDisponible = 0;
                double porcentajeHorasParada = 0;

                if (obj != null)
                {
                    foreach (var item in obj)
                    {
                        idActivosPorEmpresa.Add(item.idActivo);
                    }

                    if (idActivosPorEmpresa != null)
                    {
                        foreach (var idActivo in idActivosPorEmpresa)
                        {
                            obtenerPorActivo = await _dalc.GetPorActivoAsync(idActivo);
                            countHours = 0;
                            foreach (var activo in obtenerPorActivo)
                            {

                                if (activo.fechaHoraReactivacion.HasValue)
                                {
                                    var inicial = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaInicio);
                                    var final = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaFinal);
                                    if (inicial >= 0 && final <= 0)
                                    {
                                        countHours += activo.horasParada;
                                        count++;

                                    }
                                }

                            }

                            if (count > 0)
                            {
                                promedioHoras = countHours / count;
                            }

                            porcentajeHorasParada = (promedioHoras * 100) / horasTotales;

                            procentajeHorasDisponible = 100 - porcentajeHorasParada;

                            respuesta.Add(new ActivosParadaEmpresaDisponibleRequest()
                            {
                                idEmpresa = idEmpresa,
                                idActivo = idActivo,
                                porcentajeHorasDisponible = procentajeHorasDisponible
                            });
                        }

                    }

                    if (respuesta != null)
                    {
                        return new ResponseBase<List<ActivosParadaEmpresaDisponibleRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                        return new ResponseBase<List<ActivosParadaEmpresaDisponibleRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };

                }
                else
                {
                    return new ResponseBase<List<ActivosParadaEmpresaDisponibleRequest>>()
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
                return new ResponseBase<List<ActivosParadaEmpresaDisponibleRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosParadaEmpresaRequest>>> GetPorEmpresaAsync(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var obj = await _dalc.GetPorEmpresaAsync(idEmpresa);
                double countHours = 0;
                var obtenerPorActivo = new List<ActivosParada>();
                var respuesta = new List<ActivosParadaEmpresaRequest>();

                HashSet<Guid> idActivosPorEmpresa = new HashSet<Guid>();

                if (obj != null)
                {
                    foreach (var item in obj)
                    {
                        idActivosPorEmpresa.Add(item.idActivo);
                    }

                    if (idActivosPorEmpresa != null)
                    {
                        foreach (var idActivo in idActivosPorEmpresa)
                        {
                            obtenerPorActivo = await _dalc.GetPorActivoAsync(idActivo);
                            countHours = 0;
                            foreach (var activo in obtenerPorActivo)
                            {

                                if (activo.fechaHoraReactivacion.HasValue)
                                {
                                    var inicial = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaInicio);
                                    var final = DateTime.Compare(activo.fechaHoraReactivacion.Value.Date, fechaFinal);
                                    if (inicial >= 0 && final <= 0)
                                    {
                                        countHours += activo.horasParada;

                                    }
                                }

                            }

                            respuesta.Add(new ActivosParadaEmpresaRequest()
                            {
                                idEmpresa = idEmpresa,
                                idActivo = idActivo,
                                horasParada = countHours
                            });
                        }

                    }

                    if (respuesta != null)
                    {
                        return new ResponseBase<List<ActivosParadaEmpresaRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                        return new ResponseBase<List<ActivosParadaEmpresaRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };

                }
                else
                {
                    return new ResponseBase<List<ActivosParadaEmpresaRequest>>()
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
                return new ResponseBase<List<ActivosParadaEmpresaRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosParada>>> GetTodasPorActivoAsync(Guid idActivo)
        {
            try
            {
                var obj = await _dalc.GetPorActivoAsync(idActivo);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosParada>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosParada>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosParada>>()
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
                return new ResponseBase<List<ActivosParada>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<List<ActivosParada>>> GetPorCuadrillaAsync(long idCuadrilla)
        {
            try
            {
                var obj = await _dalc.GetPorCuadrillaAsync(idCuadrilla);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosParada>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosParada>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosParada>>()
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
                return new ResponseBase<List<ActivosParada>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosParada>> actualizarFechaReactivacion(DateTime fechaHoraReactivacion, Guid idActivo, long idActivosParada)
        {
            try
            {
                var dataActivosParada = await _dalc.GetPorActivosParadaYActivoAsync(idActivo, idActivosParada);
                if (dataActivosParada != null)
                {
                    dataActivosParada.fechaHoraReactivacion = fechaHoraReactivacion;
                    TimeSpan span = fechaHoraReactivacion.Subtract(dataActivosParada.fechaHoraParada);
                    dataActivosParada.horasParada = Convert.ToDouble(span.TotalHours);

                    var dataRespuesta = await _dalc.SetAsync(dataActivosParada, Transaction.Update);
                    return new ResponseBase<ActivosParada>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación sobre {_msg_base} realizada con exito",
                        datos = dataRespuesta
                    };
                }
                else
                    return new ResponseBase<ActivosParada>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
                        datos = null
                    };

            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosParada>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<ActivosParada>> SetAsync(ActivosParada objeto, Transaction transaccion)
        {
            try
            {
                var data = await _dalc.SetAsync(objeto, transaccion);
                if (data != null)
                {
                    return new ResponseBase<ActivosParada>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación sobre {_msg_base} realizada con exito",
                        datos = data
                    };
                }
                else
                    return new ResponseBase<ActivosParada>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
                        datos = data
                    };

            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosParada>()
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
