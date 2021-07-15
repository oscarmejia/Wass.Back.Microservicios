using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Seguridad.Models.Entity;
using Wass.Back.Seguridad.Models.Enum;
using Wass.Back.Seguridad.Models.Peticiones.Base;
using Wass.Back.Seguridad.Models.Peticiones.Usuario;
using Wass.Back.Seguridad.Rabbit.Context;
using Wass.Back.Seguridad.Rabbit.DALC;

namespace Wass.Back.Seguridad.Kiwi.Bussines
{
    public class BORoles
    {
		public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
		private readonly DALCRoles _dalc;

		public BORoles(SeguridadContext context)
		{
			_dalc = new DALCRoles(context);
		}

		public async Task<ResponseBase<List<UsuariosRoles>>> Get(long idUsuario)
		{
			try
			{
				var usuario = await _dalc.GetRolesUsuario(idUsuario);
				if (usuario != null)
				{
					return new ResponseBase<List<UsuariosRoles>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = usuario
					};
				}
				else
				{
					return new ResponseBase<List<UsuariosRoles>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El usuario consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<UsuariosRoles>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<UsuariosRoles>> Editar(UsuariosRoles roles)
		{
			try
			{
				var data = await _dalc.editarRolesUsuario(roles);
				if (data != null)
				{
					return new ResponseBase<UsuariosRoles>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación realizada con exito",
						datos = data
					};
				}
				else
				{
					return new ResponseBase<UsuariosRoles>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación solicitada no se pudo realizar.",
						datos = data
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<UsuariosRoles>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<UsuariosRoles>>> Set(RequestRoles datos, Transaction trans)
		{
			try
			{
				if (trans == Transaction.Delete)
				{
					return new ResponseBase<List<UsuariosRoles>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = $"La operación de eliminar menús no ha sido implementada.",
						datos = null
					};
				}
				else
				{
					var data = await _dalc.SetRolesUsuario(datos);
					if (data != null)
					{
						return new ResponseBase<List<UsuariosRoles>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = $"Operación realizada con exito",
							datos = data
						};
					}
					else
					{
						return new ResponseBase<List<UsuariosRoles>>()
						{
							codigo = (int)HttpStatusCode.InternalServerError,
							estado = false,
							mensaje = $"La operación solicitada no se pudo realizar.",
							datos = data
						};
					}
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<UsuariosRoles>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}
	}
}
