using System;
using System.Collections.Generic;
using System.Text;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
    public class GruposActivosRequest
    {
        public long id { get; set; }
        public detalleActivosRequest Activo { get; set; }
        public long idPlan { get; set; }
        public bool estado { get; set; }
    }
}
