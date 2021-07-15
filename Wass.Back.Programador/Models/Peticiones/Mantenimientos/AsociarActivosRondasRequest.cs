using System;
using System.Collections.Generic;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
	public class AsociarActivosRondasRequest
	{
		public List<long> idGrupo { get; set; }
		public List<Guid> activos { get; set; }
	}
}
