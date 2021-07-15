using System;
namespace Wass.Back.Empresa.Models.Peticiones.v1.RepuestosAlmacen
{
    public class RepuestosAlmacenRequest
    {
        public Entity.RepuestosAlmacen repuestoAlmacen { get; set; }
        public double promedioUsoDiario { get; set; }
        public double cantidadDiasParaDebajoCantidadMinima { get; set; }
    }
}
