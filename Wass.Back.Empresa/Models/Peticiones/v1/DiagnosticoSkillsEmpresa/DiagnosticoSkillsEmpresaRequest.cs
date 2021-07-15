using System;
using System.Collections.Generic;
using Wass.Back.Empresa.Models.Entity;

namespace Wass.Back.Empresa.Models.Peticiones.v1.DiagnosticoSkillsEmpresa
{
    public class DiagnosticoSkillsEmpresaRequest
    {
        public long idDiagnosticoSkillsEmpresa { get; set; }
        public long idDiagnostico { get; set; }
        public long idSkill { get; set; }
        public DateTime fechaHora { get; set; }
        public long idUsuario { get; set; }
        public List<string> skillsSeleccionados { get; set; } = new List<string>();


        public ActivosClasificacionDiagnosticos ActivosClasificacionDiagnosticos { get; set; }


        public EmpresaSkills empresaSkills { get; set; }

    }
}
