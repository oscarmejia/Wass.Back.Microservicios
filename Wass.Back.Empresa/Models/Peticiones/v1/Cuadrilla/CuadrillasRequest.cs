using System;
using System.Collections.Generic;
using Wass.Back.Empresa.Models.Entity;

namespace Wass.Back.Empresa.Models.Peticiones.v1.Cuadrilla
{
    public class CuadrillasRequest
    {

        public long idCuadrilla { get; set; }
        public long idSede { get; set; }
        public string nombreA { get; set; }
        public string nombreB { get; set; }
        public string email { get; set; }
        public int estado { get; set; }
        public UbicacionRequest zonaAtencion { get; set; }
        public UbicacionRequest ubicacionActual { get; set; }
        public string celular { get; set; }
        public int numMiembros { get; set; }

        public List<CuadrillaEmpleados> cuadrillaEmpleados { get; set; }
        public List<CuadrillasTurnos> cuadrillaTurnos { get; set; }

        public CuadrillasRequest()
        {
        }
    }

    public class UbicacionRequest
    {
        public string nombre { get; set; }
        public long latitud { get; set; }
        public long longitud { get; set; }
    }
}
