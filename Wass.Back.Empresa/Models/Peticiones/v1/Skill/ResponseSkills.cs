using System;
using System.Collections.Generic;
using Wass.Back.Empresa.Models.Entity;

namespace Wass.Back.Empresa.Models.Peticiones.v1.Skill
{
    public class ResponseSkills
    {
        public long idEmpresa { get; set; }
        public long idSkill { get; set; }
        public List<string> skills { get; set; } = new List<string>();

        public List<Entity.CuadrillaSkillsEmpresa> cuadrillaSkillsEmpresa { get; set; }
        public List<Entity.DiagnosticoSkillsEmpresa> diagnosticoSkillsEmpresa { get; set; }
    }
}
