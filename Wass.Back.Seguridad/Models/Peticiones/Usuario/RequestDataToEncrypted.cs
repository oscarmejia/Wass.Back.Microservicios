using System;
namespace Wass.Back.Seguridad.Models.Peticiones.Usuario
{
    public class RequestDataToEncrypted
    {
        public long idUsuario { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
