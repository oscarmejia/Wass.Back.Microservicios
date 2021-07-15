using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOArchivosAdjuntosActivosEquipos
    {
        private readonly DALCArchivosAdjuntosActivosEquipos _dalc;

        private readonly DALCActivosEquipos _dALCActivosEquipos;

        public BOArchivosAdjuntosActivosEquipos(EmpresaContext context)
        {
            _dalc = new DALCArchivosAdjuntosActivosEquipos(context);
            _dALCActivosEquipos = new DALCActivosEquipos(context);
        }

        public async Task<ResponseBase<ArchivosAdjuntosActivosEquipos>> Get(long idArchivosAdjuntosActivosEquipos)
        {
            try
            {
                var archivo = await _dalc.Get(idArchivosAdjuntosActivosEquipos);
                if (archivo != null)
                {
                    return new ResponseBase<ArchivosAdjuntosActivosEquipos>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<ArchivosAdjuntosActivosEquipos>()
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
                return new ResponseBase<ArchivosAdjuntosActivosEquipos>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ArchivosAdjuntosActivosEquipos>>> GetTodasPorActivosEquipos(Guid idActivo)
        {
            try
            {
                var archivo = await _dalc.GetIdActivosEquipo(idActivo);
                if (archivo != null)
                {
                    return new ResponseBase<List<ArchivosAdjuntosActivosEquipos>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<List<ArchivosAdjuntosActivosEquipos>>()
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
                return new ResponseBase<List<ArchivosAdjuntosActivosEquipos>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ArchivosAdjuntosActivosEquipos>>> GetTodas()
        {
            try
            {
                var archivo = await _dalc.GetTodas();
                if (archivo != null)
                {
                    return new ResponseBase<List<ArchivosAdjuntosActivosEquipos>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = archivo
                    };
                }
                else
                {
                    return new ResponseBase<List<ArchivosAdjuntosActivosEquipos>>()
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
                return new ResponseBase<List<ArchivosAdjuntosActivosEquipos>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ArchivosAdjuntosActivosEquipos>> guardarArchivo(ArchivosAdjuntosActivosEquipos archivos, Transaction transaction)
        {
            try
            {

                var dataArchivo = await _dalc.Set(archivos, transaction);

                if (dataArchivo != null)
                {
                    return new ResponseBase<ArchivosAdjuntosActivosEquipos>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataArchivo
                    };
                }
                else
                {
                    return new ResponseBase<ArchivosAdjuntosActivosEquipos>()
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
                return new ResponseBase<ArchivosAdjuntosActivosEquipos>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ArchivosAdjuntosActivosEquipos>> EliminarArchivo(long idArchivo)
        {
            try
            {
                var data = await _dalc.EliminarArchivo(idArchivo);
                return new ResponseBase<ArchivosAdjuntosActivosEquipos>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre Archivo realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ArchivosAdjuntosActivosEquipos>()
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
