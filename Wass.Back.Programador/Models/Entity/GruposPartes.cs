using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
    public class GruposPartes
    {
		[Key]
		public long idGruposPartes { get; set; }
		public long idGrupo { get; set; }
		public Guid idParte{ get; set; }
		public long idPlan { get; set; }
		public bool estado { get; set; }
		public long idRepuesto { get; set; }
		public long periodo { get; set; }
		public bool parada { get; set; }
	}
}
