using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wass.Back.Seguridad.Migrations
{
    public partial class seguridadMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acciones",
                columns: table => new
                {
                    idAccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acciones", x => x.idAccion);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    idGrupo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    creador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.idGrupo);
                });

            migrationBuilder.CreateTable(
                name: "GruposRoles",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idGrupo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposRoles", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    idMenu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPadre = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    ruta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opc1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opc2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opc3 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.idMenu);
                });

            migrationBuilder.CreateTable(
                name: "RolMenuAccion",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMenu = table.Column<int>(type: "int", nullable: false),
                    idAccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolMenuAccion", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    idEmpleado = table.Column<long>(type: "bigint", nullable: false),
                    idEstado = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    editor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosContacto",
                columns: table => new
                {
                    idContacto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    documento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombreEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paginaWebEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefonoEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosContacto", x => x.idContacto);
                    table.ForeignKey(
                        name: "FK_UsuariosContacto_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    idUsuarioRol = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    idRol = table.Column<long>(type: "bigint", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => x.idUsuarioRol);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosContacto_idUsuario",
                table: "UsuariosContacto",
                column: "idUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_idUsuario",
                table: "UsuariosRoles",
                column: "idUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acciones");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "GruposRoles");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "RolMenuAccion");

            migrationBuilder.DropTable(
                name: "UsuariosContacto");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
