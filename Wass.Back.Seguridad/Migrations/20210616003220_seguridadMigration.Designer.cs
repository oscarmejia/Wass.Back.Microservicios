﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wass.Back.Seguridad.Rabbit.Context;

namespace Wass.Back.Seguridad.Migrations
{
    [DbContext(typeof(SeguridadContext))]
    [Migration("20210616003220_seguridadMigration")]
    partial class seguridadMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.Acciones", b =>
                {
                    b.Property<int>("idAccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("accion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idAccion");

                    b.ToTable("Acciones");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.Grupos", b =>
                {
                    b.Property<int>("idGrupo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("creador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("editor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaEdicion")
                        .HasColumnType("datetime2");

                    b.Property<string>("grupo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idGrupo");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.GruposRoles", b =>
                {
                    b.Property<int>("idRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("idGrupo")
                        .HasColumnType("int");

                    b.HasKey("idRol");

                    b.ToTable("GruposRoles");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.Menus", b =>
                {
                    b.Property<int>("idMenu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("idPadre")
                        .HasColumnType("int");

                    b.Property<string>("opc1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("opc2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("opc3")
                        .HasColumnType("int");

                    b.Property<string>("ruta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idMenu");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.RolMenuAccion", b =>
                {
                    b.Property<int>("idRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("idAccion")
                        .HasColumnType("int");

                    b.Property<int>("idMenu")
                        .HasColumnType("int");

                    b.HasKey("idRol");

                    b.ToTable("RolMenuAccion");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.Usuarios", b =>
                {
                    b.Property<long>("idUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("creador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("editor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaEdicion")
                        .HasColumnType("datetime2");

                    b.Property<long>("idEmpleado")
                        .HasColumnType("bigint");

                    b.Property<long>("idEmpresa")
                        .HasColumnType("bigint");

                    b.Property<int>("idEstado")
                        .HasColumnType("int");

                    b.Property<string>("passw")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idUsuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.UsuariosContacto", b =>
                {
                    b.Property<long>("idContacto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("apellidos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("documento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("idUsuario")
                        .HasColumnType("bigint");

                    b.Property<string>("nombreEmpresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paginaWebEmpresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telefonoEmpresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipoDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idContacto");

                    b.HasIndex("idUsuario")
                        .IsUnique();

                    b.ToTable("UsuariosContacto");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.UsuariosRoles", b =>
                {
                    b.Property<long>("idUsuarioRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("creador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<long>("idRol")
                        .HasColumnType("bigint");

                    b.Property<long>("idUsuario")
                        .HasColumnType("bigint");

                    b.HasKey("idUsuarioRol");

                    b.HasIndex("idUsuario");

                    b.ToTable("UsuariosRoles");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.UsuariosContacto", b =>
                {
                    b.HasOne("Wass.Back.Seguridad.Models.Entity.Usuarios", "usuarioContacto")
                        .WithOne("usuarioContacto")
                        .HasForeignKey("Wass.Back.Seguridad.Models.Entity.UsuariosContacto", "idUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("usuarioContacto");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.UsuariosRoles", b =>
                {
                    b.HasOne("Wass.Back.Seguridad.Models.Entity.Usuarios", "usuarioRoles")
                        .WithMany("usuarioRoles")
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("usuarioRoles");
                });

            modelBuilder.Entity("Wass.Back.Seguridad.Models.Entity.Usuarios", b =>
                {
                    b.Navigation("usuarioContacto");

                    b.Navigation("usuarioRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
