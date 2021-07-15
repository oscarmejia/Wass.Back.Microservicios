using System;
using System.Collections.Generic;
using Wass.Back.Empresa.Models.Entity;

namespace Wass.Back.Empresa.Models.Peticiones.v1.Comentario
{
    public class ComentarioRequest
    {
        public long idComentario { get; set; }
        public long idTopicoAComentar { get; set; }
        public long idEmpleado { get; set; }
        public string urlImagen { get; set; }
        public DateTime fechaHoraComentario { get; set; }
        public bool eliminado { get; set; }
        public long like { get; set; }
        public List<string> idUsuariosLike { get; set; }
        public bool replica { get; set; }
        public long? idComentarioPadre { get; set; }
        public string comentario { get; set; }
        public Empleados empleados { get; set; }
    }
}
