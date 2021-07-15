using System;
namespace Wass.Back.Seguridad.Models.Peticiones.Usuario
{
    public class RequestCorreo
    {
        public string destinatario { get; set; }
        public string asunto { get; set; }
        public string contenido { get; set; }
        public string[] conCopia { get; set; }
    }
}
