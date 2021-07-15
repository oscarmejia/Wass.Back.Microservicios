using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Correo;

namespace Wass.Back.Empresa.Kiwi.Interface
{
    public interface IBOUtilityConrreoElectronico
    {
        ResponseBase<RequestCorreo> DeserializeSolicitud(string json);
        Task<ResponseBase<bool>> EnviarCorreo(RequestCorreo correo, List<IFormFile> adjuntos);
        Task<List<(Stream file, string type, string name)>> ConvertirAdjuntos(List<IFormFile> files);
    }
}
