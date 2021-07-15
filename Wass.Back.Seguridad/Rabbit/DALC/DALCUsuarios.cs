using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Seguridad.Models.Entity;
using Wass.Back.Seguridad.Models.Enum;
using Wass.Back.Seguridad.Models.Peticiones.Usuario;
using Wass.Back.Seguridad.Rabbit.Context;

namespace Wass.Back.Seguridad.Rabbit.DALC
{
    public class DALCUsuarios
    {
		private readonly SeguridadContext _context;
		private readonly DALCTransacciones<Usuarios> _transact;


		public DALCUsuarios(SeguridadContext context)
		{
			_context = context;
			_transact = new DALCTransacciones<Usuarios>(context);
		}

		public async Task<Usuarios> Get(long idUsuario)
		{
			return await _context.Usuarios.Where(x => x.idUsuario == idUsuario)
				.Include(x => x.usuarioRoles)
				.Include(x => x.usuarioContacto)
				.FirstOrDefaultAsync();
		}

		public async Task<Usuarios> GetUsuarioByEmail(string email)
		{
			return await _context.Usuarios.Where(x => x.email == email && x.idEstado == 2)
				.Include(x => x.usuarioRoles)
				.Include(x => x.usuarioContacto)
				.FirstOrDefaultAsync();
		}

		public async Task<Usuarios> GetUserProspectoPorEmpresa(long idEmpresa)
		{
			return await _context.Usuarios.Where(x => x.idEmpresa == idEmpresa && x.idEmpleado == 0 && x.idEstado == (int)EstadoUsuario.Activo)
				.Include(x => x.usuarioRoles)
				.Include(x => x.usuarioContacto)
				.FirstOrDefaultAsync();
		}

		public async Task<List<Usuarios>> GetUserPorEmpresa(long idEmpresa)
		{
			return await _context.Usuarios.Where(x => x.idEmpresa == idEmpresa && x.idEstado == (int)EstadoUsuario.Activo)
				.Include(x => x.usuarioRoles)
				.Include(x => x.usuarioContacto)
				.ToListAsync();
		}


		public async Task<Usuarios> GetAutenticar(string email, string passw)
		{
			return await _context.Usuarios.Where(x => x.email == email && x.passw == passw && x.idEstado == (int)EstadoUsuario.Activo)
				.Include(x => x.usuarioRoles)
				.FirstOrDefaultAsync();
		}

		public async Task<Usuarios> Set(Usuarios objeto, Transaction transaccion)
		{
			switch (transaccion)
			{
				case Transaction.Insert:
					_context.Add(objeto);
					if (IsEmpty(objeto.usuarioContacto)) _context.Entry(objeto.usuarioContacto).State = EntityState.Detached;
					await _context.SaveChangesAsync();

					if (IsEmpty(objeto.usuarioContacto)) objeto.usuarioContacto = null;
					return objeto;

				case Transaction.Update:
					_context.Update(objeto);
					if (IsEmpty(objeto.usuarioContacto)) _context.Entry(objeto.usuarioContacto).State = EntityState.Detached;
					await _context.SaveChangesAsync();

					if (IsEmpty(objeto.usuarioContacto)) objeto.usuarioContacto = null;
					return objeto;

				default:

					return objeto;
			}
		}

		public async Task<Usuarios> Asociar(RequestAsociar asociar)
		{
			var find = await _context.Usuarios.FirstOrDefaultAsync(x => x.idUsuario == asociar.idUsuario);
			if (find != null)
			{
				find.idEmpresa = asociar.idEmpresa;
				find.idEmpleado = asociar.idEmpleado;
				find.idEstado = asociar.idEstado;

				await _transact.Actualizar(find);
				return find;
			}
			else
			{
				return null;
			}
		}

		public async Task<Usuarios> CambiarContrasena(RequestContrasena cambiar)
		{
			var find = await _context.Usuarios.FirstOrDefaultAsync(x => x.idUsuario == cambiar.idUsuario);
			if (find != null)
			{
				find.passw = cambiar.contrasenaNueva;

				await _transact.Actualizar(find);
				return find;
			}
			else
			{
				return null;
			}
		}

		private bool IsEmpty(UsuariosContacto contacto)
		{
			if (contacto == null) return false;

			var isNull = 0;
			if (string.IsNullOrWhiteSpace(contacto.nombres)) isNull += 1;
			if (string.IsNullOrWhiteSpace(contacto.apellidos)) isNull += 1;
			if (string.IsNullOrWhiteSpace(contacto.tipoDocumento)) isNull += 1;
			if (string.IsNullOrWhiteSpace(contacto.documento)) isNull += 1;
			if (string.IsNullOrWhiteSpace(contacto.nombreEmpresa)) isNull += 1;
			if (string.IsNullOrWhiteSpace(contacto.paginaWebEmpresa)) isNull += 1;
			if (string.IsNullOrWhiteSpace(contacto.telefonoEmpresa)) isNull += 1;

			return isNull == 7;
		}
	}
}
