using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class DañosRepuestos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idDañosRepuestos { get; set; }
        public long idRepuesto { get; set; }
        public DateTime fechaHora { get; set; }
        public long cantidad { get; set; }
        public string motivo { get; set; }
        public long idAlmacen { get; set; }
        public long idUsuario { get; set; }
        public long existenciaActual { get; set; }
    }
}
