using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class GruposAcciones
    {
		[Key]
		public long idGruposAcciones { get; set; }
		public long idGrupo { get; set; }
		public long idAccion { get; set; }
		public long idPlan { get; set; }
		public bool estado { get; set; }

		[ForeignKey("idPlan")]
		[JsonIgnore]
		public PlanesMantenimientoPreventivo Plan { get; set; }
	}
}
