using System.Collections.Generic;
using Wass.Back.Programador.Models.Enum;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
    public class BusquedasOrdenesRequest
    {
        public long? idEmpresa { get; set; }
        public long? idSede { get; set; }
        public long? idOrden { get; set; }
        public Servicio servicio { get; set; }
        public long? idProveedorAsignado { get; set; }
        public long? programador { get; set; }
        public long? aprobador { get; set; }
        public List<int> estados { get; set; } = new List<int>();
    }
}
