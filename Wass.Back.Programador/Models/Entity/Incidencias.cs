using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class Incidencias
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long idIncidencias { get; set; }
        public long idOrden { get; set; }
        public DateTime fechaHora { get; set; }
        public string nombreUsuario { get; set; }
        public string descripcion { get; set; }
        public List<ArchivosAdjuntosIncidencias> ArchivosAdjuntos { get; set; }

        [ForeignKey("idOrden")]
        [JsonIgnore]
        public OrdenesTrabajo OrdenesTrabajo { get; set; }
    }
}
