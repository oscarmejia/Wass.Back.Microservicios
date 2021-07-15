using System;
using System.Collections.Generic;

namespace Wass.Back.Programador.Models.Peticiones.SkillLicitacion
{
    public class SkillResponse
    {
        public long idSkillLicitacion { get; set; }
        public long idLicitacion { get; set; }
        public List<string> skills { get; set; } = new List<string>();
    }
}
