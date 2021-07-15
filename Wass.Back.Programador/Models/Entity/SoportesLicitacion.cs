using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Programador.Models.Entity
{
    public class SoportesLicitacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idSoporteLicitacion { get; set; }
        public string nombre { get; set; }
        public string urlSoporteLicitacion { get; set; }
        public long idLicitacion { get; set; }

        [ForeignKey("idLicitacion")]
        [JsonIgnore]
        public Licitacion licitacion { get; set; }

        public SoportesLicitacion()
        {
        }
    }
}
