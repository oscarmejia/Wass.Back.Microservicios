using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosAdquisicion
    {
        [Key]
        public Guid idActivosAdquisicion { set; get; }
        public Guid? idActivosEquipos { set; get; }
        public Guid? idActivosFlotas { set; get; }
        public string NumFactura { set; get; }
        public string Lote { set; get; }
        public long idTipoAdquisicion { set; get; }
        public decimal Precio { set; get; }
        public DateTime FechaAdquision { set; get; }
        public DateTime? FechaRetiro { set; get; }
        public decimal PrecioVenta { set; get; }
        public int Garantia { set; get; }
        public string TipoGarantia { set; get; }
        public string Observaciones { set; get; }
        public string Adquisisiones { set; get; }
        public bool Eliminado { set; get; }

        [ForeignKey("idActivosEquipos")]
        public ActivosEquipos equipo { get; set; }

        [ForeignKey("idActivosFlotas")]
        public ActivosFlotas flota { get; set; }
    }
}
