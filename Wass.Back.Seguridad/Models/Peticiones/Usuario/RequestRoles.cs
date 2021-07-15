using System;
using System.Collections.Generic;

namespace Wass.Back.Seguridad.Models.Peticiones.Usuario
{
	public class RequestRoles
	{
		public long idUsuario { get; set; }
		public string creador { get; set; }
		public DateTime fechaCreacion { get; set; }
		public List<RequestRol> Roles { get; set; }
	}

	public class RequestRol
	{
		public long idRol { get; set; }
		public bool activo { get; set; }
	}
}
