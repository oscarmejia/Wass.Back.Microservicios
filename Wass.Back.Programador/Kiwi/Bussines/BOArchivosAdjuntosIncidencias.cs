using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BOArchivosAdjuntosIncidencias
    {
        private readonly DALCArchivosAdjuntosIncidencias _dalc;

        public BOArchivosAdjuntosIncidencias(ProgramadorContext context)
        {
            _dalc = new DALCArchivosAdjuntosIncidencias(context);
        }

        public async Task<ResponseBase<ArchivosAdjuntosIncidencias>> Get(long idArchivosAdjuntosIncidencias)
        {
            try
            {
                var archivo = await _dalc.Get(idArchivosAdjuntosIncidencias);
                if (archivo != null)
                {
                    return new ResponseBase<ArchivosAdjuntosIncidencias>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<ArchivosAdjuntosIncidencias>()
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
                return new ResponseBase<ArchivosAdjuntosIncidencias>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ArchivosAdjuntosIncidencias>>> GetTodasPorIncidencia(long idIncidencia)
        {
            try
            {
                var archivo = await _dalc.GetIdIncidencia(idIncidencia);
                if (archivo != null)
                {
                    return new ResponseBase<List<ArchivosAdjuntosIncidencias>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<List<ArchivosAdjuntosIncidencias>>()
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
                return new ResponseBase<List<ArchivosAdjuntosIncidencias>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ArchivosAdjuntosIncidencias>>> GetTodas()
        {
            try
            {
                var archivo = await _dalc.GetTodas();
                if (archivo != null)
                {
                    return new ResponseBase<List<ArchivosAdjuntosIncidencias>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<List<ArchivosAdjuntosIncidencias>>()
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
                return new ResponseBase<List<ArchivosAdjuntosIncidencias>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ArchivosAdjuntosIncidencias>> guardarArchivo(ArchivosAdjuntosIncidencias archivos, Transaction transaction)
        {
            try
            {

                var dataArchivo = await _dalc.Set(archivos, transaction);

                if (dataArchivo != null)
                {
                    return new ResponseBase<ArchivosAdjuntosIncidencias>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataArchivo
                    };
                }
                else
                {
                    return new ResponseBase<ArchivosAdjuntosIncidencias>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se pudo realizar",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ArchivosAdjuntosIncidencias>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ArchivosAdjuntosIncidencias>> EliminarArchivo(long idArchivo)
        {
            try
            {
                var data = await _dalc.EliminarArchivo(idArchivo);
                return new ResponseBase<ArchivosAdjuntosIncidencias>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre Archivo realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ArchivosAdjuntosIncidencias>()
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
