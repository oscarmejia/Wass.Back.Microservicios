using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Comentario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idComentario { get; set; }
        public long idTopicoAComentar { get; set; }
        public long idEmpleado { get; set; }
        public string urlImagen { get; set; }
        public DateTime fechaHoraComentario { get; set; }
        public bool eliminado { get; set; }
        public long like { get; set; }
        public string idUsuariosLike { get; set; }
        public bool replica { get; set; }
        public long? idComentarioPadre { get; set; }
        public string comentario { get; set; }

        [ForeignKey("idTopicoAComentar")]
        [JsonIgnore]
        public TopicoAComentar TopicoAComentar { get; set; }

        [ForeignKey("idEmpleado")]
        [JsonIgnore]
        public Empleados Empleados { get; set; }
    }
}
