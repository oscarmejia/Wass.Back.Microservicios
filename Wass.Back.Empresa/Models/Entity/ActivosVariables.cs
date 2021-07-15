using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosVariables
    {
        [Key]
        public long idActivoVariable { get; set; }
        public long idActivoClasificacionVariable { get; set; }
        public Guid? idActivoFlota { get; set; }
        public Guid? idActivoEquipo { get; set; }
        public decimal valor { get; set; }
        public bool eliminado { get; set; }
        public bool activo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string creador { get; set; }
        public DateTime fechaEdicion { get; set; }
        public string editor { get; set; }
    }
}
