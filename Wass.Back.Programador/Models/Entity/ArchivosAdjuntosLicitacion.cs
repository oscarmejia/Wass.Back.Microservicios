using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Programador.Models.Entity
{
    public class ArchivosAdjuntosLicitacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idArchivoAdjuntoLicitacion { get; set; }
        public string nombre { get; set; }
        public string urlArchivoAdjuntoLicitacion { get; set; }
        public long idLicitacion { get; set; }
        public bool eliminada { get; set; }

        [ForeignKey("idLicitacion")]
        [JsonIgnore]
        public Licitacion Licitacion { get; set; }
    }
}
