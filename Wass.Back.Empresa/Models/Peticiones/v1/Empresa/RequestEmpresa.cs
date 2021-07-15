using System;
using Wass.Back.Empresa.Models.Enum;

namespace Wass.Back.Empresa.Models.Peticiones.v1.Empresa
{
    public class RequestEmpresa
    {
        public long idEmpresa { get; set; }
        public TipoAfiliacion tipoAfiliacion { get; set; } = TipoAfiliacion.CLIENTE;
        public int idEstado { get; set; } = (int)EmpresaEstados.Creada;
        public string razonSocial { get; set; }
        public string nit { get; set; }
        public int digVerficacion { get; set; }
        public string aprobador { get; set; }
        public string mensaje { get; set; }
        public bool eliminado { get; set; } = false;
        public string creador { get; set; }
        public DateTime fechaCreacion { get; set; } = DateTime.Now;
        public string editor { get; set; }
        public DateTime fechaEdicion { get; set; } = DateTime.Now;
        //public List<EmpresaChecks> ListaEmpresaChecks { get; set; }
        public string urlLogoEmpresa { get; set; }
    }
}
