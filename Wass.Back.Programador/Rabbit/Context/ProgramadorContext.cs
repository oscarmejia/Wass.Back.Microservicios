using System;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Programador.Models.Entity;

namespace Wass.Back.Programador.Rabbit.Context
{
    public class ProgramadorContext : DbContext
    {
        public ProgramadorContext(DbContextOptions<ProgramadorContext> options) : base(options)
        {
        }

		public DbSet<OrdenesTrabajo> OrdenesTrabajo { get; set; }
		public DbSet<MantenimientoCorrectivo> MantenimientoCorrectivo { get; set; }
		public DbSet<MantenimientoAviso> MantenimientoAviso { get; set; }
		public DbSet<Agenda> Agenda { get; set; }
		public DbSet<Cotizaciones> Cotizaciones { get; set; }
		public DbSet<Licitacion> Licitacion { get; set; }
		public DbSet<CronogramaLicitacion> CronogramaLicitacion { get; set; }
		public DbSet<SoportesLicitacion> SoportesLicitacion { get; set; }
		public DbSet<SkillLicitacion> SkillLicitacion { get; set; }

		public DbSet<ArchivosAdjuntosCotizacion> ArchivosAdjuntosCotizacion { get; set; }

		public DbSet<Turnos> Turnos { get; set; }
		public DbSet<CuadrillasTurnos> CuadrillasTurnos { get; set; }
		public DbSet<RespuestaCuestionario> RespuestaCuestionario { get; set; }

		public DbSet<MantenimientoRondas> MantenimientoRondas { get; set; }
		public DbSet<PlanesRondas> PlanesRondas { get; set; }
		public DbSet<Grupos> Grupos { get; set; }
		public DbSet<Variables> Variables { get; set; }
		public DbSet<GruposActivos> GruposActivos { get; set; }
		public DbSet<GruposVariables> GruposVariables { get; set; }
		//public DbSet<BusquedaSkillsEmpresaLicitacion> BusquedaSkillsEmpresaLicitacion { get; set; }

		public DbSet<ArchivosAdjuntosOrdenesTrabajo> ArchivosAdjuntosOrdenesTrabajo { get; set; }
		public DbSet<ComentarioActivosClasificacionDiagnosticoAcciones> ComentarioActivosClasificacionDiagnosticoAcciones { get; set; }
		public DbSet<RespuestaSolicitudPedido> RespuestaSolicitudPedido { get; set; }
		public DbSet<PlanesMantenimientoPreventivo> PlanesMantenimientoPreventivo { get; set; }
		public DbSet<PlanMantenimientoPreventivo> PlanMantenimientoPreventivo { get; set; }
		public DbSet<GruposMantenimientoPreventivo> GruposMantenimientoPreventivo { get; set; }

		public DbSet<GruposPartes> GruposPartes { get; set; }
		public DbSet<GruposActivosMantenimientoPreventivo> GruposActivosMantenimientoPreventivo { get; set; }
		public DbSet<GruposAcciones> GruposAcciones { get; set; }
		public DbSet<MantenimientoPreventivo> MantenimientoPreventivo { get; set; }

		public DbSet<CondicionesVariables> CondicionesVariables { get; set; }
		public DbSet<MantenimientoCondiciones> MantenimientoCondiciones { get; set; }
		public DbSet<RespuestaAccionesPlanMantenimientoPreventivo> RespuestaAccionesPlanMantenimientoPreventivo { get; set; }
		public DbSet<RespuestaMantenimientoCorrectivo> RespuestaMantenimientoCorrectivo { get; set; }
		public DbSet<Incidencias> Incidencias { get; set; }
		public DbSet<ArchivosAdjuntosIncidencias> ArchivosAdjuntosIncidencias { get; set; }
		public DbSet<ArchivosAdjuntosLicitacion> ArchivosAdjuntosLicitacion { get; set; }
		public DbSet<RespuestaVariableRonda> RespuestaVariableRonda { get; set; }
	}
}
