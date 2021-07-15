using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Kiwi.Interface;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;

namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOActivosClasificacionDiagnosticosSkills : IBOCrud<ActivosClasificacionDiagnosticosSkills> 
    {
        public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
        private readonly DALCActivosClasificacionDiagnosticosSkills _dalc;
        private readonly string _msg_base;
        public BOActivosClasificacionDiagnosticosSkills(EmpresaContext context)
        {
            _dalc = new DALCActivosClasificacionDiagnosticosSkills(context);
            _msg_base = " diagnostico / skill ";
        }

        public async Task<ResponseBase<ActivosClasificacionDiagnosticosSkills>> GetAsync(long id)
        {
            try
            {
                var datos = await _dalc.GetAsync(id);

                if (datos != null)
                {
                    return new ResponseBase<ActivosClasificacionDiagnosticosSkills>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<ActivosClasificacionDiagnosticosSkills>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = $"El {_msg_base} no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosClasificacionDiagnosticosSkills>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosClasificacionDiagnosticosSkills>> GetPorSkillDiagnosticoAsync(long idDiagnostico, long idSkill)
        {
            try
            {
                var datos = await _dalc.GetPorSkillDiagnosticoAsync(idDiagnostico, idSkill);

                if (datos != null)
                {
                    return new ResponseBase<ActivosClasificacionDiagnosticosSkills>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = string.Empty,
                        datos = datos
                    };
                }
                else
                {
                    return new ResponseBase<ActivosClasificacionDiagnosticosSkills>()
                    {
                        codigo = (int)HttpStatusCode.NotFound,
                        estado = true,
                        mensaje = $"El {_msg_base} no esta disponible.",
                        datos = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosClasificacionDiagnosticosSkills>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>> GetAllAsync()
        {
            try
            {
                var obj = await _dalc.GetAllAsync();

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
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
                return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>> GetPorDiagnosticoAsync(long idDiagnostico)
        {
            try
            {
                var obj = await _dalc.GetPorDiagnosticoAsync(idDiagnostico);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
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
                return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>> GetPorSkillAsync(long idSkill)
        {
            try
            {
                var obj = await _dalc.GetPorSkillAsync(idSkill);

                if (obj != null)
                {
                    if (obj.Count > 0)
                        return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
                        {
                            codigo = (int)HttpStatusCode.OK,
                            estado = true,
                            mensaje = string.Empty,
                            datos = obj
                        };
                    else
                        return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
                        {
                            codigo = (int)HttpStatusCode.NotFound,
                            estado = true,
                            mensaje = $"No hay {_msg_base} disponibles.",
                            datos = null
                        };
                }
                else
                {
                    return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
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
                return new ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>()
                {
                    codigo = (int)HttpStatusCode.InternalServerError,
                    estado = false,
                    mensaje = $"Error: {ex.Message}",
                    datos = null
                };
            }
        }

        public async Task<ResponseBase<ActivosClasificacionDiagnosticosSkills>> SetAsync(ActivosClasificacionDiagnosticosSkills objeto, Transaction transaccion)
        {
            try
            {
                var data = await _dalc.SetAsync(objeto, transaccion);
                if (data != null)
                {
                    return new ResponseBase<ActivosClasificacionDiagnosticosSkills>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = $"Operación sobre {_msg_base} realizada con exito",
                        datos = data
                    };
                }
                else
                    return new ResponseBase<ActivosClasificacionDiagnosticosSkills>()
                    {
                        codigo = (int)HttpStatusCode.InternalServerError,
                        estado = false,
                        mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
                        datos = data
                    };

            }
            catch (Exception ex)
            {
                return new ResponseBase<ActivosClasificacionDiagnosticosSkills>()
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
