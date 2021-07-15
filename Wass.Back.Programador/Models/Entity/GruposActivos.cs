using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
	public class GruposActivos
	{
		[Key]
		public long id { get; set; }
		public Guid idActivo { get; set; }
		public long idPlan { get; set; }
		public bool estado { get; set; }
	}
}
