using System;
namespace Wass.Back.Seguridad.Models.Peticiones.Usuario
{
    public class RequestMenus
    {
        public int idMenu { get; set; }
        public int? idPadre { get; set; }
        public string descripcion { get; set; }
        public string icon { get; set; }
        public bool activo { get; set; }
        public string ruta { get; set; }
    }
}
