using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Seguridad.Models.Entity
{
    public class UsuariosRoles
    {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long idUsuarioRol { get; set; }
		public long idUsuario { get; set; }
		public long idRol { get; set; }
		public DateTime fechaCreacion { get; set; }
		public string creador { get; set; }
		public bool activo { get; set; }

		[ForeignKey("idUsuario")]
		[JsonIgnore]
		public Usuarios usuarioRoles { get; set; }
	}
}
