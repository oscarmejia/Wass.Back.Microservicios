using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
	public class PlanesRondas
	{
		[Key]
		public long idPlan { get; set; }

		//tipoPlan : 1 => Ronda
		//tipoPlan : 2 => Condiciones
		public long tipoPlan { get; set; }
		public long idCategoria { get; set; }
		public long idClasificacion1 { get; set; }
		public long idClasificacion2 { get; set; }
		public long idEmpresa { get; set; }
		public long idSede { get; set; }
		public string marca { get; set; }

		//public List<OrdenesTrabajo> ordenes { get; set; }

		[NotMapped]
		public List<GruposActivos> GruposActivos { get; set; }

		

		[NotMapped]
		public List<GruposVariables> GruposVariables { get; set; }
	}
}
