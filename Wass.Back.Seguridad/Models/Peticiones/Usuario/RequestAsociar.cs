using System;
namespace Wass.Back.Seguridad.Models.Peticiones.Usuario
{
    public class RequestAsociar
    {
        public long idUsuario { get; set; }
        public long idEmpresa { get; set; }
        public long idEmpleado { get; set; }
        public int idEstado { get; set; }
    }
}
