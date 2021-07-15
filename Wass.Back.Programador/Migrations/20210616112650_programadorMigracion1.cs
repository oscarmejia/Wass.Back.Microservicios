using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wass.Back.Programador.Migrations
{
    public partial class programadorMigracion1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    idAgenda = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRecurso = table.Column<long>(type: "bigint", nullable: false),
                    idOrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    horaInicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    horaFin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    tipoRecurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.idAgenda);
                });

            migrationBuilder.CreateTable(
                name: "ComentarioActivosClasificacionDiagnosticoAcciones",
                columns: table => new
                {
                    idComentarioDiagnosticosAcciones = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idClasificacion = table.Column<long>(type: "bigint", nullable: false),
                    idDiagnostico = table.Column<long>(type: "bigint", nullable: false),
                    idMantenimientoCorrectivo = table.Column<long>(type: "bigint", nullable: true),
                    idAviso = table.Column<long>(type: "bigint", nullable: true),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioActivosClasificacionDiagnosticoAcciones", x => x.idComentarioDiagnosticosAcciones);
                });

            migrationBuilder.CreateTable(
                name: "CondicionesVariables",
                columns: table => new
                {
                    idCondicionesVariables = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    idVariable = table.Column<long>(type: "bigint", nullable: false),
                    comparadorCondicion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valorCondicion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accionQueRealiza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicionesVariables", x => x.idCondicionesVariables);
                });

            migrationBuilder.CreateTable(
                name: "CuadrillasTurnos",
                columns: table => new
                {
                    idCuadrillasTurnos = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idTurno = table.Column<long>(type: "bigint", nullable: false),
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuadrillasTurnos", x => x.idCuadrillasTurnos);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    idGrupo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    periodo = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.idGrupo);
                });

            migrationBuilder.CreateTable(
                name: "GruposActivos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idActivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposActivos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GruposActivosMantenimientoPreventivo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposActivosMantenimientoPreventivo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GruposMantenimientoPreventivo",
                columns: table => new
                {
                    idGrupo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    periodo = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    parada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposMantenimientoPreventivo", x => x.idGrupo);
                });

            migrationBuilder.CreateTable(
                name: "GruposPartes",
                columns: table => new
                {
                    idGruposPartes = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idGrupo = table.Column<long>(type: "bigint", nullable: false),
                    idParte = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    idRepuesto = table.Column<long>(type: "bigint", nullable: false),
                    periodo = table.Column<long>(type: "bigint", nullable: false),
                    parada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposPartes", x => x.idGruposPartes);
                });

            migrationBuilder.CreateTable(
                name: "GruposVariables",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idGrupo = table.Column<long>(type: "bigint", nullable: false),
                    idVariable = table.Column<long>(type: "bigint", nullable: false),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    valor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    periodo = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposVariables", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MantenimientoCondiciones",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCondicion = table.Column<long>(type: "bigint", nullable: false),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    idActivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idVariable = table.Column<long>(type: "bigint", nullable: false),
                    acciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantenimientoCondiciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MantenimientoCorrectivo",
                columns: table => new
                {
                    idMantenimientoCorrectivo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOrden = table.Column<long>(type: "bigint", nullable: false),
                    idOrdenAviso = table.Column<long>(type: "bigint", nullable: false),
                    idDiagnostico = table.Column<long>(type: "bigint", nullable: true),
                    falla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    acciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantenimientoCorrectivo", x => x.idMantenimientoCorrectivo);
                });

            migrationBuilder.CreateTable(
                name: "PlanesMantenimientoPreventivo",
                columns: table => new
                {
                    idPlan = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCategoria = table.Column<long>(type: "bigint", nullable: false),
                    idClasificacion1 = table.Column<long>(type: "bigint", nullable: true),
                    idClasificacion2 = table.Column<long>(type: "bigint", nullable: true),
                    prioridad = table.Column<int>(type: "int", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesMantenimientoPreventivo", x => x.idPlan);
                });

            migrationBuilder.CreateTable(
                name: "PlanesRondas",
                columns: table => new
                {
                    idPlan = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoPlan = table.Column<long>(type: "bigint", nullable: false),
                    idCategoria = table.Column<long>(type: "bigint", nullable: false),
                    idClasificacion1 = table.Column<long>(type: "bigint", nullable: false),
                    idClasificacion2 = table.Column<long>(type: "bigint", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    marca = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesRondas", x => x.idPlan);
                });

            migrationBuilder.CreateTable(
                name: "PlanMantenimientoPreventivo",
                columns: table => new
                {
                    idPlanMantenimientoPreventivo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    idActivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idGrupo = table.Column<long>(type: "bigint", nullable: false),
                    fechaUltimoMantenimientoPreventivo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanMantenimientoPreventivo", x => x.idPlanMantenimientoPreventivo);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaAccionesPlanMantenimientoPreventivo",
                columns: table => new
                {
                    idRespuesta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idParte = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idAccion = table.Column<long>(type: "bigint", nullable: false),
                    idMantenimientoPreventivo = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    respuesta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaAccionesPlanMantenimientoPreventivo", x => x.idRespuesta);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaCuestionario",
                columns: table => new
                {
                    idRespuestaCuestionario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCuestionario = table.Column<long>(type: "bigint", nullable: false),
                    idCotizacion = table.Column<long>(type: "bigint", nullable: false),
                    idLicitacion = table.Column<long>(type: "bigint", nullable: false),
                    respuesta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    opciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaCuestionario", x => x.idRespuestaCuestionario);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaMantenimientoCorrectivo",
                columns: table => new
                {
                    idRespuestaMantenimientoCorrectivo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMantenimientoCorrectivo = table.Column<long>(type: "bigint", nullable: false),
                    idDiagnostico = table.Column<long>(type: "bigint", nullable: true),
                    respuesta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaMantenimientoCorrectivo", x => x.idRespuestaMantenimientoCorrectivo);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaSolicitudPedido",
                columns: table => new
                {
                    idRespuestaSolicitudPedido = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSolicitudPedido = table.Column<long>(type: "bigint", nullable: false),
                    idCotizacion = table.Column<long>(type: "bigint", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaSolicitudPedido", x => x.idRespuestaSolicitudPedido);
                });

            migrationBuilder.CreateTable(
                name: "SkillLicitacion",
                columns: table => new
                {
                    idSkillLicitacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idLicitacion = table.Column<long>(type: "bigint", nullable: false),
                    skills = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLicitacion", x => x.idSkillLicitacion);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    idTurno = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diaInicial = table.Column<int>(type: "int", nullable: false),
                    diaFinal = table.Column<int>(type: "int", nullable: false),
                    horaInicial = table.Column<TimeSpan>(type: "time", nullable: false),
                    horaFinal = table.Column<TimeSpan>(type: "time", nullable: false),
                    creador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.idTurno);
                });

            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    idVarible = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idClasificacion = table.Column<long>(type: "bigint", nullable: false),
                    idUnidadMedida = table.Column<long>(type: "bigint", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.idVarible);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesTrabajo",
                columns: table => new
                {
                    idOrden = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    idServicio = table.Column<int>(type: "int", nullable: false),
                    prioridad = table.Column<int>(type: "int", nullable: false),
                    aprobador = table.Column<long>(type: "bigint", nullable: true),
                    programador = table.Column<long>(type: "bigint", nullable: true),
                    idProveedorAsignado = table.Column<long>(type: "bigint", nullable: true),
                    idEstadoOrden = table.Column<int>(type: "int", nullable: false),
                    fechaProgramacionInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaProgramacionCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    creador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivoAnulacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    eliminada = table.Column<bool>(type: "bit", nullable: false),
                    fechaLimiteServicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    variableDecision = table.Column<long>(type: "bigint", nullable: true),
                    nivelUrgencia = table.Column<long>(type: "bigint", nullable: true),
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: true),
                    fechaAtencion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    facturada = table.Column<bool>(type: "bit", nullable: false),
                    datosActivos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mantenimientoCorrectivoidMantenimientoCorrectivo = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesTrabajo", x => x.idOrden);
                    table.ForeignKey(
                        name: "FK_OrdenesTrabajo_MantenimientoCorrectivo_mantenimientoCorrectivoidMantenimientoCorrectivo",
                        column: x => x.mantenimientoCorrectivoidMantenimientoCorrectivo,
                        principalTable: "MantenimientoCorrectivo",
                        principalColumn: "idMantenimientoCorrectivo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GruposAcciones",
                columns: table => new
                {
                    idGruposAcciones = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idGrupo = table.Column<long>(type: "bigint", nullable: false),
                    idAccion = table.Column<long>(type: "bigint", nullable: false),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposAcciones", x => x.idGruposAcciones);
                    table.ForeignKey(
                        name: "FK_GruposAcciones_PlanesMantenimientoPreventivo_idPlan",
                        column: x => x.idPlan,
                        principalTable: "PlanesMantenimientoPreventivo",
                        principalColumn: "idPlan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosAdjuntosOrdenesTrabajo",
                columns: table => new
                {
                    idArchivosAdjuntosOrdenesTrabajo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlArchivosAdjuntosOrdenesTrabajo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idOrdenesTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAdjuntosOrdenesTrabajo", x => x.idArchivosAdjuntosOrdenesTrabajo);
                    table.ForeignKey(
                        name: "FK_ArchivosAdjuntosOrdenesTrabajo_OrdenesTrabajo_idOrdenesTrabajo",
                        column: x => x.idOrdenesTrabajo,
                        principalTable: "OrdenesTrabajo",
                        principalColumn: "idOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    idIncidencias = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOrden = table.Column<long>(type: "bigint", nullable: false),
                    fechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.idIncidencias);
                    table.ForeignKey(
                        name: "FK_Incidencias_OrdenesTrabajo_idOrden",
                        column: x => x.idOrden,
                        principalTable: "OrdenesTrabajo",
                        principalColumn: "idOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Licitacion",
                columns: table => new
                {
                    idLicitacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaLimiteRepCotizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    urgencia = table.Column<long>(type: "bigint", nullable: false),
                    moneda = table.Column<long>(type: "bigint", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idOrden = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idCuestionario = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<long>(type: "bigint", nullable: false),
                    idSolicitudPedido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoLicitacion = table.Column<long>(type: "bigint", nullable: false),
                    empresasInvitadas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    skillsidSkillLicitacion = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitacion", x => x.idLicitacion);
                    table.ForeignKey(
                        name: "FK_Licitacion_OrdenesTrabajo_idOrden",
                        column: x => x.idOrden,
                        principalTable: "OrdenesTrabajo",
                        principalColumn: "idOrden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licitacion_SkillLicitacion_skillsidSkillLicitacion",
                        column: x => x.skillsidSkillLicitacion,
                        principalTable: "SkillLicitacion",
                        principalColumn: "idSkillLicitacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MantenimientoAviso",
                columns: table => new
                {
                    idAviso = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOrden = table.Column<long>(type: "bigint", nullable: false),
                    idDiagnostico = table.Column<long>(type: "bigint", nullable: false),
                    idCondicionesVariables = table.Column<long>(type: "bigint", nullable: false),
                    detalleAviso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    correctivo = table.Column<bool>(type: "bit", nullable: false),
                    observacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantenimientoAviso", x => x.idAviso);
                    table.ForeignKey(
                        name: "FK_MantenimientoAviso_CondicionesVariables_idCondicionesVariables",
                        column: x => x.idCondicionesVariables,
                        principalTable: "CondicionesVariables",
                        principalColumn: "idCondicionesVariables",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MantenimientoAviso_OrdenesTrabajo_idOrden",
                        column: x => x.idOrden,
                        principalTable: "OrdenesTrabajo",
                        principalColumn: "idOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MantenimientoPreventivo",
                columns: table => new
                {
                    idMantenimientoPreventivo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOrden = table.Column<long>(type: "bigint", nullable: false),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    idGrupo = table.Column<long>(type: "bigint", nullable: false),
                    fechaPropuestaProgramacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    parada = table.Column<bool>(type: "bit", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Acciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantenimientoPreventivo", x => x.idMantenimientoPreventivo);
                    table.ForeignKey(
                        name: "FK_MantenimientoPreventivo_OrdenesTrabajo_idOrden",
                        column: x => x.idOrden,
                        principalTable: "OrdenesTrabajo",
                        principalColumn: "idOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MantenimientoRondas",
                columns: table => new
                {
                    idRonda = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOrden = table.Column<long>(type: "bigint", nullable: false),
                    idPlan = table.Column<long>(type: "bigint", nullable: false),
                    idActivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idGrupo = table.Column<long>(type: "bigint", nullable: false),
                    fechaUltimoMantenimientoRondas = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaPropuestaProgramacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    observacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantenimientoRondas", x => x.idRonda);
                    table.ForeignKey(
                        name: "FK_MantenimientoRondas_OrdenesTrabajo_idOrden",
                        column: x => x.idOrden,
                        principalTable: "OrdenesTrabajo",
                        principalColumn: "idOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosAdjuntosIncidencias",
                columns: table => new
                {
                    idArchivosAdjuntosIncidencias = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlArchivosAdjuntosIncidencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idIncidencias = table.Column<long>(type: "bigint", nullable: false),
                    eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAdjuntosIncidencias", x => x.idArchivosAdjuntosIncidencias);
                    table.ForeignKey(
                        name: "FK_ArchivosAdjuntosIncidencias_Incidencias_idIncidencias",
                        column: x => x.idIncidencias,
                        principalTable: "Incidencias",
                        principalColumn: "idIncidencias",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosAdjuntosLicitacion",
                columns: table => new
                {
                    idArchivoAdjuntoLicitacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlArchivoAdjuntoLicitacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idLicitacion = table.Column<long>(type: "bigint", nullable: false),
                    eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAdjuntosLicitacion", x => x.idArchivoAdjuntoLicitacion);
                    table.ForeignKey(
                        name: "FK_ArchivosAdjuntosLicitacion_Licitacion_idLicitacion",
                        column: x => x.idLicitacion,
                        principalTable: "Licitacion",
                        principalColumn: "idLicitacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cotizaciones",
                columns: table => new
                {
                    idCotizacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fechaPropuestaServicioCotizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechafinalizacionPropuestaServicioCotizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: false),
                    eliminada = table.Column<bool>(type: "bit", nullable: false),
                    estado = table.Column<long>(type: "bigint", nullable: false),
                    idLicitacion = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    tipoCotizacion = table.Column<long>(type: "bigint", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idRespuestaSolicitudPedido = table.Column<long>(type: "bigint", nullable: false),
                    IdOrdenPago = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizaciones", x => x.idCotizacion);
                    table.ForeignKey(
                        name: "FK_Cotizaciones_Licitacion_idLicitacion",
                        column: x => x.idLicitacion,
                        principalTable: "Licitacion",
                        principalColumn: "idLicitacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CronogramaLicitacion",
                columns: table => new
                {
                    idCronogramaLicitacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    etapa = table.Column<long>(type: "bigint", nullable: false),
                    fechaLimite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idLicitacion = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CronogramaLicitacion", x => x.idCronogramaLicitacion);
                    table.ForeignKey(
                        name: "FK_CronogramaLicitacion_Licitacion_idLicitacion",
                        column: x => x.idLicitacion,
                        principalTable: "Licitacion",
                        principalColumn: "idLicitacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoportesLicitacion",
                columns: table => new
                {
                    idSoporteLicitacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlSoporteLicitacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idLicitacion = table.Column<long>(type: "bigint", nullable: false),
                    LicitacionidLicitacion = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoportesLicitacion", x => x.idSoporteLicitacion);
                    table.ForeignKey(
                        name: "FK_SoportesLicitacion_Licitacion_LicitacionidLicitacion",
                        column: x => x.LicitacionidLicitacion,
                        principalTable: "Licitacion",
                        principalColumn: "idLicitacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaVariableRonda",
                columns: table => new
                {
                    idRespuestaVariableRonda = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRonda = table.Column<long>(type: "bigint", nullable: false),
                    idVariable = table.Column<long>(type: "bigint", nullable: false),
                    variable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    respuesta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MantenimientoRondasidRonda = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaVariableRonda", x => x.idRespuestaVariableRonda);
                    table.ForeignKey(
                        name: "FK_RespuestaVariableRonda_MantenimientoRondas_MantenimientoRondasidRonda",
                        column: x => x.MantenimientoRondasidRonda,
                        principalTable: "MantenimientoRondas",
                        principalColumn: "idRonda",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosAdjuntosCotizacion",
                columns: table => new
                {
                    idArchivoAdjuntoCotizacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlArchivoAdjuntoCotizacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCotizacion = table.Column<long>(type: "bigint", nullable: false),
                    eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAdjuntosCotizacion", x => x.idArchivoAdjuntoCotizacion);
                    table.ForeignKey(
                        name: "FK_ArchivosAdjuntosCotizacion_Cotizaciones_idCotizacion",
                        column: x => x.idCotizacion,
                        principalTable: "Cotizaciones",
                        principalColumn: "idCotizacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntosCotizacion_idCotizacion",
                table: "ArchivosAdjuntosCotizacion",
                column: "idCotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntosIncidencias_idIncidencias",
                table: "ArchivosAdjuntosIncidencias",
                column: "idIncidencias");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntosLicitacion_idLicitacion",
                table: "ArchivosAdjuntosLicitacion",
                column: "idLicitacion");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntosOrdenesTrabajo_idOrdenesTrabajo",
                table: "ArchivosAdjuntosOrdenesTrabajo",
                column: "idOrdenesTrabajo");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_idLicitacion",
                table: "Cotizaciones",
                column: "idLicitacion");

            migrationBuilder.CreateIndex(
                name: "IX_CronogramaLicitacion_idLicitacion",
                table: "CronogramaLicitacion",
                column: "idLicitacion");

            migrationBuilder.CreateIndex(
                name: "IX_GruposAcciones_idPlan",
                table: "GruposAcciones",
                column: "idPlan");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_idOrden",
                table: "Incidencias",
                column: "idOrden");

            migrationBuilder.CreateIndex(
                name: "IX_Licitacion_idOrden",
                table: "Licitacion",
                column: "idOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Licitacion_skillsidSkillLicitacion",
                table: "Licitacion",
                column: "skillsidSkillLicitacion");

            migrationBuilder.CreateIndex(
                name: "IX_MantenimientoAviso_idCondicionesVariables",
                table: "MantenimientoAviso",
                column: "idCondicionesVariables");

            migrationBuilder.CreateIndex(
                name: "IX_MantenimientoAviso_idOrden",
                table: "MantenimientoAviso",
                column: "idOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MantenimientoPreventivo_idOrden",
                table: "MantenimientoPreventivo",
                column: "idOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MantenimientoRondas_idOrden",
                table: "MantenimientoRondas",
                column: "idOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesTrabajo_mantenimientoCorrectivoidMantenimientoCorrectivo",
                table: "OrdenesTrabajo",
                column: "mantenimientoCorrectivoidMantenimientoCorrectivo");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaVariableRonda_MantenimientoRondasidRonda",
                table: "RespuestaVariableRonda",
                column: "MantenimientoRondasidRonda");

            migrationBuilder.CreateIndex(
                name: "IX_SoportesLicitacion_LicitacionidLicitacion",
                table: "SoportesLicitacion",
                column: "LicitacionidLicitacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropTable(
                name: "ArchivosAdjuntosCotizacion");

            migrationBuilder.DropTable(
                name: "ArchivosAdjuntosIncidencias");

            migrationBuilder.DropTable(
                name: "ArchivosAdjuntosLicitacion");

            migrationBuilder.DropTable(
                name: "ArchivosAdjuntosOrdenesTrabajo");

            migrationBuilder.DropTable(
                name: "ComentarioActivosClasificacionDiagnosticoAcciones");

            migrationBuilder.DropTable(
                name: "CronogramaLicitacion");

            migrationBuilder.DropTable(
                name: "CuadrillasTurnos");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "GruposAcciones");

            migrationBuilder.DropTable(
                name: "GruposActivos");

            migrationBuilder.DropTable(
                name: "GruposActivosMantenimientoPreventivo");

            migrationBuilder.DropTable(
                name: "GruposMantenimientoPreventivo");

            migrationBuilder.DropTable(
                name: "GruposPartes");

            migrationBuilder.DropTable(
                name: "GruposVariables");

            migrationBuilder.DropTable(
                name: "MantenimientoAviso");

            migrationBuilder.DropTable(
                name: "MantenimientoCondiciones");

            migrationBuilder.DropTable(
                name: "MantenimientoPreventivo");

            migrationBuilder.DropTable(
                name: "PlanesRondas");

            migrationBuilder.DropTable(
                name: "PlanMantenimientoPreventivo");

            migrationBuilder.DropTable(
                name: "RespuestaAccionesPlanMantenimientoPreventivo");

            migrationBuilder.DropTable(
                name: "RespuestaCuestionario");

            migrationBuilder.DropTable(
                name: "RespuestaMantenimientoCorrectivo");

            migrationBuilder.DropTable(
                name: "RespuestaSolicitudPedido");

            migrationBuilder.DropTable(
                name: "RespuestaVariableRonda");

            migrationBuilder.DropTable(
                name: "SoportesLicitacion");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropTable(
                name: "Cotizaciones");

            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "PlanesMantenimientoPreventivo");

            migrationBuilder.DropTable(
                name: "CondicionesVariables");

            migrationBuilder.DropTable(
                name: "MantenimientoRondas");

            migrationBuilder.DropTable(
                name: "Licitacion");

            migrationBuilder.DropTable(
                name: "OrdenesTrabajo");

            migrationBuilder.DropTable(
                name: "SkillLicitacion");

            migrationBuilder.DropTable(
                name: "MantenimientoCorrectivo");
        }
    }
}
