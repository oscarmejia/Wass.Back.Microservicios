using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class MantenimientoPreventivo
    {
        [Key]
        public long idMantenimientoPreventivo { get; set; }
        public long idOrden { get; set; }
        public long idPlan { get; set; }
        public long idGrupo { get; set; }
        public DateTime? fechaPropuestaProgramacion { get; set; }
        public bool parada { get; set; }
        public bool eliminado { get; set; }
        public string Acciones { get; set; }

        [ForeignKey("idOrden")]
        public OrdenesTrabajo orden { get; set; }

    }

}
