using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Conversacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idConversacion { get; set; }
        public long usuario1 { get; set; }
        public long usuario2 { get; set; }
        public int noLeido { get; set; }
        public bool eliminado { get; set; }
        public List<MensajesConversacion> Mensajes { get; set; }
    }
}
