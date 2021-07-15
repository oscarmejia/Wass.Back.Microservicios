using System;
using System.Collections.Generic;

namespace Wass.Back.Programador.Models.Peticiones.RespuestaCuestionario
{
    public class RespuestaCuestionarioRequest
    {
        public long idRespuestaCuestionario { get; set; }
        public long idCuestionario { get; set; }
        public long idCotizacion { get; set; }
        public long idLicitacion { get; set; }
        public List<RespuestaRequest> respuesta { get; set; }
        public bool activo { get; set; }
        public List<OpcionesRequest> opciones { get; set; }

    }

    public class OpcionesRequest
    {
        public string idOpcion { get; set; }
        public string idPregunta { get; set; }
        public string opcion { get; set; }
        public bool chek { get; set; }
        public string tipoRespuesta { get; set; }
    }

    public class RespuestaRequest
    {
        public string idRespuesta { get; set; }
        public string idPregunta { get; set; }
        public string respuesta { get; set; }
        public string tipoRespuesta { get; set; }
    }
}
