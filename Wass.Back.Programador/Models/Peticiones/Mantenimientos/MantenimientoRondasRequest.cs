using System;
using System.Collections.Generic;
using System.Text;
using Wass.Back.Programador.Models.Entity;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
    public class MantenimientoRondasRequest
    {
		public long idRonda { get; set; }
		public long idOrden { get; set; }
		public long idPlan { get; set; }
		public Guid idActivo { get; set; }
		public long idGrupo { get; set; }
		public DateTime fechaUltimoMantenimientoRondas { get; set; }
		public DateTime fechaPropuestaProgramacion { get; set; }
		public bool estado { get; set; }

		public string observacion { get; set; }

		public List<RespuestaVariableRonda> respuestasVariableRondas { get; set; }
	}
}
