using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Seguridad.Models.Entity
{
    public class Menus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMenu { get; set; }
        public int? idPadre { get; set; }
        public string descripcion { get; set; }
        public string icon { get; set; }
        public bool activo { get; set; }
        public string ruta { get; set; }
        public string opc1 { get; set; }
        public string opc2 { get; set; }
        public int? opc3 { get; set; }
    }
}
