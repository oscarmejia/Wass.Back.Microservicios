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
    public class BOMantenimientoRondas
    {
		private readonly DALCMantenimientoRondas _dalc;
		private readonly DALCOrdenesTrabajo _dalcOrdenesTrabajo;

		private readonly string _msg_base;

		public BOMantenimientoRondas(ProgramadorContext context)
		{
			_dalc = new DALCMantenimientoRondas(context);
			_dalcOrdenesTrabajo = new DALCOrdenesTrabajo(context);
			_msg_base = " mantenimiento rondas";
		}

		public async Task<ResponseBase<MantenimientoRondas>> Get(long idRonda)
		{
			try
			{
				var datos = await _dalc.Get(idRonda);
				
				if (datos != null)
				{
					
					return new ResponseBase<MantenimientoRondas>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<MantenimientoRondas>()
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
				return new ResponseBase<MantenimientoRondas>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<MantenimientoRondas>> GetPorOrdenAsync(long idOrden)
		{
			try
			{
				var datos = await _dalc.GetPorOrdenAsync(idOrden);
				if (datos != null)
				{
					return new ResponseBase<MantenimientoRondas>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<MantenimientoRondas>()
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
				return new ResponseBase<MantenimientoRondas>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<MantenimientoRondas>>> GetAll()
		{
			try
			{
				var obj = await _dalc.GetAll();
				

				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<MantenimientoRondas>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = obj
						};
					else
						return new ResponseBase<List<MantenimientoRondas>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = $"No hay {_msg_base} disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<MantenimientoRondas>>()
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
				return new ResponseBase<List<MantenimientoRondas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<MantenimientoRondas>>> GetAllPorPlan(long idPlan)
		{
			try
			{
				var obj = await _dalc.GetAllPorPlan(idPlan);


				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<MantenimientoRondas>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = obj
						};
					else
						return new ResponseBase<List<MantenimientoRondas>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = $"No hay {_msg_base} disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<MantenimientoRondas>>()
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
				return new ResponseBase<List<MantenimientoRondas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		public async Task<ResponseBase<MantenimientoRondas>> Set(MantenimientoRondas objeto, Transaction transaccion)
		{
			try
			{
				
				var datos = await _dalc.Set(objeto, transaccion);
				
				if (datos != null)
				{
					return new ResponseBase<MantenimientoRondas>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación sobre {_msg_base} realizada con exito",
						datos = datos
					};
				}
				else
					return new ResponseBase<MantenimientoRondas>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
						datos = null
					};

			}
			catch (Exception ex)
			{
				return new ResponseBase<MantenimientoRondas>()
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
