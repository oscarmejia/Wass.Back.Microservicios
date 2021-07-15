using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.Cotizaciones;
using Wass.Back.Programador.Models.Peticiones.Licitacion;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BOLicitacion
    {

        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCLicitacion _dalc;
        private readonly BOCotizaciones _BOCotizaciones;
        private readonly DALCOrdenesTrabajo _dalcOrdenesTrabajo;

        public BOLicitacion(ProgramadorContext context)
        {
            _dalc = new DALCLicitacion(context);
            _dalcOrdenesTrabajo = new DALCOrdenesTrabajo(context);
            _BOCotizaciones = new BOCotizaciones(context);
        }

        public async Task<ResponseBase<LicitacionRequest>> Get(long idLicitacion)
        {
            try
            {
                var licitacion = await _dalc.Get(idLicitacion);

                var data = new LicitacionRequest()
                {
                    idLicitacion = licitacion.idLicitacion,
                    fechaLimiteRepCotizacion = licitacion.fechaLimiteRepCotizacion,
                    urgencia = licitacion.urgencia,
                    moneda = licitacion.moneda,
                    observaciones = licitacion.observaciones,
                    idOrden = licitacion.idOrden,
                    idSede = licitacion.idSede,
                    idEmpresa = licitacion.idEmpresa,
                    idCuestionario = licitacion.idCuestionario,
                    estado = licitacion.estado,
                    idSolicitudPedido = licitacion != null ? JsonConvert.DeserializeObject<List<long>>(licitacion.idSolicitudPedido) : new List<long>(),
                    empresasInvitadas = licitacion != null ? JsonConvert.DeserializeObject<List<long>>(licitacion.empresasInvitadas) : new List<long>(),
                    tipoLicitacion = licitacion.tipoLicitacion,
                    cronograma = licitacion.cronograma,
                    soportes = licitacion.soportes,
                    cotizaciones = licitacion.cotizaciones,
                    skills = licitacion.skills,
                    ArchivosAdjuntos = licitacion.ArchivosAdjuntos
                };
                if (licitacion != null)
                {
                    return new ResponseBase<LicitacionRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<LicitacionRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro esta licitacion",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<LicitacionRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<LicitacionRequest>> GetIdOrden(long idOrden)
        {
            try
            {
                var licitacion = await _dalc.GetIdOrden(idOrden);

                var data = new LicitacionRequest()
                {
                    idLicitacion = licitacion.idLicitacion,
                    fechaLimiteRepCotizacion = licitacion.fechaLimiteRepCotizacion,
                    urgencia = licitacion.urgencia,
                    moneda = licitacion.moneda,
                    observaciones = licitacion.observaciones,
                    idOrden = licitacion.idOrden,
                    idSede = licitacion.idSede,
                    idEmpresa = licitacion.idEmpresa,
                    idCuestionario = licitacion.idCuestionario,
                    estado = licitacion.estado,
                    idSolicitudPedido = licitacion != null ? JsonConvert.DeserializeObject<List<long>>(licitacion.idSolicitudPedido) : new List<long>(),
                    tipoLicitacion = licitacion.tipoLicitacion,
                    empresasInvitadas = licitacion != null ? JsonConvert.DeserializeObject<List<long>>(licitacion.empresasInvitadas) : new List<long>(),
                    cronograma = licitacion.cronograma,
                    soportes = licitacion.soportes,
                    cotizaciones = licitacion.cotizaciones,
                    skills = licitacion.skills,
                    ArchivosAdjuntos = licitacion.ArchivosAdjuntos
                };
                if (licitacion != null)
                {
                    return new ResponseBase<LicitacionRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<LicitacionRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitacion asociada a esta orden",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<LicitacionRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<List<LicitacionRequest>>> GetTodas()
        {
            try
            {
                var licitacion = await _dalc.GetTodas();

                var ob = new List<LicitacionRequest>();
                if (licitacion != null)
                {
                    foreach (var item in licitacion)
                    {
                        ob.Add(new LicitacionRequest()
                        {
                            idLicitacion = item.idLicitacion,
                            fechaLimiteRepCotizacion = item.fechaLimiteRepCotizacion,
                            urgencia = item.urgencia,
                            moneda = item.moneda,
                            observaciones = item.observaciones,
                            idOrden = item.idOrden,
                            idSede = item.idSede,
                            idEmpresa = item.idEmpresa,
                            idCuestionario = item.idCuestionario,
                            estado = item.estado,
                            idSolicitudPedido = item != null ? JsonConvert.DeserializeObject<List<long>>(item.idSolicitudPedido) : new List<long>(),
                            tipoLicitacion = item.tipoLicitacion,
                            empresasInvitadas = item != null ? JsonConvert.DeserializeObject<List<long>>(item.empresasInvitadas) : new List<long>(),
                            cronograma = item.cronograma,
                            soportes = item.soportes,
                            cotizaciones = item.cotizaciones,
                            skills = item.skills,
                            ArchivosAdjuntos = item.ArchivosAdjuntos
                        });
                    }
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitacion asociada a esta orden",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<LicitacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }



        public async Task<ResponseBase<List<LicitacionRequest>>> GetAllByInvitation(long idEmpresa)
        {
            try
            {
                var licitacion = await _dalc.getLicitacionPorInvitacion(idEmpresa);

                var ob = new List<LicitacionRequest>();
                var obf = new List<LicitacionRequest>();
                if (licitacion != null)
                {
                    foreach (var item in licitacion)
                    {
                        ob.Add(new LicitacionRequest()
                        {
                            idLicitacion = item.idLicitacion,
                            fechaLimiteRepCotizacion = item.fechaLimiteRepCotizacion,
                            urgencia = item.urgencia,
                            moneda = item.moneda,
                            observaciones = item.observaciones,
                            idOrden = item.idOrden,
                            idSede = item.idSede,
                            idEmpresa = item.idEmpresa,
                            idCuestionario = item.idCuestionario,
                            estado = item.estado,
                            idSolicitudPedido = item != null ? JsonConvert.DeserializeObject<List<long>>(item.idSolicitudPedido) : new List<long>(),
                            tipoLicitacion = item.tipoLicitacion,
                            empresasInvitadas = item != null ? JsonConvert.DeserializeObject<List<long>>(item.empresasInvitadas) : new List<long>(),
                            cronograma = item.cronograma,
                            OrdenTrabajo = new OrdenLicitacion()
                            {
                                nombreActivo = item != null ? JsonConvert.DeserializeObject<List<ActivosRequest>>(item.OrdenTrabajo.datosActivos) : new List<ActivosRequest>(),
                                tipoLicitacion = item.OrdenTrabajo.idServicio
                            },
                            soportes = item.soportes,
                            cotizaciones = item.cotizaciones,
                            skills = item.skills,
                            ArchivosAdjuntos = item.ArchivosAdjuntos
                        }); ;
                    }

                    foreach (var item in ob)
                    {
                        if (item.empresasInvitadas.Contains(idEmpresa))
                        {
                            obf.Add(item);
                        }
                    }
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = obf
                    };
                }
                else
                {
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitacion asociada a esta orden",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<LicitacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<List<LicitacionRequest>>> GetAllBySkillSedePais(long idSkill, long idSede)
        {
            try
            {
                var licitacion = await _dalc.getLicitacionBySkillPaisSede(idSkill, idSede);

                var ob = new List<LicitacionRequest>();
                if (licitacion != null)
                {
                    foreach (var item in licitacion)
                    {
                        if (item.skills.idSkillLicitacion == idSkill)
                        {
                            ob.Add(new LicitacionRequest()
                            {
                                idLicitacion = item.idLicitacion,
                                fechaLimiteRepCotizacion = item.fechaLimiteRepCotizacion,
                                urgencia = item.urgencia,
                                moneda = item.moneda,
                                observaciones = item.observaciones,
                                idOrden = item.idOrden,
                                idSede = item.idSede,
                                idEmpresa = item.idEmpresa,
                                idCuestionario = item.idCuestionario,
                                estado = item.estado,
                                idSolicitudPedido = item != null ? JsonConvert.DeserializeObject<List<long>>(item.idSolicitudPedido) : new List<long>(),
                                tipoLicitacion = item.tipoLicitacion,
                                empresasInvitadas = item != null ? JsonConvert.DeserializeObject<List<long>>(item.empresasInvitadas) : new List<long>(),
                                cronograma = item.cronograma,
                                soportes = item.soportes,
                                cotizaciones = item.cotizaciones,
                                skills = item.skills,
                                ArchivosAdjuntos = item.ArchivosAdjuntos
                            });
                        }
                    }
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitacion asociada a esta orden",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<LicitacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<LicitacionRequest>>> GetTodasPorSede(long idSede)
        {
            try
            {
                var licitacion = await _dalc.GetIdSede(idSede);
                var ob = new List<LicitacionRequest>();
                if (licitacion != null)
                {
                    foreach (var item in licitacion)
                    {
                        ob.Add(new LicitacionRequest()
                        {
                            idLicitacion = item.idLicitacion,
                            fechaLimiteRepCotizacion = item.fechaLimiteRepCotizacion,
                            urgencia = item.urgencia,
                            moneda = item.moneda,
                            observaciones = item.observaciones,
                            idOrden = item.idOrden,
                            idSede = item.idSede,
                            idEmpresa = item.idEmpresa,
                            idCuestionario = item.idCuestionario,
                            estado = item.estado,
                            idSolicitudPedido = item != null ? JsonConvert.DeserializeObject<List<long>>(item.idSolicitudPedido) : new List<long>(),
                            tipoLicitacion = item.tipoLicitacion,
                            empresasInvitadas = item != null ? JsonConvert.DeserializeObject<List<long>>(item.empresasInvitadas) : new List<long>(),
                            cronograma = item.cronograma,
                            soportes = item.soportes,
                            cotizaciones = item.cotizaciones,
                            skills = item.skills,
                            ArchivosAdjuntos = item.ArchivosAdjuntos
                        });
                    }
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitaciones asociada a esta Sede",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<LicitacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivoEnOrdenTrabajo>>> GetActivosEnOrdenTrabajoSede(long idSede)
        {
            try
            {
                var licitacion = await _dalc.GetIdSede(idSede);
                var listaActivos = new List<ActivoTipoEnOrdenTrabajo>();
                HashSet<string> idActivos = new HashSet<string>();
                var ob = new List<ActivoEnOrdenTrabajo>();
                if (licitacion != null && licitacion.Count > 0)
                {
                    foreach (var item in licitacion)
                    {
                        var licitacionEnOrdenTrabajo = await _dalcOrdenesTrabajo.Get(item.idOrden);
                        if (licitacionEnOrdenTrabajo != null)
                        {
                            var datosActivosEnOrdenTrabajo = licitacionEnOrdenTrabajo.datosActivos;
                            dynamic datosActivos = JsonConvert.DeserializeObject<Object>(datosActivosEnOrdenTrabajo);

                            string idActivoEnOrdenTrabajo = datosActivos[0]["idActivo"];
                            string tipoActivo = datosActivos[0]["tipo"];
                            if (idActivos.Contains(idActivoEnOrdenTrabajo) == false)
                            {
                                idActivos.Add(idActivoEnOrdenTrabajo);
                                var jSONActivos = new ActivoTipoEnOrdenTrabajo()
                                {
                                    idActivo = idActivoEnOrdenTrabajo,
                                    tipo = tipoActivo
                                };
                                listaActivos.Add(jSONActivos);
                            }

                        }
                    }
                    ob.Add(new ActivoEnOrdenTrabajo()
                    {
                        Activos = listaActivos
                    });
                    return new ResponseBase<List<ActivoEnOrdenTrabajo>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<ActivoEnOrdenTrabajo>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitaciones asociada a esta Sede",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ActivoEnOrdenTrabajo>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivoEnOrdenTrabajo>> GetActivosEnOrdenTrabajoLicitacion(long idLicitacion)
        {
            try
            {
                var licitacion = await _dalc.Get(idLicitacion);
                var listaActivos = new List<ActivoTipoEnOrdenTrabajo>();
                HashSet<string> idActivos = new HashSet<string>();

                if (licitacion != null)
                {
                    var licitacionEnOrdenTrabajo = await _dalcOrdenesTrabajo.Get(licitacion.idOrden);
                    if (licitacionEnOrdenTrabajo != null)
                    {
                        var datosActivosEnOrdenTrabajo = licitacionEnOrdenTrabajo.datosActivos;
                        dynamic datosActivos = JsonConvert.DeserializeObject<Object>(datosActivosEnOrdenTrabajo);

                        string idActivoEnOrdenTrabajo = datosActivos[0]["idActivo"];
                        string tipoActivo = datosActivos[0]["tipo"];
                        if (idActivos.Contains(idActivoEnOrdenTrabajo) == false)
                        {
                            idActivos.Add(idActivoEnOrdenTrabajo);
                            var jSONActivos = new ActivoTipoEnOrdenTrabajo()
                            {
                                idActivo = idActivoEnOrdenTrabajo,
                                tipo = tipoActivo
                            };
                            listaActivos.Add(jSONActivos);
                        }

                    }
                    var ob = new ActivoEnOrdenTrabajo()
                    {
                        Activos = listaActivos
                    };
                    return new ResponseBase<ActivoEnOrdenTrabajo>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<ActivoEnOrdenTrabajo>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitaciones asociada a esta Sede",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivoEnOrdenTrabajo>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ComprasPorSede>> GetComprasPorSede(long idSede)
        {
            try
            {
                var licitaciones = await _dalc.GetIdSede(idSede);
                decimal sumaCostos = 0;


                if (licitaciones != null)
                {

                    foreach (var licitacion in licitaciones)
                    {
                        var sumaCotizaciones = await _BOCotizaciones.GetSumaUltimoAnio(licitacion.idLicitacion);
                        if (sumaCotizaciones.datos != null)
                        {
                            foreach (var item in sumaCotizaciones.datos)
                            {
                                sumaCostos += item.suma;
                            }
                        }
                    }
                    var data = new ComprasPorSede()
                    {
                        idSede = idSede,
                        sumaTotal = sumaCostos
                    };
                    return new ResponseBase<ComprasPorSede>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<ComprasPorSede>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitacion asociada a esta orden",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ComprasPorSede>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<List<LicitacionRequest>>> GetTodasPorEmpresa(long idEmpresa)
        {
            try
            {
                var licitacion = await _dalc.GetIdEmpresa(idEmpresa);
                var ob = new List<LicitacionRequest>();
                if (licitacion != null)
                {
                    foreach (var item in licitacion)
                    {
                        ob.Add(new LicitacionRequest()
                        {
                            idLicitacion = item.idLicitacion,
                            fechaLimiteRepCotizacion = item.fechaLimiteRepCotizacion,
                            urgencia = item.urgencia,
                            moneda = item.moneda,
                            observaciones = item.observaciones,
                            idOrden = item.idOrden,
                            idSede = item.idSede,
                            idEmpresa = item.idEmpresa,
                            idCuestionario = item.idCuestionario,
                            estado = item.estado,
                            idSolicitudPedido = item != null ? JsonConvert.DeserializeObject<List<long>>(item.idSolicitudPedido) : new List<long>(),
                            tipoLicitacion = item.tipoLicitacion,
                            empresasInvitadas = item != null ? JsonConvert.DeserializeObject<List<long>>(item.empresasInvitadas) : new List<long>(),
                            cronograma = item.cronograma,
                            soportes = item.soportes,
                            cotizaciones = item.cotizaciones,
                            skills = item.skills,
                            ArchivosAdjuntos = item.ArchivosAdjuntos,
                            
                        });
                    }
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<LicitacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitaciones asociada a esta Empresa",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<LicitacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

 


     

        public async Task<ResponseBase<LicitacionSumaUltimoAnio>> GetSumaUltimoAnio(long idEmpresa)
        {
            try
            {
                var licitaciones = await _dalc.GetIdEmpresaAdjudicadas(idEmpresa);
                var anioActual = DateTime.Now.Year;
                decimal sumaCostos = 0;

                if (licitaciones != null)
                {
                    foreach (var licitacion in licitaciones)
                    {
                        var sumaCotizaciones = await _BOCotizaciones.GetSumaUltimoAnio(licitacion.idLicitacion);
                        if (sumaCotizaciones.datos != null)
                        {
                            foreach (var item in sumaCotizaciones.datos)
                            {
                                sumaCostos += item.suma;
                            }
                        }

                    }
                    var respuesta = new LicitacionSumaUltimoAnio()
                    {
                        anio = anioActual,
                        suma = sumaCostos
                    };
                    return new ResponseBase<LicitacionSumaUltimoAnio>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<LicitacionSumaUltimoAnio>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitaciones asociada a esta Empresa",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<LicitacionSumaUltimoAnio>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        public async Task<ResponseBase<List<LicitacionSuma>>> GetSumaMesAMesPorAnio(long idEmpresa)
        {
            try
            {
                var licitaciones = await _dalc.GetIdEmpresaAdjudicadas(idEmpresa);
                HashSet<long> listaAnios = new HashSet<long>();
                var sumasEnCotizaciones = new List<SumasEnCotizaciones>();
                var respuesta = new List<LicitacionSuma>();

                if (licitaciones != null)
                {

                    var listaDatosMes = new List<CotizacionesResponse>();
                    foreach (var licitacion in licitaciones)
                    {
                        var sumaCotizaciones = await _BOCotizaciones.GetSumaMesAMesPorAnio(licitacion.idLicitacion, idEmpresa);
                        if (sumaCotizaciones.datos != null)
                        {
                            foreach (var item in sumaCotizaciones.datos)
                            {
                                listaAnios.Add(item.anio);
                            }

                            foreach (var item in sumaCotizaciones.datos)
                            {
                                listaDatosMes.Add(item);
                            }
                        }

                    }
                    List<long> listaAniosOrdenada = new List<long>(listaAnios);
                    listaAniosOrdenada.Sort();
                    foreach (int anio in listaAniosOrdenada)
                    {
                        var sumasLicitacionesPorMes = new List<SumaLicitacionesPorMes>();
                        for (int i = 1; i <= 12; i++)
                        {
                            decimal suma = 0;
                            foreach (var item in listaDatosMes)
                            {
                                if (item.anio == anio)
                                {
                                    foreach (var mes in item.meses)
                                    {
                                        if (mes.mes == i)
                                        {
                                            suma += mes.suma;
                                        }
                                    }
                                }
                            }
                            sumasLicitacionesPorMes.Add(new SumaLicitacionesPorMes()
                            {
                                mes = i,
                                suma = suma
                            });
                        }

                        respuesta.Add(new LicitacionSuma()
                        {
                            anio = anio,
                            meses = sumasLicitacionesPorMes
                        });
                    }


                    return new ResponseBase<List<LicitacionSuma>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = respuesta
                    };
                }
                else
                {
                    return new ResponseBase<List<LicitacionSuma>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "No se encontro licitaciones asociada a esta Empresa",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<LicitacionSuma>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<LicitacionRequest>> guardarLicitacion(LicitacionRequest licitacion, Transaction transaction)
        {
            try
            {
                var data_transformada = new Licitacion()
                {
                    idLicitacion = licitacion.idLicitacion,
                    fechaLimiteRepCotizacion = licitacion.fechaLimiteRepCotizacion,
                    urgencia = licitacion.urgencia,
                    moneda = licitacion.moneda,
                    observaciones = licitacion.observaciones,
                    idOrden = licitacion.idOrden,
                    idSede = licitacion.idSede,
                    idEmpresa = licitacion.idEmpresa,
                    idCuestionario = licitacion.idCuestionario,
                    estado = licitacion.estado,
                    idSolicitudPedido = JsonConvert.SerializeObject(licitacion.idSolicitudPedido).ToString(),
                    tipoLicitacion = licitacion.tipoLicitacion,
                    empresasInvitadas = JsonConvert.SerializeObject(licitacion.empresasInvitadas).ToString(),
                    cronograma = licitacion.cronograma,
                    soportes = licitacion.soportes,
                    cotizaciones = licitacion.cotizaciones,
                    skills = licitacion.skills,
                    ArchivosAdjuntos = licitacion.ArchivosAdjuntos,
                  
                };
                var datalicitacion = await _dalc.Set(data_transformada, transaction);

                var data = new LicitacionRequest()
                {
                    idLicitacion = datalicitacion.idLicitacion,
                    fechaLimiteRepCotizacion = datalicitacion.fechaLimiteRepCotizacion,
                    urgencia = datalicitacion.urgencia,
                    moneda = datalicitacion.moneda,
                    observaciones = datalicitacion.observaciones,
                    idOrden = datalicitacion.idOrden,
                    idSede = datalicitacion.idSede,
                    idEmpresa = datalicitacion.idEmpresa,
                    idCuestionario = datalicitacion.idCuestionario,
                    estado = datalicitacion.estado,
                    idSolicitudPedido = datalicitacion != null ? JsonConvert.DeserializeObject<List<long>>(datalicitacion.idSolicitudPedido) : new List<long>(),
                    tipoLicitacion = datalicitacion.tipoLicitacion,
                    empresasInvitadas = datalicitacion != null ? JsonConvert.DeserializeObject<List<long>>(datalicitacion.empresasInvitadas) : new List<long>(),
                    cronograma = datalicitacion.cronograma,
                    soportes = datalicitacion.soportes,
                    cotizaciones = datalicitacion.cotizaciones,
                    skills = datalicitacion.skills,
                    ArchivosAdjuntos = datalicitacion.ArchivosAdjuntos,
                 
                };

                if (data != null)
                {
                    return new ResponseBase<LicitacionRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<LicitacionRequest>()
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
                return new ResponseBase<LicitacionRequest>()
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
