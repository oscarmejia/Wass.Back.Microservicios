using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class MensajesConversacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idMensajesConversacion { get; set; }
        public long idConversacion { get; set; }
        public long idSender { get; set; }
        public DateTime FechaHoraMensaje { get; set; }
        public bool eliminado { get; set; }
        public string mensaje { get; set; }

        [ForeignKey("idConversacion")]
        [JsonIgnore]
        public Conversacion Conversacion { get; set; }
    }
}
