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
    public class BOCuestionario
    {
        private readonly DALCCuestionario _dalc;

        public BOCuestionario(EmpresaContext context)
        {
            _dalc = new DALCCuestionario(context);
        }

        public async Task<ResponseBase<Cuestionario>> Get(long idCuestionario)
        {
            try
            {
                var cuestionario = await _dalc.Get(idCuestionario);

                if (cuestionario != null)
                {
                    return new ResponseBase<Cuestionario>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cuestionario
                    };
                }
                else
                {
                    return new ResponseBase<Cuestionario>()
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
                return new ResponseBase<Cuestionario>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Cuestionario>>> GetTodas()
        {
            try
            {
                var cuestionario = await _dalc.GetTodas();

                if (cuestionario != null && cuestionario.Count > 0)
                {
                    return new ResponseBase<List<Cuestionario>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cuestionario
                    };
                }
                else
                {
                    return new ResponseBase<List<Cuestionario>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Cuestionario>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Cuestionario>>> GetTodasPorEmpresa(long idEmpresa)
        {
            try
            {
                var cuestionario = await _dalc.GetPorEmpresa(idEmpresa);

                if (cuestionario != null && cuestionario.Count > 0)
                {
                    return new ResponseBase<List<Cuestionario>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = cuestionario
                    };
                }
                else
                {
                    return new ResponseBase<List<Cuestionario>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = "La consulta no arrojo resultado",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Cuestionario>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Cuestionario>> GuardarCuestionario(Cuestionario cuestionario, Transaction transaction)
        {
            try
            {
                var DataCuestionario = await _dalc.Set(cuestionario, transaction);

                if (DataCuestionario != null)
                {
                    return new ResponseBase<Cuestionario>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion realizada con exito",
                        datos = DataCuestionario
                    };
                }
                else
                {
                    return new ResponseBase<Cuestionario>()
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
                return new ResponseBase<Cuestionario>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Cuestionario>> Eliminar(Cuestionario pregunta)
        {
            try
            {
                var result = await _dalc.Eliminar(pregunta);
                if (result.estado)
                {
                    return new ResponseBase<Cuestionario>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = result.mensaje,
                        datos = null
                    };
                }
                else
                {
                    return new ResponseBase<Cuestionario>()
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
                return new ResponseBase<Cuestionario>()
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
