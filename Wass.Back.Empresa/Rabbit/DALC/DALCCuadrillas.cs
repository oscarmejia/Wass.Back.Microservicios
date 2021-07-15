using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.Interface;
namespace Wass.Back.Empresa.Rabbit.DALC
{
    public class DALCCuadrillas : IDALCCrud<Cuadrillas>
	{
		private readonly EmpresaContext _context;
		private readonly DALCTransacciones<Cuadrillas> _transact;

		public DALCCuadrillas(EmpresaContext context)
		{
			_context = context;
			_transact = new DALCTransacciones<Cuadrillas>(context);
		}

		public async Task<Cuadrillas> GetAsync(long id)
		{
			var cuadrilla = await _context.Cuadrillas.Where(x => x.idCuadrilla == id)
				.Include(x => x.cuadrillaEmpleados)
				.Include("cuadrillaEmpleados.empleado")
				.Include(x => x.cuadrillaTurnos)
				.Include(x => x.cuadrillaSkillsEmpresa)
				.FirstOrDefaultAsync();

			var turnos = await
							(from turno in _context.Turnos
							 join relacion in _context.CuadrillasTurnos on turno.idTurno equals relacion.idTurno
							 where relacion.idCuadrilla == cuadrilla.idCuadrilla
							 select turno)
						  .ToListAsync();

			cuadrilla.listadoTurnos = turnos;
			return cuadrilla;
		}

		public async Task<List<Cuadrillas>> GetAllAsync()
		{
			var cuadrillas = await Queryable().Include(x => x.cuadrillaSkillsEmpresa).ToListAsync();
			return cuadrillas;
		}

		public async Task<List<Cuadrillas>> GetPorEstadoAsync(int estado)
		{
			var cuadrillas = Queryable();
			cuadrillas = cuadrillas.Where(x => x.estado == estado);

			return await cuadrillas.Include(x => x.cuadrillaSkillsEmpresa).ToListAsync();
		}

		public async Task<List<Cuadrillas>> GetPorSedeAsync(long idSede)
		{
			var cuadrillas = Queryable();
			cuadrillas = cuadrillas.Where(x => x.idSede == idSede);

			return await cuadrillas.Include(x => x.cuadrillaSkillsEmpresa).ToListAsync();
		}

		public async Task<List<Cuadrillas>> GetPorEmpresaAsync(long idEmpresa)
		{
			var sql = (from cuadrilla in Queryable()
					   join sede in _context.Sedes on cuadrilla.idSede equals sede.idSede
					   where sede.idEmpresa == idEmpresa
					   select cuadrilla
					   )
					   .AsQueryable();

			return await sql.ToListAsync();
		}

		public async Task<Cuadrillas> SetAsync(Cuadrillas objeto, Transaction transaccion)
		{
			switch (transaccion)
			{
				case Transaction.Insert:
					return await _transact.Crear(objeto);
				case Transaction.Update:
					return await _transact.Actualizar(objeto);
				default:
					return objeto;
			}
		}

		private IQueryable<Cuadrillas> Queryable()
		{
			var cuadrillas = (from cuadrilla in _context.Cuadrillas.Include(x => x.cuadrillaTurnos)
							  select new Cuadrillas()
							  {
								  nombreA = cuadrilla.nombreA,
								  celular = cuadrilla.celular,
								  email = cuadrilla.email,
								  estado = cuadrilla.estado,
								  zonaAtencion = cuadrilla.zonaAtencion,
								  ubicacionActual = cuadrilla.ubicacionActual,
								  idCuadrilla = cuadrilla.idCuadrilla,
								  idSede = cuadrilla.idSede,
								  nombreB = cuadrilla.nombreB,
								  numMiembros = cuadrilla.numMiembros,
								  sedes = cuadrilla.sedes,
								  cuadrillaEmpleados = (from cuadem in _context.CuadrillaEmpleados
														where cuadem.idCuadrilla == cuadrilla.idCuadrilla
														select new CuadrillaEmpleados()
														{
															creador = cuadem.creador,
															editor = cuadem.editor,
															eliminado = cuadem.eliminado,
															estado = cuadem.estado,
															fechaCreacion = cuadem.fechaCreacion,
															fechaEdicion = cuadem.fechaEdicion,
															idCuadrilla = cuadem.idCuadrilla,
															idEmpleado = cuadem.idEmpleado,
															lider = cuadem.lider,
															empleado = (from empl in _context.Empleados
																		where empl.idEmpleado == cuadem.idEmpleado
																		select empl)
																		.FirstOrDefault(),
														})
														.ToList(),
								  cuadrillaTurnos = cuadrilla.cuadrillaTurnos,
								  listadoTurnos = _context.Turnos.Where(z => cuadrilla.cuadrillaTurnos.Select(w => w.idTurno).Contains(z.idTurno)).ToList()
							  })
						  .AsQueryable();

			return cuadrillas;
		}
	}
}
