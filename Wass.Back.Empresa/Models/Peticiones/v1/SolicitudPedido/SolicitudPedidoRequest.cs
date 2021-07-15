using System;
using System.Collections.Generic;

namespace Wass.Back.Empresa.Models.Peticiones.v1.SolicitudPedido
{
    public class SolicitudPedidoRequest
    {
        public long idSolicitudPedido { get; set; }
        //Estado : Creada, Enviada, Cancelada
        public long estado { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaEnvio { get; set; }
        public DateTime? fechaCancelacion { get; set; }
        public long idSede { get; set; }
        //Nivel de Urgencia: Baja, Media, Alta
        public long nivelUrgencia { get; set; }
        public long idUsuarioCreador { get; set; }
        public string comentario { get; set; }
        public List<DetalleRequest> detalle { get; set; }
    }
    public class DetalleRequest
    {
        public long idRepuesto { get; set; }
        public string nombre { get; set; }
        public string codigoBarras { get; set; }
        public string unidad { get; set; }
        public decimal costoUnitario { get; set; }
        public long cantidadSolicitudRepuesto { get; set; }
    }
}
