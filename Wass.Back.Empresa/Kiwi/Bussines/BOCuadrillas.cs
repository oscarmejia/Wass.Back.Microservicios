using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Cuadrilla;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOCuadrillas
    {
		public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
		private readonly DALCCuadrillas _dalc;
		private readonly DALCTurnos _dalcTurnos;

		public BOCuadrillas(EmpresaContext context)
		{
			_dalc = new DALCCuadrillas(context);
			_dalcTurnos = new DALCTurnos(context);
		}

		public async Task<ResponseBase<Cuadrillas>> GetAsync(long id)
		{
			try
			{
				var obj = await _dalc.GetAsync(id);

				if (obj != null)
				{
					return new ResponseBase<Cuadrillas>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = obj
					};
				}
				else
				{
					return new ResponseBase<Cuadrillas>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El empleado consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Cuadrillas>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<CuadrillasRequest>> GetUbicacionAsync(long id)
		{
			try
			{
				var obj = await _dalc.GetAsync(id);

				if (obj != null)
				{
					var ubicacion = new CuadrillasRequest()
					{
						idCuadrilla = obj.idCuadrilla,
						idSede = obj.idSede,
						nombreA = obj.nombreA,
						nombreB = obj.nombreB,
						estado = obj.estado,
						email = obj.email,
						celular = obj.celular,
						zonaAtencion = !String.IsNullOrEmpty(obj.zonaAtencion) ? JsonConvert.DeserializeObject<UbicacionRequest>(obj.zonaAtencion) : new UbicacionRequest(),
						ubicacionActual = !String.IsNullOrEmpty(obj.ubicacionActual) ? JsonConvert.DeserializeObject<UbicacionRequest>(obj.ubicacionActual) : new UbicacionRequest(),
						numMiembros = obj.numMiembros,
						cuadrillaEmpleados = obj.cuadrillaEmpleados,
						cuadrillaTurnos = obj.cuadrillaTurnos
					};
					return new ResponseBase<CuadrillasRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = ubicacion
					};
				}
				else
				{
					return new ResponseBase<CuadrillasRequest>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El empleado consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<CuadrillasRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Cuadrillas>>> GetAllAsync()
		{
			try
			{
				var obj = await _dalc.GetAllAsync();

				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<Cuadrillas>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = obj
						};
					else
						return new ResponseBase<List<Cuadrillas>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay empleados disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<Cuadrillas>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de empleados no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Cuadrillas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Cuadrillas>>> GetPorSedeAsync(long idSede)
		{
			try
			{
				var obj = await _dalc.GetPorSedeAsync(idSede);

				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<Cuadrillas>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = obj
						};
					else
						return new ResponseBase<List<Cuadrillas>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay cuadrillas disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<Cuadrillas>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de cuadrillas no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Cuadrillas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Cuadrillas>>> GetPorEmpresaAsync(long idEmpresa)
		{
			try
			{
				var obj = await _dalc.GetPorEmpresaAsync(idEmpresa);

				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<Cuadrillas>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = obj
						};
					else
						return new ResponseBase<List<Cuadrillas>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay Cuadrillas disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<Cuadrillas>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de Cuadrillas no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Cuadrillas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Cuadrillas>>> GetPorEstadoAsync(int estado)
		{
			try
			{
				var obj = await _dalc.GetPorEstadoAsync(estado);

				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<Cuadrillas>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = obj
						};
					else
						return new ResponseBase<List<Cuadrillas>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay cuadrillas disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<Cuadrillas>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de cuadrillas no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Cuadrillas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Cuadrillas>> SetAsync(Cuadrillas objeto, Transaction transaccion)
		{
			try
			{
				if (transaccion == Transaction.Delete)
				{
					return new ResponseBase<Cuadrillas>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = $"La operación de eliminar cuadrillas no ha sido implementada.",
						datos = null
					};
				}
				else
				{
					var data = await _dalc.SetAsync(objeto, transaccion);
					if (data != null)
					{
						return new ResponseBase<Cuadrillas>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = $"Operación realizada con exito",
							datos = data
						};
					}
					else
						return new ResponseBase<Cuadrillas>()
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
				return new ResponseBase<Cuadrillas>()
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
