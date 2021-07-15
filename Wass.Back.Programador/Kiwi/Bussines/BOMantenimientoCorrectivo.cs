using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
	public class BOMantenimientoCorrectivo : IBOCrud<MantenimientoCorrectivo>
	{
		public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
		private readonly DALCMantenimientoCorrectivo _dalc;
		private readonly DALCOrdenesTrabajo _dalcOrdenesTrabajo;

		private readonly string _msg_base;

		public BOMantenimientoCorrectivo(ProgramadorContext context)
		{
			_dalc = new DALCMantenimientoCorrectivo(context);
			_dalcOrdenesTrabajo = new DALCOrdenesTrabajo(context);
			_msg_base = " mantenimiento correctivo";
		}

		#region Validacion de reglas de negocio
		//private async Task<ResponseBase<MantenimientoCorrectivo>> validarReglasNegocio(MantenimientoCorrectivo datos)
		//{
  //          var result = await validarEmpleadoCuadrilla(datos);
  //          if (!result.codigo.Equals((int)HttpStatusCode.OK))
  //              return result;

  //          if (datos.lider)
  //          {
  //              result = await validarLiderEquipo(datos);
  //              if (!result.codigo.Equals((int)HttpStatusCode.OK))
  //                  return result;
  //          }

  //          return await validarUnicidadCuadrilla(datos);
  //          return null;
		//}

		#endregion

		public async Task<ResponseBase<MantenimientoCorrectivo>> Get(long id)
		{
			try
			{
				var datos = await _dalc.Get(id);
				if (datos != null)
				{
					return new ResponseBase<MantenimientoCorrectivo>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<MantenimientoCorrectivo>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = $"La {_msg_base} no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<MantenimientoCorrectivo>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<MantenimientoCorrectivo>> GetPorOrdenAsync(long idOrden)
		{
			try
			{
				var datos = await _dalc.GetPorOrdenAsync(idOrden);
				if (datos != null)
				{
					return new ResponseBase<MantenimientoCorrectivo>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<MantenimientoCorrectivo>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = $"La {_msg_base} no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<MantenimientoCorrectivo>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<MantenimientoCorrectivo>>> GetAll()
		{
			try
			{
				var obj = await _dalc.GetAll();
				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<MantenimientoCorrectivo>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = obj
						};
					else
						return new ResponseBase<List<MantenimientoCorrectivo>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = $"No hay {_msg_base} disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<MantenimientoCorrectivo>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = $"La consulta de {_msg_base} no retornó resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<MantenimientoCorrectivo>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		
		public async Task<ResponseBase<List<MantenimientoCorrectivo>>> GetPorOrdenAvisoAsync(long idOrdenAviso)
		{
			try
			{
				var obj = await _dalc.GetPorOrdenAvisoAsync(idOrdenAviso);
				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<MantenimientoCorrectivo>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = obj
						};
					else
						return new ResponseBase<List<MantenimientoCorrectivo>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = $"No hay {_msg_base} disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<MantenimientoCorrectivo>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = $"La consulta de {_msg_base} no retornó resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<MantenimientoCorrectivo>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<MantenimientoCorrectivo>> Set(MantenimientoCorrectivo objeto, Transaction transaccion)
		{
			try
			{
				var data = await _dalc.Set(objeto, transaccion);
				if (data != null)
				{
					return new ResponseBase<MantenimientoCorrectivo>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación sobre {_msg_base} realizada con exito",
						datos = data
					};
				}
				else
					return new ResponseBase<MantenimientoCorrectivo>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
						datos = data
					};

			}
			catch (Exception ex)
			{
				return new ResponseBase<MantenimientoCorrectivo>()
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
