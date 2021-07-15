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
    public class BOPlanMantenimientoPreventivo
    {
		private readonly DALCPlanMantenimientoPreventivo _dalc;

		public BOPlanMantenimientoPreventivo(ProgramadorContext context)
		{
			_dalc = new DALCPlanMantenimientoPreventivo(context);
		}


		//--------------------
		//  MANTENIMIENTO PREVENTIVO PLAN
		//--------------------
		public async Task<ResponseBase<PlanesMantenimientoPreventivoRequest>> Get(long id)
		{
			try
			{
				var plan = await _dalc.Get(id);
				var activos = new List<GruposActivosMantenimientoPreventivoRequest>();
				if(plan.GruposActivos != null)
                {
					foreach (var item in plan.GruposActivos)
					{
						activos.Add(new GruposActivosMantenimientoPreventivoRequest()
						{
							id = item.id,
							Activo = JsonConvert.DeserializeObject<detalleActivosRequest>(item.Activo),
							idPlan = item.idPlan,
							estado = item.estado
						});
					}
				}
				var respuesta = new PlanesMantenimientoPreventivoRequest()
				{
					idPlan = plan.idPlan,
					idClasificacion1 = plan.idClasificacion1,
					idClasificacion2 = plan.idClasificacion2,
					idCategoria = plan.idCategoria,
					prioridad = plan.prioridad,
					idEmpresa = plan.idEmpresa,
					idSede = plan.idSede,
					marca = plan.marca,
					GruposActivos = activos,
					GruposPartes = plan.GruposPartes,
					GruposAcciones = plan.GruposAcciones,
					estado =plan.estado
				};

				return new ResponseBase<PlanesMantenimientoPreventivoRequest>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = respuesta
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<PlanesMantenimientoPreventivoRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanesMantenimientoPreventivoRequest>>> GetPorCategoria(long idCategoria)
		{
			try
			{
				var planes = await _dalc.GetPorCategoria(idCategoria);
				var activos = new List<GruposActivosMantenimientoPreventivoRequest>();
				var respuesta = new List<PlanesMantenimientoPreventivoRequest>();
				foreach(var plan in planes)
                {
					foreach (var item in plan.GruposActivos)
					{
						activos.Add(new GruposActivosMantenimientoPreventivoRequest()
						{
							id = item.id,
							Activo = JsonConvert.DeserializeObject<detalleActivosRequest>(item.Activo),
							idPlan = item.idPlan,
							estado = item.estado
						});
					}
					respuesta.Add(new PlanesMantenimientoPreventivoRequest()
					{
						idPlan = plan.idPlan,
						idClasificacion1 = plan.idClasificacion1,
						idClasificacion2 = plan.idClasificacion2,
						idCategoria = plan.idCategoria,
						prioridad = plan.prioridad,
						idEmpresa = plan.idEmpresa,
						idSede = plan.idSede,
						marca = plan.marca,
						GruposActivos = activos,
						GruposPartes = plan.GruposPartes,
						GruposAcciones = plan.GruposAcciones,
						estado = plan.estado
					});
				}

				return new ResponseBase<List<PlanesMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = respuesta
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanesMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		public async Task<ResponseBase<List<PlanesMantenimientoPreventivoRequest>>> GetAllPorEmpresa(long idEmpresa)
		{
			try
			{
				var planes = await _dalc.GetAllPorEmpresa(idEmpresa);
				var activos = new List<GruposActivosMantenimientoPreventivoRequest>();
				var respuesta = new List<PlanesMantenimientoPreventivoRequest>();
				foreach (var plan in planes)
				{
					foreach (var item in plan.GruposActivos)
					{
						activos.Add(new GruposActivosMantenimientoPreventivoRequest()
						{
							id = item.id,
							Activo = JsonConvert.DeserializeObject<detalleActivosRequest>(item.Activo),
							idPlan = item.idPlan,
							estado = item.estado
						});
					}
					respuesta.Add(new PlanesMantenimientoPreventivoRequest()
					{
						idPlan = plan.idPlan,
						idClasificacion1 = plan.idClasificacion1,
						idClasificacion2 = plan.idClasificacion2,
						idCategoria = plan.idCategoria,
						prioridad = plan.prioridad,
						idEmpresa = plan.idEmpresa,
						idSede = plan.idSede,
						marca = plan.marca,
						GruposActivos = activos,
						GruposPartes = plan.GruposPartes,
						GruposAcciones = plan.GruposAcciones,
						estado = plan.estado
					});
				}

				return new ResponseBase<List<PlanesMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = respuesta
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanesMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}
		public async Task<ResponseBase<List<PlanesMantenimientoPreventivoRequest>>> GetAll()
		{
			try
			{
				var planes = await _dalc.GetAll();
				var activos = new List<GruposActivosMantenimientoPreventivoRequest>();
				var respuesta = new List<PlanesMantenimientoPreventivoRequest>();
				foreach (var plan in planes)
				{
					foreach (var item in plan.GruposActivos)
					{
						activos.Add(new GruposActivosMantenimientoPreventivoRequest()
						{
							id = item.id,
							Activo = JsonConvert.DeserializeObject<detalleActivosRequest>(item.Activo),
							idPlan = item.idPlan,
							estado = item.estado
						});
					}
					respuesta.Add(new PlanesMantenimientoPreventivoRequest()
					{
						idPlan = plan.idPlan,
						idClasificacion1 = plan.idClasificacion1,
						idClasificacion2 = plan.idClasificacion2,
						idCategoria = plan.idCategoria,
						prioridad = plan.prioridad,
						idEmpresa = plan.idEmpresa,
						idSede = plan.idSede,
						marca = plan.marca,
						GruposActivos = activos,
						GruposPartes = plan.GruposPartes,
						GruposAcciones = plan.GruposAcciones,
						estado = plan.estado
					});
				}

				return new ResponseBase<List<PlanesMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = respuesta
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanesMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanesMantenimientoPreventivoRequest>>> GetPorParametros(long idCategoria, long idClasificacion1, long idClasificacion2, long idSede, string marca)
		{
			try
			{
				var planes = await _dalc.GetPorParametros(idCategoria, idClasificacion1, idClasificacion2, idSede, marca);
				var activos = new List<GruposActivosMantenimientoPreventivoRequest>();
				var respuesta = new List<PlanesMantenimientoPreventivoRequest>();
				foreach (var plan in planes)
				{
					foreach (var item in plan.GruposActivos)
					{
						activos.Add(new GruposActivosMantenimientoPreventivoRequest()
						{
							id = item.id,
							Activo = JsonConvert.DeserializeObject<detalleActivosRequest>(item.Activo),
							idPlan = item.idPlan,
							estado = item.estado
						});
					}
					respuesta.Add(new PlanesMantenimientoPreventivoRequest()
					{
						idPlan = plan.idPlan,
						idClasificacion1 = plan.idClasificacion1,
						idClasificacion2 = plan.idClasificacion2,
						idCategoria = plan.idCategoria,
						prioridad = plan.prioridad,
						idEmpresa = plan.idEmpresa,
						idSede = plan.idSede,
						marca = plan.marca,
						GruposActivos = activos,
						GruposPartes = plan.GruposPartes,
						GruposAcciones = plan.GruposAcciones,
						estado = plan.estado 
					});
				}

				return new ResponseBase<List<PlanesMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = respuesta
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanesMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		
		public async Task<ResponseBase<PlanesMantenimientoPreventivo>> Set(PlanesMantenimientoPreventivo objeto, Transaction transaccion)
		{
			try
			{
				var result = await _dalc.Set(objeto, transaccion);

				return new ResponseBase<PlanesMantenimientoPreventivo>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = result
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<PlanesMantenimientoPreventivo>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		//-------------------
		//  PARTES
		//-------------------
		public async Task<ResponseBase<GruposMantenimientoPreventivo>> CrearGrupoPartes(GruposMantenimientoPreventivo grupo)
		{
			try
			{
				var parts = await _dalc.CrearGrupoPartes(grupo);

				return new ResponseBase<GruposMantenimientoPreventivo>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<GruposMantenimientoPreventivo>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		
		public async Task<ResponseBase<List<GruposMantenimientoPreventivo>>> QuitarGrupo(long idGrupo)
		{
			try
			{
				var parts = await _dalc.QuitarGrupo(idGrupo);

				return new ResponseBase<List<GruposMantenimientoPreventivo>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposMantenimientoPreventivo>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposPartes>>> AgregarPartes(long idGrupo, long idPlan, List<Guid> partes)
		{
			try
			{
				var parts = await _dalc.AgregarPartes(idGrupo, idPlan, partes);

				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposPartes>>> QuitarPartes(long idGrupo, long idPlan, Guid idPartes)
		{
			try
			{
				var parts = await _dalc.QuitarPartes(idGrupo, idPlan, idPartes);

				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposPartes>>> ConsultarGrupoId(long idGrupo)
		{
			try
			{
				var parts = await _dalc.ConsultarPartesGrupoId(idGrupo);

				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposPartes>>> ConsultarPartesGrupoId(long idGrupo)
		{
			try
			{
				var parts = await _dalc.ConsultarPartesGrupoId(idGrupo);

				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposPartes>>> ConsultarPartesIdClasificacion(long idClasificacion)
		{
			try
			{
				var parts = await _dalc.ConsultarPartesIdClasificacion(idClasificacion);

				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<GruposPartes>> actualizarParada(long idGrupo, long idPlan)
		{
			try
			{
				var data = await _dalc.actualizarParada(idGrupo, idPlan);


				return new ResponseBase<GruposPartes>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = $"Operación sobre el Grupo Partes realizada con exito",
					datos = data
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<GruposPartes>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}
		public async Task<ResponseBase<List<GruposPartes>>> ConsultarGruposPartes()
		{
			try
			{
				var parts = await _dalc.ConsultarGruposPartes();

				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposPartes>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		//-------------------
		//  ACCIONES
		//-------------------
		public async Task<ResponseBase<List<GruposAcciones>>> AsociarAccionesPlan(long idPlan, long idGrupo, List<long> idAcciones)
		{
			try
			{
				var parts = await _dalc.AsociarAccionesPlan(idPlan, idGrupo, idAcciones);

				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}



		public async Task<ResponseBase<List<GruposAcciones>>> AgregarAccionIdConGrupoIdAPlan(long idPlan, long idGrupo, long idAccion)
		{
			try
			{
				var parts = await _dalc.AgregarAccionIdConGrupoIdAPlan(idPlan, idGrupo, idAccion);

				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposAcciones>>> QuitarAcciones(long idGrupo, long idPlan, long idAccion)
		{
			try
			{
				var parts = await _dalc.QuitarAcciones(idGrupo, idPlan, idAccion);

				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposAcciones>>> ConsultarAccionesGrupoId(long idGrupo)
		{
			try
			{
				var parts = await _dalc.ConsultarAccionesGrupoId(idGrupo);

				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		
		public async Task<ResponseBase<List<GruposAcciones>>> ConsultarGruposAcciones()
		{
			try
			{
				var parts = await _dalc.ConsultarGruposAcciones();

				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposAcciones>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		//----------------
		//   ACTIVOS 
		//----------------
		public async Task<ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>> AsociarActivosPlan(long idPlan, List<detalleActivosRequest> Activos)
		{
			try
			{
				var parts = await _dalc.AsociarActivosPlan(idPlan, Activos);

				return new ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>> DesasociarActivosPlan(long idPlan, string idActivo)
		{
			try
			{
				var parts = await _dalc.DesasociarActivosPlan(idPlan, idActivo);

				return new ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<GruposActivosMantenimientoPreventivoRequest>> AsociarActivoIdConGrupoIdAPlan(long idPlan, detalleActivosRequest Activo)
		{
			try
			{
				var parts = await _dalc.AsociarActivoIdConGrupoIdAPlan(idPlan, Activo);

				return new ResponseBase<GruposActivosMantenimientoPreventivoRequest>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<GruposActivosMantenimientoPreventivoRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>> ConsultarActivosIdPlan(long idPlan)
		{
			try
			{
				var parts = await _dalc.ConsultarActivosIdPlan(idPlan);

				return new ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		
		public async Task<ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>> ConsultarActivosIdActivoConIdPlan(string idActivo, long idPlan)
		{
			try
			{
				var parts = await _dalc.ConsultarActivosIdActivoConIdPlan(idActivo, idPlan);

				return new ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		//--------------------
		//   MANTENIMIENTOS PREVENTIVOS CREADOS
		//--------------------
		public async Task<ResponseBase<PlanMantenimientoPreventivo>> ActualizarMantenimientoPreventivoConActivosGrupos(long idPlanMantenimientoPreventivo, long idPlan, long idGrupo, string idActivo, DateTime fecha)
		{
			try
			{
				var parts = await _dalc.ActualizarMantenimientoPreventivoConActivosGrupos(idPlanMantenimientoPreventivo, idPlan, idGrupo, idActivo, fecha);

				return new ResponseBase<PlanMantenimientoPreventivo>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<PlanMantenimientoPreventivo>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanMantenimientoPreventivo>>> GetTodosMantenimientoPreventivo()
		{
			try
			{
				var parts = await _dalc.GetTodosMantenimientoPreventivo();

				return new ResponseBase<List<PlanMantenimientoPreventivo>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanMantenimientoPreventivo>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanMantenimientoPreventivo>>> GetMantenimientoPreventivoPorPlan(long idPlan)
		{
			try
			{
				var parts = await _dalc.GetMantenimientoPreventivoPorPlan(idPlan);

				return new ResponseBase<List<PlanMantenimientoPreventivo>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanMantenimientoPreventivo>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanMantenimientoPreventivo>>>  GetMantenimientoPreventivoPorPlanYActivo(long idPlan, string idActivo)
		{
			try
			{
				var parts = await _dalc.GetMantenimientoPreventivoPorPlanYActivo(idPlan, idActivo);

				return new ResponseBase<List<PlanMantenimientoPreventivo>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanMantenimientoPreventivo>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<PlanMantenimientoPreventivo>> SetPlanMantenimientoPreventivo(PlanMantenimientoPreventivo objeto, Transaction transaccion)
		{
			try
			{
				var result = await _dalc.SetPlanMantenimientoPreventivo(objeto, transaccion);

				return new ResponseBase<PlanMantenimientoPreventivo>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = result
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<PlanMantenimientoPreventivo>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<PlanMantenimientoPreventivo>> EliminarPlanMantenimientoPreventivo(long idPlanMantenimientoPreventivo)
		{
			try
			{
				var data = await _dalc.EliminarPlanMantenimientoPreventivo(idPlanMantenimientoPreventivo);


				return new ResponseBase<PlanMantenimientoPreventivo>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = $"Operación sobre Plan Mantenimiento realizada con exito",
					datos = data
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<PlanMantenimientoPreventivo>()
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
