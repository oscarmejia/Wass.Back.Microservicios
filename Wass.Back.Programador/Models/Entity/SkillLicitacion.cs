using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Programador.Models.Entity
{
    public class SkillLicitacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idSkillLicitacion { get; set; }
        public long idLicitacion { get; set; }
        public string skills { get; set; }

        [ForeignKey("idLicitacion")]
        [JsonIgnore]
        public Licitacion licitacion { get; set; }
        public SkillLicitacion()
        {
        }
    }
}
