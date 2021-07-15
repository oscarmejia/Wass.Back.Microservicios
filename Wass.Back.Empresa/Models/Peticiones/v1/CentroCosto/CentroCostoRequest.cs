using System;
using System.Collections.Generic;
using Wass.Back.Empresa.Models.Entity;

namespace Wass.Back.Empresa.Models.Peticiones.v1.CentroCosto
{
    public class CentroCostoRequest
    {
        public long idCentroCosto { get; set; }
        public CentroCostoPadreRequest idCentroCostoPadre { get; set; }
        public long idEmpresa { get; set; }
        public string nombre { get; set; }
        public bool eliminado { get; set; }
        public List<Sedes> Sedes { get; set; }
    }

    public class CentroCostoPadreRequest
    {
        public long idCentroCosto { get; set; }
        public string idCentroCostoPadre { get; set; }
        public long idEmpresa { get; set; }
        public string nombre { get; set; }
        public bool eliminado { get; set; }
        public List<Sedes> Sedes { get; set; }
    }
}
