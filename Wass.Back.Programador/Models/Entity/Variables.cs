using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wass.Back.Programador.Models.Entity
{
	public class Variables
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
