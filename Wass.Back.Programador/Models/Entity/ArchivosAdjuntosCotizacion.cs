using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Programador.Models.Entity
{
    public class ArchivosAdjuntosCotizacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idArchivoAdjuntoCotizacion { get; set; }
        public string nombre { get; set; }
        public string urlArchivoAdjuntoCotizacion { get; set; }
        public long idCotizacion { get; set; }
        public bool eliminada { get; set; }

        [ForeignKey("idCotizacion")]
        [JsonIgnore]
        public Cotizaciones Cotizacion { get; set; }

    }
}
