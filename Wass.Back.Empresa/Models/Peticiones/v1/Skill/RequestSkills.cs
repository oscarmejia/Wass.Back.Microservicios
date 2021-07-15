using System;
using System.Collections.Generic;

namespace Wass.Back.Empresa.Models.Peticiones.v1.Skill
{
    public class RequestSkills
    {
        public long idEmpresa { get; set; }
        public long idSkill { get; set; }
        public List<string> skills { get; set; } = new List<string>();
    }
}
