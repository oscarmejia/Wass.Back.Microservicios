using System;
using System.Collections.Generic;

namespace Wass.Back.Programador.Models.Peticiones.RespuestaSolicitudPedido
{
    public class RespuestaSolicitudPedidoRequest
    {
        public long idRespuestaSolicitudPedido { get; set; }
        public long idSolicitudPedido { get; set; }
        public long idCotizacion { get; set; }
        public List<DetalleRequest> detalle { get; set; }


    }
    public class DetalleRequest
    {
        public long idRepuesto { get; set; }
        public long precioUnitario { get; set; }
        public long cantidadSolicitudRepuesto { get; set; }
        public bool adjudicado { get; set; }
    }
}
