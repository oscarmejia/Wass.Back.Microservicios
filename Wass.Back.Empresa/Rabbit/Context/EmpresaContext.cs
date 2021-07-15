using System;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Empresa.Models.Entity;

namespace Wass.Back.Empresa.Rabbit.Context
{
    public class EmpresaContext : DbContext
    {

        public EmpresaContext(DbContextOptions<EmpresaContext> options) : base(options)
        {
        }

        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<Sedes> Sedes { get; set; }
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Cuadrillas> Cuadrillas { get; set; }
        public DbSet<Paises> Paises { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<Municipios> Municipios { get; set; }
        public DbSet<Cargos> Cargos { get; set; }
        public DbSet<Listas> Listas { get; set; }
        public DbSet<ListasValores> ListasValores { get; set; }
        public DbSet<EmpresaSoportes> EmpresaSoportes { get; set; }
        public DbSet<CuadrillaEmpleados> CuadrillaEmpleados { get; set; }
        public DbSet<CentrosTrabajo> CentrosTrabajo { get; set; }
        public DbSet<ActivosEquipos> ActivosEquipos { get; set; }
        public DbSet<ActivosAdquisicion> ActivosAdquisicion { get; set; }
        public DbSet<ActivosCaracteristicas> ActivosCaracteristicas { get; set; }
        public DbSet<ActivosUbicacion> ActivosUbicacion { get; set; }
        public DbSet<ActivosFlotas> ActivosFlotas { get; set; }
        public DbSet<ActivosCategorizacion> ActivosCategorizacion { get; set; }
        public DbSet<ActivosPartes> ActivosPartes { get; set; }
        public DbSet<ActivosClasificacion> ActivosClasificacion { get; set; }
        public DbSet<EmpresaSkills> EmpresaSkills { get; set; }
        public DbSet<ActivosClasificacionAcciones> ActivosClasificacionAcciones { get; set; }
        public DbSet<ActivosClasificacionDiagnosticosAcciones> ActivosClasificacionDiagnosticosAcciones { get; set; }
        public DbSet<ActivosClasificacionDiagnosticos> ActivosClasificacionDiagnosticos { get; set; }
        public DbSet<ActivosClasificacionVariables> ActivosClasificacionVariables { get; set; }
        public DbSet<ActivosVariables> ActivosVariables { get; set; }
        public DbSet<ActivosVariablesHistorico> ActivosVariablesHistorico { get; set; }
        public DbSet<ActivosClasificacionDiagnosticosSkills> ActivosClasificacionDiagnosticosSkills { get; set; }
        public DbSet<Turnos> Turnos { get; set; }
        public DbSet<CuadrillasTurnos> CuadrillasTurnos { get; set; }
        public DbSet<EmpresaChecks> EmpresaChecks { get; set; }
        public DbSet<Recomendaciones> Recomendaciones { get; set; }
        public DbSet<Certificacion> Certificaciones { get; set; }
        public DbSet<Herramientas> Herramientas { get; set; }
        public DbSet<Preguntas> Preguntas { get; set; }
        public DbSet<Cuestionario> Cuestionario { get; set; }
        public DbSet<CuestionarioPreguntas> CuestionarioPreguntas { get; set; }

        public DbSet<ArchivosAdjuntosActivosEquipos> ArchivosAdjuntosActivosEquipos { get; set; }
        public DbSet<ArchivosAdjuntosActivosFlotas> ArchivosAdjuntosActivosFlotas { get; set; }
        public DbSet<RespuestaActivosVariables> RespuestaActivosVariables { get; set; }
        public DbSet<TopicoAComentar> TopicoAComentar { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Calificacion> Calificacion { get; set; }
        public DbSet<Almacen> Almacen { get; set; }
        public DbSet<Repuestos> Repuestos { get; set; }
        public DbSet<RepuestosAlmacen> RepuestosAlmacen { get; set; }
        public DbSet<AjustesAlmacenes> AjustesAlmacenes { get; set; }
        public DbSet<DañosRepuestos> DañosRepuestos { get; set; }
        public DbSet<OrdenEntregaAlmacen> OrdenEntregaAlmacen { get; set; }
        public DbSet<RecepcionRepuestos> RecepcionRepuestos { get; set; }
        public DbSet<TransferenciasInternasAlmacenes> TransferenciasInternasAlmacenes { get; set; }
        public DbSet<Conversacion> Conversacion { get; set; }
        public DbSet<KardexRepuesto> ConsultaFechasInventarios { get; set; }
        public DbSet<RepuestosDiagnostico> RepuestosDiagnostico { get; set; }
        public DbSet<CentroCosto> CentroCosto { get; set; }
        public DbSet<SolicitudPedido> SolicitudPedido { get; set; }
        public DbSet<ActivosParada> ActivosParada { get; set; }
        public DbSet<CuadrillaSkillsEmpresa> CuadrillaSkillsEmpresa { get; set; }
        public DbSet<DiagnosticoSkillsEmpresa> DiagnosticoSkillsEmpresa { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<MarcaEmpresa> MarcaEmpresa { get; set; }

        public DbSet<MensajesConversacion> MensajesConversacion { get; set; }
        public DbSet<TipoActivo> TipoActivo { get; set; }
        public DbSet<MarcaActivo> MarcaActivo { get; set; }
        public DbSet<RepuestosGrupoPartes> RepuestosGrupoPartes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CuadrillaSkill> CuadrillaSkill { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivosEquipos>().HasOne(x => x.adquisicion).WithOne(x => x.equipo); ;
            modelBuilder.Entity<ActivosEquipos>().HasMany(x => x.caracteristicas).WithOne(x => x.equipo);
            modelBuilder.Entity<ActivosEquipos>().HasOne(x => x.ubicacion).WithOne(x => x.equipo);
            modelBuilder.Entity<ActivosFlotas>().HasOne(x => x.adquisicion).WithOne(x => x.flota);
            modelBuilder.Entity<ActivosFlotas>().HasMany(x => x.caracteristicas).WithOne(x => x.flota);
            modelBuilder.Entity<ActivosFlotas>().HasOne(x => x.ubicacion).WithOne(x => x.flota);

            //modelBuilder.Entity<Sedes>().HasMany(x => x.empleados).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
