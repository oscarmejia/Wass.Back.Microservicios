using System;
namespace Wass.Back.Seguridad.Models.Peticiones.Usuario
{
    public class RequestContrasena
    {
        public long idUsuario { get; set; }
        public string contrasenaAnterior { get; set; }
        public string contrasenaNueva { get; set; }
    }
}
