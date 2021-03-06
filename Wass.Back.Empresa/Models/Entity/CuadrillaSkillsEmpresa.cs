using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class CuadrillaSkillsEmpresa
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCuadrillaSkillsEmpresa { get; set; }
        public long idCuadrilla { get; set; }
        public long idSkill { get; set; }
        public DateTime fechaHora { get; set; }
        public long idUsuario { get; set; }
        public string skillsSeleccionados { get; set; }

        [ForeignKey("idCuadrilla")]
        //[JsonIgnore]
        public Cuadrillas cuadrilla { get; set; }

        [ForeignKey("idSkill")]
        //[JsonIgnore]
        public EmpresaSkills empresaSkills { get; set; }
    }
}
