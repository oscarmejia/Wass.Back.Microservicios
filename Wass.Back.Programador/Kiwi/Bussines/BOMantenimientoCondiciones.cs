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
	public class BOMantenimientoCondiciones
	{
		private readonly DALCMantenimientoCondiciones _dalc;

		public BOMantenimientoCondiciones(ProgramadorContext context)
		{
			_dalc = new DALCMantenimientoCondiciones(context);
		}

		public async Task<ResponseBase<CondicionesVariables>> Get(long id)
		{
			try
			{
				var plan = await _dalc.Get(id);

				return new ResponseBase<CondicionesVariables>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = plan
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<CondicionesVariables>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<CondicionesVariables>>> GetByIdPlan(long idPlan)
		{
			try
			{
				var plan = await _dalc.GetByIdPlan(idPlan);

				return new ResponseBase<List<CondicionesVariables>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = plan
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<CondicionesVariables>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<CondicionesVariables>>> GetAll()
		{
			try
			{
				var planes = await _dalc.GetAll();

				return new ResponseBase<List<CondicionesVariables>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = planes
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<CondicionesVariables>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<CondicionesVariables>> Set(CondicionesVariables objeto, Transaction transaccion)
		{
			try
			{
				var result = await _dalc.Set(objeto, transaccion);

				return new ResponseBase<CondicionesVariables>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = result
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<CondicionesVariables>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<MantenimientoCondiciones>> Orden(MantenimientoCondiciones objeto)
		{
			try
			{
				var result = await _dalc.Orden(objeto);

				return new ResponseBase<MantenimientoCondiciones>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = result
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<MantenimientoCondiciones>()
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
