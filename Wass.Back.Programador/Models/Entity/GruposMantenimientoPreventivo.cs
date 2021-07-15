using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class GruposMantenimientoPreventivo
    {
		[Key]
		public long idGrupo { get; set; }
		public long idPlan { get; set; }
		public long periodo { get; set; }
		public bool estado { get; set; }
		public bool parada { get; set; }

		[NotMapped]
		public List<Guid> partes { get; set; }
	}
}
