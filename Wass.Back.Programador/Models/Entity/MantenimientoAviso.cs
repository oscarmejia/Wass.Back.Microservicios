using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class MantenimientoAviso
    {
        [Key]
        public long idAviso { get; set; }
        public long idOrden { get; set; }
        public long idDiagnostico { get; set; }
        public long idCondicionesVariables { get; set; }
        public string detalleAviso { get; set; }
        public bool correctivo { get; set; }
        public string observacion { get; set; }

        //public OrdenesTrabajo orden { get; set; }
        [ForeignKey("idOrden")]
        public OrdenesTrabajo orden { get; set; }

        [ForeignKey("idCondicionesVariables")]
        public CondicionesVariables condicion { get; set; }
    }
}
