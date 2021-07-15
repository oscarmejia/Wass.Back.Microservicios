using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.Interface;
using WASS.Back.Programador.core.rabbit.DALC;

namespace Wass.Back.Programador.Rabbit.DALC
{
	public class DALCPlanMantenimientoRondas : IDALCCrud<PlanesRondas>
	{
		private readonly ProgramadorContext _context;
		private readonly DALCTransacciones<PlanesRondas> _transact;
		private readonly DALCMantenimientoCondiciones _condiciones;
		private readonly DALCOrdenesTrabajo _ordenes;
		private readonly DALCMantenimientoAviso dALCMantenimientoAviso;
		private readonly DALCOrdenesTrabajo _dalcOrdenesTrabajo;

		public DALCPlanMantenimientoRondas(ProgramadorContext context)
		{
			_context = context;
			_transact = new DALCTransacciones<PlanesRondas>(context);
			_condiciones = new DALCMantenimientoCondiciones(context);
			_ordenes = new DALCOrdenesTrabajo(context);
			dALCMantenimientoAviso = new DALCMantenimientoAviso(context);
			_dalcOrdenesTrabajo = new DALCOrdenesTrabajo(context);
		}


		//----------------
		//   RONDAS PLAN
		//----------------
		public async Task<PlanesRondas> Get(long id)
		{
			//'return await _context.MantenimientoRondas.Where(x => x.idRonda == id).FirstOrDefaultAsync();'
			var sql = (from r in _context.PlanesRondas
					   //join g in _context.Grupos on r.idPlan equals g.idPlan
					   select new PlanesRondas()
					   {
						   idPlan = r.idPlan,
						   tipoPlan = r.tipoPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   GruposActivos = _context.GruposActivos.Where(a => a.idPlan == id && a.estado).ToList(),
						   GruposVariables = _context.GruposVariables.Where(a => a.idPlan == id && a.estado).ToList()
					   }).Where(x => x.idPlan == id).FirstOrDefaultAsync();

			return await sql;
		}

		public async Task<List<PlanesRondas>> GetPorCategoria(long idCategoria)
		{
			//'return await _context.MantenimientoRondas.Where(x => x.idCategoria == idCategoria).ToListAsync();'
			var sql = (from r in _context.PlanesRondas
					   //join g in _context.Grupos on r.idPlan equals g.idPlan
					   select new PlanesRondas()
					   {
						   idPlan = r.idPlan,
						   tipoPlan = r.tipoPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   GruposActivos = _context.GruposActivos.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposVariables = _context.GruposVariables.Where(a => a.idGrupo == r.idPlan && a.estado).ToList()
					   }).Where(x => x.idCategoria == idCategoria).ToListAsync();

			return await sql;
		}

		public async Task<List<PlanesRondas>> GetAll()
		{
			//'return await _context.MantenimientoRondas.ToListAsync();'
			var sql = (from r in _context.PlanesRondas
					   //join g in _context.Grupos on r.idPlan equals g.idPlan
					   select new PlanesRondas()
					   {
						   idPlan = r.idPlan,
						   tipoPlan = r.tipoPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   GruposActivos = _context.GruposActivos.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposVariables = _context.GruposVariables.Where(a => a.idPlan == r.idPlan && a.estado).ToList()
					   }).ToListAsync();

			return await sql;
		}

		public async Task<List<PlanesRondas>> GetAllPorEmpresa(long idEmpresa)
		{
			var sql = (from r in _context.PlanesRondas
					   //join g in _context.Grupos on r.idPlan equals g.idPlan
					   select new PlanesRondas()
					   {
						   idPlan = r.idPlan,
						   tipoPlan = r.tipoPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   GruposActivos = _context.GruposActivos.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposVariables = _context.GruposVariables.Where(a => a.idPlan == r.idPlan && a.estado).ToList()
					   }).Where(x => x.idEmpresa == idEmpresa)
					   .ToListAsync();

			return await sql;
		}

		public async Task<List<PlanesRondas>> GetAllPorEmpresaTipoPlan(long idEmpresa,long tipoPlan)
		{
			var sql = (from r in _context.PlanesRondas
						   //join g in _context.Grupos on r.idPlan equals g.idPlan
					   select new PlanesRondas()
					   {
						   idPlan = r.idPlan,
						   tipoPlan = r.tipoPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   GruposActivos = _context.GruposActivos.Where(a => a.idPlan == r.idPlan).ToList(),
						   GruposVariables = _context.GruposVariables.Where(a => a.idPlan == r.idPlan).ToList()
					   }).Where(x => x.idEmpresa == idEmpresa && x.tipoPlan == tipoPlan).ToListAsync();

			return await sql;
		}

		public async Task<List<PlanesRondas>> GetPorParametros(long idCategoria, long idClasificacion1, long idClasificacion2, long idSede, string marca)
		{
			var sql = await _context.PlanesRondas.Where(x => x.idCategoria == idCategoria && x.idClasificacion1 == idClasificacion1 &&
																x.idClasificacion2 == idClasificacion2 && x.idSede == idSede && x.marca == marca).ToListAsync();

			return sql;
		}

		

		public async Task<PlanesRondas> Set(PlanesRondas objeto, Transaction transaccion)
		{
			//en bussines condicionar que si existe por parametros entonces no registrar (arriba)
			switch (transaccion)
			{
				case Transaction.Insert:
					return await _transact.Crear(objeto);
				case Transaction.Delete:
					return await _transact.Actualizar(objeto);
				case Transaction.Update:
					return await _transact.Actualizar(objeto);
				default:
					return objeto;
			}
		}


		//----------------
		//   VARIABLES 
		//----------------
		public async Task<Grupos> CrearGrupoVariables(Grupos grupo)
		{
			_context.Grupos.Add(grupo);
			var result = await _context.SaveChangesAsync();

			if (result > 0)
			{
				var lst = new List<GruposVariables>();
				foreach (var item in grupo.variables)
				{
					lst.Add(new GruposVariables()
					{
						estado = true,
						idGrupo = grupo.idGrupo,
						idPlan = grupo.idPlan,
						idVariable = item.id,
						periodo = grupo.periodo,
						valor = item.valor
					});
				}

				var validate = await _context.Variables.Where(x => lst.Select(z => z.idVariable).Contains(x.idVarible) && !x.eliminado).ToListAsync();
				if (validate != null && (validate.Count >= lst.Count))
				{
					_context.GruposVariables.AddRange(lst);
					await _context.SaveChangesAsync();
				}
				else
				{
					_context.Grupos.Remove(grupo);
					return null;
				}
			}

			return grupo;
		}

		public async Task<List<Grupos>> ActualizarGrupo(Grupos grupo)
		{
			_context.Grupos.Update(grupo);
			await _context.SaveChangesAsync();

			if (grupo.variables.Count > 0)
			{
				var validate = await _context.Variables.Where(x => grupo.variables.Select(z => z.id).Contains(x.idVarible) && !x.eliminado).ToListAsync();
				if (validate != null)
				{
					foreach (var delvar in grupo.variables)
					{
						await QuitarVariables(grupo.idGrupo, grupo.idPlan, delvar.id);
					}
					await AgregarVariables(grupo.idGrupo, grupo.idPlan, grupo.variables);
					return await _context.Grupos.Where(x => x.estado).ToListAsync();
				}
				else
				{
					//_context.Grupos.Remove(grupo);
					return null;
				}
			}

			return await _context.Grupos.Where(x => x.estado).ToListAsync();
		}

		public async Task<List<Grupos>> QuitarGrupo(long idGrupo)
		{
			var find = await _context.Grupos.FirstOrDefaultAsync(x => x.idGrupo == idGrupo && x.estado);

			if (find != null)
			{
				find.estado = false;
				_context.Grupos.Update(find);
				await _context.SaveChangesAsync();

				var variables = await _context.GruposVariables.Where(x => x.idGrupo == idGrupo && x.estado).ToListAsync();
				foreach (var item in variables)
				{
					item.estado = false;
				}
				_context.GruposVariables.UpdateRange(variables);
				await _context.SaveChangesAsync();

				//var activos = await _context.GruposActivos.Where(x => x.estado).ToListAsync();
				//foreach (var item in activos)
				//{
				//	item.estado = false;
				//}
				//_context.GruposActivos.UpdateRange(activos);
				//await _context.SaveChangesAsync();
			}

			return await _context.Grupos.Where(x => x.estado).ToListAsync();
		}

		public async Task<List<Variables>> AgregarVariables(long idGrupo, long idPlan, List<VariableEnGrupo> variables)
		{
			var lst = new List<GruposVariables>();
			var grupo = await _context.GruposVariables.Where(x => x.idGrupo == idGrupo && x.estado).FirstOrDefaultAsync();
			foreach (var item in variables)
			{
				lst.Add(new GruposVariables()
				{
					estado = true,
					idGrupo = idGrupo,
					idPlan = idPlan,
					idVariable = item.id,
					periodo = grupo.periodo,
					valor = item.valor
				});
			}

			var validate = await _context.Variables.Where(x => lst.Select(z => z.idVariable).Contains(x.idVarible) && !x.eliminado).ToListAsync();

			if (validate != null && (validate.Count >= lst.Count))
			{
				_context.GruposVariables.AddRange(lst);
				await _context.SaveChangesAsync();

				var vars = await ConsultarVariablesGrupoId(idGrupo);
				return vars;
			}
			else
			{
				return null;
			}

		}

		public async Task<List<Variables>> QuitarVariables(long idGrupo, long idPlan, long idVariable)
		{
			var find = await _context.GruposVariables.FirstOrDefaultAsync(x => x.idGrupo == idGrupo && x.idPlan == idPlan && x.idVariable == idVariable && x.estado);
			if (find != null)
			{
				find.estado = false;

				_context.GruposVariables.Update(find);
				await _context.SaveChangesAsync();
			}

			var vars = await ConsultarVariablesGrupoId(idGrupo);
			return vars;
		}

		public async Task<List<GruposVariables>> ConsultarGrupoId(long idGrupo)
		{
			var sql = await _context.GruposVariables.Where(x => x.idGrupo == idGrupo && x.estado).ToListAsync();

			return sql;
		}

		public async Task<List<Variables>> ConsultarVariablesGrupoId(long idGrupo)
		{
			var sql = await _context.GruposVariables.Where(x => x.idGrupo == idGrupo && x.estado).ToListAsync();
			var vars = await _context.Variables.Where(x => sql.Select(z => z.idVariable).Contains(x.idVarible) && !x.eliminado).ToListAsync();

			return vars;
		}

		public async Task<List<Variables>> ConsultarVariablesIdClasificacion(long idClasificacion)
		{
			var sql = await _context.Variables.Where(x => x.idClasificacion == idClasificacion && !x.eliminado).ToListAsync();

			return sql;
		}

		public async Task<List<GruposVariables>> ConsultarGruposVariables()
		{
			var sql = await _context.GruposVariables.Where(x => x.estado).ToListAsync();

			return sql;
		}


		//----------------
		//   ACTIVOS 
		//----------------
		public async Task<List<GruposActivos>> AsociarActivosPlan(long idPlan, List<Guid> idActivos)
		{
			var lst = new List<GruposActivos>();
			foreach (var a in idActivos)
			{
				lst.Add(new GruposActivos()
				{
					idActivo = a,
					idPlan = idPlan,
					estado = true
				});
			}
			_context.GruposActivos.AddRange(lst);
			await _context.SaveChangesAsync();

			return lst;
		}

		public async Task<List<GruposActivos>> DesasociarActivosPlan(long idPlan, Guid idActivo)
		{
			var find = await _context.GruposActivos.FirstOrDefaultAsync(x => x.idActivo == idActivo && x.idPlan == idPlan && x.estado);
			if (find != null)
			{
				find.estado = false;
				_context.GruposActivos.Update(find);
				await _context.SaveChangesAsync();
			}

			//var ronda = await _context.MantenimientoRondas.FirstOrDefaultAsync(x => x.idActivo == idActivo && x.idPlan == idPlan && x.estado);
			//if (ronda != null)
			//{
			//	ronda.estado = false;
			//	_context.MantenimientoRondas.Update(ronda);
			//	await _context.SaveChangesAsync();
			//}
			return await ConsultarActivosIdPlan(idPlan);
		}

		public async Task<GruposActivos> AsociarActivoIdConGrupoIdAPlan(long idPlan, Guid idActivo)
		{
			var item = new GruposActivos()
			{
				idActivo = idActivo,
				idPlan = idPlan,
				estado = true
			};

			_context.GruposActivos.Add(item);
			await _context.SaveChangesAsync();

			return item;

			//var find = await _context.GruposActivos.FirstOrDefaultAsync(x => x.idActivo == idActivo && x.idPlan == idPlan && x.estado);

			//if(find == null)
			//{
			//	_context.GruposActivos.Add(item);
			//	var result = await _context.SaveChangesAsync();

			//	if (result > 0)
			//	{
			//		if(idGrupo != 0)
			//                 {
			//			var grupo = await _context.Grupos.FirstOrDefaultAsync(x => x.idGrupo == idGrupo);
			//			var ronda = new MantenimientoRondas()
			//			{
			//				fechaPropuestaProgramacion = DateTime.Now.AddHours(grupo.periodo),
			//				estado = true,
			//				idActivo = idActivo,
			//				idGrupo = idGrupo,
			//				idPlan = idPlan,
			//				idOrden = 0,
			//				fechaUltimoMantenimientoPreventivo = DateTime.Now
			//			};
			//			_context.MantenimientoRondas.Add(ronda);
			//			await _context.SaveChangesAsync();
			//		}

			//	}

			//	return await ConsultarActivosIdPlan(idPlan);
			//}
			//else
			//{
			//	var findMantenimiento = await _context.MantenimientoRondas.FirstOrDefaultAsync(x => x.idActivo == idActivo && x.idPlan == idPlan && x.idGrupo == idGrupo && x.estado);
			//	var respuesta = new List<GruposActivos>();

			//	if(findMantenimiento == null)
			//             {
			//		var grupo = await _context.Grupos.FirstOrDefaultAsync(x => x.idGrupo == idGrupo);
			//		var ronda = new MantenimientoRondas()
			//		{
			//			fechaPropuestaProgramacion = DateTime.Now.AddHours(grupo.periodo),
			//			estado = true,
			//			idActivo = idActivo,
			//			idGrupo = idGrupo,
			//			idPlan = idPlan,
			//			idOrden = 0,
			//			fechaUltimoMantenimientoPreventivo = DateTime.Now
			//		};
			//		_context.MantenimientoRondas.Add(ronda);
			//		await _context.SaveChangesAsync();

			//		respuesta = await ConsultarActivosIdPlan(idPlan);
			//	}
			//             else
			//             {
			//		respuesta = null;
			//             }
			//	return respuesta;
			//}
		}

		public async Task<List<GruposActivos>> ConsultarActivosIdPlan(long idPlan)
		{
			var sql = await _context.GruposActivos.Where(x => x.idPlan == idPlan && x.estado).ToListAsync();

			return sql;
		}

		public async Task<List<GruposActivos>> ConsultarActivosIdGrupoConIdPlan(long idPlan)
		{
			var sql = await _context.GruposActivos.Where(x => x.idPlan == idPlan && x.estado).ToListAsync();

			return sql;
		}

		public async Task<List<GruposActivos>> ConsultarActivosIdActivoConIdPlan(Guid idActivo, long idPlan)
		{
			var sql = await _context.GruposActivos.Where(x => x.idActivo == idActivo && x.idPlan == idPlan && x.estado).ToListAsync();

			return sql;
		}


		//--------------------
		//   RONDAS CREADAS
		//--------------------

		public async Task<MantenimientoRondas> SetPlanMantenimientoRondas(ActualizarRondasRequest objeto, Transaction transaccion)
		{

			var grupo = await _context.Grupos.FirstOrDefaultAsync(x => x.idGrupo == objeto.idGrupo);
			var plan = await Get(objeto.idPlan);
			var fechaPropuestaProgramacion = objeto.fechaUltimoMantenimientoRondas.AddHours(grupo.periodo);
			//var activosGrupos = await ConsultarActivosIdPlan(objeto.idPlan);
			var activos = new List<detalleActivosRequest>();

			//if (activosGrupos != null)
			//{
			//	foreach (var item in activosGrupos)
			//	{
			//		var Activo = new detalleActivosRequest()
			//		{
			//			idActivo = Convert.ToString(item.idActivo),
			//			llave = item.
			//		};
			//		activos.Add(Activo);
			//	}
			//}

			foreach(var item in objeto.datosOrden.datosActivos)
            {
				var Activo = new detalleActivosRequest()
				{
					idActivo = item.idActivo,
					llave = item.llave,
					nombre = item.nombre,
					tipo = item.tipo
				};

				activos.Add(Activo);
			}

			
			var nuevaOrdenTrabajo = await OrdenTrabajoMantenimientoRondas(plan.idEmpresa, plan.idSede, 157, fechaPropuestaProgramacion, objeto.fechaUltimoMantenimientoRondas, activos);

			var grupoAcciones = await _context.GruposAcciones.Where(x => x.idGrupo == objeto.idGrupo && x.idPlan == objeto.idPlan && x.estado).ToListAsync();
			var acciones = new List<long>();
			foreach (var item in grupoAcciones)
			{
				acciones.Add(item.idAccion);
			}
			
            var ronda = new MantenimientoRondas()
            {
                fechaPropuestaProgramacion = fechaPropuestaProgramacion,
				estado = true,
                idActivo = objeto.idActivo,
                idGrupo = objeto.idGrupo,
                idPlan = objeto.idPlan,
                idOrden = nuevaOrdenTrabajo.idOrden,
				fechaUltimoMantenimientoRondas = objeto.fechaUltimoMantenimientoRondas,
				observacion = objeto.observacion
			};
            _context.MantenimientoRondas.Add(ronda);
            await _context.SaveChangesAsync();

            return ronda;
		}

		public async Task<OrdenesTrabajo> OrdenTrabajoMantenimientoRondas(long idEmpresa, long idSede, int prioridad, DateTime fechaAtencion, DateTime fechaCreacion, List<detalleActivosRequest> datosActivos)
		{
			var objeto = new OrdenesTrabajo()
			{
				idEmpresa = idEmpresa,
				idSede = idSede,
				idServicio = 4,
				prioridad = prioridad,
				idEstadoOrden = 56,
				creador = "",
				motivoAnulacion = "",
				editor = "",
				fechaCreacion = fechaCreacion,
				eliminada = false,
				facturada = false,
				datosActivos = JsonConvert.SerializeObject(datosActivos)
			};

			var respuesta = await _dalcOrdenesTrabajo.Set(objeto, Transaction.Insert);

			return respuesta;

		}
		public async Task<MantenimientoRondas> ActualizarRondasConActivosGrupos(ActualizarRondasRequest data)
		{
			var ronda = await _context.MantenimientoRondas.FirstOrDefaultAsync(x => x.idPlan == data.idPlan && x.idGrupo == data.idGrupo && x.idActivo == data.idActivo && x.estado);
			if (ronda != null)
			{
				var grupo = await _context.Grupos.FirstOrDefaultAsync(x => x.idGrupo == ronda.idGrupo);

				ronda.fechaUltimoMantenimientoRondas = data.fechaUltimoMantenimientoRondas;
				var fechaPropuestaProgramacion = data.fechaUltimoMantenimientoRondas.AddHours(grupo.periodo);
				_context.MantenimientoRondas.Update(ronda);
				
				await _context.SaveChangesAsync();

				//CREAR RONDAS
				var ordenTrabajo = new OrdenesTrabajo()
				{
					idEmpresa = data.datosOrden.idEmpresa,
					idSede = data.datosOrden.idSede,
					idServicio = 4,
					prioridad = data.datosOrden.prioridad,
					idEstadoOrden = 56,
					creador = "",
					motivoAnulacion = "",
					editor = "",
					fechaCreacion = data.datosOrden.fechaCreacion,
					eliminada = false,
					facturada = false,
					datosActivos = JsonConvert.SerializeObject(data.datosOrden.datosActivos),
				};
				//'var ordenTrabajos = await _ordenes.GetAllPorActivo(data.idActivo);'
				//'var ordenTrabajo = ordenTrabajos.OrderByDescending(x => x.fechaCreacion).FirstOrDefault();'

				//'ordenTrabajo.fechaCreacion = DateTime.UtcNow.AddHours(-5);'
				//'ordenTrabajo.fechaProgramacionInicio = ordenTrabajo.fechaCreacion;'
				//'ordenTrabajo.fechaProgramacionCierre = ronda.fechaPropuestaProgramacion;'

				await _ordenes.Set(ordenTrabajo, Transaction.Insert);
				await _context.SaveChangesAsync();

				//ronda.idOrden = ordenTrabajo.idOrden;
				var crearRonda = new MantenimientoRondas()
				{
					fechaPropuestaProgramacion = fechaPropuestaProgramacion,
					estado = true,
					idActivo = data.idActivo,
					idGrupo = data.idGrupo,
					idPlan = data.idPlan,
					idOrden = ordenTrabajo.idOrden,
					fechaUltimoMantenimientoRondas = DateTime.Now,
					observacion = data.observacion
				};
				_context.MantenimientoRondas.Add(crearRonda);
				await _context.SaveChangesAsync();
				//_context.MantenimientoRondas.Update(ronda);
				//await _context.SaveChangesAsync();

				return crearRonda;
			}
			else
			{
				return null;
			}
		}

		public async Task<RespuestaActivosVariables> ActualizarRondasConVariableGrupos(ActualizarRondasVarsRequest data, string nombreVariable)
		{
			var ronda = await _context.MantenimientoRondas.FirstOrDefaultAsync(x => x.idPlan == data.idPlan && x.idGrupo == data.idGrupo && x.idActivo == data.idActivo && x.estado);
			var grupo = await _context.Grupos.FirstOrDefaultAsync(x => x.idGrupo == ronda.idGrupo);
			var plan = await _context.PlanesRondas.FirstOrDefaultAsync(x => x.idPlan == data.idPlan);
			var variable = await _context.GruposVariables.FirstOrDefaultAsync(x => x.idGrupo == grupo.idGrupo && x.idVariable == data.idVariable);
			var condicion = await _context.CondicionesVariables.FirstOrDefaultAsync(x => x.idVariable == data.idVariable);
			var alerta = false;
			var respuestaActivosVariables = new RespuestaActivosVariables();
			variable.valor = data.valor;
			_context.GruposVariables.Update(variable);
			await _context.SaveChangesAsync();


			if (condicion != null)
			{
				switch (condicion.comparadorCondicion)
				{
					case "<=":
						alerta = double.Parse(data.valor) <= double.Parse(condicion.valorCondicion);
						break;
					case ">=":
						alerta = double.Parse(data.valor) >= double.Parse(condicion.valorCondicion);
						break;
					case ">":
						alerta = double.Parse(data.valor) > double.Parse(condicion.valorCondicion);
						break;
					case "<":
						alerta = double.Parse(data.valor) < double.Parse(condicion.valorCondicion);
						break;
				}
				if (alerta)
				{

					//var idActivo = datosActivos[0]["idActivo"];
					var idActivo = data.datosOrden.datosActivos[0].idActivo;
					if(data.datosOrden.datosActivos[0].tipo == "Equipo")
                    {
						respuestaActivosVariables = new RespuestaActivosVariables()
						{
							idActivoVariable = data.idVariable,
							idClasificacion = plan.idClasificacion1,
							idCategorizacion = plan.idCategoria,
							idActivoFlota = null,
							idActivoEquipo = Guid.Parse(idActivo),
							respuesta = data.valor,
						};
					}
					if (data.datosOrden.datosActivos[0].tipo == "Flota")
					{
						respuestaActivosVariables = new RespuestaActivosVariables()
						{
							idActivoVariable = data.idVariable,
							idClasificacion = plan.idClasificacion1,
							idCategorizacion = plan.idCategoria,
							idActivoFlota = Guid.Parse(idActivo),
							idActivoEquipo = null,
							respuesta = data.valor,
						};
					}

					//CREAR ORDEN TRABAJO

					var nuevaOrdenTrabajo = await OrdenTrabajoMantenimientoAviso(data.datosOrden.idEmpresa, data.datosOrden.idSede, data.datosOrden.fechaCreacion, data.datosOrden.datosActivos);
					
					var detalle = ("Mantenimiento basado en condiciones: " + nombreVariable + "(" + variable.valor + ")").ToString();
					var mantenimientoAviso = new MantenimientoAviso()
					{
						idOrden = nuevaOrdenTrabajo.idOrden,
						detalleAviso = detalle,
						correctivo = false,
						idDiagnostico = 0,
						observacion = "",
					};
					await dALCMantenimientoAviso.Set(mantenimientoAviso, Transaction.Insert);
					await _context.SaveChangesAsync();

				}
			}

			return respuestaActivosVariables;
		}

		public async Task<OrdenesTrabajo> OrdenTrabajoMantenimientoAviso(long idEmpresa, long idSede, DateTime fechaCreacion, List<detalleActivosRequest> datosActivos)
		{
			var objeto = new OrdenesTrabajo()
			{
				idEmpresa = idEmpresa,
				idSede = idSede,
				idServicio = 1,
				prioridad = 156,
				idEstadoOrden = 56,
				creador = "",
				motivoAnulacion = "",
				editor = "",
				fechaCreacion = fechaCreacion,
				eliminada = false,
				facturada = false,
				datosActivos = JsonConvert.SerializeObject(datosActivos)
			};

			var respuesta = await _ordenes.Set(objeto, Transaction.Insert);
			await _context.SaveChangesAsync();
			return respuesta;

		}

		public async Task<MantenimientoRondas> GetPorOrdenAsync(long idOrden)
		{
			return await _context.MantenimientoRondas.Where(x => x.idOrden == idOrden).Include(x => x.respuestasVariableRondas).FirstOrDefaultAsync();
		}

		public async Task<List<MantenimientoRondas>> GetAllPorGrupo(long idGrupo)
		{
			return await _context.MantenimientoRondas.Where(x => x.idGrupo == idGrupo).ToListAsync();
		}

		public async Task<List<MantenimientoRondas>> GetMantenimientoRondasPorPlan(long idPlan)
		{
			var sql = await _context.MantenimientoRondas.Where(x => x.idPlan == idPlan && x.estado).ToListAsync();

			return sql;
		}

		public async Task<List<MantenimientoRondas>> GetMantenimientoRondasPorPlanYActivo(long idPlan, Guid idActivo)
		{
			var sql = await _context.MantenimientoRondas.Where(x => x.idPlan == idPlan && x.idActivo == idActivo && x.estado).ToListAsync();

			return sql;
		}
	}
}
