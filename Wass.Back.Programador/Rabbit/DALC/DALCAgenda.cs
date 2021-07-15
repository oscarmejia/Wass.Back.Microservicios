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
	public class DALCAgenda : IDALCCrud<Agenda>
	{
		private readonly ProgramadorContext _context;
		private readonly DALCTransacciones<Agenda> _transact;

		public DALCAgenda(ProgramadorContext context)
		{
			_context = context;
			_transact = new DALCTransacciones<Agenda>(context);
		}

		public async Task<List<Agenda>> GetAll()
		{
			return await _context.Agenda.ToListAsync();
		}

		public async Task<Agenda> Get(long id)
		{
			return await _context.Agenda.Where(x => x.idAgenda == id && x.estado).FirstOrDefaultAsync();
		}

		public async Task<List<Agenda>> GetIdRecurso(long idRecurso)
		{
			return await _context.Agenda.Where(x => x.idRecurso == idRecurso && x.estado).ToListAsync();
		}

		public async Task<List<Agenda>> GetIdRecurso(long idRecurso, DateTime fechaInicial, DateTime fechaFinal)
		{
			var sql = _context.Agenda.Where(x => x.idRecurso == idRecurso).AsQueryable();
			sql = sql.Where(x => fechaInicial >= AsignarHoras(x.fechaInicio, x.horaInicio) && fechaInicial <= AsignarHoras(x.fechaFin, x.horaFin)
								 ||
								 fechaFinal <= AsignarHoras(x.fechaFin, x.horaFin) && fechaFinal >= AsignarHoras(x.fechaInicio, x.horaInicio)
								 ||
								 fechaInicial <= AsignarHoras(x.fechaInicio, x.horaInicio) && fechaFinal >= AsignarHoras(x.fechaFin, x.horaFin)
								 ||
								 fechaInicial >= AsignarHoras(x.fechaInicio, x.horaInicio) && fechaFinal <= AsignarHoras(x.fechaFin, x.horaFin)).AsQueryable();
			return await sql.ToListAsync();
		}

		public async Task<List<Agenda>> GetIdOrdenTrabajo(long idOrden)
		{
			return await _context.Agenda.Where(x => x.idOrdenTrabajo == idOrden && x.estado).ToListAsync();
		}

		public async Task<Agenda> Set(Agenda objeto, Transaction transaccion)
		{
			switch (transaccion)
			{
				case Transaction.Insert:
					return await _transact.Crear(objeto);
				case Transaction.Update:
					return await _transact.Eliminar(objeto);
				default:
					return objeto;
			}
		}

		public async Task<Agenda> CancelAgenda(long idAgenda)
		{
			var get = await _context.Agenda.FirstOrDefaultAsync(x => x.idAgenda == idAgenda);
			get.estado = false;
			_context.Update(get);
			await _context.SaveChangesAsync();

			return get;
		}

		public async Task<List<Turnos>> getTurnosRecurso(long idRecurso)
		{
			var result = await (from t in _context.Turnos
								join c in _context.CuadrillasTurnos on t.idTurno equals c.idTurno
								where c.idCuadrilla == idRecurso
								select t).Distinct().ToListAsync();
			return result;
		}

		private DateTime AsignarHoras(DateTime fecha, string horas)
		{
			var strFecha = fecha.ToString("yyyy-MM-dd");
			var nfecha = Convert.ToDateTime(strFecha);

			var split = horas.Split(":");

			nfecha = nfecha.AddHours(int.Parse(split[0]));
			nfecha = nfecha.AddMinutes(int.Parse(split[1]));
			nfecha = nfecha.AddSeconds(int.Parse(split[2]));

			return nfecha;
		}
	}
}
