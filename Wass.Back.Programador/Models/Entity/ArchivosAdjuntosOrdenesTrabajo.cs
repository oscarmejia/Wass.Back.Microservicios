using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;
namespace Wass.Back.Programador.Models.Entity
{
    public class ArchivosAdjuntosOrdenesTrabajo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idArchivosAdjuntosOrdenesTrabajo { get; set; }
        public string nombre { get; set; }
        public string urlArchivosAdjuntosOrdenesTrabajo { get; set; }
        public long idOrdenesTrabajo { get; set; }
        public bool eliminada { get; set; }

        [ForeignKey("idOrdenesTrabajo")]
        [JsonIgnore]
        public OrdenesTrabajo OrdenesTrabajo { get; set; }
    }
}
