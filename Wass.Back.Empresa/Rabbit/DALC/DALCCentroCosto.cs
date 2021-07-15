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
    public class DALCCentroCosto
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<CentroCosto> _DALCTransaccion;

        public DALCCentroCosto(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<CentroCosto>(context);
        }

        public async Task<CentroCosto> Get(long idCentroCosto)
        {
            return await _context.CentroCosto.Where(x => x.idCentroCosto == idCentroCosto && !x.eliminado)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CentroCosto>> GetPorCentroCostoPadre(string idCentroCostoPadre)
        {
            return await _context.CentroCosto.Where(x => x.idCentroCostoPadre == idCentroCostoPadre && !x.eliminado)
                .Include(x => x.Sedes)
                .ToListAsync();
        }

        public async Task<List<CentroCosto>> GetTodas()
        {
            return await _context.CentroCosto.Where(x => !x.eliminado)
                .Include(x => x.Sedes)
                .ToListAsync();
        }

        public async Task<List<CentroCosto>> GetTodasPorEmpresa(long idEmpresa)
        {
            return await _context.CentroCosto.Where(x => x.idEmpresa == idEmpresa && !x.eliminado)
                .Include(x => x.Sedes)
                .ToListAsync();
        }

        public async Task<CentroCosto> EliminarCentroCosto(long idCentroCosto)
        {
            var get = await _context.CentroCosto.FirstOrDefaultAsync(x => x.idCentroCosto == idCentroCosto);
            var centroCostoHijos = await GetPorCentroCostoPadre(get.idCentroCosto.ToString());
            get.eliminado = true;
            _context.Update(get);
            await _context.SaveChangesAsync();
            if (centroCostoHijos.Count > 0)
            {
                foreach (var item in centroCostoHijos)
                {
                    item.eliminado = true;

                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
            }

            return get;
        }


        public async Task<CentroCosto> Set(CentroCosto centroCosto, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(centroCosto);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(centroCosto);

                default:
                    return centroCosto;
            }
        }
    }
}
