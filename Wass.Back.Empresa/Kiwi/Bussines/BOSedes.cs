using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Empresa;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
	public class BOSedes
	{
		public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
		private readonly DALCSedes _dalc;

		public BOSedes(EmpresaContext context)
		{
			_dalc = new DALCSedes(context);
		}

		public async Task<ResponseBase<Sedes>> GetSedeAsync(long idEmpresa)
		{
			try
			{
				var empresa = await _dalc.Get(idEmpresa);

				if (empresa != null)
				{
					return new ResponseBase<Sedes>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = empresa
					};
				}
				else
				{
					return new ResponseBase<Sedes>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "La sede consultada no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Sedes>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Sedes>>> GetSedesAsync(long idEmpresa)
		{
			try
			{
				var sedes = await _dalc.GetByEmpresa(idEmpresa);

				if (sedes != null)
				{
					if (sedes.Count > 0)
						return new ResponseBase<List<Sedes>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = "La empresa tiene las siguientes sedes asociadas",
							datos = sedes
						};
					else
						return new ResponseBase<List<Sedes>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = "La empresa consultada no tiene sedes asociadas.",
							datos = sedes
						};
				}
				else
				{
					return new ResponseBase<List<Sedes>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "La empresa consultada no tienes sedes disponibles.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Sedes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Sedes>> SaveAsync(RequestSede _data, Transaction trans)
		{
			var ob = JsonConvert.DeserializeObject<Sedes>(JsonConvert.SerializeObject(_data));

			if (trans == Transaction.Delete)
			{
				var result = await _dalc.Eliminar(_data.idSede);
				if (result.estado)
				{
					return new ResponseBase<Sedes>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación realizada con exito",
						datos = null
					};
				}
				else
				{
					return new ResponseBase<Sedes>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación solicitada no se pudo realizar.",
						datos = null
					};
				}

			}
			else
			{
				var data = await _dalc.Set(ob, trans);
				if (data != null)
				{
					return new ResponseBase<Sedes>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación realizada con exito",
						datos = data
					};
				}
				else
					return new ResponseBase<Sedes>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación solicitada no se pudo realizar.",
						datos = data
					};
			}
		}
	}
}
