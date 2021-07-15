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
    public class BOIncidencias
    {
        private readonly ProgramadorContext _context;

        private readonly DALCIncidencias _dalc;

        private readonly string _msg_base;

        public BOIncidencias(ProgramadorContext context)
        {
            _context = context;
            _dalc = new DALCIncidencias(_context);
            _msg_base = " Incidencias";
        }

        public async Task<ResponseBase<Incidencias>> getAsync(long idIncidencias)
        {
            try
            {
                var incidencias = await _dalc.getAsync(idIncidencias);

                if (incidencias != null)
                {
                    return new ResponseBase<Incidencias>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = incidencias
                    };
                }
                else
                {
                    return new ResponseBase<Incidencias>()
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
                return new ResponseBase<Incidencias>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Incidencias>>> getTodasAsync()
        {
            try
            {

                var incidencias = await _dalc.getTodasAsync();

                if (incidencias != null)
                {
                    return new ResponseBase<List<Incidencias>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = incidencias
                    };
                }
                else
                {
                    return new ResponseBase<List<Incidencias>>()
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
                return new ResponseBase<List<Incidencias>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }
        
        public async Task<ResponseBase<Incidencias>> setAsync(Incidencias objeto, Transaction transaccion)
        {
            try
            {
                
                var data = await _dalc.setAsync(objeto, transaccion);

                if (data != null)
                {
                    return new ResponseBase<Incidencias>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<Incidencias>()
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
                return new ResponseBase<Incidencias>()
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
