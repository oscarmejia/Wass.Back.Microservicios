using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.Interface;
using WASS.Back.Programador.core.rabbit.DALC;

namespace Wass.Back.Programador.Rabbit.DALC
{
	public class DALCMantenimientoCondiciones : IDALCCrud<CondicionesVariables>
	{
		private readonly ProgramadorContext _context;
		private readonly DALCTransacciones<CondicionesVariables> _transact;

		public DALCMantenimientoCondiciones(ProgramadorContext context)
		{
			_context = context;
			_transact = new DALCTransacciones<CondicionesVariables>(context);
		}

		public async Task<CondicionesVariables> Get(long id)
		{
			var sql = await _context.CondicionesVariables.FirstOrDefaultAsync(x => x.idCondicionesVariables == id);
			return sql;
		}

		public async Task<List<CondicionesVariables>> GetByIdPlan(long idPlan)
		{
			var sql = await _context.CondicionesVariables.Where(x => x.idPlan == idPlan && x.estado)
				
				.ToListAsync();
			return sql;
		}

		public async Task<List<CondicionesVariables>> GetAll()
		{
			var sql = await _context.CondicionesVariables.ToListAsync();
			return sql;
		}

		public async Task<List<CondicionesVariables>> GetAllPorPlan(long idPlan)
		{
			var sql = await _context.CondicionesVariables.Where(x => x.idPlan == idPlan)
				//.Include(x => x.mantenimientos)
				.ToListAsync();
			return sql;
		}

		public async Task<CondicionesVariables> Set(CondicionesVariables objeto, Transaction transaccion)
		{
			switch (transaccion)
			{
				case Transaction.Insert:
					objeto.estado = true;
					return await _transact.Crear(objeto);
				case Transaction.Delete:
					return await _transact.Eliminar(objeto);
				case Transaction.Update:
					return await _transact.Actualizar(objeto);
				default:
					return objeto;
			}
		}

		public async Task<MantenimientoCondiciones> Orden(MantenimientoCondiciones objeto)
		{
			var exist = await _context.MantenimientoCondiciones.FirstOrDefaultAsync(x => x.idCondicion == objeto.idCondicion && 
																x.idPlan == objeto.idPlan && 
																x.idActivo == objeto.idActivo &&
																x.idVariable == objeto.idVariable);

			if(exist == null)
			{
				_ = _context.MantenimientoCondiciones.Add(objeto);
				_ = await _context.SaveChangesAsync();
			}
			else
			{
				exist.fecha = objeto.fecha;
				exist.acciones = objeto.acciones;
				exist.estado = objeto.estado;

				_ = _context.MantenimientoCondiciones.Update(exist);
				_ = await _context.SaveChangesAsync();
			}			

			return objeto;
		}

	}
}
