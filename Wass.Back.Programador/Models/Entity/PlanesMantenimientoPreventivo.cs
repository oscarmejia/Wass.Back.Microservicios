using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class PlanesMantenimientoPreventivo
    {
		[Key]
		public long idPlan { get; set; }
		public long idCategoria { get; set; }
		public long? idClasificacion1 { get; set; }
		public long? idClasificacion2 { get; set; }
		public int prioridad { get; set; }
		public long idEmpresa { get; set; }
		public long idSede { get; set; }
		public string marca { get; set; }
		public bool estado { get; set; }

		[NotMapped]
		public List<GruposActivosMantenimientoPreventivo> GruposActivos { get; set; }

		[NotMapped]
		public List<GruposPartes> GruposPartes { get; set; }

		[NotMapped]
		public List<GruposAcciones> GruposAcciones { get; set; }
	}
}
