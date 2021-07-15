using System;
using System.Collections.Generic;
using System.Text;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
    public class AsociarActivosMantenimientoPreventivoRequest
    {
        public List<detalleActivosRequest> activos { get; set; }
    }
    public class detalleActivosRequest
    {
        public string idActivo { get; set; }
        public string llave { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; } // flota o equipos
    }
}
