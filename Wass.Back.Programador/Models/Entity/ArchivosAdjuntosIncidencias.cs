using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class ArchivosAdjuntosIncidencias
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idArchivosAdjuntosIncidencias { get; set; }
        public string nombre { get; set; }
        public string urlArchivosAdjuntosIncidencias { get; set; }
        public long idIncidencias { get; set; }
        public bool eliminada { get; set; }

        [ForeignKey("idIncidencias")]
        [JsonIgnore]
        public Incidencias Incidencias { get; set; }
    }
}
