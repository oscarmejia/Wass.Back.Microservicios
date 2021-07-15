using System;
using System.Collections.Generic;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;

namespace Wass.Back.Programador.Models.Peticiones.Licitacion
{
    public class LicitacionRequest
    {
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
        public List<long> empresasInvitadas { get; set; }
        public List<long> idSolicitudPedido { get; set; } = new List<long>();
        public long tipoLicitacion { get; set; }

        public OrdenLicitacion OrdenTrabajo { get; set; }

        public List<CronogramaLicitacion> cronograma { get; set; }


        public List<SoportesLicitacion> soportes { get; set; }


        public List<Entity.Cotizaciones> cotizaciones { get; set; }

        public Entity.SkillLicitacion skills { get; set; }
        public List<ArchivosAdjuntosLicitacion> ArchivosAdjuntos { get; set; }
    }

    public class LicitacionSuma
    {
        public int anio { get; set; }
        public List<SumaLicitacionesPorMes> meses { get; set; } = new List<SumaLicitacionesPorMes>();
    }

    public class OrdenLicitacion
    {
        public long tipoLicitacion { get; set; }
        public List<ActivosRequest> nombreActivo { get; set; }
    }

    public class LicitacionSumaUltimoAnio
    {
        public int anio { get; set; }
        public decimal suma { get; set; }
    }
    public class SumaLicitacionesPorMes
    {
        public int mes { get; set; }
        public decimal suma { get; set; }
    }

    public class SumasEnCotizaciones
    {
        public List<decimal> sumasDelMes { get; set; } = new List<decimal>();
    }

    public class ListaAniosSumas
    {
        public int anio { get; set; }
        public List<List<decimal>> datosMes { get; set; } = new List<List<decimal>>();
    }

    public class ActivoEnOrdenTrabajo
    {
        public List<ActivoTipoEnOrdenTrabajo> Activos { get; set; } = new List<ActivoTipoEnOrdenTrabajo>();
    }

    public class ActivoTipoEnOrdenTrabajo
    {
        public string idActivo { get; set; }
        public string tipo { get; set; }
    }

    public class ComprasPorSede
    {
        public long idSede { get; set; }
        public decimal sumaTotal { get; set; }
    }
}
