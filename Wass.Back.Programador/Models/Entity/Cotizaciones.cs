using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;

namespace Wass.Back.Programador.Models.Entity
{
    public class Cotizaciones
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCotizacion { get; set; }
        public decimal Costo { get; set; }
        public DateTime fechaPropuestaServicioCotizacion { get; set; }
        public DateTime fechafinalizacionPropuestaServicioCotizacion { get; set; }
        public long idCuadrilla { get; set; }
        public bool eliminada { get; set; }
        public long estado { get; set; }
        public long idLicitacion { get; set; }
        public long idSede { get; set; }
        public long idEmpresa { get; set; }
        public long tipoCotizacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public long idRespuestaSolicitudPedido { get; set; }
        public long IdOrdenPago { get; set; }
        public List<ArchivosAdjuntosCotizacion> ArchivosAdjuntos { get; set; }

        [ForeignKey("idLicitacion")]
        public Licitacion Licitacion { get; set; }


        [ForeignKey("idRespuestaSolicitudPedido")]
        [JsonIgnore]
        public RespuestaSolicitudPedido RespuestaSolicitudPedido { get; set; }



    }
}
