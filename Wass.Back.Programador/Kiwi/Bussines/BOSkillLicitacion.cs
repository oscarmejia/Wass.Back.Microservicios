using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.SkillLicitacion;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BOSkillLicitacion
    {

        private readonly DALCSkillLicitacion _dalc;

        public BOSkillLicitacion(ProgramadorContext context)
        {
            _dalc = new DALCSkillLicitacion(context);
        }

        public async Task<ResponseBase<SkillResponse>> Get (long idSkillLicitacion)
        {
            try
            {
                var skill = await _dalc.Get(idSkillLicitacion);

                var ob = new SkillResponse()
                {
                    idSkillLicitacion = skill.idSkillLicitacion,
                    idLicitacion = skill.idLicitacion,
                    skills = skill != null ? JsonConvert.DeserializeObject<List<string>>(skill.skills) : new List<string>()
                };

                if (skill != null)
                {
                    return new ResponseBase<SkillResponse>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<SkillResponse>()
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
                return new ResponseBase<SkillResponse>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<SkillResponse>>> GetTodas()
        {
            try
            {
                var skills = await _dalc.GetTodas();
                var ob = new List<SkillResponse>();

                foreach (var item in skills)
                {
                    ob.Add(new SkillResponse()
                    {
                        idSkillLicitacion = item.idSkillLicitacion,
                        idLicitacion = item.idLicitacion,
                        skills = item != null ? JsonConvert.DeserializeObject<List<string>>(item.skills) : new List<string>()
                    });
                }

                if (skills != null)
                {
                    return new ResponseBase<List<SkillResponse>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<SkillResponse>>()
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
                return new ResponseBase<List<SkillResponse>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<SkillResponse>> GetPorLicitacion(long idLicitacion)
        {
            try
            {
                var skill = await _dalc.GetPorLicitacion(idLicitacion);

                if (skill != null)
                {
                    var data = new SkillResponse()
                    {
                        idSkillLicitacion = skill.idSkillLicitacion,
                        idLicitacion = skill.idLicitacion,
                        skills = skill != null ? JsonConvert.DeserializeObject<List<string>>(skill.skills) : new List<string>()
                    };

                    return new ResponseBase<SkillResponse>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = data
                    };
                }
                else
                {
                    return new ResponseBase<SkillResponse>()
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
                return new ResponseBase<SkillResponse>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<SkillResponse>> GuardarSkills(SkillRequest skill, Transaction transaction)
        {
            try
            {
                var obSkill = new SkillLicitacion()
                {
                    idSkillLicitacion = skill.idSkillLicitacion,
                    idLicitacion = skill.idLicitacion,
                    skills = JsonConvert.SerializeObject(skill.skills).ToString()
                };

                var dataSkill = await _dalc.Set(obSkill, transaction);

                var dataSkillResponse = new SkillResponse()
                {
                    idSkillLicitacion = dataSkill.idSkillLicitacion,
                    idLicitacion = dataSkill.idLicitacion,
                    skills = JsonConvert.DeserializeObject<List<string>>(dataSkill.skills)
                };

                if (dataSkill != null)
                {
                    return new ResponseBase<SkillResponse>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = "Operacion creada correctamente",
                        datos = dataSkillResponse
                    };
                }
                else
                {
                    return new ResponseBase<SkillResponse>()
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
                return new ResponseBase<SkillResponse>()
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
