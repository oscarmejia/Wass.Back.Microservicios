using System;
using System.Collections.Generic;

namespace Wass.Back.Programador.Models.Peticiones.Cotizaciones
{
    public class CotizacionesResponse
    {
        public int anio { get; set; }
        public List<SumaCotizacionesPorMes> meses { get; set; } = new List<SumaCotizacionesPorMes>();
    }
    public class SumaCotizacionesPorMes
    {
        public int mes { get; set; }
        public decimal suma { get; set; }
    }

    public class CotizacionesUltimoAnioResponse
    {
        public int anio { get; set; }
        public decimal suma { get; set; }
    }
}
