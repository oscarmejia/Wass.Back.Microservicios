using System;
using System.Collections.Generic;

namespace Wass.Back.Empresa.Models.Peticiones.v1.TransferenciasInternasAlmacenes
{
    public class TransferenciasInternasAlmacenesRequest
    {
        public long idTransferenciasInternasAlmacenes { get; set; }
        public long idAlmacenEmisor { get; set; }
        public long idAlmacenReceptor { get; set; }
        public List<RepuestosRequest> repuestos { get; set; }
        public DateTime fechaHora { get; set; }
        public long motivoTransferencia { get; set; }
        public OrdenTrabajoRequest ordenTrabajo { get; set; }
        public long idEmpresa { get; set; }
        public long estado { get; set; }
    }
    public class OrdenTrabajoRequest
    {
        public string idOrdenTrabajo { get; set; }
        public string idUsuario { get; set; }
    }
    public class RepuestosRequest
    {
        public string idRepuesto { get; set; }
        public string cantidad { get; set; }
        public long existenciaActual { get; set; }
    }
}
