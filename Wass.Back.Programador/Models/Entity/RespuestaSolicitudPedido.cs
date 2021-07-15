using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class RespuestaSolicitudPedido
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idRespuestaSolicitudPedido { get; set; }
        public long idSolicitudPedido { get; set; }
        public long idCotizacion { get; set; }
        public string detalle { get; set; }

        [ForeignKey("idCotizacion")]
        [JsonIgnore]
        public Cotizaciones Cotizacion { get; set; }
    }
}
