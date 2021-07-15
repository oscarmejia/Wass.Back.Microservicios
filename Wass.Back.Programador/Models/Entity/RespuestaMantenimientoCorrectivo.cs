using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
    public class RespuestaMantenimientoCorrectivo
    {
        [Key]
        public long idRespuestaMantenimientoCorrectivo { get; set; }
        public long idMantenimientoCorrectivo { get; set; }
        public long? idDiagnostico { get; set; }
        public string respuesta { get; set; }
    }
}
