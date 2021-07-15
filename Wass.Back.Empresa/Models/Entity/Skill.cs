using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class Skill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idSkill { get; set; }
        public long idEmpresa { get; set; }
        public string skill { get; set; }
    }
}
