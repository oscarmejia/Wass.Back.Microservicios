using System;
namespace Wass.Back.Empresa.Models.Peticiones.v1.KardexRepuesto
{
    public class RecepcionRequest
    {
        public long idRecepcionRepuestos { get; set; }
        public DateTime fechaHora { get; set; }
        public string repuestos { get; set; }
        public long idAlmacen { get; set; }
        public long idUsuario { get; set; }
        public long tipoOperacion { get; set; }
    }

    public class AjustesRequest
    {
        public long idAjustesAlmacenes { get; set; }
        public long idRepuesto { get; set; }
        public long cantidadAnterior { get; set; }
        public long cantidadNueva { get; set; }
        public DateTime fechaHora { get; set; }
        public long idAlmacen { get; set; }
        public string motivo { get; set; }
        public long idUsuario { get; set; }
        public long tipoOperacion { get; set; }
        public long existenciaActual { get; set; }
    }

    public class DañosRequest
    {
        public long idDañosRepuestos { get; set; }
        public long idRepuesto { get; set; }
        public DateTime fechaHora { get; set; }
        public long cantidad { get; set; }
        public string motivo { get; set; }
        public long idAlmacen { get; set; }
        public long idUsuario { get; set; }
        public long tipoOperacion { get; set; }
        public long existenciaActual { get; set; }
    }

    public class TransferenciasRequest
    {
        public long idTransferenciasInternasAlmacenes { get; set; }
        public long idAlmacenEmisor { get; set; }
        public long idAlmacenReceptor { get; set; }
        public string repuestos { get; set; }
        public DateTime fechaHora { get; set; }
        public long motivoTransferencia { get; set; }
        public string ordenTrabajo { get; set; }
        public long tipoOperacion { get; set; }
        public long estado { get; set; }
    }

    public class EntregaRequest
    {
        public long idOrdenEntregaAlmacen { get; set; }
        public string repuestos { get; set; }
        public long idOrdenTrabajo { get; set; }
        public DateTime fechaHora { get; set; }
        public long idAlmacen { get; set; }
        public long idCuadrilla { get; set; }
        public long idSede { get; set; }
        public long tipoOperacion { get; set; }
    }
}
