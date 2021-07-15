using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.BusquedaSkillsEmpresaLicitacion;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BOBusquedaSkillsEmpresaLicitacion
    {
        private readonly DALCBusquedaSkillsEmpresaLicitacion _dalc;

        public BOBusquedaSkillsEmpresaLicitacion(ProgramadorContext context)
        {
            _dalc = new DALCBusquedaSkillsEmpresaLicitacion(context);
        }

        public async Task<ResponseBase<List<Licitacion>>> Get(BusquedaSkillsEmpresaLicitacionRequest buscar)
        {
            try
            {
                var ob = new List<Licitacion>();
                

                foreach (var skill in buscar.data)
                {
                    var skill_valido = "";
                    var palabras = skill.Split(' ');

                    foreach(var palabra in palabras)
                    {
                        if (palabra != "el" && palabra != "la" && palabra != "lo" && palabra != "los" && palabra != "las" && palabra != "de")
                        {
                            skill_valido = skill_valido + palabra + " ";
                        }
                        
                    }
                    var busqueda = await _dalc.Get(skill_valido.ToLower().Trim());
                    if (busqueda.Count > 0)
                    {
                        foreach (var item in busqueda)
                        {
                            if (!ob.Contains(item))
                            {
                                ob.Add(item);
                            }
                        }
                    }
                }

                if (ob != null)
                {
                    return new ResponseBase<List<Licitacion>>()
                    {
                        codigo = (int)HttpStatusCode.OK,
                        estado = true,
                        mensaje = String.Empty,
                        datos = ob
                    };
                }
                else
                {
                    return new ResponseBase<List<Licitacion>>()
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
                return new ResponseBase<List<Licitacion>>()
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
