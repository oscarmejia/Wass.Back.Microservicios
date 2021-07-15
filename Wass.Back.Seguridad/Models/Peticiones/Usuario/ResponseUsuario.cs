using System;
using System.Collections.Generic;
using Wass.Back.Seguridad.Models.Entity;

namespace Wass.Back.Seguridad.Models.Peticiones.Usuario
{
    public class ResponseUsuario
    {
        public long idUsuario { get; set; }
        public long idEmpresa { get; set; }
        public long idEmpleado { get; set; }
        public string email { get; set; }
        public string token { get; set; }

        public List<UsuariosRoles> roles { get; set; }
    }
}
