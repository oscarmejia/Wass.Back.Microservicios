using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
    public class PlanMantenimientoPreventivo
    {
		[Key]
		public long idPlanMantenimientoPreventivo { get; set; }
		public long idPlan { get; set; }
		public string idActivo { get; set; }
		public long idGrupo { get; set; }
		public DateTime fechaUltimoMantenimientoPreventivo { get; set; }
		public bool estado { get; set; }
	}
}
