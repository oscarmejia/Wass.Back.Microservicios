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
    public class BOArchivosAdjuntosCotizacion
    {

        private readonly DALCArchivosAdjuntosCotizacion _dalc;

        private readonly DLACCotizaciones _dLACCotizaciones;

        public BOArchivosAdjuntosCotizacion(ProgramadorContext context)
        {
            _dalc = new DALCArchivosAdjuntosCotizacion(context);
            _dLACCotizaciones = new DLACCotizaciones(context);
        }

        public async Task<ResponseBase<ArchivosAdjuntosCotizacion>> Get(long idArchivoAdjuntoCotizacion)
        {
            try
            {
                var archivo = await _dalc.Get(idArchivoAdjuntoCotizacion);
                if (archivo != null)
                {
                    return new ResponseBase<ArchivosAdjuntosCotizacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<ArchivosAdjuntosCotizacion>()
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
                return new ResponseBase<ArchivosAdjuntosCotizacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ArchivosAdjuntosCotizacion>>> GetTodasPorCotizacion(long idCotizacion)
        {
            try
            {
                var archivo = await _dalc.GetIdCotizacion(idCotizacion);
                if (archivo != null)
                {
                    return new ResponseBase<List<ArchivosAdjuntosCotizacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<List<ArchivosAdjuntosCotizacion>>()
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
                return new ResponseBase<List<ArchivosAdjuntosCotizacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ArchivosAdjuntosCotizacion>>> GetTodas()
        {
            try
            {
                var archivo = await _dalc.GetTodas();
                if (archivo != null)
                {
                    return new ResponseBase<List<ArchivosAdjuntosCotizacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<List<ArchivosAdjuntosCotizacion>>()
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
                return new ResponseBase<List<ArchivosAdjuntosCotizacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ArchivosAdjuntosCotizacion>> guardarArchivo(ArchivosAdjuntosCotizacion archivos, Transaction transaction)
        {
            try
            {

                var dataArchivo = await _dalc.Set(archivos, transaction);

                if (dataArchivo != null)
                {
                    return new ResponseBase<ArchivosAdjuntosCotizacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataArchivo
                    };
                }
                else
                {
                    return new ResponseBase<ArchivosAdjuntosCotizacion>()
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
                return new ResponseBase<ArchivosAdjuntosCotizacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ArchivosAdjuntosCotizacion>> EliminarArchivo(long idArchivo)
        {
            try
            {
                var data = await _dalc.EliminarArchivo(idArchivo);
                return new ResponseBase<ArchivosAdjuntosCotizacion>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre Archivo realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ArchivosAdjuntosCotizacion>()
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
