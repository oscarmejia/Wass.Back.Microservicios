using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosUbicacion
    {
        [Key]
        public Guid idUbicacion { get; set; }
        public Guid? idActivosEquipos { get; set; }
        public Guid? idActivoFlota { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public long idTipoUbicacion { get; set; }
        public long? idCentroTrabajo { get; set; }
        public string direccion { get; set; }
        public bool eliminado { get; set; }
        public DateTime fechaCreacion { get; set; }


        [ForeignKey("idActivosEquipos")]
        [NotMapped]
        public ActivosEquipos equipo { get; set; }

        [ForeignKey("idActivoFlota")]
        [NotMapped]
        public ActivosFlotas flota { get; set; }

    }
}
