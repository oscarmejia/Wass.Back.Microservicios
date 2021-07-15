using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BOOrdenesTrabajo : IBOCrud<OrdenTrabajoRequest>
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCOrdenesTrabajo _dalc;
        private readonly DALCMantenimientoAviso _dalcMantenimientoAviso;
        private readonly DALCMantenimientoCorrectivo _dalcMantenimientoCorrectivo;
        private readonly DALCMantenimientoPreventivo _dalcMantenimientoPreventivo;
        private readonly DALCPlanMantenimientoRondas _dalcMantenimientoRondas;
        private readonly DALCLicitacion _dalcLicitacion;
        private readonly DLACCotizaciones _dalcCotizacion;
        private readonly string _msg_base;

        public BOOrdenesTrabajo(ProgramadorContext context)
        {
            _dalc = new DALCOrdenesTrabajo(context);
            _msg_base = " ordenes de trabajo";
            _dalcMantenimientoAviso = new DALCMantenimientoAviso(context);
            _dalcMantenimientoCorrectivo = new DALCMantenimientoCorrectivo(context);
            _dalcMantenimientoPreventivo = new DALCMantenimientoPreventivo(context);
            _dalcMantenimientoRondas = new DALCPlanMantenimientoRondas(context);
            _dalcLicitacion = new DALCLicitacion(context);
            _dalcCotizacion = new DLACCotizaciones(context);
        }



        public async Task<ResponseBase<OrdenTrabajoRequest>> GetPrueba(long id)
        {
            try
            {
                var datos = await _dalc.GetPrueba(id);
                if (datos != null)
                {
                    var data = new OrdenTrabajoRequest()
                    {
                        aprobador = datos.aprobador,
                        creador = datos.creador,
                        editor = datos.editor,
                        eliminada = datos.eliminada,
                        facturada = datos.facturada,
                        idEmpresa = datos.idEmpresa,
                        idSede = datos.idSede,
                        idOrden = datos.idOrden,
                        prioridad = (Prioridad)datos.prioridad,
                        fechaProgramacionInicio = datos.fechaProgramacionInicio,
                        fechaProgramacionCierre = datos.fechaProgramacionCierre,
                        motivoAnulacion = datos.motivoAnulacion,
                        programador = datos.programador,
                        idServicio = (Servicio)datos.idServicio,
                        fechaCierre = datos.fechaCierre,
                        fechaCreacion = datos.fechaCreacion,
                        fechaLimiteServicio = datos.fechaLimiteServicio,
                        nivelUrgencia = datos.nivelUrgencia,
                        variableDecision = datos.variableDecision,
                        idProveedorAsignado = datos.idProveedorAsignado,
                        idEstadoOrden = (EstadosOrden)datos.idEstadoOrden,
                        fechaEdicion = datos.fechaEdicion,
                        idCuadrilla = datos.idCuadrilla,
                        fechaAtencion = datos.fechaAtencion,
                        datosActivos = !string.IsNullOrEmpty(datos.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(datos.datosActivos) : new List<ActivosRequest>(),
                        ArchivosAdjuntos = datos.ArchivosAdjuntos,
                        Incidencias = datos.Incidencias,
                        mantenimientoAviso = datos.mantenimientoAviso,
                        mantenimientoCorrectivo = datos.mantenimientoCorrectivo,
                        mantenimientoPreventivo = datos.mantenimientoPreventivo,
                        mantenimientoRondas = datos.mantenimientoRondas
                    };
                    return new ResponseBase<OrdenTrabajoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<OrdenTrabajoRequest>()
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
                return new ResponseBase<OrdenTrabajoRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetAllPrueba()
        {
            try
            {
                var obj = await _dalc.GetAllPrueba();
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias,
                                mantenimientoAviso = item.mantenimientoAviso,
                                mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                mantenimientoPreventivo = item.mantenimientoPreventivo,
                                mantenimientoRondas = item.mantenimientoRondas
                            };
                            datos.Add(data);
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<OrdenTrabajoRequest>> Get(long id)
        {
            try
            {
                var datos = await _dalc.Get(id);
                if (datos != null)
                {

                    var data = new OrdenTrabajoRequest()
                    {
                        aprobador = datos.aprobador,
                        creador = datos.creador,
                        editor = datos.editor,
                        eliminada = datos.eliminada,
                        facturada = datos.facturada,
                        idEmpresa = datos.idEmpresa,
                        idSede = datos.idSede,
                        idOrden = datos.idOrden,
                        prioridad = (Prioridad)datos.prioridad,
                        fechaProgramacionInicio = datos.fechaProgramacionInicio,
                        fechaProgramacionCierre = datos.fechaProgramacionCierre,
                        motivoAnulacion = datos.motivoAnulacion,
                        programador = datos.programador,
                        idServicio = (Servicio)datos.idServicio,
                        fechaCierre = datos.fechaCierre,
                        fechaCreacion = datos.fechaCreacion,
                        fechaLimiteServicio = datos.fechaLimiteServicio,
                        nivelUrgencia = datos.nivelUrgencia,
                        variableDecision = datos.variableDecision,
                        idProveedorAsignado = datos.idProveedorAsignado,
                        idEstadoOrden = (EstadosOrden)datos.idEstadoOrden,
                        fechaEdicion = datos.fechaEdicion,
                        idCuadrilla = datos.idCuadrilla,
                        fechaAtencion = datos.fechaAtencion,
                        datosActivos = !string.IsNullOrEmpty(datos.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(datos.datosActivos) : new List<ActivosRequest>(),
                        ArchivosAdjuntos = datos.ArchivosAdjuntos,
                        Incidencias = datos.Incidencias,
                        mantenimientoAviso = datos.mantenimientoAviso,
                        mantenimientoCorrectivo = datos.mantenimientoCorrectivo,
                        mantenimientoPreventivo = datos.mantenimientoPreventivo,
                        mantenimientoRondas = datos.mantenimientoRondas
                    };



                    return new ResponseBase<OrdenTrabajoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<OrdenTrabajoRequest>()
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
                return new ResponseBase<OrdenTrabajoRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        

        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetHistoricoActivoAsync(Guid idActivo)
        {
            try
            {
                var obj = await _dalc.GetHistoricoActivoAsync(idActivo);
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias,
                                mantenimientoAviso = item.mantenimientoAviso,
                                mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                mantenimientoPreventivo = item.mantenimientoPreventivo,
                                mantenimientoRondas = item.mantenimientoRondas
                            };
                           
                            datos.Add(data);
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetHistoricoActivoVariable(Guid idActivo)
        {
            try
            {
                var obj = await _dalc.GetHistoricoActivoVariable(idActivo);
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                            var mantenimientoRonda = await _dalcMantenimientoRondas.GetPorOrdenAsync(item.idOrden);
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias,
                                mantenimientoAviso = item.mantenimientoAviso,
                                mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                mantenimientoPreventivo = item.mantenimientoPreventivo,
                                mantenimientoRondas = item.mantenimientoRondas
                            };
                            
                            
                            datos.Add(data);
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<TiempoPromedioRequest>> GetAllPorFechaCierreLlenaMantenimientoCorrectivo(long cantidadOrdenesTrabajo, long idDiagnostico)
        {
            try
            {
                var obj = await _dalc.GetAllPorFechaCierreLlena();
                var mantenimientosCorrectivo = await _dalcMantenimientoCorrectivo.GetAllPorDiagnostico(idDiagnostico);
                long count = 0;
                TimeSpan horasDiferencia = TimeSpan.Zero; // Cantidad de Horas que hay entre la fecha de Atencion del mantenimiento y la fecha de cierre de la Orden de trabajo
                double tiempo = 0;
                double tiempoPromedio = 0;
                if (obj != null && mantenimientosCorrectivo != null)
                {
                    if (obj.Count > 0 && mantenimientosCorrectivo.Count > 0)
                    {
                        var datos = new List<MantenimientoRequest>();
                        var respuesta = new TiempoPromedioRequest();
                        foreach (var item in obj)
                        {
                            foreach (var mantenimiento in mantenimientosCorrectivo)
                            {
                                if (mantenimiento.idOrden == item.idOrden && item.fechaAtencion != null && count < cantidadOrdenesTrabajo)
                                {
                                    horasDiferencia = Convert.ToDateTime(item.fechaCierre).Subtract(Convert.ToDateTime(item.fechaAtencion));
                                    Console.WriteLine("Horas: " + horasDiferencia.TotalHours);

                                    datos.Add(new MantenimientoRequest()
                                    {
                                        mantenimiento = mantenimiento,
                                        fechaAtencion = Convert.ToDateTime(item.fechaAtencion),
                                        fechaCierreOrden = Convert.ToDateTime(item.fechaCierre),
                                    });

                                    count++;
                                    tiempo += horasDiferencia.TotalHours;
                                    Console.WriteLine("Tiempo Horas: " + tiempo);
                                }
                            }
                        }

                        if (count > 0)
                        {
                            tiempoPromedio = tiempo / count;
                            Console.WriteLine("Tiempo Promedio: " + tiempoPromedio);
                        }

                        if (datos.Count > 0)
                        {

                            respuesta = new TiempoPromedioRequest()
                            {
                                mantenimientoRequest = datos,
                                tiempoPromedioTrabajo = tiempoPromedio
                            };

                        }
                        return new ResponseBase<TiempoPromedioRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                    {
                        return new ResponseBase<TiempoPromedioRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<TiempoPromedioRequest>()
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
                return new ResponseBase<TiempoPromedioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<TiempoPromedioRequest>> GetAllPorFechaCierreLlenaMantenimientoPreventivo(long cantidadOrdenesTrabajo, long idGrupo)
        {
            try
            {
                var obj = await _dalc.GetAllPorFechaCierreLlena();
                var mantenimientosPreventivo = await _dalcMantenimientoPreventivo.GetAllPorGrupo(idGrupo);
                long count = 0;
                TimeSpan horasDiferencia = TimeSpan.Zero; // Cantidad de Horas que hay entre la fecha de Atencion del mantenimiento y la fecha de cierre de la Orden de trabajo
                double tiempo = 0;
                double tiempoPromedio = 0;
                if (obj != null && mantenimientosPreventivo != null)
                {
                    if (obj.Count > 0 && mantenimientosPreventivo.Count > 0)
                    {
                        var datos = new List<MantenimientoRequest>();
                        var respuesta = new TiempoPromedioRequest();
                        foreach (var item in obj)
                        {
                            foreach (var mantenimiento in mantenimientosPreventivo)
                            {
                                if (mantenimiento.idOrden == item.idOrden && item.fechaAtencion != null && count < cantidadOrdenesTrabajo)
                                {
                                    horasDiferencia = Convert.ToDateTime(item.fechaCierre).Subtract(Convert.ToDateTime(item.fechaAtencion));
                                    Console.WriteLine("Horas: " + horasDiferencia.TotalHours);

                                    datos.Add(new MantenimientoRequest()
                                    {
                                        mantenimiento = mantenimiento,
                                        fechaAtencion = Convert.ToDateTime(item.fechaAtencion),
                                        fechaCierreOrden = Convert.ToDateTime(item.fechaCierre),
                                    });

                                    count++;
                                    tiempo += horasDiferencia.TotalHours;
                                    Console.WriteLine("Tiempo Horas: " + tiempo);
                                }
                            }
                        }

                        if (count > 0)
                        {
                            tiempoPromedio = tiempo / count;
                            Console.WriteLine("Tiempo Promedio: " + tiempoPromedio);
                        }

                        if (datos.Count > 0)
                        {
                            respuesta = new TiempoPromedioRequest()
                            {
                                mantenimientoRequest = datos,
                                tiempoPromedioTrabajo = tiempoPromedio
                            };

                        }
                        return new ResponseBase<TiempoPromedioRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                    {
                        return new ResponseBase<TiempoPromedioRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<TiempoPromedioRequest>()
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
                return new ResponseBase<TiempoPromedioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<TiempoPromedioRequest>> GetAllPorFechaCierreLlenaMantenimientoRondas(long cantidadOrdenesTrabajo, long idGrupo)
        {
            try
            {
                var obj = await _dalc.GetAllPorFechaCierreLlena();
                var mantenimientosRonda = await _dalcMantenimientoRondas.GetAllPorGrupo(idGrupo);
                long count = 0;
                TimeSpan horasDiferencia = TimeSpan.Zero; // Cantidad de Horas que hay entre la fecha de Atencion del mantenimiento y la fecha de cierre de la Orden de trabajo
                double tiempo = 0;
                double tiempoPromedio = 0;
                if (obj != null && mantenimientosRonda != null)
                {
                    if (obj.Count > 0 && mantenimientosRonda.Count > 0)
                    {
                        var datos = new List<MantenimientoRequest>();
                        var respuesta = new TiempoPromedioRequest();
                        foreach (var item in obj)
                        {
                            foreach (var mantenimiento in mantenimientosRonda)
                            {
                                if (mantenimiento.idOrden == item.idOrden && item.fechaAtencion != null && count < cantidadOrdenesTrabajo)
                                {
                                    horasDiferencia = Convert.ToDateTime(item.fechaCierre).Subtract(Convert.ToDateTime(item.fechaAtencion));
                                    Console.WriteLine("Horas: " + horasDiferencia.TotalHours);

                                    datos.Add(new MantenimientoRequest()
                                    {
                                        mantenimiento = mantenimiento,
                                        fechaAtencion = Convert.ToDateTime(item.fechaAtencion),
                                        fechaCierreOrden = Convert.ToDateTime(item.fechaCierre),
                                    });

                                    count++;
                                    tiempo += horasDiferencia.TotalHours;
                                    Console.WriteLine("Tiempo Horas: " + tiempo);
                                }
                            }
                        }

                        if (count > 0)
                        {
                            tiempoPromedio = tiempo / count;
                            Console.WriteLine("Tiempo Promedio: " + tiempoPromedio);
                        }

                        if (datos.Count > 0)
                        {
                            respuesta = new TiempoPromedioRequest()
                            {
                                mantenimientoRequest = datos,
                                tiempoPromedioTrabajo = tiempoPromedio
                            };

                        }
                        return new ResponseBase<TiempoPromedioRequest>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = respuesta
                        };
                    }
                    else
                    {
                        return new ResponseBase<TiempoPromedioRequest>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<TiempoPromedioRequest>()
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
                return new ResponseBase<TiempoPromedioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetAll()
        {
            try
            {
                var obj = await _dalc.GetAll();
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                            
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias
                            };

                            datos.Add(data);
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetTodasPorCuadrillaAsync(long idCuadrilla)
        {
            try
            {
                var obj = await _dalc.GetTodasPorCuadrillaAsync(idCuadrilla);
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias,
                                mantenimientoAviso = item.mantenimientoAviso,
                                mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                mantenimientoPreventivo = item.mantenimientoPreventivo,
                                mantenimientoRondas = item.mantenimientoRondas
                            };
                            
                            datos.Add(data);
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetAllPorSede(long idSede)
        {
            try
            {
                var obj = await _dalc.GetAllPorSede(idSede);
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                            var mantenimientoAviso = await _dalcMantenimientoAviso.GetPorOrdenAsync(item.idOrden);
                            var mantenimientoCorrectivo = await _dalcMantenimientoCorrectivo.GetPorOrdenAsync(item.idOrden);
                            var mantenimientoPreventivo = await _dalcMantenimientoPreventivo.GetPorOrdenAsync(item.idOrden);
                            var mantenimientoRonda = await _dalcMantenimientoRondas.GetPorOrdenAsync(item.idOrden);
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias,
                                mantenimientoAviso = item.mantenimientoAviso,
                                mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                mantenimientoPreventivo = item.mantenimientoPreventivo,
                                mantenimientoRondas = item.mantenimientoRondas
                            };
                            datos.Add(data);
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetAllPorEmpresa(long idEmpresa)
        {
            try
            {
                var obj = await _dalc.GetAllPorEmpresa(idEmpresa);
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                           
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias,
                                mantenimientoAviso = item.mantenimientoAviso,
                                mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                mantenimientoPreventivo = item.mantenimientoPreventivo,
                                mantenimientoRondas = item.mantenimientoRondas
                            };
                            datos.Add(data);
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetAllPorEmpresaPorTercerizar(long idEmpresa)
        {
            try
            {
                var obj = await _dalc.GetAllPorEmpresaPorTercerizar(idEmpresa);
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                            
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias,
                                mantenimientoAviso = item.mantenimientoAviso,
                                mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                mantenimientoPreventivo = item.mantenimientoPreventivo,
                                mantenimientoRondas = item.mantenimientoRondas
                            };
                        
                            datos.Add(data);
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetAllPorEmpresaEnEjecucion(long idEmpresa)
        {
            try
            {
                var obj = await _dalc.GetAllSinCerrar(idEmpresa);
                
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                            var licitacionesAdjudicadas = await _dalcLicitacion.GetIdEmpresaAdjudicadasPorOrdenTrabajo(idEmpresa, item.idOrden);
                            foreach(var licitacion in licitacionesAdjudicadas)
                            {
                                var cotizacionesAdjudicadas = await _dalcCotizacion.GetPorLicitacionEmpresa(licitacion.idLicitacion, idEmpresa);
                                if (cotizacionesAdjudicadas != null && cotizacionesAdjudicadas.Count > 0)
                                {
                                    
                                    var data = new OrdenTrabajoRequest()
                                    {
                                        aprobador = item.aprobador,
                                        creador = item.creador,
                                        editor = item.editor,
                                        eliminada = item.eliminada,
                                        facturada = item.facturada,
                                        idEmpresa = item.idEmpresa,
                                        idSede = item.idSede,
                                        idOrden = item.idOrden,
                                        prioridad = (Prioridad)item.prioridad,
                                        fechaProgramacionInicio = item.fechaProgramacionInicio,
                                        fechaProgramacionCierre = item.fechaProgramacionCierre,
                                        motivoAnulacion = item.motivoAnulacion,
                                        programador = item.programador,
                                        idServicio = (Servicio)item.idServicio,
                                        fechaCierre = item.fechaCierre,
                                        fechaCreacion = item.fechaCreacion,
                                        fechaLimiteServicio = item.fechaLimiteServicio,
                                        nivelUrgencia = item.nivelUrgencia,
                                        variableDecision = item.variableDecision,
                                        idProveedorAsignado = item.idProveedorAsignado,
                                        idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                        fechaEdicion = item.fechaEdicion,
                                        idCuadrilla = item.idCuadrilla,
                                        fechaAtencion = item.fechaAtencion,
                                        datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                        ArchivosAdjuntos = item.ArchivosAdjuntos,
                                        Incidencias = item.Incidencias,
                                        mantenimientoAviso = item.mantenimientoAviso,
                                        mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                        mantenimientoPreventivo = item.mantenimientoPreventivo,
                                        mantenimientoRondas = item.mantenimientoRondas
                                    };
                                 
                                  
                                    datos.Add(data);
                                }
                            }
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetAllPorActivo(Guid idActivo)
        {
            try
            {
                var obj = await _dalc.GetAllPorActivo(idActivo);
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                           
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias,
                                mantenimientoAviso = item.mantenimientoAviso,
                                mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                mantenimientoPreventivo = item.mantenimientoPreventivo,
                                mantenimientoRondas = item.mantenimientoRondas
                            };
                       
                            datos.Add(data);
                        }

                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<OrdenTrabajoRequest>>> GetPorBusquedaAsync(BusquedasOrdenesRequest mensaje)
        {
            try
            {
                var obj = await _dalc.GetPorBusquedaAsync(mensaje);
                if (obj != null)
                {
                    if (obj.Count > 0)
                    {
                        var datos = new List<OrdenTrabajoRequest>();
                        foreach (var item in obj)
                        {
                            
                            var data = new OrdenTrabajoRequest()
                            {
                                aprobador = item.aprobador,
                                creador = item.creador,
                                editor = item.editor,
                                eliminada = item.eliminada,
                                facturada = item.facturada,
                                idEmpresa = item.idEmpresa,
                                idSede = item.idSede,
                                idOrden = item.idOrden,
                                prioridad = (Prioridad)item.prioridad,
                                fechaProgramacionInicio = item.fechaProgramacionInicio,
                                fechaProgramacionCierre = item.fechaProgramacionCierre,
                                motivoAnulacion = item.motivoAnulacion,
                                programador = item.programador,
                                idServicio = (Servicio)item.idServicio,
                                fechaCierre = item.fechaCierre,
                                fechaCreacion = item.fechaCreacion,
                                fechaLimiteServicio = item.fechaLimiteServicio,
                                nivelUrgencia = item.nivelUrgencia,
                                variableDecision = item.variableDecision,
                                idProveedorAsignado = item.idProveedorAsignado,
                                idEstadoOrden = (EstadosOrden)item.idEstadoOrden,
                                fechaEdicion = item.fechaEdicion,
                                idCuadrilla = item.idCuadrilla,
                                fechaAtencion = item.fechaAtencion,
                                datosActivos = !string.IsNullOrEmpty(item.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.datosActivos) : new List<ActivosRequest>(),
                                ArchivosAdjuntos = item.ArchivosAdjuntos,
                                Incidencias = item.Incidencias,
                                mantenimientoAviso = item.mantenimientoAviso,
                                mantenimientoCorrectivo = item.mantenimientoCorrectivo,
                                mantenimientoPreventivo = item.mantenimientoPreventivo,
                                mantenimientoRondas = item.mantenimientoRondas
                            };
                         
                         
                            datos.Add(data);
                        }
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = datos
                        };
                    }
                    else
                    {
                        return new ResponseBase<List<OrdenTrabajoRequest>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<List<OrdenTrabajoRequest>>()
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
                return new ResponseBase<List<OrdenTrabajoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<OrdenTrabajoRequest>> Set(OrdenTrabajoRequest objeto, Transaction transaccion)
        {
            try
            {
                #region Preparando el objeto
                var objeto_transformado = new OrdenesTrabajo()
                {
                    aprobador = objeto.aprobador,
                    creador = objeto.creador,
                    editor = objeto.editor,
                    eliminada = objeto.eliminada,
                    facturada = objeto.facturada,
                    idEmpresa = objeto.idEmpresa,
                    idSede = objeto.idSede,
                    idOrden = objeto.idOrden,
                    prioridad = (int)objeto.prioridad,
                    fechaProgramacionInicio = objeto.fechaProgramacionInicio,
                    fechaProgramacionCierre = objeto.fechaProgramacionCierre,
                    motivoAnulacion = objeto.motivoAnulacion,
                    programador = objeto.programador,
                    idServicio = (int)objeto.idServicio,
                    fechaCierre = objeto.fechaCierre,
                    fechaCreacion = objeto.fechaCreacion,
                    fechaLimiteServicio = objeto.fechaLimiteServicio,
                    nivelUrgencia = objeto.nivelUrgencia,
                    variableDecision = objeto.variableDecision,
                    idProveedorAsignado = objeto.idProveedorAsignado,
                    idEstadoOrden = (int)objeto.idEstadoOrden,
                    fechaEdicion = objeto.fechaEdicion,
                    idCuadrilla = objeto.idCuadrilla,
                    fechaAtencion = objeto.fechaAtencion,
                    datosActivos = JsonConvert.SerializeObject(objeto.datosActivos)
                };

                var data_transformada = await _dalc.Set(objeto_transformado, transaccion);
                var data = new OrdenTrabajoRequest()
                {
                    aprobador = data_transformada.aprobador,
                    creador = data_transformada.creador,
                    editor = data_transformada.editor,
                    eliminada = data_transformada.eliminada,
                    facturada = data_transformada.facturada,
                    idEmpresa = data_transformada.idEmpresa,
                    idSede = data_transformada.idSede,
                    idOrden = data_transformada.idOrden,
                    prioridad = (Prioridad)data_transformada.prioridad,
                    fechaProgramacionInicio = data_transformada.fechaProgramacionInicio,
                    fechaProgramacionCierre = data_transformada.fechaProgramacionCierre,
                    motivoAnulacion = data_transformada.motivoAnulacion,
                    programador = data_transformada.programador,
                    idServicio = (Servicio)data_transformada.idServicio,
                    fechaCierre = data_transformada.fechaCierre,
                    fechaCreacion = data_transformada.fechaCreacion,
                    fechaLimiteServicio = data_transformada.fechaLimiteServicio,
                    nivelUrgencia = data_transformada.nivelUrgencia,
                    variableDecision = data_transformada.variableDecision,
                    idProveedorAsignado = data_transformada.idProveedorAsignado,
                    idEstadoOrden = (EstadosOrden)data_transformada.idEstadoOrden,
                    fechaEdicion = data_transformada.fechaEdicion,
                    idCuadrilla = data_transformada.idCuadrilla,
                    fechaAtencion = data_transformada.fechaAtencion,
                    datosActivos = !string.IsNullOrEmpty(data_transformada.datosActivos) ? JsonConvert.DeserializeObject<List<ActivosRequest>>(data_transformada.datosActivos) : new List<ActivosRequest>()
                };

                #endregion
                if (data != null)
                {
                    return new ResponseBase<OrdenTrabajoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación sobre {_msg_base} realizada con exito",
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<OrdenTrabajoRequest>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
                        datos = data
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<OrdenTrabajoRequest>()
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
