using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosParada
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idActivosParada { get; set; }
        public Guid idActivo { get; set; }
        public long idOrden { get; set; }
        public DateTime fechaHoraParada { get; set; }
        public DateTime? fechaHoraReactivacion { get; set; }
        public long tipoOrden { get; set; }
        public long idCuadrilla { get; set; }
        public long idSede { get; set; }
        public long idEmpresa { get; set; }
        public double horasParada { get; set; }
    }
}
