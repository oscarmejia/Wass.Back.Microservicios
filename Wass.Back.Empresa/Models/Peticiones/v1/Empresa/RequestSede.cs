using System;
namespace Wass.Back.Empresa.Models.Peticiones.v1.Empresa
{
    public class RequestSede
    {
        public long idSede { get; set; }
        public long idEmpresa { get; set; }
        public int idMunicipio { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public bool estado { get; set; }
        public bool eliminado { get; set; }
        public string creador { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string editor { get; set; }
        public DateTime fechaEdicion { get; set; }
        public long idCentroCosto { get; set; }
        public string personaContacto { get; set; }
        public string telefono { get; set; }
    }
}
