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
    public class BOTopicoAComentar
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }

        private readonly DALCTopicoAComentar _dalc;

        public BOTopicoAComentar(EmpresaContext context)
        {
            _dalc = new DALCTopicoAComentar(context);
        }

        public async Task<ResponseBase<List<TopicoAComentar>>> GetTodas()
        {
            try
            {
                var comentario = await _dalc.GetTodas();

                if (comentario != null)
                {
                    return new ResponseBase<List<TopicoAComentar>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = comentario
                    };
                }
                else
                {
                    return new ResponseBase<List<TopicoAComentar>>()
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
                return new ResponseBase<List<TopicoAComentar>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<TopicoAComentar>>> GetOrdenadasPorFecha()
        {
            try
            {
                var comentario = await _dalc.GetOrdenadasPorFecha();


                if (comentario != null)
                {
                    return new ResponseBase<List<TopicoAComentar>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = comentario
                    };
                }
                else
                {
                    return new ResponseBase<List<TopicoAComentar>>()
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
                return new ResponseBase<List<TopicoAComentar>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<TopicoAComentar>> GetPorId(long idTopicoAComentar)
        {
            try
            {
                var comentario = await _dalc.Get(idTopicoAComentar);
                if (comentario != null)
                {
                    return new ResponseBase<TopicoAComentar>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = comentario
                    };
                }
                else
                {
                    return new ResponseBase<TopicoAComentar>()
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
                return new ResponseBase<TopicoAComentar>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<TopicoAComentar>> EliminarComentario(long idTopicoAComentar)
        {
            try
            {
                var data = await _dalc.EliminarComentario(idTopicoAComentar);

                return new ResponseBase<TopicoAComentar>()
                {
                    codigo = (int)HttpStatusCode.OK,
                    estado = true,
                    mensaje = $"Operación sobre el Comentario realizada con exito",
                    datos = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<TopicoAComentar>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<TopicoAComentar>>> getTodasPorSedeAsync(long idSede)
        {
            try
            {

                var comentario = await _dalc.getTodasPorSedeAsync(idSede);


                if (comentario != null)
                {
                    return new ResponseBase<List<TopicoAComentar>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = comentario
                    };
                }
                else
                {
                    return new ResponseBase<List<TopicoAComentar>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<TopicoAComentar>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<TopicoAComentar>> GetPorTipoTopicoIdTopico(long tipoTopico, string idTopico)
        {
            try
            {
                var comentario = await _dalc.GetPorTipoTopicoIdTopico(tipoTopico, idTopico);

                if (comentario != null)
                {
                    return new ResponseBase<TopicoAComentar>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = comentario
                    };
                }
                else
                {
                    return new ResponseBase<TopicoAComentar>()
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
                return new ResponseBase<TopicoAComentar>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<TopicoAComentar>>> getTodasPorEmpresaAsync(long idEmpresa)
        {
            try
            {

                var comentario = await _dalc.getTodasPorEmpresaAsync(idEmpresa);

                if (comentario != null)
                {
                    return new ResponseBase<List<TopicoAComentar>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = comentario
                    };
                }
                else
                {
                    return new ResponseBase<List<TopicoAComentar>>()
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
                return new ResponseBase<List<TopicoAComentar>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<TopicoAComentar>>> GetFeed()
        {
            try
            {

                var comentario = await _dalc.GetFeed();


                if (comentario != null)
                {
                    return new ResponseBase<List<TopicoAComentar>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = comentario
                    };
                }
                else
                {
                    return new ResponseBase<List<TopicoAComentar>>()
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
                return new ResponseBase<List<TopicoAComentar>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<TopicoAComentar>> guardarComentario(TopicoAComentar comentario, Transaction transaction)
        {
            try
            {
                var idTopicoComentado = await _dalc.GetPorIdTopico(comentario.idTopico);


                if (idTopicoComentado == null)
                {
                    var dataComentario = await _dalc.Set(comentario, transaction);
                    if (dataComentario != null)
                    {
                        return new ResponseBase<TopicoAComentar>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = "Operacion realizada con exito",
                            datos = dataComentario
                        };
                    }
                    else
                    {
                        return new ResponseBase<TopicoAComentar>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = false,
                            mensaje = "La operacion no se ha podido completar",
                            datos = null
                        };
                    }
                }
                else
                {
                    return new ResponseBase<TopicoAComentar>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se ha podido completar. idTopico ya existe",
                        datos = null
                    };
                }


            }
            catch (Exception ex)
            {
                return new ResponseBase<TopicoAComentar>()
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
