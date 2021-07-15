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
    public class BOCuadrillaSkill
    {
        private readonly DALCCuadrillaSkill _dalc;

        public BOCuadrillaSkill(EmpresaContext context)
        {
            _dalc = new DALCCuadrillaSkill(context);
        }

        public async Task<ResponseBase<CuadrillaSkill>> get(long idCuadrillaSkill)
        {
            try
            {
                var cuadrillaSkill = await _dalc.get(idCuadrillaSkill);
                if (cuadrillaSkill != null)
                {
                    return new ResponseBase<CuadrillaSkill>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = cuadrillaSkill
                    };
                }
                else
                {
                    return new ResponseBase<CuadrillaSkill>()
                    {
                        codigo = (int)HttpStatusCode.NoContent,
                        estado = true,
                        mensaje = "La consulta no arrojo resultados",
                        datos = { }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuadrillaSkill>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CuadrillaSkill>>> getTodos()
        {
            try
            {
                var cuadrillaSkill = await _dalc.getTodos();
                if (cuadrillaSkill != null)
                {
                    return new ResponseBase<List<CuadrillaSkill>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = cuadrillaSkill
                    };
                }
                else
                {
                    return new ResponseBase<List<CuadrillaSkill>>()
                    {
                        codigo = (int)HttpStatusCode.NoContent,
                        estado = true,
                        mensaje = "La consulta no arrojo resultados",
                        datos = { }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CuadrillaSkill>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CuadrillaSkill>>> getTodospoCuadrilla(long idCuadrilla)
        {
            try
            {
                var cuadrillaSkill = await _dalc.getTodosporCuadrilla(idCuadrilla);
                if (cuadrillaSkill != null)
                {
                    return new ResponseBase<List<CuadrillaSkill>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = cuadrillaSkill
                    };
                }
                else
                {
                    return new ResponseBase<List<CuadrillaSkill>>()
                    {
                        codigo = (int)HttpStatusCode.NoContent,
                        estado = true,
                        mensaje = "La consulta no arrojo resultados",
                        datos = { }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CuadrillaSkill>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<CuadrillaSkill>>> getTodosporSkill(long idSkill)
        {
            try
            {
                var cuadrillaSkill = await _dalc.getTodosporSkill(idSkill);
                if (cuadrillaSkill != null)
                {
                    return new ResponseBase<List<CuadrillaSkill>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = cuadrillaSkill
                    };
                }
                else
                {
                    return new ResponseBase<List<CuadrillaSkill>>()
                    {
                        codigo = (int)HttpStatusCode.NoContent,
                        estado = true,
                        mensaje = "La consulta no arrojo resultados",
                        datos = { }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CuadrillaSkill>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<CuadrillaSkill>> guardar(CuadrillaSkill cuadrillaSkill, Transaction transaction)
        {
            try
            {
                var cuadrillaSkillNew = await _dalc.set(cuadrillaSkill, transaction);
                if (cuadrillaSkillNew != null)
                {
                    return new ResponseBase<CuadrillaSkill>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = cuadrillaSkillNew
                    };
                }
                else
                {
                    return new ResponseBase<CuadrillaSkill>()
                    {
                        codigo = (int)HttpStatusCode.NoContent,
                        estado = true,
                        mensaje = "La consulta no arrojo resultados",
                        datos = { }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<CuadrillaSkill>()
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
