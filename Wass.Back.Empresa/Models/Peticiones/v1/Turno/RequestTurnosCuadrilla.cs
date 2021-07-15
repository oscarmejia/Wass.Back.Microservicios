using System;
using System.Collections.Generic;

namespace Wass.Back.Empresa.Models.Peticiones.v1.Turno
{
    public class RequestTurnosCuadrilla
    {
       
        public long id { get; set; }
        public long idCuadrilla { get; set; }
        public List<long> idTurnos { get; set; }
    }
}
