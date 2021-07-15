using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class CuestionarioPreguntas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCuestionarioPregunta { get; set; }
        public long idCuestionario { get; set; }
        public long idPregunta { get; set; }
        public bool activo { get; set; }

        [ForeignKey("idCuestionario")]
        [JsonIgnore]
        public Cuestionario Cuestionario { get; set; }

        [ForeignKey("idPregunta")]
        public Preguntas Preguntas { get; set; }
    }
}
