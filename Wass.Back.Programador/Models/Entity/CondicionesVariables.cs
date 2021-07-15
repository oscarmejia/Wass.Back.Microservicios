using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
	public class CondicionesVariables
	{
		[Key]
		public long idCondicionesVariables { get; set; }
		public long idPlan { get; set; }
		public long idVariable { get; set; }
		public string comparadorCondicion { get; set; }
		public string valorCondicion { get; set; }
		public string accionQueRealiza { get; set; }
		public bool estado { get; set; }

        public List<MantenimientoAviso> mantenimientos { get; set; }
    }
}
