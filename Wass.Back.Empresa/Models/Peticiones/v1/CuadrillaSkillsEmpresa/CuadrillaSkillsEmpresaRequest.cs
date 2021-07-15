using System;
using System.Collections.Generic;
using Wass.Back.Empresa.Models.Entity;

namespace Wass.Back.Empresa.Models.Peticiones.v1.CuadrillaSkillsEmpresa
{
    public class CuadrillaSkillsEmpresaRequest
    {
        public long idCuadrillaSkillsEmpresa { get; set; }
        public long idCuadrilla { get; set; }
        public long idSkill { get; set; }
        public DateTime fechaHora { get; set; }
        public long idUsuario { get; set; }
        public List<string> skillsSeleccionados { get; set; } = new List<string>();

        
        public Cuadrillas cuadrilla { get; set; }

       
        public EmpresaSkills empresaSkills { get; set; }
    }
}
