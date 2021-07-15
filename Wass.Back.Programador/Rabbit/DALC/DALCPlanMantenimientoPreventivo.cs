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
    public class DALCPlanMantenimientoPreventivo : IDALCCrud<PlanesMantenimientoPreventivo>
    {
		private readonly ProgramadorContext _context;
		private readonly DALCTransacciones<PlanesMantenimientoPreventivo> _transact;
		private readonly DALCTransacciones<PlanMantenimientoPreventivo> _transactPlanMantenimiento;
		private readonly DALCOrdenesTrabajo _dalcOrdenesTrabajo;

		public DALCPlanMantenimientoPreventivo(ProgramadorContext context)
		{
			_context = context;
			_transact = new DALCTransacciones<PlanesMantenimientoPreventivo>(context);
			_transactPlanMantenimiento = new DALCTransacciones<PlanMantenimientoPreventivo>(context);
			_dalcOrdenesTrabajo = new DALCOrdenesTrabajo(context);
		}


		//----------------
		//   MANTENIMIENTO PREVENTIVO PLAN
		//----------------
		public async Task<PlanesMantenimientoPreventivo> Get(long id)
		{
			
			var sql = (from r in _context.PlanesMantenimientoPreventivo
					   //join g in _context.GruposMantenimientoPreventivo on r.idPlan equals g.idPlan
					   select new PlanesMantenimientoPreventivo()
					   {
						   idPlan = r.idPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   prioridad = r.prioridad,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   estado = r.estado,
						   GruposActivos = _context.GruposActivosMantenimientoPreventivo.Where(a => a.idPlan == id && a.estado).ToList(),
						   GruposPartes = _context.GruposPartes.Where(a => a.idPlan == id && a.estado).ToList(),
						   GruposAcciones = _context.GruposAcciones.Where(a => a.idPlan == id && a.estado).ToList()
					   }).Where(x => x.idPlan == id && x.estado).FirstOrDefaultAsync();
			
			return await sql;
		}

		public async Task<List<PlanesMantenimientoPreventivo>> GetPorCategoria(long idCategoria)
		{
			
			var sql = (from r in _context.PlanesMantenimientoPreventivo
					   //join g in _context.GruposMantenimientoPreventivo on r.idPlan equals g.idPlan
					   select new PlanesMantenimientoPreventivo()
					   {
						   idPlan = r.idPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   prioridad = r.prioridad,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   estado = r.estado,
						   GruposActivos = _context.GruposActivosMantenimientoPreventivo.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposPartes = _context.GruposPartes.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposAcciones = _context.GruposAcciones.Where(a => a.idPlan == r.idPlan && a.estado).ToList()
					   }).Where(x => x.idCategoria == idCategoria && x.estado).ToListAsync();

			return await sql;
		}

		public async Task<List<PlanesMantenimientoPreventivo>> GetAll()
		{
			
			var sql = (from r in _context.PlanesMantenimientoPreventivo
					   //join g in _context.GruposMantenimientoPreventivo on r.idPlan equals g.idPlan
					   select new PlanesMantenimientoPreventivo()
					   {
						   idPlan = r.idPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   prioridad = r.prioridad,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   estado = r.estado,
						   GruposActivos = _context.GruposActivosMantenimientoPreventivo.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposPartes = _context.GruposPartes.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposAcciones = _context.GruposAcciones.Where(a => a.idPlan == r.idPlan && a.estado).ToList()
					   }).Where(x => x.estado).ToListAsync();

			return await sql;
		}

		public async Task<List<PlanesMantenimientoPreventivo>> GetAllPorEmpresa(long idEmpresa)
		{

			var sql = (from r in _context.PlanesMantenimientoPreventivo
					  // join g in _context.GruposMantenimientoPreventivo on r.idPlan equals g.idPlan
					   select new PlanesMantenimientoPreventivo()
					   {
						   idPlan = r.idPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   prioridad = r.prioridad,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   estado = r.estado,
						   GruposActivos = _context.GruposActivosMantenimientoPreventivo.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposPartes = _context.GruposPartes.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposAcciones = _context.GruposAcciones.Where(a => a.idPlan == r.idPlan && a.estado).ToList()
					   }).Where(x => x.estado && x.idEmpresa == idEmpresa).ToListAsync();

			return await sql;
		}
		
		public async Task<List<PlanesMantenimientoPreventivo>> GetPorParametros(long idCategoria, long idClasificacion1, long idClasificacion2, long idSede, string marca)
		{
			//var sql = await _context.PlanesMantenimientoPreventivo.Where(x => x.idCategoria == idCategoria && x.idClasificacion1 == idClasificacion1 &&
			//													x.idClasificacion2 == idClasificacion2 && x.idSede == idSede && x.marca == marca).ToListAsync();

			var sql = (from r in _context.PlanesMantenimientoPreventivo
					   where r.idCategoria == idCategoria && r.idClasificacion1 == idClasificacion1 &&
						r.idClasificacion2 == idClasificacion2 && r.idSede == idSede && r.marca == marca
					   //join g in _context.GruposMantenimientoPreventivo on r.idPlan equals g.idPlan
					   select new PlanesMantenimientoPreventivo()
					   {
						   idPlan = r.idPlan,
						   idClasificacion1 = r.idClasificacion1,
						   idClasificacion2 = r.idClasificacion2,
						   idCategoria = r.idCategoria,
						   prioridad = r.prioridad,
						   idEmpresa = r.idEmpresa,
						   idSede = r.idSede,
						   marca = r.marca,
						   estado = r.estado,
						   GruposActivos = _context.GruposActivosMantenimientoPreventivo.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposPartes = _context.GruposPartes.Where(a => a.idPlan == r.idPlan && a.estado).ToList(),
						   GruposAcciones = _context.GruposAcciones.Where(a => a.idPlan == r.idPlan && a.estado).ToList()
					   }).Where(x => x.estado).ToListAsync();

			return await sql;
		}

	
		public async Task<PlanesMantenimientoPreventivo> Set(PlanesMantenimientoPreventivo objeto, Transaction transaccion)
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
		//   PARTES 
		//----------------
		public async Task<GruposMantenimientoPreventivo> CrearGrupoPartes(GruposMantenimientoPreventivo grupo)
		{
			_context.GruposMantenimientoPreventivo.Add(grupo);
			var result = await _context.SaveChangesAsync();

			if (result > 0)
			{
				var lst = new List<GruposPartes>();
				foreach (var item in grupo.partes)
				{
					lst.Add(new GruposPartes()
					{
						estado = true,
						idGrupo = grupo.idGrupo,
						idPlan = grupo.idPlan,
						idParte = item,
						periodo = grupo.periodo,
						parada = grupo.parada
					});
				}

				_context.GruposPartes.AddRange(lst);
				await _context.SaveChangesAsync();
			}

			return grupo;
		}


		public async Task<List<GruposMantenimientoPreventivo>> QuitarGrupo(long idGrupo)
		{
			var find = await _context.GruposMantenimientoPreventivo.FirstOrDefaultAsync(x => x.idGrupo == idGrupo && x.estado);

			if (find != null)
			{
				find.estado = false;
				_context.GruposMantenimientoPreventivo.Update(find);
				await _context.SaveChangesAsync();

				var partes = await _context.GruposPartes.Where(x => x.idGrupo == idGrupo && x.estado).ToListAsync();
				foreach (var item in partes)
				{
					item.estado = false;
				}
				_context.GruposPartes.UpdateRange(partes);
				await _context.SaveChangesAsync();

				var acciones = await _context.GruposAcciones.Where(x => x.idGrupo == idGrupo && x.estado).ToListAsync();
				foreach (var item in acciones)
				{
					item.estado = false;
				}
				_context.GruposAcciones.UpdateRange(acciones);
				await _context.SaveChangesAsync();
			}

			return await _context.GruposMantenimientoPreventivo.Where(x => x.estado).ToListAsync();
		}

		public async Task<List<GruposPartes>> AgregarPartes(long idGrupo, long idPlan, List<Guid> partes)
		{
			var lst = new List<GruposPartes>();
			var grupo = await _context.GruposPartes.Where(x => x.idGrupo == idGrupo && x.estado).FirstOrDefaultAsync();
			foreach (var item in partes)
			{
				lst.Add(new GruposPartes()
				{
					estado = true,
					idGrupo = idGrupo,
					idPlan = idPlan,
					idParte = item,
					periodo = grupo.periodo,
					parada = grupo.parada
				});
			}

			_context.GruposPartes.AddRange(lst);
			await _context.SaveChangesAsync();

			var parts = await ConsultarPartesGrupoId(idGrupo);
			return parts;
		}

		public async Task<List<GruposPartes>> QuitarPartes(long idGrupo, long idPlan, Guid idParte)
		{
			var find = await _context.GruposPartes.FirstOrDefaultAsync(x => x.idGrupo == idGrupo && x.idPlan == idPlan && x.idParte == idParte && x.estado);
			if (find != null)
			{
				find.estado = false;

				_context.GruposPartes.Update(find);
				await _context.SaveChangesAsync();
			}

			var parts = await ConsultarPartesGrupoId(idGrupo);
			return parts;
		}


		public async Task<List<GruposPartes>> ConsultarPartesGrupoId(long idGrupo)
		{
			var sql = await _context.GruposPartes.Where(x => x.idGrupo == idGrupo && x.estado).ToListAsync();
			return sql;
		}


		public async Task<List<GruposPartes>> ConsultarPartesIdClasificacion(long idClasificacion)
		{
			var sql = await _context.PlanesMantenimientoPreventivo.Where(x => x.idClasificacion1 == idClasificacion && x.estado).ToListAsync();
			var partes = await _context.GruposPartes.Where(x => x.estado && sql.Select(z => z.idPlan).Contains(x.idPlan)).ToListAsync();
			return partes;
		}
		public async Task<GruposPartes> actualizarParada(long idGrupo, long idPlan)
		{
			var get = await _context.GruposPartes.FirstOrDefaultAsync(x => x.idGrupo == idGrupo && x.idPlan == idPlan);
			if (get.parada == false)
			{
				get.parada = true;
				_context.Update(get);
				await _context.SaveChangesAsync();
			}
			else
			{
				get.parada = false;
				_context.Update(get);
				await _context.SaveChangesAsync();
			}

			return get;
		}

		public async Task<List<GruposPartes>> ConsultarGruposPartes()
		{
			var sql = await _context.GruposPartes.Where(x => x.estado).ToListAsync();

			return sql;
		}

		//----------------
		//   ACCIONES
		//----------------
		public async Task<List<GruposAcciones>> AsociarAccionesPlan(long idPlan, long idGrupo, List<long> idAcciones)
		{
			var lst = new List<GruposAcciones>();
			foreach (var a in idAcciones)
			{
				lst.Add(new GruposAcciones()
				{
					idAccion = a,
					idGrupo = idGrupo,
					idPlan = idPlan,
					estado = true
				});
			}
			_context.GruposAcciones.AddRange(lst);
			await _context.SaveChangesAsync();

			return lst;
		}


		public async Task<List<GruposAcciones>> AgregarAccionIdConGrupoIdAPlan(long idPlan, long idGrupo, long idAccion)
		{
			var item = new GruposAcciones()
			{
				idAccion = idAccion,
				idGrupo = idGrupo,
				idPlan = idPlan,
				estado = true
			};

			_context.GruposAcciones.Add(item);
			var result = await _context.SaveChangesAsync();

			return await ConsultarAccionesGrupoId(idGrupo);
		}

		public async Task<List<GruposAcciones>> QuitarAcciones(long idGrupo, long idPlan, long idAccion)
		{
			var find = await _context.GruposAcciones.FirstOrDefaultAsync(x => x.idGrupo == idGrupo && x.idPlan == idPlan && x.idAccion == idAccion && x.estado);
			if (find != null)
			{
				find.estado = false;

				_context.GruposAcciones.Update(find);
				await _context.SaveChangesAsync();
			}

			var parts = await ConsultarAccionesGrupoId(idGrupo);
			return parts;
		}

		public async Task<List<GruposAcciones>> ConsultarAccionesGrupoId(long idGrupo)
		{
			var sql = await _context.GruposAcciones.Where(x => x.idGrupo == idGrupo && x.estado).ToListAsync();

			return sql;
		}


		public async Task<List<GruposAcciones>> ConsultarGruposAcciones()
		{
			var sql = await _context.GruposAcciones.Where(x => x.estado).ToListAsync();

			return sql;
		}


		//----------------
		//   ACTIVOS 
		//----------------
		public async Task<List<GruposActivosMantenimientoPreventivoRequest>> AsociarActivosPlan(long idPlan, List<detalleActivosRequest> Activos)
		{
			var lst = new List<GruposActivosMantenimientoPreventivo>();
			var respuesta = new List<GruposActivosMantenimientoPreventivoRequest>();
			foreach (var a in Activos)
			{
				lst.Add(new GruposActivosMantenimientoPreventivo()
				{
					Activo = JsonConvert.SerializeObject(a),
					idPlan = idPlan,
					estado = true
				});
			}
			_context.GruposActivosMantenimientoPreventivo.AddRange(lst);
			await _context.SaveChangesAsync();
			if(lst.Count > 0)
            {
				foreach(var item in lst)
                {
					respuesta.Add(new GruposActivosMantenimientoPreventivoRequest()
					{
						id = item.id,
						Activo = JsonConvert.DeserializeObject<detalleActivosRequest>(item.Activo),
						idPlan = item.idPlan,
						estado = item.estado
					});
                }
            }
			return respuesta;
		}

		public async Task<List<GruposActivosMantenimientoPreventivoRequest>> DesasociarActivosPlan(long idPlan, string idActivo)
		{
			var find = await _context.GruposActivosMantenimientoPreventivo.FirstOrDefaultAsync(x => x.Activo.Contains(idActivo) && x.idPlan == idPlan && x.estado);
			if (find != null)
			{
				find.estado = false;
				_context.GruposActivosMantenimientoPreventivo.Update(find);
				await _context.SaveChangesAsync();
			}

			return await ConsultarActivosIdPlan(idPlan);
		}

		public async Task<GruposActivosMantenimientoPreventivoRequest> AsociarActivoIdConGrupoIdAPlan(long idPlan, detalleActivosRequest Activo)
		{
			var item = new GruposActivosMantenimientoPreventivo()
			{
				Activo = JsonConvert.SerializeObject(Activo),
				idPlan = idPlan,
				estado = true
			};

			_context.GruposActivosMantenimientoPreventivo.Add(item);
			await _context.SaveChangesAsync();

			var respuesta = new GruposActivosMantenimientoPreventivoRequest()
			{
				id = item.id,
				Activo = JsonConvert.DeserializeObject<detalleActivosRequest>(item.Activo),
				idPlan = item.idPlan,
				estado = item.estado
			};

			return respuesta;
		}

		public async Task<List<GruposActivosMantenimientoPreventivoRequest>> ConsultarActivosIdPlan(long idPlan)
		{
			var sql = await _context.GruposActivosMantenimientoPreventivo.Where(x => x.idPlan == idPlan && x.estado).ToListAsync();
			var respuesta = new List<GruposActivosMantenimientoPreventivoRequest>();
			if (sql.Count > 0)
			{
				foreach (var item in sql)
				{
					respuesta.Add(new GruposActivosMantenimientoPreventivoRequest()
					{
						id = item.id,
						Activo = JsonConvert.DeserializeObject<detalleActivosRequest>(item.Activo),
						idPlan = item.idPlan,
						estado = item.estado
					});
				}
			}
			return respuesta;
		}

		public async Task<List<GruposActivosMantenimientoPreventivoRequest>> ConsultarActivosIdActivoConIdPlan(string idActivo, long idPlan)
		{
			var sql = await _context.GruposActivosMantenimientoPreventivo.Where(x => x.Activo.Contains(idActivo) && x.idPlan == idPlan && x.estado).ToListAsync();
			var respuesta = new List<GruposActivosMantenimientoPreventivoRequest>();
			if (sql.Count > 0)
			{
				foreach (var item in sql)
				{
					respuesta.Add(new GruposActivosMantenimientoPreventivoRequest()
					{
						id = item.id,
						Activo = JsonConvert.DeserializeObject<detalleActivosRequest>(item.Activo),
						idPlan = item.idPlan,
						estado = item.estado
					});
				}
			}
			return respuesta;
		}


		//--------------------
		//   MANTENIMIENTOS PREVENTIVOS CREADOS
		//--------------------
		public async Task<PlanMantenimientoPreventivo> SetPlanMantenimientoPreventivo(PlanMantenimientoPreventivo objeto,Transaction transaccion)
		{
			
			var grupo = await _context.GruposMantenimientoPreventivo.FirstOrDefaultAsync(x => x.idGrupo == objeto.idGrupo);

			_context.PlanMantenimientoPreventivo.Add(objeto);
			await _context.SaveChangesAsync();

			var plan = await Get(objeto.idPlan);
			var fechaPropuestaProgramacion = objeto.fechaUltimoMantenimientoPreventivo.AddHours(grupo.periodo);
			var activosGrupos = await ConsultarActivosIdPlan(objeto.idPlan);
			var activos = new List<detalleActivosRequest>();

			if (activosGrupos != null)
			{
				foreach (var item in activosGrupos)
				{
					activos.Add(item.Activo);
				}
			}


			var nuevaOrdenTrabajo = await OrdenTrabajoMantenimientoPreventivo(plan.idEmpresa, plan.idSede, 157, fechaPropuestaProgramacion, objeto.fechaUltimoMantenimientoPreventivo, activos);

			var grupoAcciones = await _context.GruposAcciones.Where(x => x.idGrupo == objeto.idGrupo && x.idPlan == objeto.idPlan && x.estado).ToListAsync();
			var acciones = new List<long>();
			foreach (var item in grupoAcciones)
			{
				acciones.Add(item.idAccion);
			}
			var crearMantenimientoPreventivo = new MantenimientoPreventivo()
			{
				idOrden = nuevaOrdenTrabajo.idOrden,
				idPlan = objeto.idPlan,
				idGrupo = objeto.idGrupo,
				fechaPropuestaProgramacion = fechaPropuestaProgramacion,
				parada = false,
				eliminado = false,
				Acciones = JsonConvert.SerializeObject(acciones),
			};
			_context.MantenimientoPreventivo.Add(crearMantenimientoPreventivo);
			await _context.SaveChangesAsync();

			return objeto;
		}
		public async Task<PlanMantenimientoPreventivo> ActualizarMantenimientoPreventivoConActivosGrupos(long idPlanMantenimientoPreventivo, long idPlan, long idGrupo, string idActivo, DateTime fecha)
		{
			var mantenimientoPreventivo = await _context.PlanMantenimientoPreventivo.FirstOrDefaultAsync(x => x.idPlan == idPlan && x.idGrupo == idGrupo && x.idActivo == idActivo && x.idPlanMantenimientoPreventivo == idPlanMantenimientoPreventivo && x.estado);
			var grupo = await _context.GruposMantenimientoPreventivo.FirstOrDefaultAsync(x => x.idGrupo == idGrupo);

			mantenimientoPreventivo.fechaUltimoMantenimientoPreventivo = fecha;
            _context.PlanMantenimientoPreventivo.Update(mantenimientoPreventivo);
			await _context.SaveChangesAsync();

			var plan = await Get(idPlan);
			var fechaPropuestaProgramacion = mantenimientoPreventivo.fechaUltimoMantenimientoPreventivo.AddHours(grupo.periodo);
			var activosGrupos = await ConsultarActivosIdPlan(idPlan);
			var activos = new List<detalleActivosRequest>();

			if(activosGrupos != null)
            {
				foreach (var item in activosGrupos)
				{
					activos.Add(item.Activo);
				}
			}


			var nuevaOrdenTrabajo = await OrdenTrabajoMantenimientoPreventivo(plan.idEmpresa, plan.idSede, 156, fechaPropuestaProgramacion, fecha, activos);

			var grupoAcciones = await _context.GruposAcciones.Where(x => x.idGrupo == idGrupo && x.idPlan == idPlan && x.estado).ToListAsync();
			var acciones = new List<long>();
			foreach (var item in grupoAcciones)
			{
				acciones.Add(item.idAccion);
			}
			var crearMantenimientoPreventivo = new MantenimientoPreventivo()
			{
				idOrden = nuevaOrdenTrabajo.idOrden,
				idPlan = idPlan,
				idGrupo = idGrupo,
				fechaPropuestaProgramacion = fechaPropuestaProgramacion,
				parada = false,
				eliminado = false,
				Acciones = JsonConvert.SerializeObject(acciones),
			};
			_context.MantenimientoPreventivo.Add(crearMantenimientoPreventivo);
			await _context.SaveChangesAsync();

			return mantenimientoPreventivo;
		}
		public async Task<OrdenesTrabajo> OrdenTrabajoMantenimientoPreventivo(long idEmpresa, long idSede, int prioridad, DateTime fechaAtencion, DateTime fechaCreacion, List<detalleActivosRequest> datosActivos)
		{
			var objeto = new OrdenesTrabajo()
			{
				idEmpresa = idEmpresa,
				idSede = idSede,
				idServicio = 3,
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
		public async Task<List<PlanMantenimientoPreventivo>> GetMantenimientoPreventivoPorPlan(long idPlan)
		{
			var sql = await _context.PlanMantenimientoPreventivo.Where(x => x.idPlan == idPlan && x.estado).ToListAsync();

			return sql;
		}

		public async Task<List<PlanMantenimientoPreventivo>> GetMantenimientoPreventivoPorPlanYActivo(long idPlan, string idActivo)
		{
			var sql = await _context.PlanMantenimientoPreventivo.Where(x => x.idPlan == idPlan && x.idActivo == idActivo && x.estado).ToListAsync();

			return sql;
		}

		public async Task<List<PlanMantenimientoPreventivo>> GetTodosMantenimientoPreventivo()
		{
			var sql = await _context.PlanMantenimientoPreventivo.Where(x => x.estado).ToListAsync();

			return sql;
		}

		public async Task<PlanMantenimientoPreventivo> EliminarPlanMantenimientoPreventivo(long idPlanMantenimientoPreventivo)
		{
			var get = await _context.PlanMantenimientoPreventivo.FirstOrDefaultAsync(x => x.idPlanMantenimientoPreventivo == idPlanMantenimientoPreventivo);
			get.estado = false;
			_context.Update(get);
			await _context.SaveChangesAsync();

			var plan = await Get(get.idPlan);
			plan.estado = false;
			_context.PlanesMantenimientoPreventivo.Update(plan);
			await _context.SaveChangesAsync();

			var mantenimientoPreventivo = await _context.MantenimientoPreventivo.Where(x => x.idPlan == get.idPlan && x.idGrupo == get.idGrupo && x.eliminado).FirstOrDefaultAsync();
			mantenimientoPreventivo.eliminado = false;
			_context.MantenimientoPreventivo.Update(mantenimientoPreventivo);
			await _context.SaveChangesAsync();

			var ordenTrabajo = await _context.OrdenesTrabajo.Where(x => !x.eliminada && x.idOrden == mantenimientoPreventivo.idOrden).FirstOrDefaultAsync();
			ordenTrabajo.eliminada = true;
			_context.OrdenesTrabajo.Update(ordenTrabajo);
			await _context.SaveChangesAsync();

			var quitarGrupoPartes = await QuitarGrupo(get.idGrupo);
			var quitarActivos = await DesasociarActivosPlan(get.idPlan, get.idActivo);

			return get;
		}

	}
}
