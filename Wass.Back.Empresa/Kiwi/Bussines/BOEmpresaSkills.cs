using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Skill;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOEmpresaSkills
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCEmpresaSkills _dalc;
        private readonly string _msg_base;

        public BOEmpresaSkills(EmpresaContext context)
        {
            _dalc = new DALCEmpresaSkills(context);
            _msg_base = " habilidad ";
        }

        public async Task<ResponseBase<ResponseSkills>> GetAsync(long id)
        {
            try
            {
                var datos = await _dalc.GetAsync(id);

                var ob = new ResponseSkills()
                {
                    idEmpresa = datos.idEmpresa,
                    idSkill = datos.idSkill,
                    skills = datos != null ? JsonConvert.DeserializeObject<List<string>>(datos.skills) : new List<string>(),
                    cuadrillaSkillsEmpresa = datos.cuadrillaSkillsEmpresa,
                    diagnosticoSkillsEmpresa = datos.diagnosticoSkillsEmpresa
                };

                if (datos != null)
                {
                    return new ResponseBase<ResponseSkills>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<ResponseSkills>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = $"La {_msg_base} no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ResponseSkills>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ResponseSkills>> GetPorEmpresaAsync(long idEmpresa)
        {
            try
            {
                var datos = await _dalc.GetPorEmpresaAsync(idEmpresa);
                var ob = new ResponseSkills()
                {
                    idEmpresa = datos.idEmpresa,
                    idSkill = datos.idSkill,
                    skills = datos != null ? JsonConvert.DeserializeObject<List<string>>(datos.skills) : new List<string>(),
                    cuadrillaSkillsEmpresa = datos.cuadrillaSkillsEmpresa,
                    diagnosticoSkillsEmpresa = datos.diagnosticoSkillsEmpresa
                };

                if (datos != null)
                {
                    return new ResponseBase<ResponseSkills>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<ResponseSkills>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = $"La {_msg_base} no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ResponseSkills>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ResponseSkills>>> GetTodasAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();
                var ob = new List<ResponseSkills>();

                foreach (var item in obj)
                {
                    ob.Add(new ResponseSkills()
                    {
                        idEmpresa = item.idEmpresa,
                        idSkill = item.idSkill,
                        skills = item != null ? JsonConvert.DeserializeObject<List<string>>(item.skills) : new List<string>(),
                        cuadrillaSkillsEmpresa = item.cuadrillaSkillsEmpresa,
                        diagnosticoSkillsEmpresa = item.diagnosticoSkillsEmpresa
                    });
                }

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ResponseSkills>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = ob
                        };
                    else
                        return new ResponseBase<List<ResponseSkills>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ResponseSkills>>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = false,
                        mensaje = $"La consulta de {_msg_base} no retornó resultados.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ResponseSkills>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ResponseSkills>> SetAsync(RequestSkills _datos, Transaction transaccion)
        {
            try
            {
                var ob = new EmpresaSkills()
                {
                    idEmpresa = _datos.idEmpresa,
                    idSkill = _datos.idSkill,
                    skills = JsonConvert.SerializeObject(_datos.skills).ToString(),
                };

                var data = await _dalc.SetAsync(ob, transaccion);
                var rst = new ResponseSkills()
                {
                    idEmpresa = _datos.idEmpresa,
                    idSkill = _datos.idSkill,
                    skills = JsonConvert.DeserializeObject<List<string>>(data.skills),
                };

                if (data != null)
                {
                    return new ResponseBase<ResponseSkills>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación sobre {_msg_base} realizada con exito",
                        datos = rst
                    };
                }
                else
                    return new ResponseBase<ResponseSkills>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
                        datos = rst
                    };

            }
            catch (Exception ex)
            {
                return new ResponseBase<ResponseSkills>()
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
