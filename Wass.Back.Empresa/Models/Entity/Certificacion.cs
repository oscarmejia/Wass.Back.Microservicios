using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Certificacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCertificado { get; set; }
        public string nombre { get; set; }
        public string organizacion { get; set; }
        public bool expira { get; set; }
        public DateTime fechaEmision { get; set; }
        public DateTime? fechaExpiracion { get; set; }
        public string idCredencial { get; set; }
        public string urlCredencial { get; set; }
        public bool eliminado { get; set; }
        public long idEmpresa { get; set; }
        public long idEmpleado { get; set; }


        [ForeignKey("idEmpresa")]
        [JsonIgnore]
        public Empresas empresas { get; set; }

        [ForeignKey("idEmpleado")]
        [JsonIgnore]
        public Empleados empleados { get; set; }
    }
}
