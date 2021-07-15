using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOEmpresaSoportes
    {
		public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
		private readonly DALCEmpresaSoportes _dalc;

		public BOEmpresaSoportes(EmpresaContext context)
		{
			_dalc = new DALCEmpresaSoportes(context);
		}

		public async Task<ResponseBase<EmpresaSoportes>> GetAsync(long id)
		{
			try
			{
				var empresa = await _dalc.GetAsync(id);

				if (empresa != null)
				{
					return new ResponseBase<EmpresaSoportes>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = empresa
					};
				}
				else
				{
					return new ResponseBase<EmpresaSoportes>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "No hay un documento de soprote disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<EmpresaSoportes>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<EmpresaSoportes>> GetAsync(Guid id)
		{
			try
			{
				var empresa = await _dalc.GetAsync(id);

				if (empresa != null)
				{
					return new ResponseBase<EmpresaSoportes>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = empresa
					};
				}
				else
				{
					return new ResponseBase<EmpresaSoportes>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "No hay un documento de soprote disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<EmpresaSoportes>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<EmpresaSoportes>>> GetPorEmpresaAsync(long id)
		{
			try
			{
				var empresa = await _dalc.GetPorEmpresaAsync(id);

				if (empresa != null)
				{
					return new ResponseBase<List<EmpresaSoportes>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = empresa
					};
				}
				else
				{
					return new ResponseBase<List<EmpresaSoportes>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "No hay un documento de soprote disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<EmpresaSoportes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<EmpresaSoportes>>> GetPorActivoEquipoAsync(Guid id)
		{
			try
			{
				var empresa = await _dalc.GetPorActivoEquipoAsync(id);

				if (empresa != null)
				{
					return new ResponseBase<List<EmpresaSoportes>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = empresa
					};
				}
				else
				{
					return new ResponseBase<List<EmpresaSoportes>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "No hay un documento de soprote disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<EmpresaSoportes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<EmpresaSoportes>>> GetPorActivoFlotaAsync(Guid id)
		{
			try
			{
				var empresa = await _dalc.GetPorActivoFlotaAsync(id);

				if (empresa != null)
				{
					return new ResponseBase<List<EmpresaSoportes>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = empresa
					};
				}
				else
				{
					return new ResponseBase<List<EmpresaSoportes>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "No hay un documento de soprote disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<EmpresaSoportes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<EmpresaSoportes>>> GetAllAsync()
		{
			try
			{
				var empresa = await _dalc.GetAllAsync();

				if (empresa != null)
				{
					return new ResponseBase<List<EmpresaSoportes>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = empresa
					};
				}
				else
				{
					return new ResponseBase<List<EmpresaSoportes>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "No hay un documento de soprote disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<EmpresaSoportes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<EmpresaSoportes>> SetAsync(EmpresaSoportes objeto, Transaction transaccion)
		{

			var data = await _dalc.SetAsync(objeto, transaccion);
			if (data != null)
			{
				return new ResponseBase<EmpresaSoportes>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = $"Operación realizada con exito",
					datos = data
				};
			}
			else
				return new ResponseBase<EmpresaSoportes>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"La operación solicitada no se pudo realizar.",
					datos = data
				};

		}
	}
}
