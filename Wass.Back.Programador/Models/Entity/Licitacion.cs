using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class Licitacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idLicitacion { get; set; }
        public DateTime fechaLimiteRepCotizacion { get; set; }
        public long urgencia { get; set; }
        public long moneda { get; set; }
        public string observaciones { get; set; }
        public long idOrden { get; set; }
        public long idSede { get; set; }
        public long idEmpresa { get; set; }
        public long idCuestionario { get; set; }
        public long estado { get; set; }
        public string idSolicitudPedido { get; set; }
        public long tipoLicitacion { get; set; }
        public string empresasInvitadas { get; set; }

        [ForeignKey("idOrden")]
        public OrdenesTrabajo OrdenTrabajo { get; set; }


        public List<CronogramaLicitacion> cronograma { get; set; }


        public List<SoportesLicitacion> soportes { get; set; }


        public List<Cotizaciones> cotizaciones { get; set; }

        public SkillLicitacion skills { get; set; }

        

        public List<ArchivosAdjuntosLicitacion> ArchivosAdjuntos { get; set; }


        public Licitacion()
        {
        }
    }
}
