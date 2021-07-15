using System;
namespace Wass.Back.Empresa.Models.Peticiones.v1.Empresa
{
    public class RequestSolicitarInfo
    {
        public long idEmpresa { get; set; }
        public string email { get; set; }
        public string Contenido { get; set; }
        public RequestSolicitarInfo()
        {
        }
    }
}
