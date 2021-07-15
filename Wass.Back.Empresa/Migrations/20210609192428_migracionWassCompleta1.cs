using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wass.Back.Empresa.Migrations
{
    public partial class migracionWassCompleta1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivosAdquisicion",
                columns: table => new
                {
                    idActivosAdquisicion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idActivosEquipos = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idActivosFlotas = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NumFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idTipoAdquisicion = table.Column<long>(type: "bigint", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaAdquision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRetiro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Garantia = table.Column<int>(type: "int", nullable: false),
                    TipoGarantia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adquisisiones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosAdquisicion", x => x.idActivosAdquisicion);
                });

            migrationBuilder.CreateTable(
                name: "ActivosCaracteristicas",
                columns: table => new
                {
                    idActivoCaracteristica = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idActivoEquipo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idActivoFlota = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Caracteristica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosCaracteristicas", x => x.idActivoCaracteristica);
                });

            migrationBuilder.CreateTable(
                name: "ActivosCategorizacion",
                columns: table => new
                {
                    idCategorizacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    categorizacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosCategorizacion", x => x.idCategorizacion);
                });

            migrationBuilder.CreateTable(
                name: "ActivosClasificacionAcciones",
                columns: table => new
                {
                    idAccion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idClasificacion = table.Column<long>(type: "bigint", nullable: false),
                    accion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosClasificacionAcciones", x => x.idAccion);
                });

            migrationBuilder.CreateTable(
                name: "ActivosClasificacionDiagnosticos",
                columns: table => new
                {
                    idDiagnostico = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idClasificacion = table.Column<long>(type: "bigint", nullable: false),
                    diagnostico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    parada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosClasificacionDiagnosticos", x => x.idDiagnostico);
                });

            migrationBuilder.CreateTable(
                name: "ActivosClasificacionVariables",
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
                    table.PrimaryKey("PK_ActivosClasificacionVariables", x => x.idVarible);
                });

            migrationBuilder.CreateTable(
                name: "ActivosFlotas",
                columns: table => new
                {
                    idActivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idSedeResponsable = table.Column<long>(type: "bigint", nullable: false),
                    idEstado = table.Column<long>(type: "bigint", nullable: false),
                    idCategoria = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlImgFlota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoBarras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idClasificacion1 = table.Column<long>(type: "bigint", nullable: false),
                    idClasificacion2 = table.Column<long>(type: "bigint", nullable: true),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Licencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odometro = table.Column<long>(type: "bigint", nullable: false),
                    Altura = table.Column<int>(type: "int", nullable: false),
                    Ancho = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<int>(type: "int", nullable: false),
                    Ejes = table.Column<int>(type: "int", nullable: false),
                    Ocupantes = table.Column<int>(type: "int", nullable: false),
                    idTipoMotor = table.Column<long>(type: "bigint", nullable: false),
                    UnidadPoder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rpm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cilindros = table.Column<int>(type: "int", nullable: false),
                    SerialMotor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idTipoCombustiblePrimario = table.Column<long>(type: "bigint", nullable: false),
                    idTipoCombustibleSecundario = table.Column<long>(type: "bigint", nullable: false),
                    Fabricante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VidaUtil = table.Column<int>(type: "int", nullable: false),
                    InicioFuncionamiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalizaFuncionamiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CentroCosto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsableEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<long>(type: "bigint", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosFlotas", x => x.idActivo);
                });

            migrationBuilder.CreateTable(
                name: "ActivosParada",
                columns: table => new
                {
                    idActivosParada = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idActivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idOrden = table.Column<long>(type: "bigint", nullable: false),
                    fechaHoraParada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaHoraReactivacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tipoOrden = table.Column<long>(type: "bigint", nullable: false),
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    horasParada = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosParada", x => x.idActivosParada);
                });

            migrationBuilder.CreateTable(
                name: "ActivosUbicacion",
                columns: table => new
                {
                    idUbicacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idActivosEquipos = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idActivoFlota = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    latitud = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    longitud = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idTipoUbicacion = table.Column<long>(type: "bigint", nullable: false),
                    idCentroTrabajo = table.Column<long>(type: "bigint", nullable: true),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosUbicacion", x => x.idUbicacion);
                });

            migrationBuilder.CreateTable(
                name: "ActivosVariables",
                columns: table => new
                {
                    idActivoVariable = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idActivoClasificacionVariable = table.Column<long>(type: "bigint", nullable: false),
                    idActivoFlota = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idActivoEquipo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    editor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosVariables", x => x.idActivoVariable);
                });

            migrationBuilder.CreateTable(
                name: "AjustesAlmacenes",
                columns: table => new
                {
                    idAjustesAlmacenes = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRepuesto = table.Column<long>(type: "bigint", nullable: false),
                    cantidadAnterior = table.Column<long>(type: "bigint", nullable: false),
                    cantidadNueva = table.Column<long>(type: "bigint", nullable: false),
                    fechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idAlmacen = table.Column<long>(type: "bigint", nullable: false),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    existenciaActual = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesAlmacenes", x => x.idAjustesAlmacenes);
                });

            migrationBuilder.CreateTable(
                name: "Almacen",
                columns: table => new
                {
                    idAlmacen = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    tipo = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaDesactivacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almacen", x => x.idAlmacen);
                });

            migrationBuilder.CreateTable(
                name: "Calificacion",
                columns: table => new
                {
                    idCalificacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    idOrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    idProveedor = table.Column<long>(type: "bigint", nullable: false),
                    calificacion = table.Column<float>(type: "real", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificacion", x => x.idCalificacion);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    idCargo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cargo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.idCargo);
                });

            migrationBuilder.CreateTable(
                name: "CentroCosto",
                columns: table => new
                {
                    idCentroCosto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCentroCostoPadre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCosto", x => x.idCentroCosto);
                });

            migrationBuilder.CreateTable(
                name: "ConsultaFechasInventarios",
                columns: table => new
                {
                    idKardexRepuesto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consulta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultaFechasInventarios", x => x.idKardexRepuesto);
                });

            migrationBuilder.CreateTable(
                name: "Conversacion",
                columns: table => new
                {
                    idConversacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario1 = table.Column<long>(type: "bigint", nullable: false),
                    usuario2 = table.Column<long>(type: "bigint", nullable: false),
                    noLeido = table.Column<int>(type: "int", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversacion", x => x.idConversacion);
                });

            migrationBuilder.CreateTable(
                name: "Cuestionario",
                columns: table => new
                {
                    idCuestionario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuestionario", x => x.idCuestionario);
                });

            migrationBuilder.CreateTable(
                name: "DañosRepuestos",
                columns: table => new
                {
                    idDañosRepuestos = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRepuesto = table.Column<long>(type: "bigint", nullable: false),
                    fechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cantidad = table.Column<long>(type: "bigint", nullable: false),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idAlmacen = table.Column<long>(type: "bigint", nullable: false),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    existenciaActual = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DañosRepuestos", x => x.idDañosRepuestos);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    idDepto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPais = table.Column<int>(type: "int", nullable: false),
                    depto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.idDepto);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaChecks",
                columns: table => new
                {
                    idEmpresaCheck = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idLista = table.Column<long>(type: "bigint", nullable: false),
                    idValor = table.Column<long>(type: "bigint", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaChecks", x => x.idEmpresaCheck);
                    table.ForeignKey(
                        name: "FK_EmpresaChecks_Empresas_idEmpresa",
                        column: x => x.idEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "idEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaSkills",
                columns: table => new
                {
                    idSkill = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    skills = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaSkills", x => x.idSkill);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaSoportes",
                columns: table => new
                {
                    idSoporte = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idTipoDocumento = table.Column<int>(type: "int", nullable: false),
                    rutaArchivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    idActivosEquipos = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idActivosFlotas = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaSoportes", x => x.idSoporte);
                    table.ForeignKey(
                        name: "FK_EmpresaSoportes_Empresas_idEmpresa",
                        column: x => x.idEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "idEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Herramientas",
                columns: table => new
                {
                    idHerramienta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipo = table.Column<long>(type: "bigint", nullable: false),
                    marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<long>(type: "bigint", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    codigoActivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Herramientas", x => x.idHerramienta);
                });

            migrationBuilder.CreateTable(
                name: "Listas",
                columns: table => new
                {
                    idLista = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripción = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listas", x => x.idLista);
                });

            migrationBuilder.CreateTable(
                name: "MarcaActivo",
                columns: table => new
                {
                    idMarcaActivo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    marcaActivo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaActivo", x => x.idMarcaActivo);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    idMunicipio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDepto = table.Column<int>(type: "int", nullable: false),
                    municipio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.idMunicipio);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    idPais = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.idPais);
                });

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    idPregunta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoRespuesta = table.Column<long>(type: "bigint", nullable: false),
                    opciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.idPregunta);
                });

            migrationBuilder.CreateTable(
                name: "Recomendaciones",
                columns: table => new
                {
                    idRecomendacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresaRecomienda = table.Column<long>(type: "bigint", nullable: false),
                    idEmpresaRecomendada = table.Column<long>(type: "bigint", nullable: false),
                    recomendacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendaciones", x => x.idRecomendacion);
                });

            migrationBuilder.CreateTable(
                name: "Repuestos",
                columns: table => new
                {
                    idRepuestos = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codigoBarras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCategoria = table.Column<long>(type: "bigint", nullable: false),
                    idClasificacion = table.Column<long>(type: "bigint", nullable: false),
                    unidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    especificaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tiempoCompra = table.Column<long>(type: "bigint", nullable: false),
                    tiempoEntregaPromedio = table.Column<long>(type: "bigint", nullable: false),
                    numeroMinimoPedido = table.Column<long>(type: "bigint", nullable: false),
                    costoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    clasificacionABC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    criticidad = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repuestos", x => x.idRepuestos);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaActivosVariables",
                columns: table => new
                {
                    idRespuestaActivosVariables = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idActivoVariable = table.Column<long>(type: "bigint", nullable: false),
                    idClasificacion = table.Column<long>(type: "bigint", nullable: false),
                    idCategorizacion = table.Column<long>(type: "bigint", nullable: false),
                    idActivoFlota = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idActivoEquipo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    respuesta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaActivosVariables", x => x.idRespuestaActivosVariables);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    idSkill = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    skill = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.idSkill);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudPedido",
                columns: table => new
                {
                    idSolicitudPedido = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<long>(type: "bigint", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaCancelacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    nivelUrgencia = table.Column<long>(type: "bigint", nullable: false),
                    idUsuarioCreador = table.Column<long>(type: "bigint", nullable: false),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idLicitacion = table.Column<long>(type: "bigint", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudPedido", x => x.idSolicitudPedido);
                });

            migrationBuilder.CreateTable(
                name: "TipoActivo",
                columns: table => new
                {
                    idTipoActivo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoActivo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoActivo", x => x.idTipoActivo);
                });

            migrationBuilder.CreateTable(
                name: "TopicoAComentar",
                columns: table => new
                {
                    idTopicoAComentar = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    idEmpleado = table.Column<long>(type: "bigint", nullable: false),
                    idTopico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoTopico = table.Column<long>(type: "bigint", nullable: false),
                    urlImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicoAComentar", x => x.idTopicoAComentar);
                });

            migrationBuilder.CreateTable(
                name: "TransferenciasInternasAlmacenes",
                columns: table => new
                {
                    idTransferenciasInternasAlmacenes = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idAlmacenEmisor = table.Column<long>(type: "bigint", nullable: false),
                    idAlmacenReceptor = table.Column<long>(type: "bigint", nullable: false),
                    repuestos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    motivoTransferencia = table.Column<long>(type: "bigint", nullable: false),
                    ordenTrabajo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    estado = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferenciasInternasAlmacenes", x => x.idTransferenciasInternasAlmacenes);
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
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.idTurno);
                });

            migrationBuilder.CreateTable(
                name: "ActivosClasificacion",
                columns: table => new
                {
                    idClasificacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCategorizacion = table.Column<long>(type: "bigint", nullable: false),
                    idSubClasificaicon = table.Column<long>(type: "bigint", nullable: true),
                    clasificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prioridad = table.Column<long>(type: "bigint", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosClasificacion", x => x.idClasificacion);
                    table.ForeignKey(
                        name: "FK_ActivosClasificacion_ActivosCategorizacion_idCategorizacion",
                        column: x => x.idCategorizacion,
                        principalTable: "ActivosCategorizacion",
                        principalColumn: "idCategorizacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivosClasificacion_ActivosClasificacion_idSubClasificaicon",
                        column: x => x.idSubClasificaicon,
                        principalTable: "ActivosClasificacion",
                        principalColumn: "idClasificacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivosClasificacionDiagnosticosAcciones",
                columns: table => new
                {
                    idDiagnosticosAcciones = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDiagnostico = table.Column<long>(type: "bigint", nullable: false),
                    idAccion = table.Column<long>(type: "bigint", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosClasificacionDiagnosticosAcciones", x => x.idDiagnosticosAcciones);
                    table.ForeignKey(
                        name: "FK_ActivosClasificacionDiagnosticosAcciones_ActivosClasificacionAcciones_idAccion",
                        column: x => x.idAccion,
                        principalTable: "ActivosClasificacionAcciones",
                        principalColumn: "idAccion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivosClasificacionDiagnosticosAcciones_ActivosClasificacionDiagnosticos_idDiagnostico",
                        column: x => x.idDiagnostico,
                        principalTable: "ActivosClasificacionDiagnosticos",
                        principalColumn: "idDiagnostico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosAdjuntosActivosFlotas",
                columns: table => new
                {
                    idArchivoAdjuntoActivosFlotas = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlArchivoAdjuntoActivosFlotas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idActivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAdjuntosActivosFlotas", x => x.idArchivoAdjuntoActivosFlotas);
                    table.ForeignKey(
                        name: "FK_ArchivosAdjuntosActivosFlotas_ActivosFlotas_idActivo",
                        column: x => x.idActivo,
                        principalTable: "ActivosFlotas",
                        principalColumn: "idActivo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivosVariablesHistorico",
                columns: table => new
                {
                    idActivoVariableHistorico = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idActivoVariable = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosVariablesHistorico", x => x.idActivoVariableHistorico);
                    table.ForeignKey(
                        name: "FK_ActivosVariablesHistorico_ActivosVariables_idActivoVariable",
                        column: x => x.idActivoVariable,
                        principalTable: "ActivosVariables",
                        principalColumn: "idActivoVariable",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenEntregaAlmacen",
                columns: table => new
                {
                    idOrdenEntregaAlmacen = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    repuestos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idOrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    fechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idAlmacen = table.Column<long>(type: "bigint", nullable: false),
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenEntregaAlmacen", x => x.idOrdenEntregaAlmacen);
                    table.ForeignKey(
                        name: "FK_OrdenEntregaAlmacen_Almacen_idAlmacen",
                        column: x => x.idAlmacen,
                        principalTable: "Almacen",
                        principalColumn: "idAlmacen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecepcionRepuestos",
                columns: table => new
                {
                    idRecepcionRepuestos = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    repuestos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idAlmacen = table.Column<long>(type: "bigint", nullable: false),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecepcionRepuestos", x => x.idRecepcionRepuestos);
                    table.ForeignKey(
                        name: "FK_RecepcionRepuestos_Almacen_idAlmacen",
                        column: x => x.idAlmacen,
                        principalTable: "Almacen",
                        principalColumn: "idAlmacen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    idSede = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idMunicipio = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    creador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idCentroCosto = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.idSede);
                    table.ForeignKey(
                        name: "FK_Sedes_CentroCosto_idCentroCosto",
                        column: x => x.idCentroCosto,
                        principalTable: "CentroCosto",
                        principalColumn: "idCentroCosto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sedes_Empresas_idEmpresa",
                        column: x => x.idEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "idEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MensajesConversacion",
                columns: table => new
                {
                    idMensajesConversacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idConversacion = table.Column<long>(type: "bigint", nullable: false),
                    idSender = table.Column<long>(type: "bigint", nullable: false),
                    FechaHoraMensaje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    mensaje = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensajesConversacion", x => x.idMensajesConversacion);
                    table.ForeignKey(
                        name: "FK_MensajesConversacion_Conversacion_idConversacion",
                        column: x => x.idConversacion,
                        principalTable: "Conversacion",
                        principalColumn: "idConversacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivosClasificacionDiagnosticosSkills",
                columns: table => new
                {
                    idDiagnosticosSkills = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDiagnostico = table.Column<long>(type: "bigint", nullable: false),
                    idSkill = table.Column<long>(type: "bigint", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosClasificacionDiagnosticosSkills", x => x.idDiagnosticosSkills);
                    table.ForeignKey(
                        name: "FK_ActivosClasificacionDiagnosticosSkills_ActivosClasificacionDiagnosticos_idDiagnostico",
                        column: x => x.idDiagnostico,
                        principalTable: "ActivosClasificacionDiagnosticos",
                        principalColumn: "idDiagnostico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivosClasificacionDiagnosticosSkills_EmpresaSkills_idSkill",
                        column: x => x.idSkill,
                        principalTable: "EmpresaSkills",
                        principalColumn: "idSkill",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticoSkillsEmpresa",
                columns: table => new
                {
                    idDiagnosticoSkillsEmpresa = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDiagnostico = table.Column<long>(type: "bigint", nullable: false),
                    idSkill = table.Column<long>(type: "bigint", nullable: false),
                    fechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    skillsSeleccionados = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticoSkillsEmpresa", x => x.idDiagnosticoSkillsEmpresa);
                    table.ForeignKey(
                        name: "FK_DiagnosticoSkillsEmpresa_ActivosClasificacionDiagnosticos_idDiagnostico",
                        column: x => x.idDiagnostico,
                        principalTable: "ActivosClasificacionDiagnosticos",
                        principalColumn: "idDiagnostico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosticoSkillsEmpresa_EmpresaSkills_idSkill",
                        column: x => x.idSkill,
                        principalTable: "EmpresaSkills",
                        principalColumn: "idSkill",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListasValores",
                columns: table => new
                {
                    idValor = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idLista = table.Column<int>(type: "int", nullable: false),
                    valor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasValores", x => x.idValor);
                    table.ForeignKey(
                        name: "FK_ListasValores_Listas_idLista",
                        column: x => x.idLista,
                        principalTable: "Listas",
                        principalColumn: "idLista",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuestionarioPreguntas",
                columns: table => new
                {
                    idCuestionarioPregunta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCuestionario = table.Column<long>(type: "bigint", nullable: false),
                    idPregunta = table.Column<long>(type: "bigint", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuestionarioPreguntas", x => x.idCuestionarioPregunta);
                    table.ForeignKey(
                        name: "FK_CuestionarioPreguntas_Cuestionario_idCuestionario",
                        column: x => x.idCuestionario,
                        principalTable: "Cuestionario",
                        principalColumn: "idCuestionario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuestionarioPreguntas_Preguntas_idPregunta",
                        column: x => x.idPregunta,
                        principalTable: "Preguntas",
                        principalColumn: "idPregunta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepuestosAlmacen",
                columns: table => new
                {
                    idRepuestosAlmacen = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidadActual = table.Column<long>(type: "bigint", nullable: false),
                    cantidadMinima = table.Column<long>(type: "bigint", nullable: false),
                    cantidadOptima = table.Column<long>(type: "bigint", nullable: false),
                    cantidadMaxima = table.Column<long>(type: "bigint", nullable: false),
                    idAlmacen = table.Column<long>(type: "bigint", nullable: false),
                    idRepuestos = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepuestosAlmacen", x => x.idRepuestosAlmacen);
                    table.ForeignKey(
                        name: "FK_RepuestosAlmacen_Almacen_idAlmacen",
                        column: x => x.idAlmacen,
                        principalTable: "Almacen",
                        principalColumn: "idAlmacen",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepuestosAlmacen_Repuestos_idRepuestos",
                        column: x => x.idRepuestos,
                        principalTable: "Repuestos",
                        principalColumn: "idRepuestos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepuestosDiagnostico",
                columns: table => new
                {
                    idRepuestosDiagnostico = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRepuestos = table.Column<long>(type: "bigint", nullable: false),
                    idDiagnostico = table.Column<long>(type: "bigint", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    cantidad = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepuestosDiagnostico", x => x.idRepuestosDiagnostico);
                    table.ForeignKey(
                        name: "FK_RepuestosDiagnostico_ActivosClasificacionDiagnosticos_idDiagnostico",
                        column: x => x.idDiagnostico,
                        principalTable: "ActivosClasificacionDiagnosticos",
                        principalColumn: "idDiagnostico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepuestosDiagnostico_Repuestos_idRepuestos",
                        column: x => x.idRepuestos,
                        principalTable: "Repuestos",
                        principalColumn: "idRepuestos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepuestosGrupoPartes",
                columns: table => new
                {
                    idRepuestosGrupoPartes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRepuestos = table.Column<long>(type: "bigint", nullable: false),
                    idGrupoPartes = table.Column<long>(type: "bigint", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    cantidad = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepuestosGrupoPartes", x => x.idRepuestosGrupoPartes);
                    table.ForeignKey(
                        name: "FK_RepuestosGrupoPartes_Repuestos_idRepuestos",
                        column: x => x.idRepuestos,
                        principalTable: "Repuestos",
                        principalColumn: "idRepuestos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivosPartes",
                columns: table => new
                {
                    idParte = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idClasificacion = table.Column<long>(type: "bigint", nullable: false),
                    idSubParte = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    parte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosPartes", x => x.idParte);
                    table.ForeignKey(
                        name: "FK_ActivosPartes_ActivosClasificacion_idClasificacion",
                        column: x => x.idClasificacion,
                        principalTable: "ActivosClasificacion",
                        principalColumn: "idClasificacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivosPartes_ActivosPartes_idSubParte",
                        column: x => x.idSubParte,
                        principalTable: "ActivosPartes",
                        principalColumn: "idParte",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    idMarca = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSubMarca = table.Column<long>(type: "bigint", nullable: true),
                    marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.idMarca);
                    table.ForeignKey(
                        name: "FK_Marca_ActivosClasificacion_idSubMarca",
                        column: x => x.idSubMarca,
                        principalTable: "ActivosClasificacion",
                        principalColumn: "idClasificacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CentrosTrabajo",
                columns: table => new
                {
                    idCentroTrabajo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSubCentroTrabajo = table.Column<long>(type: "bigint", nullable: true),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosTrabajo", x => x.idCentroTrabajo);
                    table.ForeignKey(
                        name: "FK_CentrosTrabajo_CentrosTrabajo_idSubCentroTrabajo",
                        column: x => x.idSubCentroTrabajo,
                        principalTable: "CentrosTrabajo",
                        principalColumn: "idCentroTrabajo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentrosTrabajo_Sedes_idSede",
                        column: x => x.idSede,
                        principalTable: "Sedes",
                        principalColumn: "idSede",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuadrillas",
                columns: table => new
                {
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    nombreA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombreB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false),
                    zonaAtencion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ubicacionActual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numMiembros = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuadrillas", x => x.idCuadrilla);
                    table.ForeignKey(
                        name: "FK_Cuadrillas_Sedes_idSede",
                        column: x => x.idSede,
                        principalTable: "Sedes",
                        principalColumn: "idSede",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    idEmpleado = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCargo = table.Column<int>(type: "int", nullable: false),
                    idSede = table.Column<long>(type: "bigint", nullable: false),
                    idEstadoEmpleado = table.Column<int>(type: "int", nullable: false),
                    idTipoDocumento = table.Column<long>(type: "bigint", nullable: false),
                    numDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sedeidSede = table.Column<long>(type: "bigint", nullable: true),
                    estadoChat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlFotoEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.idEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleados_Sedes_sedeidSede",
                        column: x => x.sedeidSede,
                        principalTable: "Sedes",
                        principalColumn: "idSede",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivosEquipos",
                columns: table => new
                {
                    idActivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idSedeResponsable = table.Column<long>(type: "bigint", nullable: false),
                    idEstado = table.Column<long>(type: "bigint", nullable: false),
                    idCategoria = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoBarras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlImgEquipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idClasificacion1 = table.Column<long>(type: "bigint", nullable: false),
                    idClasificacion2 = table.Column<long>(type: "bigint", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fabricante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idMarca = table.Column<long>(type: "bigint", nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VidaUtil = table.Column<int>(type: "int", nullable: false),
                    InicioFuncionamiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalizaFuncionamiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Responsable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsableEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<long>(type: "bigint", nullable: false),
                    otros = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    idSkill = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosEquipos", x => x.idActivo);
                    table.ForeignKey(
                        name: "FK_ActivosEquipos_Marca_idMarca",
                        column: x => x.idMarca,
                        principalTable: "Marca",
                        principalColumn: "idMarca",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivosEquipos_Skills_idSkill",
                        column: x => x.idSkill,
                        principalTable: "Skills",
                        principalColumn: "idSkill",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarcaEmpresa",
                columns: table => new
                {
                    idMarcaEmpresa = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMarca = table.Column<long>(type: "bigint", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaEmpresa", x => x.idMarcaEmpresa);
                    table.ForeignKey(
                        name: "FK_MarcaEmpresa_Empresas_idEmpresa",
                        column: x => x.idEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "idEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarcaEmpresa_Marca_idMarca",
                        column: x => x.idMarca,
                        principalTable: "Marca",
                        principalColumn: "idMarca",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuadrillaSkill",
                columns: table => new
                {
                    idCuadrillaSkill = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: false),
                    idSkill = table.Column<long>(type: "bigint", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuadrillaSkill", x => x.idCuadrillaSkill);
                    table.ForeignKey(
                        name: "FK_CuadrillaSkill_Cuadrillas_idCuadrilla",
                        column: x => x.idCuadrilla,
                        principalTable: "Cuadrillas",
                        principalColumn: "idCuadrilla",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuadrillaSkill_Skills_idSkill",
                        column: x => x.idSkill,
                        principalTable: "Skills",
                        principalColumn: "idSkill",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuadrillaSkillsEmpresa",
                columns: table => new
                {
                    idCuadrillaSkillsEmpresa = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: false),
                    idSkill = table.Column<long>(type: "bigint", nullable: false),
                    fechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    skillsSeleccionados = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuadrillaSkillsEmpresa", x => x.idCuadrillaSkillsEmpresa);
                    table.ForeignKey(
                        name: "FK_CuadrillaSkillsEmpresa_Cuadrillas_idCuadrilla",
                        column: x => x.idCuadrilla,
                        principalTable: "Cuadrillas",
                        principalColumn: "idCuadrilla",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuadrillaSkillsEmpresa_EmpresaSkills_idSkill",
                        column: x => x.idSkill,
                        principalTable: "EmpresaSkills",
                        principalColumn: "idSkill",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_CuadrillasTurnos_Cuadrillas_idCuadrilla",
                        column: x => x.idCuadrilla,
                        principalTable: "Cuadrillas",
                        principalColumn: "idCuadrilla",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificaciones",
                columns: table => new
                {
                    idCertificado = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expira = table.Column<bool>(type: "bit", nullable: false),
                    fechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idCredencial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlCredencial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idEmpleado = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificaciones", x => x.idCertificado);
                    table.ForeignKey(
                        name: "FK_Certificaciones_Empleados_idEmpleado",
                        column: x => x.idEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "idEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificaciones_Empresas_idEmpresa",
                        column: x => x.idEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "idEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    idComentario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idTopicoAComentar = table.Column<long>(type: "bigint", nullable: false),
                    idEmpleado = table.Column<long>(type: "bigint", nullable: false),
                    urlImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaHoraComentario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    like = table.Column<long>(type: "bigint", nullable: false),
                    idUsuariosLike = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    replica = table.Column<bool>(type: "bit", nullable: false),
                    idComentarioPadre = table.Column<long>(type: "bigint", nullable: true),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.idComentario);
                    table.ForeignKey(
                        name: "FK_Comentario_Empleados_idEmpleado",
                        column: x => x.idEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "idEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentario_TopicoAComentar_idTopicoAComentar",
                        column: x => x.idTopicoAComentar,
                        principalTable: "TopicoAComentar",
                        principalColumn: "idTopicoAComentar",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuadrillaEmpleados",
                columns: table => new
                {
                    idEmpleadoCuadrilla = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idCuadrilla = table.Column<long>(type: "bigint", nullable: false),
                    idEmpleado = table.Column<long>(type: "bigint", nullable: false),
                    lider = table.Column<bool>(type: "bit", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuadrillaEmpleados", x => x.idEmpleadoCuadrilla);
                    table.ForeignKey(
                        name: "FK_CuadrillaEmpleados_Cuadrillas_idCuadrilla",
                        column: x => x.idCuadrilla,
                        principalTable: "Cuadrillas",
                        principalColumn: "idCuadrilla",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuadrillaEmpleados_Empleados_idEmpleado",
                        column: x => x.idEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "idEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosAdjuntosActivosEquipos",
                columns: table => new
                {
                    idArchivoAdjuntoActivosEquipos = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    urlArchivoAdjuntoActivosEquipos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idActivo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAdjuntosActivosEquipos", x => x.idArchivoAdjuntoActivosEquipos);
                    table.ForeignKey(
                        name: "FK_ArchivosAdjuntosActivosEquipos_ActivosEquipos_idActivo",
                        column: x => x.idActivo,
                        principalTable: "ActivosEquipos",
                        principalColumn: "idActivo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivosClasificacion_idCategorizacion",
                table: "ActivosClasificacion",
                column: "idCategorizacion");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosClasificacion_idSubClasificaicon",
                table: "ActivosClasificacion",
                column: "idSubClasificaicon");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosClasificacionDiagnosticosAcciones_idAccion",
                table: "ActivosClasificacionDiagnosticosAcciones",
                column: "idAccion");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosClasificacionDiagnosticosAcciones_idDiagnostico",
                table: "ActivosClasificacionDiagnosticosAcciones",
                column: "idDiagnostico");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosClasificacionDiagnosticosSkills_idDiagnostico",
                table: "ActivosClasificacionDiagnosticosSkills",
                column: "idDiagnostico");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosClasificacionDiagnosticosSkills_idSkill",
                table: "ActivosClasificacionDiagnosticosSkills",
                column: "idSkill");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosEquipos_idMarca",
                table: "ActivosEquipos",
                column: "idMarca");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosEquipos_idSkill",
                table: "ActivosEquipos",
                column: "idSkill");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosPartes_idClasificacion",
                table: "ActivosPartes",
                column: "idClasificacion");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosPartes_idSubParte",
                table: "ActivosPartes",
                column: "idSubParte");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosVariablesHistorico_idActivoVariable",
                table: "ActivosVariablesHistorico",
                column: "idActivoVariable");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntosActivosEquipos_idActivo",
                table: "ArchivosAdjuntosActivosEquipos",
                column: "idActivo");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntosActivosFlotas_idActivo",
                table: "ArchivosAdjuntosActivosFlotas",
                column: "idActivo");

            migrationBuilder.CreateIndex(
                name: "IX_CentrosTrabajo_idSede",
                table: "CentrosTrabajo",
                column: "idSede");

            migrationBuilder.CreateIndex(
                name: "IX_CentrosTrabajo_idSubCentroTrabajo",
                table: "CentrosTrabajo",
                column: "idSubCentroTrabajo");

            migrationBuilder.CreateIndex(
                name: "IX_Certificaciones_idEmpleado",
                table: "Certificaciones",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Certificaciones_idEmpresa",
                table: "Certificaciones",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_idEmpleado",
                table: "Comentario",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_idTopicoAComentar",
                table: "Comentario",
                column: "idTopicoAComentar");

            migrationBuilder.CreateIndex(
                name: "IX_CuadrillaEmpleados_idCuadrilla",
                table: "CuadrillaEmpleados",
                column: "idCuadrilla");

            migrationBuilder.CreateIndex(
                name: "IX_CuadrillaEmpleados_idEmpleado",
                table: "CuadrillaEmpleados",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Cuadrillas_idSede",
                table: "Cuadrillas",
                column: "idSede");

            migrationBuilder.CreateIndex(
                name: "IX_CuadrillaSkill_idCuadrilla",
                table: "CuadrillaSkill",
                column: "idCuadrilla");

            migrationBuilder.CreateIndex(
                name: "IX_CuadrillaSkill_idSkill",
                table: "CuadrillaSkill",
                column: "idSkill");

            migrationBuilder.CreateIndex(
                name: "IX_CuadrillaSkillsEmpresa_idCuadrilla",
                table: "CuadrillaSkillsEmpresa",
                column: "idCuadrilla");

            migrationBuilder.CreateIndex(
                name: "IX_CuadrillaSkillsEmpresa_idSkill",
                table: "CuadrillaSkillsEmpresa",
                column: "idSkill");

            migrationBuilder.CreateIndex(
                name: "IX_CuadrillasTurnos_idCuadrilla",
                table: "CuadrillasTurnos",
                column: "idCuadrilla");

            migrationBuilder.CreateIndex(
                name: "IX_CuestionarioPreguntas_idCuestionario",
                table: "CuestionarioPreguntas",
                column: "idCuestionario");

            migrationBuilder.CreateIndex(
                name: "IX_CuestionarioPreguntas_idPregunta",
                table: "CuestionarioPreguntas",
                column: "idPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticoSkillsEmpresa_idDiagnostico",
                table: "DiagnosticoSkillsEmpresa",
                column: "idDiagnostico");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticoSkillsEmpresa_idSkill",
                table: "DiagnosticoSkillsEmpresa",
                column: "idSkill");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_sedeidSede",
                table: "Empleados",
                column: "sedeidSede");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaChecks_idEmpresa",
                table: "EmpresaChecks",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaSoportes_idEmpresa",
                table: "EmpresaSoportes",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_ListasValores_idLista",
                table: "ListasValores",
                column: "idLista");

            migrationBuilder.CreateIndex(
                name: "IX_Marca_idSubMarca",
                table: "Marca",
                column: "idSubMarca");

            migrationBuilder.CreateIndex(
                name: "IX_MarcaEmpresa_idEmpresa",
                table: "MarcaEmpresa",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_MarcaEmpresa_idMarca",
                table: "MarcaEmpresa",
                column: "idMarca");

            migrationBuilder.CreateIndex(
                name: "IX_MensajesConversacion_idConversacion",
                table: "MensajesConversacion",
                column: "idConversacion");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenEntregaAlmacen_idAlmacen",
                table: "OrdenEntregaAlmacen",
                column: "idAlmacen");

            migrationBuilder.CreateIndex(
                name: "IX_RecepcionRepuestos_idAlmacen",
                table: "RecepcionRepuestos",
                column: "idAlmacen");

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosAlmacen_idAlmacen",
                table: "RepuestosAlmacen",
                column: "idAlmacen");

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosAlmacen_idRepuestos",
                table: "RepuestosAlmacen",
                column: "idRepuestos");

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosDiagnostico_idDiagnostico",
                table: "RepuestosDiagnostico",
                column: "idDiagnostico");

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosDiagnostico_idRepuestos",
                table: "RepuestosDiagnostico",
                column: "idRepuestos");

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosGrupoPartes_idRepuestos",
                table: "RepuestosGrupoPartes",
                column: "idRepuestos");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_idCentroCosto",
                table: "Sedes",
                column: "idCentroCosto");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_idEmpresa",
                table: "Sedes",
                column: "idEmpresa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivosAdquisicion");

            migrationBuilder.DropTable(
                name: "ActivosCaracteristicas");

            migrationBuilder.DropTable(
                name: "ActivosClasificacionDiagnosticosAcciones");

            migrationBuilder.DropTable(
                name: "ActivosClasificacionDiagnosticosSkills");

            migrationBuilder.DropTable(
                name: "ActivosClasificacionVariables");

            migrationBuilder.DropTable(
                name: "ActivosParada");

            migrationBuilder.DropTable(
                name: "ActivosPartes");

            migrationBuilder.DropTable(
                name: "ActivosUbicacion");

            migrationBuilder.DropTable(
                name: "ActivosVariablesHistorico");

            migrationBuilder.DropTable(
                name: "AjustesAlmacenes");

            migrationBuilder.DropTable(
                name: "ArchivosAdjuntosActivosEquipos");

            migrationBuilder.DropTable(
                name: "ArchivosAdjuntosActivosFlotas");

            migrationBuilder.DropTable(
                name: "Calificacion");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "CentrosTrabajo");

            migrationBuilder.DropTable(
                name: "Certificaciones");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "ConsultaFechasInventarios");

            migrationBuilder.DropTable(
                name: "CuadrillaEmpleados");

            migrationBuilder.DropTable(
                name: "CuadrillaSkill");

            migrationBuilder.DropTable(
                name: "CuadrillaSkillsEmpresa");

            migrationBuilder.DropTable(
                name: "CuadrillasTurnos");

            migrationBuilder.DropTable(
                name: "CuestionarioPreguntas");

            migrationBuilder.DropTable(
                name: "DañosRepuestos");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "DiagnosticoSkillsEmpresa");

            migrationBuilder.DropTable(
                name: "EmpresaChecks");

            migrationBuilder.DropTable(
                name: "EmpresaSoportes");

            migrationBuilder.DropTable(
                name: "Herramientas");

            migrationBuilder.DropTable(
                name: "ListasValores");

            migrationBuilder.DropTable(
                name: "MarcaActivo");

            migrationBuilder.DropTable(
                name: "MarcaEmpresa");

            migrationBuilder.DropTable(
                name: "MensajesConversacion");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "OrdenEntregaAlmacen");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "RecepcionRepuestos");

            migrationBuilder.DropTable(
                name: "Recomendaciones");

            migrationBuilder.DropTable(
                name: "RepuestosAlmacen");

            migrationBuilder.DropTable(
                name: "RepuestosDiagnostico");

            migrationBuilder.DropTable(
                name: "RepuestosGrupoPartes");

            migrationBuilder.DropTable(
                name: "RespuestaActivosVariables");

            migrationBuilder.DropTable(
                name: "SolicitudPedido");

            migrationBuilder.DropTable(
                name: "TipoActivo");

            migrationBuilder.DropTable(
                name: "TransferenciasInternasAlmacenes");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "ActivosClasificacionAcciones");

            migrationBuilder.DropTable(
                name: "ActivosVariables");

            migrationBuilder.DropTable(
                name: "ActivosEquipos");

            migrationBuilder.DropTable(
                name: "ActivosFlotas");

            migrationBuilder.DropTable(
                name: "TopicoAComentar");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Cuadrillas");

            migrationBuilder.DropTable(
                name: "Cuestionario");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "EmpresaSkills");

            migrationBuilder.DropTable(
                name: "Listas");

            migrationBuilder.DropTable(
                name: "Conversacion");

            migrationBuilder.DropTable(
                name: "Almacen");

            migrationBuilder.DropTable(
                name: "ActivosClasificacionDiagnosticos");

            migrationBuilder.DropTable(
                name: "Repuestos");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "ActivosClasificacion");

            migrationBuilder.DropTable(
                name: "CentroCosto");

            migrationBuilder.DropTable(
                name: "ActivosCategorizacion");
        }
    }
}
