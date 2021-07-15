using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Calificacion;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOCalificacion
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCCalificacion _dalc;

        public BOCalificacion(EmpresaContext context)
        {
            _dalc = new DALCCalificacion(context);
        }

        public async Task<ResponseBase<List<CalificacionRequest>>> GetTodas()
        {
            try
            {
                var calificacion = await _dalc.GetTodas();
                var datos = new List<CalificacionRequest>();

                if (calificacion != null && calificacion.Count > 0)
                {
                    foreach (var item in calificacion)
                    {
                        var data = new CalificacionRequest()
                        {
                            idCalificacion = item.idCalificacion,
                            idEmpresa = item.idEmpresa,
                            idSede = item.idSede,
                            idOrdenTrabajo = item.idOrdenTrabajo,
                            idProveedor = item.idProveedor,
                            calificacion = item.calificacion,
                            descripcion = item.descripcion,
                            motivo = !String.IsNullOrEmpty(item.motivo) ? JsonConvert.DeserializeObject<MotivoRequest>(item.motivo) : new MotivoRequest()

                        };

                        datos.Add(data);
                    }


                    return new ResponseBase<List<CalificacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<List<CalificacionRequest>>()
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
                return new ResponseBase<List<CalificacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CalificacionRequest>> GetPromedioCalificacion(long idProveedor, long idSede, long idEmpresa)
        {
            try
            {
                var calificacion = await _dalc.getTodasPorProveedorAsync(idProveedor);

                float promedio = 0;
                float contador = 0;
                float suma = 0;

                foreach (var item in calificacion)
                {
                    contador++;
                    suma += item.calificacion;

                }

                if (contador > 0)
                {
                    promedio = suma / contador;
                }

                var buscarProveedor = await _dalc.GetPorProveedorSedeEmpresa(idProveedor, idSede, idEmpresa);


                if (calificacion != null && contador > 0 && buscarProveedor != null)
                {
                    var data = new CalificacionRequest()
                    {
                        idCalificacion = buscarProveedor.idCalificacion,
                        idEmpresa = buscarProveedor.idEmpresa,
                        idSede = buscarProveedor.idSede,
                        idOrdenTrabajo = buscarProveedor.idOrdenTrabajo,
                        idProveedor = buscarProveedor.idProveedor,
                        calificacion = buscarProveedor.calificacion,
                        descripcion = buscarProveedor.descripcion,
                        motivo = !String.IsNullOrEmpty(buscarProveedor.motivo) ? JsonConvert.DeserializeObject<MotivoRequest>(buscarProveedor.motivo) : new MotivoRequest()

                    };
                    return new ResponseBase<CalificacionRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<CalificacionRequest>()
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
                return new ResponseBase<CalificacionRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CalificacionRequest>> GetPorId(long idCalificacion)
        {
            try
            {
                var calificacion = await _dalc.Get(idCalificacion);
                if (calificacion != null)
                {
                    var data = new CalificacionRequest()
                    {
                        idCalificacion = calificacion.idCalificacion,
                        idEmpresa = calificacion.idEmpresa,
                        idSede = calificacion.idSede,
                        idOrdenTrabajo = calificacion.idOrdenTrabajo,
                        idProveedor = calificacion.idProveedor,
                        calificacion = calificacion.calificacion,
                        descripcion = calificacion.descripcion,
                        motivo = !String.IsNullOrEmpty(calificacion.motivo) ? JsonConvert.DeserializeObject<MotivoRequest>(calificacion.motivo) : new MotivoRequest()

                    };
                    return new ResponseBase<CalificacionRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<CalificacionRequest>()
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
                return new ResponseBase<CalificacionRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CalificacionRequest>>> getTodasPorSedeAsync(long idSede)
        {
            try
            {

                var calificacion = await _dalc.getTodasPorSedeAsync(idSede);
                var ob = new List<CalificacionRequest>();

                foreach (var item in calificacion)
                {
                    ob.Add(new CalificacionRequest()
                    {
                        idCalificacion = item.idCalificacion,
                        idEmpresa = item.idEmpresa,
                        idSede = item.idSede,
                        idOrdenTrabajo = item.idOrdenTrabajo,
                        idProveedor = item.idProveedor,
                        calificacion = item.calificacion,
                        descripcion = item.descripcion,
                        motivo = !String.IsNullOrEmpty(item.motivo) ? JsonConvert.DeserializeObject<MotivoRequest>(item.motivo) : new MotivoRequest()
                    });
                }

                if (calificacion != null)
                {
                    return new ResponseBase<List<CalificacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<CalificacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CalificacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CalificacionRequest>>> getTodasPorOrdenTrabajoAsync(long idOrdenTrabajo)
        {
            try
            {

                var calificacion = await _dalc.getTodasPorOrdenTrabajoAsync(idOrdenTrabajo);
                var ob = new List<CalificacionRequest>();

                foreach (var item in calificacion)
                {
                    ob.Add(new CalificacionRequest()
                    {
                        idCalificacion = item.idCalificacion,
                        idEmpresa = item.idEmpresa,
                        idSede = item.idSede,
                        idOrdenTrabajo = item.idOrdenTrabajo,
                        idProveedor = item.idProveedor,
                        calificacion = item.calificacion,
                        descripcion = item.descripcion,
                        motivo = !String.IsNullOrEmpty(item.motivo) ? JsonConvert.DeserializeObject<MotivoRequest>(item.motivo) : new MotivoRequest()
                    });
                }

                if (calificacion != null)
                {
                    return new ResponseBase<List<CalificacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<CalificacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CalificacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CalificacionRequest>>> getTodasPorEmpresaAsync(long idEmpresa)
        {
            try
            {

                var calificacion = await _dalc.getTodasPorEmpresaAsync(idEmpresa);
                var ob = new List<CalificacionRequest>();

                foreach (var item in calificacion)
                {
                    ob.Add(new CalificacionRequest()
                    {
                        idCalificacion = item.idCalificacion,
                        idEmpresa = item.idEmpresa,
                        idSede = item.idSede,
                        idOrdenTrabajo = item.idOrdenTrabajo,
                        idProveedor = item.idProveedor,
                        calificacion = item.calificacion,
                        descripcion = item.descripcion,
                        motivo = !String.IsNullOrEmpty(item.motivo) ? JsonConvert.DeserializeObject<MotivoRequest>(item.motivo) : new MotivoRequest()
                    });
                }

                if (calificacion != null)
                {
                    return new ResponseBase<List<CalificacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<CalificacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta  no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CalificacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CalificacionRequest>>> getTodasPorProveedorAsync(long idProveedor)
        {
            try
            {

                var calificacion = await _dalc.getTodasPorProveedorAsync(idProveedor);
                var ob = new List<CalificacionRequest>();

                foreach (var item in calificacion)
                {
                    ob.Add(new CalificacionRequest()
                    {
                        idCalificacion = item.idCalificacion,
                        idEmpresa = item.idEmpresa,
                        idSede = item.idSede,
                        idOrdenTrabajo = item.idOrdenTrabajo,
                        idProveedor = item.idProveedor,
                        calificacion = item.calificacion,
                        descripcion = item.descripcion,
                        motivo = !String.IsNullOrEmpty(item.motivo) ? JsonConvert.DeserializeObject<MotivoRequest>(item.motivo) : new MotivoRequest()
                    });
                }

                if (calificacion != null)
                {
                    return new ResponseBase<List<CalificacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<CalificacionRequest>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta  no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CalificacionRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CalificacionRequest>> guardarCalificacion(CalificacionRequest calificacion, Transaction transaction)
        {
            try
            {
                bool valido = false;
                var dataRespuesta = new CalificacionRequest();

                var dataTransformada = new Calificacion()
                {
                    idCalificacion = calificacion.idCalificacion,
                    idEmpresa = calificacion.idEmpresa,
                    idSede = calificacion.idSede,
                    idOrdenTrabajo = calificacion.idOrdenTrabajo,
                    idProveedor = calificacion.idProveedor,
                    calificacion = calificacion.calificacion,
                    descripcion = calificacion.descripcion,
                    motivo = JsonConvert.SerializeObject(calificacion.motivo)

                };

                if (calificacion.calificacion >= 1 && calificacion.calificacion <= 5)
                {
                    var dataCalificacion = await _dalc.Set(dataTransformada, transaction);
                    var data = new CalificacionRequest()
                    {
                        idCalificacion = dataCalificacion.idCalificacion,
                        idEmpresa = dataCalificacion.idEmpresa,
                        idSede = dataCalificacion.idSede,
                        idOrdenTrabajo = dataCalificacion.idOrdenTrabajo,
                        idProveedor = dataCalificacion.idProveedor,
                        calificacion = dataCalificacion.calificacion,
                        descripcion = dataCalificacion.descripcion,
                        motivo = !String.IsNullOrEmpty(dataCalificacion.motivo) ? JsonConvert.DeserializeObject<MotivoRequest>(dataCalificacion.motivo) : new MotivoRequest()

                    };

                    dataRespuesta = data;

                    valido = true;
                }
                else
                {
                    return new ResponseBase<CalificacionRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "La calificacion debe ser entre 1 y 5",
                        datos = null
                    };
                }

                if (valido == true)
                {
                    return new ResponseBase<CalificacionRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataRespuesta
                    };
                }
                else
                {
                    return new ResponseBase<CalificacionRequest>()
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
                return new ResponseBase<CalificacionRequest>()
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
