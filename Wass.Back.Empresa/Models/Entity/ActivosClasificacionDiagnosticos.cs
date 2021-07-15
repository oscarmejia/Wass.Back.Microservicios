using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosClasificacionDiagnosticos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idDiagnostico { get; set; }
        public long idClasificacion { get; set; }
        public string diagnostico { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
        public bool eliminado { get; set; }
        public bool parada { get; set; }
        public List<ActivosClasificacionDiagnosticosAcciones> ActivosClasificacionDiagnosticosAcciones { get; set; }
        public List<RepuestosDiagnostico> RepuestosDiagnostico { get; set; }
        public List<DiagnosticoSkillsEmpresa> DiagnosticoSkillsEmpresa { get; set; }
    }
}
