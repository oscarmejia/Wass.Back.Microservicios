using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BOSoportesLicitacion
    {

        private readonly DALCSoportesLicitacion _dalc;

        public BOSoportesLicitacion(ProgramadorContext context)
        {
            _dalc = new DALCSoportesLicitacion(context);
        }

        public async Task<ResponseBase<SoportesLicitacion>> Get( long idSoporteLicitacion)
        {
            try
            {
                var soporte = await _dalc.Get(idSoporteLicitacion);
                if (soporte != null)
                {
                    return new ResponseBase<SoportesLicitacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = soporte
                    };
                }
                else
                {
                    return new ResponseBase<SoportesLicitacion>()
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
                return new ResponseBase<SoportesLicitacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<SoportesLicitacion>>> GetTodasPorlicitacion(long idLicitacion)
        {
            try
            {
                var soporte = await _dalc.GetIdLicitacion(idLicitacion);
                if (soporte != null)
                {
                    return new ResponseBase<List<SoportesLicitacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = soporte
                    };
                }
                else
                {
                    return new ResponseBase<List<SoportesLicitacion>>()
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
                return new ResponseBase<List<SoportesLicitacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<SoportesLicitacion>>> GetTodas()
        {
            try
            {
                var soporte = await _dalc.GetTodas();
                if (soporte != null)
                {
                    return new ResponseBase<List<SoportesLicitacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = soporte
                    };
                }
                else
                {
                    return new ResponseBase<List<SoportesLicitacion>>()
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
                return new ResponseBase<List<SoportesLicitacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<SoportesLicitacion>> guardarSoporte(SoportesLicitacion soportes, Transaction transaction)
        {
            try
            {
                var dataSoporte = await _dalc.Set(soportes, transaction);
                if (dataSoporte != null)
                {
                    return new ResponseBase<SoportesLicitacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataSoporte
                    };
                }
                else
                {
                    return new ResponseBase<SoportesLicitacion>()
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
                return new ResponseBase<SoportesLicitacion>()
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
