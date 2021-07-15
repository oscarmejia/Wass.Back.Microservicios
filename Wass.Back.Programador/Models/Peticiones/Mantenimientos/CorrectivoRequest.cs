using System.Collections.Generic;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
    public class CorrectivoRequest
    {
        public long idRespuestaMantenimientoCorrectivo { get; set; }
        public long idMantenimientoCorrectivo { get; set; }
        public long? idDiagnostico { get; set; }
        public List<RespuestaCorrectivoRequest> respuesta { get; set; }
    }

    public class RespuestaCorrectivoRequest
    {
        public string idAccion { get; set; }
        public string respuesta { get; set; }
    }
}
