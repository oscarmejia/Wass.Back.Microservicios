using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class DiagnosticoSkillsEmpresa
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idDiagnosticoSkillsEmpresa { get; set; }
        public long idDiagnostico { get; set; }
        public long idSkill { get; set; }
        public DateTime fechaHora { get; set; }
        public long idUsuario { get; set; }
        public string skillsSeleccionados { get; set; }

        [ForeignKey("idDiagnostico")]
        //[JsonIgnore]
        public ActivosClasificacionDiagnosticos ActivosClasificacionDiagnosticos { get; set; }

        [ForeignKey("idSkill")]
        //[JsonIgnore]
        public EmpresaSkills empresaSkills { get; set; }
    }
}
