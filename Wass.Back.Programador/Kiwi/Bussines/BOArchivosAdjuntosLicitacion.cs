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
    public class BOArchivosAdjuntosLicitacion
    {
        private readonly DALCArchivosAdjuntosLicitacion _dalc;

        private readonly DALCLicitacion _dalcLicitacion;

        public BOArchivosAdjuntosLicitacion(ProgramadorContext context)
        {
            _dalc = new DALCArchivosAdjuntosLicitacion(context);
            _dalcLicitacion = new DALCLicitacion(context);
        }

        public async Task<ResponseBase<ArchivosAdjuntosLicitacion>> Get(long idArchivosAdjuntosLicitacion)
        {
            try
            {
                var archivo = await _dalc.Get(idArchivosAdjuntosLicitacion);
                if (archivo != null)
                {
                    return new ResponseBase<ArchivosAdjuntosLicitacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<ArchivosAdjuntosLicitacion>()
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
                return new ResponseBase<ArchivosAdjuntosLicitacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ArchivosAdjuntosLicitacion>>> GetIdLicitacion(long idLicitacion)
        {
            try
            {
                var archivo = await _dalc.GetIdLicitacion(idLicitacion);
                if (archivo != null)
                {
                    return new ResponseBase<List<ArchivosAdjuntosLicitacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<List<ArchivosAdjuntosLicitacion>>()
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
                return new ResponseBase<List<ArchivosAdjuntosLicitacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ArchivosAdjuntosLicitacion>>> GetTodas()
        {
            try
            {
                var archivo = await _dalc.GetTodas();
                if (archivo != null)
                {
                    return new ResponseBase<List<ArchivosAdjuntosLicitacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<List<ArchivosAdjuntosLicitacion>>()
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
                return new ResponseBase<List<ArchivosAdjuntosLicitacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ArchivosAdjuntosLicitacion>> guardarArchivo(ArchivosAdjuntosLicitacion archivos, Transaction transaction)
        {
            try
            {

                var dataArchivo = await _dalc.Set(archivos, transaction);

                if (dataArchivo != null)
                {
                    return new ResponseBase<ArchivosAdjuntosLicitacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataArchivo
                    };
                }
                else
                {
                    return new ResponseBase<ArchivosAdjuntosLicitacion>()
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
                return new ResponseBase<ArchivosAdjuntosLicitacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ArchivosAdjuntosLicitacion>> EliminarArchivo(long idArchivo)
        {
            try
            {
                var data = await _dalc.EliminarArchivo(idArchivo);
                return new ResponseBase<ArchivosAdjuntosLicitacion>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre Archivo realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ArchivosAdjuntosLicitacion>()
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
