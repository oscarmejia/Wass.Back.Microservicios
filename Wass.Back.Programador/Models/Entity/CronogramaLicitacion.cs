using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Programador.Models.Entity
{
    public class CronogramaLicitacion
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCronogramaLicitacion { get; set; }
        public long etapa { get; set; }
        public DateTime fechaLimite { get; set; }
        public long idLicitacion { get; set; }

        [ForeignKey("idLicitacion")]
        [JsonIgnore]
        public Licitacion licitacion { get; set; }



        public CronogramaLicitacion()
        {
        }
    }
}
