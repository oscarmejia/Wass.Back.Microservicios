using System;
using System.Collections.Generic;

namespace Wass.Back.Empresa.Models.Peticiones.v1.RespuestaActivosVariables
{
    public class RespuestaActivosVariablesResponse
    {
        public long idRespuestaActivosVariables { get; set; }
        public long idActivoVariable { get; set; }
        public long idClasificacion { get; set; }
        public long idCategorizacion { get; set; }
        public Guid? idActivoFlota { get; set; }
        public Guid? idActivoEquipo { get; set; }
        public List<RespuestaResponse> respuesta { get; set; }
    }

    public class RespuestaResponse
    {
        public string idRespuesta { get; set; }
        public string idActivoVariable { get; set; }
        public string respuesta { get; set; }
    }
}
