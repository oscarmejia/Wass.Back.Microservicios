using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosClasificacionVariables
    {
        [Key]
        public long idVarible { get; set; }
        public long idClasificacion { get; set; }
        public long idUnidadMedida { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        public bool eliminado { get; set; }
    }
}
