using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ArchivosAdjuntosActivosEquipos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idArchivoAdjuntoActivosEquipos { get; set; }
        public string nombre { get; set; }
        public string urlArchivoAdjuntoActivosEquipos { get; set; }
        public Guid idActivo { get; set; }
        public bool eliminada { get; set; }

        [ForeignKey("idActivo")]
        [JsonIgnore]
        public ActivosEquipos ActivosEquipos { get; set; }
    }
}
