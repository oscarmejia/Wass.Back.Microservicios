using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.RespuestaSolicitudPedido;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BORespuestaSolicitudPedido
    {
        private readonly DALCRespuestaSolicitudPedido _dalc;

        public BORespuestaSolicitudPedido(ProgramadorContext context)
        {
            _dalc = new DALCRespuestaSolicitudPedido(context);
        }

        public async Task<ResponseBase<RespuestaSolicitudPedidoRequest>> GetPorId(long idRespuestaSolicitudPedido)
        {
            try
            {
                var respuestaSolicitudPedido = await _dalc.Get(idRespuestaSolicitudPedido);
                var ob = new RespuestaSolicitudPedidoRequest()
                {
                   idRespuestaSolicitudPedido = respuestaSolicitudPedido.idRespuestaSolicitudPedido,
                   idSolicitudPedido = respuestaSolicitudPedido.idSolicitudPedido,
                   idCotizacion = respuestaSolicitudPedido.idCotizacion,
                   detalle = !String.IsNullOrEmpty(respuestaSolicitudPedido.detalle) ? JsonConvert.DeserializeObject<List<DetalleRequest>>(respuestaSolicitudPedido.detalle) : new List<DetalleRequest>(),
                };

                if (respuestaSolicitudPedido != null)
                {
                    return new ResponseBase<RespuestaSolicitudPedidoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<RespuestaSolicitudPedidoRequest>()
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
                return new ResponseBase<RespuestaSolicitudPedidoRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<RespuestaSolicitudPedidoRequest>>> GetTodas()
        {
            try
            {
                var respuestaSolicitudPedido = await _dalc.GetTodas();
                var ob = new List<RespuestaSolicitudPedidoRequest>();

                foreach(var item in respuestaSolicitudPedido)
                {
                    ob.Add(new RespuestaSolicitudPedidoRequest()
                    {
                        idRespuestaSolicitudPedido = item.idRespuestaSolicitudPedido,
                        idSolicitudPedido = item.idSolicitudPedido,
                        idCotizacion = item.idCotizacion,
                        detalle = !String.IsNullOrEmpty(item.detalle) ? JsonConvert.DeserializeObject<List<DetalleRequest>>(item.detalle) : new List<DetalleRequest>(),
                    });
                }

                if (respuestaSolicitudPedido != null)
                {
                    return new ResponseBase<List<RespuestaSolicitudPedidoRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<RespuestaSolicitudPedidoRequest>>()
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
                return new ResponseBase<List<RespuestaSolicitudPedidoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<RespuestaSolicitudPedidoRequest>> guardarRespuestaSolicitudPedido(RespuestaSolicitudPedidoRequest respuestaSolicitudPedido, Transaction transaction)
        {
            try
            {
                var data = new RespuestaSolicitudPedido()
                {
                    idRespuestaSolicitudPedido = respuestaSolicitudPedido.idRespuestaSolicitudPedido,
                    idSolicitudPedido = respuestaSolicitudPedido.idSolicitudPedido,
                    idCotizacion = respuestaSolicitudPedido.idCotizacion,
                    detalle = JsonConvert.SerializeObject(respuestaSolicitudPedido.detalle),
                };
                var dataRespuestaSolicitudPedido = await _dalc.Set(data, transaction);

                var dataTransformada = new RespuestaSolicitudPedidoRequest()
                {
                    idRespuestaSolicitudPedido = dataRespuestaSolicitudPedido.idRespuestaSolicitudPedido,
                    idSolicitudPedido = dataRespuestaSolicitudPedido.idSolicitudPedido,
                    idCotizacion = dataRespuestaSolicitudPedido.idCotizacion,
                    detalle = !String.IsNullOrEmpty(dataRespuestaSolicitudPedido.detalle) ? JsonConvert.DeserializeObject<List<DetalleRequest>>(dataRespuestaSolicitudPedido.detalle) : new List<DetalleRequest>(),
                };

                if (dataRespuestaSolicitudPedido != null)
                {
                    return new ResponseBase<RespuestaSolicitudPedidoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataTransformada
                    };
                }
                else
                {
                    return new ResponseBase<RespuestaSolicitudPedidoRequest>()
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
                return new ResponseBase<RespuestaSolicitudPedidoRequest>()
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
