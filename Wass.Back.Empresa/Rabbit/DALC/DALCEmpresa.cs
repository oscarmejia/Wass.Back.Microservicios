using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Rabbit.DALC
{
    public class DALCEmpresa
    {
        private readonly EmpresaContext _context;
        public DALCEmpresa(EmpresaContext context)
        {
            _context = context;
        }

		public async Task<Empresas> Get(long idEmpresa)
		{
			return await _context.Empresas.Where(x => x.idEmpresa == idEmpresa && !x.eliminado)
				//.Include(x => x.ListaSedes)
				//.Include(x => x.ListaEmpresaChecks)
				//.Include(x => x.ListaEmpresaSoportes)
				.FirstOrDefaultAsync();
		}
		public async Task<Empresas> GetPorNitAsync(string nit, int digVerficacion)
		{
			return await _context.Empresas.Where(x => x.nit.Equals(nit) && x.digVerficacion.Equals(digVerficacion) && !x.eliminado)
                //.Include(x => x.ListaSedes)
                //.Include(x => x.ListaEmpresaChecks)
                //.Include(x => x.ListaEmpresaSoportes)
                .FirstOrDefaultAsync();
		}
		public async Task<List<Empresas>> GetTodas()
		{
			return await _context.Empresas.Where(x => !x.eliminado)
				//.Include(x => x.ListaSedes)
				//.Include(x => x.ListaEmpresaChecks)
				//.Include(x => x.ListaEmpresaSoportes)
				.ToListAsync();
		}

		public async Task<List<Empresas>> GetPorTipoAfiliacionAsync(TipoAfiliacion tipoAfiliacion)
		{
			return await _context.Empresas.Where(x => x.tipoAfiliacion.Equals(tipoAfiliacion.ToString("G")) && !x.eliminado)
				//.Include(x => x.ListaSedes)
				//.Include(x => x.ListaEmpresaChecks)
				//.Include(x => x.ListaEmpresaSoportes)
				.ToListAsync();
		}

		public async Task<Empresas> Set(Empresas empresa, Transaction transaccion)
		{
			switch (transaccion)
			{
				case Transaction.Insert:
					return await Crear(empresa);
				case Transaction.Update:
					return await Actualizar(empresa);
				default:
					return empresa;
			}
		}

		private async Task<Empresas> Actualizar(Empresas empresa)
		{
			_ = _context.Update(empresa);
			_ = await _context.SaveChangesAsync();
			return empresa;
		}

		private async Task<Empresas> Crear(Empresas empresa)
		{
			_ = _context.Add(empresa);
			_ = await _context.SaveChangesAsync();
			return empresa;
		}

		public async Task<ResponseTransaction> Eliminar(long idEmpresa)
		{
			try
			{
				var empresa = _context.Empresas.Where(x => x.idEmpresa == idEmpresa).FirstOrDefault();
				empresa.eliminado = true;
				_ = _context.Update(empresa);
				_ = await _context.SaveChangesAsync();
				return new ResponseTransaction()
				{
					estado = true,
					mensaje = $"Empresa eliminada con exito."
				};
			}
			catch (Exception ex)
			{
				return new ResponseTransaction()
				{
					estado = false,
					mensaje = $"Error: {ex.Message}"
				};
			}
		}
	}
}
