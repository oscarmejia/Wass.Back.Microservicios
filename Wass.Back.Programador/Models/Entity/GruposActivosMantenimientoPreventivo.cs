using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
    public class GruposActivosMantenimientoPreventivo
    {
		[Key]
		public long id { get; set; }
		public string Activo { get; set; }
		public long idPlan { get; set; }
		public bool estado { get; set; }
	}
}
