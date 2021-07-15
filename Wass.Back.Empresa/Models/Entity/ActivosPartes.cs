using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosPartes
    {
        [Key]
        public Guid idParte { get; set; }
        public long idClasificacion { get; set; }
        public Guid? idSubParte { get; set; }
        public string parte { get; set; }
        public bool eliminado { get; set; }
        public DateTime fechaCreacion { get; set; }

        [ForeignKey("idClasificacion")]
        [JsonIgnore]
        public ActivosClasificacion clasificacion { get; set; }

        [ForeignKey("idSubParte")]
        [JsonIgnore]
        public ActivosPartes subParte { get; set; }
    }
}
