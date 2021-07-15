using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
    public class BOMantenimientoPreventivo
    {
		private readonly DALCMantenimientoPreventivo _dalc;
		private readonly DALCOrdenesTrabajo _dalcOrdenesTrabajo;

		private readonly string _msg_base;

		public BOMantenimientoPreventivo(ProgramadorContext context)
		{
			_dalc = new DALCMantenimientoPreventivo(context);
			_dalcOrdenesTrabajo = new DALCOrdenesTrabajo(context);
			_msg_base = " mantenimiento preventivo";
		}

		public async Task<ResponseBase<MantenimientoPreventivoRequest>> Get(long idMantenimientoPreventivo)
		{
			try
			{
				var datos = await _dalc.Get(idMantenimientoPreventivo);
				var ob = new MantenimientoPreventivoRequest()
				{
					idMantenimientoPreventivo = datos.idMantenimientoPreventivo,
					idOrden = datos.idOrden,
					idPlan = datos.idPlan,
					idGrupo = datos.idGrupo,
					parada = datos.parada,
					eliminado = datos.eliminado,
					orden = datos.orden,
					fechaPropuestaProgramacion = datos.fechaPropuestaProgramacion,
					
					Acciones = datos != null ? JsonConvert.DeserializeObject<List<string>>(datos.Acciones) : new List<string>(),
				};
				if (datos != null)
				{
					return new ResponseBase<MantenimientoPreventivoRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = ob
					};
				}
				else
				{
					return new ResponseBase<MantenimientoPreventivoRequest>()
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
				return new ResponseBase<MantenimientoPreventivoRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<MantenimientoPreventivoRequest>> GetPorOrdenAsync(long idOrden)
		{
			try
			{
				var datos = await _dalc.GetPorOrdenAsync(idOrden);
				var ob = new MantenimientoPreventivoRequest()
				{
					idMantenimientoPreventivo = datos.idMantenimientoPreventivo,
					idOrden = datos.idOrden,
					idPlan = datos.idPlan,
					idGrupo = datos.idGrupo,
					fechaPropuestaProgramacion = datos.fechaPropuestaProgramacion,
					parada = datos.parada,
					eliminado = datos.eliminado,
					orden = datos.orden,
					Acciones = datos != null ? JsonConvert.DeserializeObject<List<string>>(datos.Acciones) : new List<string>(),
				};
				if (datos != null)
				{
					return new ResponseBase<MantenimientoPreventivoRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = ob
					};
				}
				else
				{
					return new ResponseBase<MantenimientoPreventivoRequest>()
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
				return new ResponseBase<MantenimientoPreventivoRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<MantenimientoPreventivoRequest>>> GetAll()
		{
			try
			{
				var obj = await _dalc.GetAll();
				var ob = new List<MantenimientoPreventivoRequest>();
				foreach(var item in obj)
                {
					ob.Add(new MantenimientoPreventivoRequest()
					{
						idMantenimientoPreventivo = item.idMantenimientoPreventivo,
						idOrden = item.idOrden,
						idPlan = item.idPlan,
						idGrupo = item.idGrupo,
						fechaPropuestaProgramacion = item.fechaPropuestaProgramacion,
						parada = item.parada,
						eliminado = item.eliminado,
						orden = item.orden,
						Acciones = item != null ? JsonConvert.DeserializeObject<List<string>>(item.Acciones) : new List<string>(),
					});
                }

				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<MantenimientoPreventivoRequest>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = ob
						};
					else
						return new ResponseBase<List<MantenimientoPreventivoRequest>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = $"No hay {_msg_base} disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<MantenimientoPreventivoRequest>>()
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
				return new ResponseBase<List<MantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<MantenimientoPreventivoRequest>>> GetAllPorPlan(long idPlan)
		{
			try
			{
				var obj = await _dalc.GetAllPorPlan(idPlan);
				var ob = new List<MantenimientoPreventivoRequest>();
				foreach (var item in obj)
				{
					ob.Add(new MantenimientoPreventivoRequest()
					{
						idMantenimientoPreventivo = item.idMantenimientoPreventivo,
						idOrden = item.idOrden,
						idPlan = item.idPlan,
						idGrupo = item.idGrupo,
						fechaPropuestaProgramacion = item.fechaPropuestaProgramacion,
						parada = item.parada,
						eliminado = item.eliminado,
						orden = item.orden,
						Acciones = item != null ? JsonConvert.DeserializeObject<List<string>>(item.Acciones) : new List<string>(),
					});
				}

				if (obj != null)
				{
					if (obj.Count > 0)
						return new ResponseBase<List<MantenimientoPreventivoRequest>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = ob
						};
					else
						return new ResponseBase<List<MantenimientoPreventivoRequest>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = $"No hay {_msg_base} disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<MantenimientoPreventivoRequest>>()
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
				return new ResponseBase<List<MantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		public async Task<ResponseBase<MantenimientoPreventivoRequest>> Set(MantenimientoPreventivoRequest objeto, Transaction transaccion)
		{
			try
			{
				var data_transformada = new MantenimientoPreventivo()
				{
					idMantenimientoPreventivo = objeto.idMantenimientoPreventivo,
					idOrden = objeto.idOrden,
					idPlan = objeto.idPlan,
					idGrupo = objeto.idGrupo,
					fechaPropuestaProgramacion = objeto.fechaPropuestaProgramacion,
					parada = objeto.parada,
					eliminado = objeto.eliminado,
					Acciones = JsonConvert.SerializeObject(objeto.Acciones).ToString(),
				};
				var datos = await _dalc.Set(data_transformada, transaccion);
				var ob = new MantenimientoPreventivoRequest()
				{
					idMantenimientoPreventivo = datos.idMantenimientoPreventivo,
					idOrden = datos.idOrden,
					idPlan = datos.idPlan,
					idGrupo = datos.idGrupo,
					fechaPropuestaProgramacion = datos.fechaPropuestaProgramacion,
					parada = datos.parada,
					eliminado = datos.eliminado,
					Acciones = datos != null ? JsonConvert.DeserializeObject<List<string>>(datos.Acciones) : new List<string>(),
				};
				if (datos != null)
				{
					return new ResponseBase<MantenimientoPreventivoRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación sobre {_msg_base} realizada con exito",
						datos = ob
					};
				}
				else
					return new ResponseBase<MantenimientoPreventivoRequest>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
						datos = null
					};

			}
			catch (Exception ex)
			{
				return new ResponseBase<MantenimientoPreventivoRequest>()
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
