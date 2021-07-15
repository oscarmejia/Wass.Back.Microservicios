using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class AjustesAlmacenes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idAjustesAlmacenes { get; set; }
        public long idRepuesto { get; set; }
        public long cantidadAnterior { get; set; }
        public long cantidadNueva { get; set; }
        public DateTime fechaHora { get; set; }
        public long idAlmacen { get; set; }
        public string motivo { get; set; }
        public long idUsuario { get; set; }
        public long existenciaActual { get; set; }
    }
}
