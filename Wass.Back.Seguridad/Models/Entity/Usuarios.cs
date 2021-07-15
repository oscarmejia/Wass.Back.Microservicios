using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Seguridad.Models.Entity
{
    public class Usuarios
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idUsuario { get; set; }
        public long idEmpresa { get; set; }
        public long idEmpleado { get; set; }
        public int idEstado { get; set; }
        public string email { get; set; }
        public string passw { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string creador { get; set; }
        public DateTime fechaEdicion { get; set; }
        public string editor { get; set; }

        public List<UsuariosRoles> usuarioRoles { get; set; }

        public UsuariosContacto usuarioContacto { get; set; }

        [NotMapped]
        public string urlCambioContrasena { get; set; }
        [NotMapped]
        public string urlDatosEmpresa { get; set; }
    }
}
