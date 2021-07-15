using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOEmpresaChecks
    {
		public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
		private readonly DALCEmpresaChecks _dalc;
		private readonly string _msg_base;

		public BOEmpresaChecks(EmpresaContext context)
		{
			_dalc = new DALCEmpresaChecks(context);
			_msg_base = " checks empresa ";
		}

		public async Task<ResponseBase<EmpresaChecks>> GetAsync(long id)
		{
			try
			{
				var datos = await _dalc.GetAsync(id);

				if (datos != null)
				{
					return new ResponseBase<EmpresaChecks>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<EmpresaChecks>()
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
				return new ResponseBase<EmpresaChecks>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<EmpresaChecks>>> GetPorEmpresaAsync(long idEmpresa)
		{
			try
			{
				var datos = await _dalc.GetPorEmpresaAsync(idEmpresa);

				if (datos != null)
				{
					return new ResponseBase<List<EmpresaChecks>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<List<EmpresaChecks>>()
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
				return new ResponseBase<List<EmpresaChecks>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<EmpresaChecks>>> GetTodasAsync()
		{
			try
			{
				var obj = await _dalc.GetAllAsync();
				var ob = new List<EmpresaChecks>();

				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<EmpresaChecks>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = ob
						};
					else
						return new ResponseBase<List<EmpresaChecks>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = $"No hay {_msg_base} disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<EmpresaChecks>>()
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
				return new ResponseBase<List<EmpresaChecks>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<EmpresaChecks>>> SetAsync(List<EmpresaChecks> _datos, Transaction transaccion)
		{
			try
			{
				if (transaccion == Transaction.Insert)
				{
					var list = await _dalc.GetPorEmpresaAsync(_datos[0].idEmpresa);
					var contains = list.Where(x => _datos.Select(z => z.idEmpresaCheck).Contains(x.idEmpresaCheck)).ToList();

					if (contains != null && contains.Count > 0)
					{
						return new ResponseBase<List<EmpresaChecks>>()
						{
							codigo = (int)HttpStatusCode.BadRequest,
							estado = true,
							mensaje = $"La operación realizada tiene elementos ya registrados o que no existen",
							datos = null
						};
					}
				}

				var data = await _dalc.SetAsync(_datos, transaccion);
				if (data != null)
				{
					return new ResponseBase<List<EmpresaChecks>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación sobre {_msg_base} realizada con exito",
						datos = _datos
					};
				}
				else
					return new ResponseBase<List<EmpresaChecks>>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
						datos = null
					};

			}
			catch (Exception ex)
			{
				return new ResponseBase<List<EmpresaChecks>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<EmpresaChecks>> ActivarInactivarCheckAsync(long idCheck, bool estado)
		{
			try
			{
				var find = await _dalc.GetAsync(idCheck);
				if (find != null)
				{
					var data = await _dalc.ActivarInactivarCheckAsync(find, estado);
					if (data != null)
					{
						return new ResponseBase<EmpresaChecks>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = $"Operación sobre {_msg_base} realizada con exito",
							datos = find
						};
					}
					else
						return new ResponseBase<EmpresaChecks>()
						{
							codigo = (int)HttpStatusCode.InternalServerError,
							estado = false,
							mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<EmpresaChecks>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<EmpresaChecks>()
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
