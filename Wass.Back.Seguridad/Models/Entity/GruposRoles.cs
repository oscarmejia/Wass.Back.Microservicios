using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Seguridad.Models.Entity
{
    public class GruposRoles
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idRol { get; set; }
        public int idGrupo { get; set; }
    }
}
