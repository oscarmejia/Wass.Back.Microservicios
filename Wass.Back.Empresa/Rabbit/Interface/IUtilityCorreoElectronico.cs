using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Peticiones.v1.Correo;

namespace Wass.Back.Empresa.Rabbit.Interface
{
    public interface IUtilityCorreoElectronico
    {
        Task<(bool, string)> EnviarCorreo(RequestCorreo correo, List<(Stream archivo, string tipo, string nombre)> adjuntos = null);
    }
}
