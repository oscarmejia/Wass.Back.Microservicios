using System;
using System.Collections.Generic;

namespace Wass.Back.Empresa.Models.Peticiones.v1.Preguntas
{
    public class PreguntasRequest
    {
        public long idPregunta { get; set; }
        public string nombre { get; set; }
        public long tipoRespuesta { get; set; }
        public bool activo { get; set; }
        public List<OpcionesRequest> opciones { get; set; }
        public long idEmpresa { get; set; }
        public PreguntasRequest()
        {
        }
    }

    public class OpcionesRequest
    {
        public string idOpcion { get; set; }
        public string opcion { get; set; }
    }
}
