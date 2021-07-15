using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Seguridad.Models.Entity
{
    public class Grupos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idGrupo { get; set; }
        public string grupo { get; set; }
        public bool estado { get; set; }
        public string creador { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string editor { get; set; }
        public DateTime fechaEdicion { get; set; }
    }
}
