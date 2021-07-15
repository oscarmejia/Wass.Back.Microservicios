using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class RespuestaAccionesPlanMantenimientoPreventivo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idRespuesta { get; set; }
        public Guid idParte { get; set; }
        public long idAccion { get; set; }
        public long idMantenimientoPreventivo { get; set; }
        public bool estado { get; set; }
        public string respuesta { get; set; }
    }
}
