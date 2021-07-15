using System;
using System.Collections.Generic;
using Wass.Back.Empresa.Models.Entity;

namespace Wass.Back.Empresa.Models.Peticiones.v1.RecepcionRepuestos
{
    public class RecepcionRepuestosRequest
    {
        public long idRecepcionRepuestos { get; set; }
        public DateTime fechaHora { get; set; }
        public List<RepuestosRecepcionRequest> repuestos { get; set; }
        public long idAlmacen { get; set; }
        public long idUsuario { get; set; }

    }

    public class RepuestosRecepcionRequest
    {
        public string idRepuesto { get; set; }
        public string cantidad { get; set; }
        public string costo { get; set; }
        public long existenciaActual { get; set; }
    }

    public class promedioCostoUnitarioRepuestoRequest
    {
        public Repuestos detalleRepuesto { get; set; }
        public decimal promedioCostoUnitario { get; set; }
    }
}
