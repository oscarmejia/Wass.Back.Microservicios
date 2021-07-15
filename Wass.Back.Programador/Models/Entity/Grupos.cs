using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;

namespace Wass.Back.Programador.Models.Entity
{
	public class Grupos
	{
		[Key]
		public long idGrupo { get; set; }
		public long idPlan { get; set; }
		public long periodo { get; set; }
		public bool estado { get; set; }

        [NotMapped]
        public List<VariableEnGrupo> variables { get; set; }
    }

	
}
