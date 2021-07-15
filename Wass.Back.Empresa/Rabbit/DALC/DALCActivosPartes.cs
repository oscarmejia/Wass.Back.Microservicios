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
    public class DALCActivosPartes : IDALCCrudGuid<ActivosPartes>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosPartes> _transact;

        public DALCActivosPartes(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosPartes>(context);
        }

        public async Task<ActivosPartes> GetAsync(Guid id)
        {
            return await _context.ActivosPartes.Where(x => x.idParte == id && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosPartes>> GetAllAsync()
        {
            return await _context.ActivosPartes.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosPartes>> GetPorClasificacionAsync(long idClasificacion)
        {
            return await _context.ActivosPartes.Where(x => x.idClasificacion == idClasificacion && !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosPartes>> GetPorSubParteAsync(Guid idParte)
        {
            return await _context.ActivosPartes.Where(x => x.idSubParte == idParte && !x.eliminado).ToListAsync();
        }

        public async Task<ActivosPartes> SetAsync(ActivosPartes objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.idParte = Guid.NewGuid();
                    objeto.eliminado = false;
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.eliminado = true;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    objeto.eliminado = false;
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
