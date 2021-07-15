using System;
namespace Wass.Back.Empresa.Models.Peticiones.v1.Correo
{
    public class RequestCorreo
    {
        public string destinatario { get; set; }
        public string asunto { get; set; }
        public string contenido { get; set; }
        public string[] conCopia { get; set; }
    }
}
