using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Empresa.Models.Entity
{
    public class RespuestaActivosVariables
    {
        [Key]
        public long idRespuestaActivosVariables { get; set; }
        public long idActivoVariable { get; set; }
        public long idClasificacion { get; set; }
        public long idCategorizacion { get; set; }
        public Guid? idActivoFlota { get; set; }
        public Guid? idActivoEquipo { get; set; }
        public string respuesta { get; set; }
    }
}
