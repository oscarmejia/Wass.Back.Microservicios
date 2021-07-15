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
    public class BOCronogramaLicitacion
    {
        private readonly DALCCronogramaLicitacion _dalc;

        public BOCronogramaLicitacion(ProgramadorContext context)
        {
            _dalc = new DALCCronogramaLicitacion(context);
        }

        public async Task<ResponseBase<CronogramaLicitacion>> Get (long idCronogramaLicitacion)
        {
            try
            {
                var cronograma = await _dalc.Get(idCronogramaLicitacion);
                if (cronograma != null)
                {
                    return new ResponseBase<CronogramaLicitacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cronograma
                    };
                }
                else
                {
                    return new ResponseBase<CronogramaLicitacion>()
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
                return new ResponseBase<CronogramaLicitacion>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CronogramaLicitacion>>> GetTodasPorLicitacion(long idLicitacion)
        {
            try
            {
                var cronograma = await _dalc.GetIdLicitacion(idLicitacion);

                if (cronograma != null)
                {
                    return new ResponseBase<List<CronogramaLicitacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cronograma
                    };
                }
                else
                {
                    return new ResponseBase<List<CronogramaLicitacion>>()
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
                return new ResponseBase<List<CronogramaLicitacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CronogramaLicitacion>>> GetTodas()
        {
            try
            {
                var cronograma = await _dalc.GetTodas();

                if (cronograma != null)
                {
                    return new ResponseBase<List<CronogramaLicitacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cronograma
                    };
                }
                else
                {
                    return new ResponseBase<List<CronogramaLicitacion>>()
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
                return new ResponseBase<List<CronogramaLicitacion>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CronogramaLicitacion>> guardaCronograma(CronogramaLicitacion cronograma, Transaction transaction)
        {
            try
            {
                var dataCronograma = await _dalc.Set(cronograma, transaction);
                if (dataCronograma != null)
                {
                    return new ResponseBase<CronogramaLicitacion>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataCronograma
                    };
                }
                else
                {
                    return new ResponseBase<CronogramaLicitacion>()
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
                return new ResponseBase<CronogramaLicitacion>()
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
