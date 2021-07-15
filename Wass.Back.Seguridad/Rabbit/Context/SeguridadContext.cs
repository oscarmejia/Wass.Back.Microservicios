using System;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Seguridad.Models.Entity;

namespace Wass.Back.Seguridad.Rabbit.Context
{
    public class SeguridadContext : DbContext
    {

        public SeguridadContext(DbContextOptions<SeguridadContext> options) : base(options)
        {}
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Acciones> Acciones { get; set; }
        public DbSet<GruposRoles> GruposRoles { get; set; }
        public DbSet<Grupos> Grupos { get; set; }
        public DbSet<RolMenuAccion> RolMenuAccion { get; set; }
        public DbSet<UsuariosRoles> UsuariosRoles { get; set; }
    }
}
