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
    public class DALCEmpresaChecks
    {
		private readonly EmpresaContext _context;
		private readonly DALCTransacciones<EmpresaChecks> _transaction;

		public DALCEmpresaChecks(EmpresaContext context)
		{
			_context = context;
			_transaction = new DALCTransacciones<EmpresaChecks>(context);
		}

		public async Task<EmpresaChecks> GetAsync(long id)
		{
			return await _context.EmpresaChecks.Where(x => x.idEmpresaCheck == id).FirstOrDefaultAsync();
		}

		public async Task<List<EmpresaChecks>> GetPorEmpresaAsync(long idEmpresa)
		{
			return await _context.EmpresaChecks.Where(x => x.idEmpresa == idEmpresa).ToListAsync();
		}

		public async Task<List<EmpresaChecks>> GetAllAsync()
		{
			return await _context.EmpresaChecks.ToListAsync();
		}

		public async Task<List<EmpresaChecks>> SetAsync(List<EmpresaChecks> objeto, Transaction transaccion)
		{
			switch (transaccion)
			{
				case Transaction.Insert:
					return await _transaction.CrearRango(objeto);
				case Transaction.Delete:
					return await _transaction.BorrarRango(objeto);
				case Transaction.Update:
					return await _transaction.ActualizarRango(objeto);
				default:
					return objeto;
			}
		}

		public async Task<EmpresaChecks> ActivarInactivarCheckAsync(EmpresaChecks check, bool estado)
		{
			check.estado = estado;
			_context.EmpresaChecks.Update(check);
			await _context.SaveChangesAsync();

			return check;
		}
	}
}
