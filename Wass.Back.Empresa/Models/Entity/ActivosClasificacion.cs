using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosClasificacion
    {
        [Key]
        public long idClasificacion { get; set; }
        public long idCategorizacion { get; set; }
        public long? idSubClasificaicon { get; set; }
        public string clasificacion { get; set; }
        public long prioridad { get; set; }
        public bool eliminado { get; set; }
        public DateTime fechaCreacion { get; set; }

        [ForeignKey("idCategorizacion")]
        [JsonIgnore]
        public ActivosCategorizacion categoria { get; set; }

        [ForeignKey("idSubClasificaicon")]
        [JsonIgnore]
        public ActivosClasificacion subClasificacion { get; set; }

        public List<ActivosPartes> ListaPartes { get; set; } = new List<ActivosPartes>();
    }
}
