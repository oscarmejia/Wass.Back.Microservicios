using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Calificacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCalificacion { get; set; }
        public long idEmpresa { get; set; }
        public long idSede { get; set; }
        public long idOrdenTrabajo { get; set; }
        public long idProveedor { get; set; }
        public float calificacion { get; set; }
        public string descripcion { get; set; }
        public string motivo { get; set; }
    }
}
