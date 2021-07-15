using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
	public class BOPlanMantenimientoRondas
	{
		private readonly DALCPlanMantenimientoRondas _dalc;

		public BOPlanMantenimientoRondas(ProgramadorContext context)
		{
			_dalc = new DALCPlanMantenimientoRondas(context);
		}


		//--------------------
		//  RONDAS PLAN
		//--------------------
		public async Task<ResponseBase<PlanesRondas>> Get(long id)
		{
			try
			{
				var plan = await _dalc.Get(id);

				return new ResponseBase<PlanesRondas>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = plan
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<PlanesRondas>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanesRondas>>> GetPorCategoria(long idCategoria)
		{
			try
			{
				var planes = await _dalc.GetPorCategoria(idCategoria);

				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = planes
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanesRondas>>> GetAll()
		{
			try
			{
				var planes = await _dalc.GetAll();

				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = planes
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanesRondas>>> GetAllPorEmpresa(long idEmpresa)
		{
			try
			{
				var planes = await _dalc.GetAllPorEmpresa(idEmpresa);

				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = planes
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanesRondas>>> GetAllPorEmpresaTipoPlan(long idEmpresa, long tipoPlan)
		{
			try
			{
				var planes = await _dalc.GetAllPorEmpresaTipoPlan(idEmpresa, tipoPlan);

				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = planes
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<PlanesRondas>>> GetPorParametros(long idCategoria, long idClasificacion1, long idClasificacion2, long idSede, string marca)
		{
			try
			{
				var planes = await _dalc.GetPorParametros(idCategoria, idClasificacion1, idClasificacion2, idSede, marca);

				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = planes
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<PlanesRondas>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		

		public async Task<ResponseBase<PlanesRondas>> Set(PlanesRondas objeto, Transaction transaccion)
		{
			try
			{
				var result = await _dalc.Set(objeto, transaccion);

				return new ResponseBase<PlanesRondas>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = result
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<PlanesRondas>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		//-------------------
		//  VARIABLES
		//-------------------
		public async Task<ResponseBase<Grupos>> CrearGrupoVariables(Grupos grupo)
		{
			try
			{
				var vars = await _dalc.CrearGrupoVariables(grupo);
				if (vars != null)
				{
					return new ResponseBase<Grupos>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = String.Empty,
						datos = vars
					};
				}
				else
				{
					return new ResponseBase<Grupos>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "Algunas de las variables que desea ingresar no se encuentran registradas en el sistema.",
						datos = vars
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Grupos>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Grupos>>> ActualizarGrupo(Grupos grupo)
		{
			try
			{
				var vars = await _dalc.ActualizarGrupo(grupo);
				if (vars != null)
				{
					return new ResponseBase<List<Grupos>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = String.Empty,
						datos = vars
					};
				}
				else
				{
					return new ResponseBase<List<Grupos>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "Algunas de las variables que desea ingresar no se encuentran registradas en el sistema.",
						datos = vars
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Grupos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Grupos>>> QuitarGrupo(long idGrupo)
		{
			try
			{
				var vars = await _dalc.QuitarGrupo(idGrupo);

				return new ResponseBase<List<Grupos>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Grupos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Variables>>> AgregarVariables(long idGrupo, long idPlan, List<VariableEnGrupo> variables)
		{
			try
			{
				var vars = await _dalc.AgregarVariables(idGrupo, idPlan, variables);
				if (vars != null)
				{
					return new ResponseBase<List<Variables>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = String.Empty,
						datos = vars
					};
				}
				else
				{
					return new ResponseBase<List<Variables>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "Algunas de las variables que desea ingresar no se encuentran registradas en el sistema.",
						datos = vars
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Variables>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Variables>>> QuitarVariables(long idGrupo, long idPlan, long idVariable)
		{
			try
			{
				var vars = await _dalc.QuitarVariables(idGrupo, idPlan, idVariable);

				return new ResponseBase<List<Variables>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Variables>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposVariables>>> ConsultarGrupoId(long idGrupo)
		{
			try
			{
				var vars = await _dalc.ConsultarGrupoId(idGrupo);

				return new ResponseBase<List<GruposVariables>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposVariables>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Variables>>> ConsultarVariablesGrupoId(long idGrupo)
		{
			try
			{
				var vars = await _dalc.ConsultarVariablesGrupoId(idGrupo);

				return new ResponseBase<List<Variables>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Variables>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Variables>>> ConsultarVariablesIdClasificacion(long idClasificacion)
		{
			try
			{
				var vars = await _dalc.ConsultarVariablesIdClasificacion(idClasificacion);

				return new ResponseBase<List<Variables>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Variables>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposVariables>>> ConsultarGruposVariables()
		{
			try
			{
				var vars = await _dalc.ConsultarGruposVariables();

				return new ResponseBase<List<GruposVariables>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposVariables>>()
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
		public async Task<ResponseBase<List<GruposActivos>>> AsociarActivosPlan(long idPlan, List<Guid> idActivos)
		{
			try
			{
				var vars = await _dalc.AsociarActivosPlan(idPlan, idActivos);

				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposActivos>>> DesasociarActivosPlan(long idPlan, Guid idActivo)
		{
			try
			{
				var vars = await _dalc.DesasociarActivosPlan(idPlan, idActivo);

				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<GruposActivos>> AsociarActivoIdConGrupoIdAPlan(long idPlan, Guid idActivo)
		{
			try
			{
				var vars = await _dalc.AsociarActivoIdConGrupoIdAPlan(idPlan, idActivo);
				if (vars != null)
				{
					return new ResponseBase<GruposActivos>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = String.Empty,
						datos = vars
					};
				}
				else
				{
					return new ResponseBase<GruposActivos>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "El activo que desea asociar YA se encuentra registrado",
						datos = vars
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<GruposActivos>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposActivos>>> ConsultarActivosIdPlan(long idPlan)
		{
			try
			{
				var vars = await _dalc.ConsultarActivosIdPlan(idPlan);

				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposActivos>>> ConsultarActivosIdGrupoConIdPlan(long idPlan)
		{
			try
			{
				var vars = await _dalc.ConsultarActivosIdGrupoConIdPlan(idPlan);

				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<GruposActivos>>> ConsultarActivosIdActivoConIdPlan(Guid idActivo, long idPlan)
		{
			try
			{
				var vars = await _dalc.ConsultarActivosIdActivoConIdPlan(idActivo, idPlan);

				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<GruposActivos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		//--------------------
		//   RONDAS CREADAS
		//--------------------

		public async Task<ResponseBase<MantenimientoRondas>> SetPlanMantenimientoRondas(ActualizarRondasRequest objeto, Transaction transaccion)
		{
			try
			{
				var result = await _dalc.SetPlanMantenimientoRondas(objeto, transaccion);

				return new ResponseBase<MantenimientoRondas>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = result
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
		public async Task<ResponseBase<MantenimientoRondas>> ActualizarRondasConActivosGrupos(ActualizarRondasRequest data)
		{
			try
			{
				var vars = await _dalc.ActualizarRondasConActivosGrupos(data);
				if (vars != null)
				{
					return new ResponseBase<MantenimientoRondas>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = String.Empty,
						datos = vars
					};
				}
				else
				{
					return new ResponseBase<MantenimientoRondas>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "No existen Rondas de Mantenimiento relacionadas con los parámetros",
						datos = vars
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

		public async Task<ResponseBase<RespuestaActivosVariables>> ActualizarRondasConVariablesGrupos(ActualizarRondasVarsRequest data, string nombreVariable)
		{
			try
			{
				var vars = await _dalc.ActualizarRondasConVariableGrupos(data, nombreVariable);

				return new ResponseBase<RespuestaActivosVariables>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = vars
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<RespuestaActivosVariables>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<MantenimientoRondas>>> GetMantenimientoRondasPorPlan(long idPlan)
		{
			try
			{
				var parts = await _dalc.GetMantenimientoRondasPorPlan(idPlan);

				return new ResponseBase<List<MantenimientoRondas>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
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

		public async Task<ResponseBase<List<MantenimientoRondas>>> GetMantenimientoRondasPorPlanYActivo(long idPlan, Guid idActivo)
		{
			try
			{
				var parts = await _dalc.GetMantenimientoRondasPorPlanYActivo(idPlan, idActivo);

				return new ResponseBase<List<MantenimientoRondas>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = String.Empty,
					datos = parts
				};
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


	}
}
