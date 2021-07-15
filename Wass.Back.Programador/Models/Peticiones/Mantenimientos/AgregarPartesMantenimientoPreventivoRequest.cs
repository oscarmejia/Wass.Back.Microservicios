using System;
using System.Collections.Generic;
using System.Text;

namespace WASS.Back.Programador.core.models.Peticiones.v1.Mantenimientos.Request
{
    public class AgregarPartesMantenimientoPreventivoRequest
    {
        public long idGrupo { get; set; }
        public long idPlan { get; set; }
        public List<Guid> partes { get; set; }
    }
}
