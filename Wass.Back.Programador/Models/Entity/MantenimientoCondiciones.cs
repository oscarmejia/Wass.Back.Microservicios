using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
	public class MantenimientoCondiciones
	{
		[Key]
		public long id { get; set; }
		public long idCondicion { get; set; }
		public long idPlan { get; set; }
		public Guid idActivo { get; set; }
		public long idVariable { get; set; }
		public string acciones { get; set; }
		public DateTime fecha { get; set; }
		public bool estado { get; set; }

		
	}
}
