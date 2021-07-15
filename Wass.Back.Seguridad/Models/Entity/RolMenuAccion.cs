using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Seguridad.Models.Entity
{
    public class RolMenuAccion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idRol { get; set; }
        public int idMenu { get; set; }
        public int idAccion { get; set; }
    }
}
