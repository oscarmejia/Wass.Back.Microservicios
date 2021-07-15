using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Programador.Models.Entity
{
    public class ComentarioActivosClasificacionDiagnosticoAcciones
    {
        [Key]
        public long idComentarioDiagnosticosAcciones { get; set; }
        public long idClasificacion { get; set; }
        public long idDiagnostico { get; set; }
        public long? idMantenimientoCorrectivo { get; set; }
        public long? idAviso { get; set; }
        public string comentario { get; set; }
    }
}
