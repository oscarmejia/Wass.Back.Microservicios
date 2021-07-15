using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class SolicitudPedido
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idSolicitudPedido { get; set; }
        //Estado : Creada, Enviada, Cancelada
        public long estado { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaEnvio { get; set; }
        public DateTime? fechaCancelacion { get; set; }
        public long idSede { get; set; }
        //Nivel de Urgencia: Baja, Media, Alta
        public long nivelUrgencia { get; set; }
        public long idUsuarioCreador { get; set; }
        public string comentario { get; set; }
        public long idLicitacion { get; set; }
        public string detalle { get; set; }
    }
}
