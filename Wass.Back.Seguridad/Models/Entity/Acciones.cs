using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Seguridad.Models.Entity
{
    public class Acciones
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAccion { get; set; }
        public string accion { get; set; }
    }
}
