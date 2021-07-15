using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wass.Back.Empresa.Migrations
{
    public partial class empresasMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    idEmpresa = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEstado = table.Column<int>(type: "int", nullable: false),
                    tipoAfiliacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    razonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    digVerficacion = table.Column<int>(type: "int", nullable: false),
                    aprobador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mensaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eliminado = table.Column<bool>(type: "bit", nullable: false),
                    creador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaEdicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    urlLogoEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.idEmpresa);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
