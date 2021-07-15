using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
    public class MantenimientoCorrectivo
    {
        [Key]
        public long idMantenimientoCorrectivo { get; set; }
        public long idOrden { get; set; }
        public long idOrdenAviso { get; set; }
        public long? idDiagnostico { get; set; }
        public string falla { get; set; }
        public string acciones { get; set; }     
    }
}
