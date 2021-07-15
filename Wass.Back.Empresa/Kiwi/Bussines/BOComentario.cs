using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Comentario;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOComentario
    {
        private readonly DALCComentario _dalc;

        private readonly DALCTopicoAComentar dALCComentario;


        public BOComentario(EmpresaContext context)
        {
            _dalc = new DALCComentario(context);
            dALCComentario = new DALCTopicoAComentar(context);
        }

        public async Task<ResponseBase<ComentarioRequest>> GetPorId(long idComentario)
        {
            try
            {
                var comentario = await _dalc.GetPorId(idComentario);

                var ob = new ComentarioRequest()
                {
                    idComentario = comentario.idComentario,
                    idTopicoAComentar = comentario.idTopicoAComentar,
                    idEmpleado = comentario.idEmpleado,
                    urlImagen = comentario.urlImagen,
                    fechaHoraComentario = comentario.fechaHoraComentario,
                    eliminado = comentario.eliminado,
                    empleados = comentario.Empleados,
                    like = comentario.like,
                    idUsuariosLike = comentario != null ? JsonConvert.DeserializeObject<List<string>>(comentario.idUsuariosLike) : new List<string>(),
                    replica = comentario.replica,
                    idComentarioPadre = comentario.idComentarioPadre,
                    comentario = comentario.comentario
                };

                if (comentario != null)
                {
                    return new ResponseBase<ComentarioRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<ComentarioRequest>()
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
                return new ResponseBase<ComentarioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ComentarioRequest>>> GetPorIdTopicoAComentar(long idTopicoAComentar)
        {
            try
            {
                var comentario = await _dalc.GetPorIdTopicoAComentar(idTopicoAComentar);
                var ob = new List<ComentarioRequest>();
                foreach (var item in comentario)
                {
                    ob.Add(new ComentarioRequest()
                    {
                        idComentario = item.idComentario,
                        idTopicoAComentar = item.idTopicoAComentar,
                        idEmpleado = item.idEmpleado,
                        urlImagen = item.urlImagen,
                        fechaHoraComentario = item.fechaHoraComentario,
                        eliminado = item.eliminado,
                        empleados = item.Empleados,
                        like = item.like,
                        idUsuariosLike = item != null ? JsonConvert.DeserializeObject<List<string>>(item.idUsuariosLike) : new List<string>(),
                        replica = item.replica,
                        idComentarioPadre = item.idComentarioPadre,
                        comentario = item.comentario
                    });
                }
                if (comentario != null)
                {
                    return new ResponseBase<List<ComentarioRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<ComentarioRequest>>()
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
                return new ResponseBase<List<ComentarioRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ComentarioRequest>>> GetTodas()
        {
            try
            {
                var comentarios = await _dalc.GetTodas();
                var ob = new List<ComentarioRequest>();
                foreach (var item in comentarios)
                {
                    ob.Add(new ComentarioRequest()
                    {
                        idComentario = item.idComentario,
                        idTopicoAComentar = item.idTopicoAComentar,
                        idEmpleado = item.idEmpleado,
                        urlImagen = item.urlImagen,
                        fechaHoraComentario = item.fechaHoraComentario,
                        empleados = item.Empleados,
                        eliminado = item.eliminado,
                        like = item.like,
                        idUsuariosLike = item != null ? JsonConvert.DeserializeObject<List<string>>(item.idUsuariosLike) : new List<string>(),
                        replica = item.replica,
                        idComentarioPadre = item.idComentarioPadre,
                        comentario = item.comentario
                    });
                }
                if (comentarios != null)
                {
                    return new ResponseBase<List<ComentarioRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<ComentarioRequest>>()
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
                return new ResponseBase<List<ComentarioRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ComentarioRequest>>> getTodasPorFechaAsync()
        {
            try
            {

                var comentario = await _dalc.getTodasPorFechaAsync();
                var ob = new List<ComentarioRequest>();

                foreach (var item in comentario)
                {
                    ob.Add(new ComentarioRequest()
                    {
                        idComentario = item.idComentario,
                        idTopicoAComentar = item.idTopicoAComentar,
                        idEmpleado = item.idEmpleado,
                        urlImagen = item.urlImagen,
                        fechaHoraComentario = item.fechaHoraComentario,
                        eliminado = item.eliminado,
                        like = item.like,
                        idUsuariosLike = item != null ? JsonConvert.DeserializeObject<List<string>>(item.idUsuariosLike) : new List<string>(),
                        replica = item.replica,
                        idComentarioPadre = item.idComentarioPadre,
                        comentario = item.comentario
                    });
                }

                if (comentario != null)
                {
                    return new ResponseBase<List<ComentarioRequest>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<ComentarioRequest>>()
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
                return new ResponseBase<List<ComentarioRequest>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ComentarioRequest>> ActualizarLikeComentario(long idComentario, string idUsuarioLike)
        {
            try
            {
                var comentario = await _dalc.GetPorId(idComentario);
                bool dioLike = false;

                var dataTransformada = new Comentario()
                {
                    idComentario = comentario.idComentario,
                    idTopicoAComentar = comentario.idTopicoAComentar,
                    idEmpleado = comentario.idEmpleado,
                    urlImagen = comentario.urlImagen,
                    fechaHoraComentario = comentario.fechaHoraComentario,
                    eliminado = comentario.eliminado,
                    like = comentario.like,
                    idUsuariosLike = JsonConvert.SerializeObject(comentario.idUsuariosLike),
                    replica = comentario.replica,
                    idComentarioPadre = comentario.idComentarioPadre,
                    comentario = comentario.comentario,
                };

                var results = JsonConvert.DeserializeObject<List<string>>(comentario.idUsuariosLike);


                for (int i = 0; i <= results.Count(); i++)
                {
                    if (dioLike == false && i == results.Count())
                    {
                        results.Add(idUsuarioLike);
                        comentario.idUsuariosLike = JsonConvert.SerializeObject(results);

                        var actualizarComentario = await _dalc.Set(comentario, Transaction.Update);
                        break;
                    }
                    if (i < results.Count())
                    {
                        if (results[i] == idUsuarioLike)
                        {
                            dioLike = true;

                            break;
                        }
                        else
                        {
                            dioLike = false;
                        }
                    }
                }




                if (comentario != null && dioLike == false)
                {
                    var replicaComentario = await _dalc.ActualizarLikeComentario(idComentario);

                    var data = new ComentarioRequest()
                    {
                        idComentario = replicaComentario.idComentario,
                        idTopicoAComentar = replicaComentario.idTopicoAComentar,
                        idEmpleado = replicaComentario.idEmpleado,
                        urlImagen = replicaComentario.urlImagen,
                        fechaHoraComentario = replicaComentario.fechaHoraComentario,
                        eliminado = replicaComentario.eliminado,
                        like = replicaComentario.like,
                        idUsuariosLike = results,
                        replica = replicaComentario.replica,
                        idComentarioPadre = replicaComentario.idComentarioPadre,
                        comentario = replicaComentario.comentario
                    };
                    return new ResponseBase<ComentarioRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<ComentarioRequest>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "El usuario ya dió like al Comentario",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ComentarioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<ComentarioRequest>> guardarComentario(ComentarioRequest comentario, Transaction transaction)
        {
            try
            {
                var dataTransformada = new Comentario()
                {
                    idComentario = comentario.idComentario,
                    idTopicoAComentar = comentario.idTopicoAComentar,
                    idEmpleado = comentario.idEmpleado,
                    urlImagen = comentario.urlImagen,
                    fechaHoraComentario = comentario.fechaHoraComentario,
                    eliminado = comentario.eliminado,
                    like = comentario.like,
                    idUsuariosLike = JsonConvert.SerializeObject(comentario.idUsuariosLike),
                    replica = comentario.replica,
                    idComentarioPadre = comentario.idComentarioPadre,
                    comentario = comentario.comentario,
                };
                var replicaComentario = await _dalc.Set(dataTransformada, transaction);

                var data = new ComentarioRequest()
                {
                    idComentario = replicaComentario.idComentario,
                    idTopicoAComentar = replicaComentario.idTopicoAComentar,
                    idEmpleado = replicaComentario.idEmpleado,
                    urlImagen = replicaComentario.urlImagen,
                    fechaHoraComentario = replicaComentario.fechaHoraComentario,
                    eliminado = replicaComentario.eliminado,
                    like = replicaComentario.like,
                    idUsuariosLike = replicaComentario != null ? JsonConvert.DeserializeObject<List<string>>(replicaComentario.idUsuariosLike) : new List<string>(),
                    replica = replicaComentario.replica,
                    idComentarioPadre = replicaComentario.idComentarioPadre,
                    comentario = replicaComentario.comentario
                };

                if (data != null)
                {
                    return new ResponseBase<ComentarioRequest>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<ComentarioRequest>()
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
                return new ResponseBase<ComentarioRequest>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ComentarioRequest>> EliminarComentario(long idComentario)
        {
            try
            {
                var replicaComentario = await _dalc.EliminarComentario(idComentario);
                var ob = new ComentarioRequest()
                {
                    idComentario = replicaComentario.idComentario,
                    idTopicoAComentar = replicaComentario.idTopicoAComentar,
                    idEmpleado = replicaComentario.idEmpleado,
                    urlImagen = replicaComentario.urlImagen,
                    fechaHoraComentario = replicaComentario.fechaHoraComentario,
                    eliminado = replicaComentario.eliminado,
                    like = replicaComentario.like,
                    idUsuariosLike = replicaComentario != null ? JsonConvert.DeserializeObject<List<string>>(replicaComentario.idUsuariosLike) : new List<string>(),
                    replica = replicaComentario.replica,
                    idComentarioPadre = replicaComentario.idComentarioPadre,
                    comentario = replicaComentario.comentario
                };
                return new ResponseBase<ComentarioRequest>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre Replica Comentario realizada con exito",
                    datos = ob
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ComentarioRequest>()
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
