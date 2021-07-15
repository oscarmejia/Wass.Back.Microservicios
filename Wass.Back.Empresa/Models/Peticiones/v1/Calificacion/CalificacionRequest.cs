using System;
namespace Wass.Back.Empresa.Models.Peticiones.v1.Calificacion
{
    public class CalificacionRequest
    {
        public long idCalificacion { get; set; }
        public long idEmpresa { get; set; }
        public long idSede { get; set; }
        public long idOrdenTrabajo { get; set; }
        public long idProveedor { get; set; }
        public float calificacion { get; set; }
        public string descripcion { get; set; }
        public MotivoRequest motivo { get; set; }
    }
    public class MotivoRequest
    {
        public string idMotivo { get; set; }
        public string motivo { get; set; }
    }
}
