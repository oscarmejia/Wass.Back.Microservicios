using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Cuestionario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCuestionario { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        public long idEmpresa { get; set; }

        [JsonIgnore]
        public List<CuestionarioPreguntas> cuestinarioPreguntas { get; set; }
    }
}
