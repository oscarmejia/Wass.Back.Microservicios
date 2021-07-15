using System;
using System.Collections.Generic;
using System.Text;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
    public class AgregarAccionesMantenimientoPreventivoRequest
    {
        public long idGrupo { get; set; }
        public List<long> acciones { get; set; }
    }
}
