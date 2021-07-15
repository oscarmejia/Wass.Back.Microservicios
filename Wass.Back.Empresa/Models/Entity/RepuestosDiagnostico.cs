using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class RepuestosDiagnostico
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idRepuestosDiagnostico { get; set; }
        public long idRepuestos { get; set; }
        public long idDiagnostico { get; set; }
        public bool eliminado { get; set; }
        public long cantidad { get; set; }

        [ForeignKey("idDiagnostico")]
        //[JsonIgnore]
        public ActivosClasificacionDiagnosticos ActivosClasificacionDiagnosticos { get; set; }

        [ForeignKey("idRepuestos")]
        //[JsonIgnore]
        public Repuestos Repuestos { get; set; }
    }
}
