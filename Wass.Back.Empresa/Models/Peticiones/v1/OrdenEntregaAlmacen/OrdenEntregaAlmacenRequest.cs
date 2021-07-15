using System;
using System.Collections.Generic;

namespace Wass.Back.Empresa.Models.Peticiones.v1.OrdenEntregaAlmacen
{
    public class OrdenEntregaAlmacenRequest
    {
        public long idOrdenEntregaAlmacen { get; set; }
        public List<RepuestosOrdenEntregaRequest> repuestos { get; set; }
        public long idOrdenTrabajo { get; set; }
        public DateTime fechaHora { get; set; }
        public long idAlmacen { get; set; }
        public long idCuadrilla { get; set; }
        public long idSede { get; set; }
    }

    public class RepuestosOrdenEntregaRequest
    {
        public string idRepuesto { get; set; }
        public string cantidad { get; set; }
        public long existenciaActual { get; set; }
        public long idAccion { get; set; }
    }
}
