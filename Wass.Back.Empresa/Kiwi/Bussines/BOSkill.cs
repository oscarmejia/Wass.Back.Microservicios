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
    public class BOSkill
    {
        private readonly DALCSkill _dalc;
        public BOSkill(EmpresaContext context)
        {
            _dalc = new DALCSkill(context);
        }

        public async Task<ResponseBase<Skill>> get(long idSkill)
        {
            try
            {
                var skill = await _dalc.get(idSkill);

                if (skill != null)
                {
                    return new ResponseBase<Skill>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = skill
                    };
                }
                else
                {
                    return new ResponseBase<Skill>()
                    {
                        codigo = (int)HttpStatusCode.NoContent,
                        estado = true,
                        mensaje = "La consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Skill>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }


        public async Task<ResponseBase<List<Skill>>> getTodos()
        {
            try
            {
                var skill = await _dalc.getTodos();

                if (skill != null)
                {
                    return new ResponseBase<List<Skill>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = skill
                    };
                }
                else
                {
                    return new ResponseBase<List<Skill>>()
                    {
                        codigo = (int)HttpStatusCode.NoContent,
                        estado = true,
                        mensaje = "La consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Skill>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<Skill>>> getTodosPorEmpresa(long idEmpresa)
        {
            try
            {
                var skill = await _dalc.getTodosPorEmpresa(idEmpresa);

                if (skill != null)
                {
                    return new ResponseBase<List<Skill>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = skill
                    };
                }
                else
                {
                    return new ResponseBase<List<Skill>>()
                    {
                        codigo = (int)HttpStatusCode.NoContent,
                        estado = true,
                        mensaje = "La consulta no arrojo resultados",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Skill>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<Skill>> guardar(Skill skill, Transaction transaction)
        {
            try
            {
                var skillNew = await _dalc.set(skill, transaction);

                if (skillNew != null)
                {
                    return new ResponseBase<Skill>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = skillNew
                    };
                }
                else
                {
                    return new ResponseBase<Skill>()
                    {
                        codigo = (int)HttpStatusCode.NoContent,
                        estado = true,
                        mensaje = "La operacion fallo",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<Skill>()
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
