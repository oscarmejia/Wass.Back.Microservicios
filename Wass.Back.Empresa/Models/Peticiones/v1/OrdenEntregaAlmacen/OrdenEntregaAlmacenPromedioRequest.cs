using System;
using Wass.Back.Empresa.Models.Entity;

namespace Wass.Back.Empresa.Models.Peticiones.v1.OrdenEntregaAlmacen
{
    public class OrdenEntregaAlmacenPromedioRequest
    {
        public long idSede { get; set; }
        public float promedio { get; set; }
        public Repuestos repuesto { get; set; }
    }
    public class OrdenEntregaAlmacenPromedioPorAlmacenRequest
    {
        public long idAlmacen { get; set; }
        public float promedio { get; set; }
        public Repuestos repuesto { get; set; }

    }
}
