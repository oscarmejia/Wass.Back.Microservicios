using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosClasificacionDiagnosticosAcciones
    {
        [Key]
        public long idDiagnosticosAcciones { get; set; }
        public long idDiagnostico { get; set; }
        public long idAccion { get; set; }
        public bool eliminado { get; set; }

        [ForeignKey("idDiagnostico")]
        [JsonIgnore]
        public ActivosClasificacionDiagnosticos ActivosClasificacionDiagnosticos { get; set; }

        [ForeignKey("idAccion")]
        //[JsonIgnore]
        public ActivosClasificacionAcciones ActivosClasificacionAcciones { get; set; }
    }
}
