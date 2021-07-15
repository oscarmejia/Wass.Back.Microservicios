using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Seguridad.Models.Entity;
using Wass.Back.Seguridad.Models.Peticiones.Usuario;
using Wass.Back.Seguridad.Rabbit.Context;

namespace Wass.Back.Seguridad.Rabbit.DALC
{
    public class DALCRoles
    {
		private readonly SeguridadContext _context;
		private readonly DALCTransacciones<UsuariosRoles> _transact;

		public DALCRoles(SeguridadContext context)
		{
			_context = context;
			_transact = new DALCTransacciones<UsuariosRoles>(context);
		}

		public async Task<List<UsuariosRoles>> GetRolesUsuario(long idUsuario)
		{
			return await _context.UsuariosRoles.Where(x => x.idUsuario == idUsuario).ToListAsync();
		}

		public async Task<bool> DeleteRolesUsuario(long idUsuario)
		{
			var find = await _context.UsuariosRoles.Where(x => x.idUsuario == idUsuario).ToListAsync();

			_context.UsuariosRoles.RemoveRange(find);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<List<UsuariosRoles>> SetRolesUsuario(RequestRoles roles)
		{
			var list = new List<UsuariosRoles>();
			foreach (var item in roles.Roles)
			{
				list.Add(new UsuariosRoles()
				{
					idRol = item.idRol,
					idUsuario = roles.idUsuario,
					creador = roles.creador,
					activo = item.activo,
					fechaCreacion = DateTime.UtcNow.AddHours(-5)
				});
			}

			_context.UsuariosRoles.AddRange(list);
			await _context.SaveChangesAsync();

			return await _context.UsuariosRoles.Where(x => x.idUsuario == roles.idUsuario).ToListAsync(); ;
		}

		public async Task<UsuariosRoles> editarRolesUsuario(UsuariosRoles roles)
		{
			_context.Update(roles);
			await _context.SaveChangesAsync();

			return roles;
		}
	}
}
