using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.Turno;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOTurnos
    {
		public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
		private readonly DALCTurnos _dalc;

		public BOTurnos(EmpresaContext context)
		{
			_dalc = new DALCTurnos(context);
		}

		public async Task<ResponseBase<Turnos>> GetAsync(long id)
		{
			try
			{
				var obj = await _dalc.GetAsync(id);

				if (obj != null)
				{
					return new ResponseBase<Turnos>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = obj
					};
				}
				else
				{
					return new ResponseBase<Turnos>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El turno consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Turnos>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Turnos>>> GetAllAsync()
		{
			try
			{
				var obj = await _dalc.GetAllAsync();

				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<Turnos>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = obj
						};
					else
						return new ResponseBase<List<Turnos>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay turnos disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<Turnos>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de turnos no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Turnos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Turnos>>> GetPorCuadrillaAsync(long idCuadrilla)
		{
			try
			{
				var obj = await _dalc.GetPorCuadrillaAsync(idCuadrilla);

				if (obj != null)
				{
					return new ResponseBase<List<Turnos>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = obj
					};
				}
				else
				{
					return new ResponseBase<List<Turnos>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El turno consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Turnos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Turnos>>> GetPorEmpresaAsync(long idEmpresa)
		{
			try
			{
				var obj = await _dalc.GetPorEmpresaAsync(idEmpresa);

				if (obj != null)
				{
					return new ResponseBase<List<Turnos>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = obj
					};
				}
				else
				{
					return new ResponseBase<List<Turnos>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El turno consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Turnos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Turnos>> SetAsync(Turnos objeto, Transaction transaccion)
		{
			try
			{
				var data = await _dalc.SetAsync(objeto, transaccion);
				if (data != null)
				{
					return new ResponseBase<Turnos>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación realizada con exito",
						datos = data
					};
				}
				else
					return new ResponseBase<Turnos>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación solicitada no se pudo realizar.",
						datos = data
					};

			}
			catch (Exception ex)
			{
				return new ResponseBase<Turnos>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<CuadrillasTurnos>>> SetTurnosToCuadrillaAsync(RequestTurnosCuadrilla objeto, Transaction transaccion)
		{
			try
			{
				var respuesta = true;
				var find = await _dalc.GetPorCuadrillaRelacionAsync(objeto.idCuadrilla);
				var contains = find.Where(x => objeto.idTurnos.Contains(x.idTurno)).ToList();
				var turnoscuadrillas = new List<CuadrillasTurnos>();

				foreach (var item in objeto.idTurnos)
				{
					turnoscuadrillas.Add(new CuadrillasTurnos()
					{
						idCuadrillasTurnos = transaccion == Transaction.Insert ? 0 : find.FirstOrDefault(x => x.idCuadrilla == objeto.idCuadrilla && x.idTurno == item).idCuadrillasTurnos,
						idCuadrilla = objeto.idCuadrilla,
						idTurno = item
					});
				}

				switch (transaccion)
				{
					case Transaction.Insert:
						foreach (var item in objeto.idTurnos)
						{
							turnoscuadrillas.Add(new CuadrillasTurnos()
							{
								idCuadrilla = objeto.idCuadrilla,
								idTurno = item
							});
						}

						if (contains != null && contains.Count > 0) respuesta = false;
						break;
					case Transaction.Delete:
						if (contains == null || contains.Count == 0) respuesta = false;
						break;
				}
				if (!respuesta)
				{
					return new ResponseBase<List<CuadrillasTurnos>>()
					{
						codigo = (int)HttpStatusCode.BadRequest,
						estado = true,
						mensaje = $"La operación realizada tiene elementos ya registrados o que no existen",
						datos = null
					};
				}

				var data = await _dalc.SetTurnosToCuadrillaAsync(turnoscuadrillas, transaccion);
				if (data != null)
				{
					return new ResponseBase<List<CuadrillasTurnos>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación realizada con exito",
						datos = data
					};
				}
				else
					return new ResponseBase<List<CuadrillasTurnos>>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación solicitada no se pudo realizar.",
						datos = data
					};

			}
			catch (Exception ex)
			{
				return new ResponseBase<List<CuadrillasTurnos>>()
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
