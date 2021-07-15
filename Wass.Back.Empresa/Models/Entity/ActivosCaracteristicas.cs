using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosCaracteristicas
    {
        [Key]
        public Guid idActivoCaracteristica { get; set; }
        public Guid? idActivoEquipo { get; set; }
        public Guid? idActivoFlota { get; set; }
        public string Caracteristica { get; set; }
        public string Valor { get; set; }
        public bool Eliminado { get; set; }

        [ForeignKey("idActivoEquipo")]
        public ActivosEquipos equipo { get; set; }

        [ForeignKey("idActivoFlota")]
        public ActivosFlotas flota { get; set; }
    }
}
