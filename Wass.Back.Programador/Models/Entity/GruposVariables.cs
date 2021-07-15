using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
	public class GruposVariables
	{
		[Key]
		public long id { get; set; }
		public long idGrupo { get; set; }
		public long idVariable { get; set; }
		public long idPlan { get; set; }
		public string valor { get; set; }
		public bool estado { get; set; }
		public long periodo { get; set; }
	}
}
