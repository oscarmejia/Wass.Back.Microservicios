using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Seguridad.Models.Entity
{
    public class UsuariosContacto
    {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long idContacto { get; set; }
		public long idUsuario { get; set; }
		public string nombres { get; set; }
		public string apellidos { get; set; }
		public string tipoDocumento { get; set; }
		public string documento { get; set; }
		public string nombreEmpresa { get; set; }
		public string paginaWebEmpresa { get; set; }
		public string telefonoEmpresa { get; set; }

		[ForeignKey("idUsuario")]
		public Usuarios usuario{ get; set; }
	}
}
