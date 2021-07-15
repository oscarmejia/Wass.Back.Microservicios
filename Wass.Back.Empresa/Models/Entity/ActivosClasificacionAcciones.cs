using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosClasificacionAcciones
    {
        [Key]
        public long idAccion { get; set; }
        public long idClasificacion { get; set; }
        public string accion { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
        public bool eliminado { get; set; }
        public List<ActivosClasificacionDiagnosticosAcciones> ActivosClasificacionDiagnosticosAcciones { get; set; }
    }
}
