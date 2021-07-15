using System.Collections.Generic;
using Wass.Back.Programador.Models.Entity;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
	public class AgregarVariablesRondasRequest
	{
		public long idGrupo { get; set; }
		public long idPlan { get; set; }
		public List<VariableEnGrupo> variables { get; set; }
	}

    public class VariableEnGrupo
    {
        public long id { get; set; }
        public string valor { get; set; }
    }
}
