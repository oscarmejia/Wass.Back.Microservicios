using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class CuadrillaEmpleados
    {
        [Key]
        public Guid idEmpleadoCuadrilla { get; set; }
        public long idCuadrilla { get; set; }
        public long idEmpleado { get; set; }
        public bool lider { get; set; }
        public bool estado { get; set; }
        public bool eliminado { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string creador { get; set; }
        public DateTime? fechaEdicion { get; set; }
        public string editor { get; set; }

        [ForeignKey("idCuadrilla")]
        [JsonIgnore]
        public Cuadrillas Cuadrillas { get; set; }

        [ForeignKey("idEmpleado")]
        public Empleados empleado { get; set; }
    }
}
