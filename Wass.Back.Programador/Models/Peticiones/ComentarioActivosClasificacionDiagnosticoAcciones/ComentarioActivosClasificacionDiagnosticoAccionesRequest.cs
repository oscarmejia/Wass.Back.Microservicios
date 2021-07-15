using System;
using System.Collections.Generic;

namespace Wass.Back.Programador.Models.Peticiones.ComentarioActivosClasificacionDiagnosticoAcciones
{
    public class ComentarioActivosClasificacionDiagnosticoAccionesRequest
    {
        public long idComentarioDiagnosticosAcciones { get; set; }
        public long idClasificacion { get; set; }
        public long idDiagnostico { get; set; }
        public long idMantenimientoCorrectivo { get; set; }
        public long idAviso { get; set; }

        public List<ComentarioAccionRequest> comentario { get; set; }
    }

    public class ComentarioAccionRequest
    {
        public string idcomentario { get; set; }
        public string idAccion { get; set; }
        public string comentario { get; set; }
        public string urlImagen { get; set; }
    }
}
