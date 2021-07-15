using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Empresas
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idEmpresa { get; set; }
        public int idEstado { get; set; }
        public string tipoAfiliacion { get; set; }// = TipoAfiliacion.CLIENTE.ToString("G");
        public string razonSocial { get; set; }
        public string nit { get; set; }
        public int digVerficacion { get; set; }
        public string aprobador { get; set; }
        public string mensaje { get; set; }
        public bool eliminado { get; set; }
        public string creador { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string editor { get; set; }
        public DateTime fechaEdicion { get; set; }
        public string urlLogoEmpresa { get; set; }
        public List<Sedes> ListaSedes { get; set; }
        public List<EmpresaSoportes> ListaEmpresaSoportes { get; set; }
        public List<EmpresaChecks> ListaEmpresaChecks { get; set; }
        public List<Certificacion> ListaCertificados { get; set; }
        public Empresas()
        {
        }
    }
}
