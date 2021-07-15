using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Preguntas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idPregunta { get; set; }
        public string nombre { get; set; }
        public long tipoRespuesta { get; set; }
        public string opciones { get; set; }
        public long idEmpresa { get; set; }
        public bool activo { get; set; }

        [JsonIgnore]
        public List<CuestionarioPreguntas> cuestinarioPreguntas { get; set; }
    }
}
