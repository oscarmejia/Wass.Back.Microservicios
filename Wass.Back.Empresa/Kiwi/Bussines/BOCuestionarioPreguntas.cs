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
    public class BOCuestionarioPreguntas
    {
        private readonly DALCCuestionarioPreguntas _dalc;
        public BOCuestionarioPreguntas(EmpresaContext context)
        {
            _dalc = new DALCCuestionarioPreguntas(context);
        }

        public async Task<ResponseBase<CuestionarioPreguntas>> Get(long idCuestionarioPregunta)
        {
            try
            {
                var CP = await _dalc.Get(idCuestionarioPregunta);

                if (CP != null)
                {
                    return new ResponseBase<CuestionarioPreguntas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = CP
                    };
                }
                else
                {
                    return new ResponseBase<CuestionarioPreguntas>()
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
                return new ResponseBase<CuestionarioPreguntas>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CuestionarioPreguntas>>> GetTodas()
        {
            try
            {
                var cuestionarioPregunta = await _dalc.GetTodas();

                if (cuestionarioPregunta != null)
                {
                    return new ResponseBase<List<CuestionarioPreguntas>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cuestionarioPregunta
                    };
                }
                else
                {
                    return new ResponseBase<List<CuestionarioPreguntas>>()
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
                return new ResponseBase<List<CuestionarioPreguntas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CuestionarioPreguntas>>> GetTodasPorCuestionario(long idCuestionario)
        {
            try
            {
                var cuestionarioPregunta = await _dalc.GetTodasPreguntasPorCuestionario(idCuestionario);

                if (cuestionarioPregunta != null)
                {
                    return new ResponseBase<List<CuestionarioPreguntas>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cuestionarioPregunta
                    };
                }
                else
                {
                    return new ResponseBase<List<CuestionarioPreguntas>>()
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
                return new ResponseBase<List<CuestionarioPreguntas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CuestionarioPreguntas>>> GetTodasCuestionarioPorPregunta(long idPregunta)
        {
            try
            {
                var cuestionarioPregunta = await _dalc.GetTodasPreguntasEnCuestionario(idPregunta);

                if (cuestionarioPregunta != null)
                {
                    return new ResponseBase<List<CuestionarioPreguntas>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cuestionarioPregunta
                    };
                }
                else
                {
                    return new ResponseBase<List<CuestionarioPreguntas>>()
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
                return new ResponseBase<List<CuestionarioPreguntas>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CuestionarioPreguntas>> Set(CuestionarioPreguntas cuestionarioPreguntas, Transaction transaction)
        {
            try
            {
                var dataCP = await _dalc.Set(cuestionarioPreguntas, transaction);

                if (dataCP != null)
                {
                    return new ResponseBase<CuestionarioPreguntas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "La operacion se realizo correctamente",
                        datos = dataCP
                    };
                }
                else
                {
                    return new ResponseBase<CuestionarioPreguntas>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La operacion no se realizo",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuestionarioPreguntas>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<CuestionarioPreguntas>> Eliminar(CuestionarioPreguntas pregunta)
        {
            try
            {
                var result = await _dalc.Eliminar(pregunta);
                if (result.estado)
                {
                    return new ResponseBase<CuestionarioPreguntas>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = result.mensaje,
                        datos = null
                    };
                }
                else
                {
                    return new ResponseBase<CuestionarioPreguntas>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = result.mensaje,
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuestionarioPreguntas>()
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
