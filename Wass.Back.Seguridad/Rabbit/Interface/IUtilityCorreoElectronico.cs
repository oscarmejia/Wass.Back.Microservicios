using System;
using System.Threading.Tasks;
using Wass.Back.Seguridad.Models.Peticiones.Usuario;

namespace Wass.Back.Seguridad.Rabbit.Interface
{
    public interface IUtilityCorreoElectronico
    {
        Task<(bool estado, string msgError)> EnviarCorreo(RequestCorreo correo);
    }
}
