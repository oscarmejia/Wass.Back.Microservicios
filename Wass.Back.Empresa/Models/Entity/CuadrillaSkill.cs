using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class CuadrillaSkill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCuadrillaSkill { get; set; }
        public long idCuadrilla { get; set; }
        public long idSkill { get; set; }
        public DateTime fechaCreacion { get; set; }
        public bool eliminado { get; set; }

        [ForeignKey("idCuadrilla")]
        public Cuadrillas cuadrilla { get; set; }

        [ForeignKey("idSkill")]
        public Skill skill { get; set; }
    }
}
