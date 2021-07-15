using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class RespuestaCuestionario
    {
        [Key]
        public long idRespuestaCuestionario { get; set; }
        public long idCuestionario { get; set; }
        public long idCotizacion { get; set; }
        public long idLicitacion { get; set; }
        public string respuesta { get; set; }
        public bool activo { get; set; }
        public string opciones { get; set; }

        public RespuestaCuestionario()
        {

        }
    }
}
