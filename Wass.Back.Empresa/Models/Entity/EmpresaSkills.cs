using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Empresa.Models.Entity
{
    public class EmpresaSkills
    {
        [Key]
        public long idSkill { get; set; }
        public long idEmpresa { get; set; }
        public string skills { get; set; }
        public List<CuadrillaSkillsEmpresa> cuadrillaSkillsEmpresa { get; set; }
        public List<DiagnosticoSkillsEmpresa> diagnosticoSkillsEmpresa { get; set; }
    }
}
