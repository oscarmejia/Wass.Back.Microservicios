using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.SolicitudPedido;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOSolicitudPedido
    {
        private readonly DALCSolicitudPedido _dalc;
        private readonly DALCRepuestos _dalcRepuestos;
        public BOSolicitudPedido(EmpresaContext context)
        {
            _dalc = new DALCSolicitudPedido(context);
            _dalcRepuestos = new DALCRepuestos(context);
        }

        public async Task<ResponseBase<List<SolicitudPedidoRequest>>> GetTodas()
        {
            try
            {
                var solicitudPedido = await _dalc.GetTodas();

                var ob = new List<SolicitudPedidoRequest>();

                foreach (var item in solicitudPedido)
                {
                    ob.Add(new SolicitudPedidoRequest()
                    {
                        idSolicitudPedido = item.idSolicitudPedido,
                        estado = item.estado,
                        fechaCreacion = item.fechaCreacion,
                        fechaEnvio = item.fechaEnvio,
                        fechaCancelacion = item.fechaCancelacion,
                        idSede = item.idSede,
                        nivelUrgencia = item.nivelUrgencia,
                        idUsuarioCreador = item.idUsuarioCreador,
                        comentario = item.comentario,
                        detalle = item != null ? JsonConvert.DeserializeObject<List<DetalleRequest>>(item.detalle) : new List<DetalleRequest>(),

                    });
                }

                if (solicitudPedido != null)
                {
                    return new ResponseBase<List<SolicitudPedidoRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<SolicitudPedidoRequest>>()
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
                return new ResponseBase<List<SolicitudPedidoRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<SolicitudPedidoRequest>> GetPorId(long idSolicitudPedido)
        {
            try
            {
                var solicitudPedido = await _dalc.Get(idSolicitudPedido);

                var ob = new SolicitudPedidoRequest()
                {
                    idSolicitudPedido = solicitudPedido.idSolicitudPedido,
                    estado = solicitudPedido.estado,
                    fechaCreacion = solicitudPedido.fechaCreacion,
                    fechaEnvio = solicitudPedido.fechaEnvio,
                    fechaCancelacion = solicitudPedido.fechaCancelacion,
                    idSede = solicitudPedido.idSede,
                    nivelUrgencia = solicitudPedido.nivelUrgencia,
                    idUsuarioCreador = solicitudPedido.idUsuarioCreador,
                    comentario = solicitudPedido.comentario,
                    detalle = solicitudPedido != null ? JsonConvert.DeserializeObject<List<DetalleRequest>>(solicitudPedido.detalle) : new List<DetalleRequest>(),

                };
                if (solicitudPedido != null)
                {
                    return new ResponseBase<SolicitudPedidoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<SolicitudPedidoRequest>()
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
                return new ResponseBase<SolicitudPedidoRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }



        public async Task<ResponseBase<SolicitudPedidoRequest>> guardarSolicitudPedido(SolicitudPedidoRequest solicitudPedido, Transaction transaction)
        {
            try
            {
                var dataTransformada = new SolicitudPedido()
                {
                    idSolicitudPedido = solicitudPedido.idSolicitudPedido,
                    estado = solicitudPedido.estado,
                    fechaCreacion = solicitudPedido.fechaCreacion,
                    fechaEnvio = solicitudPedido.fechaEnvio,
                    fechaCancelacion = solicitudPedido.fechaCancelacion,
                    idSede = solicitudPedido.idSede,
                    nivelUrgencia = solicitudPedido.nivelUrgencia,
                    idUsuarioCreador = solicitudPedido.idUsuarioCreador,
                    comentario = solicitudPedido.comentario,
                    detalle = JsonConvert.SerializeObject(solicitudPedido.detalle),
                };

                var dataSolicitudPedido = await _dalc.Set(dataTransformada, transaction);

                var dataRespuesta = new SolicitudPedidoRequest()
                {
                    idSolicitudPedido = dataSolicitudPedido.idSolicitudPedido,
                    estado = dataSolicitudPedido.estado,
                    fechaCreacion = dataSolicitudPedido.fechaCreacion,
                    fechaEnvio = dataSolicitudPedido.fechaEnvio,
                    fechaCancelacion = dataSolicitudPedido.fechaCancelacion,
                    idSede = dataSolicitudPedido.idSede,
                    nivelUrgencia = dataSolicitudPedido.nivelUrgencia,
                    idUsuarioCreador = dataSolicitudPedido.idUsuarioCreador,
                    comentario = dataSolicitudPedido.comentario,
                    detalle = dataSolicitudPedido != null ? JsonConvert.DeserializeObject<List<DetalleRequest>>(dataSolicitudPedido.detalle) : new List<DetalleRequest>(),

                };

                if (dataRespuesta != null)
                {
                    return new ResponseBase<SolicitudPedidoRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = dataRespuesta
                    };
                }
                else
                {
                    return new ResponseBase<SolicitudPedidoRequest>()
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
                return new ResponseBase<SolicitudPedidoRequest>()
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
