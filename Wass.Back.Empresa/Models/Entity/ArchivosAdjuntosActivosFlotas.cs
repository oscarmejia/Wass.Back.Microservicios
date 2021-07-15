using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ArchivosAdjuntosActivosFlotas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idArchivoAdjuntoActivosFlotas { get; set; }
        public string nombre { get; set; }
        public string urlArchivoAdjuntoActivosFlotas { get; set; }
        public Guid idActivo { get; set; }
        public bool eliminada { get; set; }

        [ForeignKey("idActivo")]
        [JsonIgnore]
        public ActivosFlotas ActivosFlotas { get; set; }
    }
}
