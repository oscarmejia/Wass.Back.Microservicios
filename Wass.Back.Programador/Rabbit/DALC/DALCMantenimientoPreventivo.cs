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
    public class DALCMantenimientoPreventivo
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<MantenimientoPreventivo> _transact;

        public DALCMantenimientoPreventivo(ProgramadorContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<MantenimientoPreventivo>(context);
        }

        public async Task<MantenimientoPreventivo> Get(long idMantenimientoPreventivo)
        {
            return await _context.MantenimientoPreventivo.Where(x => x.idMantenimientoPreventivo == idMantenimientoPreventivo && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<MantenimientoPreventivo>> GetAll()
        {
            return await _context.MantenimientoPreventivo.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<List<MantenimientoPreventivo>> GetAllPorGrupo(long idGrupo)
        {
            return await _context.MantenimientoPreventivo.Where(x => x.idGrupo == idGrupo).ToListAsync();
        }

        public async Task<List<MantenimientoPreventivo>> GetAllPorPlan(long idPlan)
        {
            return await _context.MantenimientoPreventivo.Where(x => x.idPlan == idPlan)
                .Include(x => x.orden)
                .ToListAsync();
        }

        public async Task<MantenimientoPreventivo> GetPorOrdenAsync(long idOrden)
        {
            return await _context.MantenimientoPreventivo.Where(x => x.idOrden == idOrden && !x.eliminado).FirstOrDefaultAsync();
        }
        
        public async Task<MantenimientoPreventivo> Set(MantenimientoPreventivo objeto, Transaction transaccion)
        {
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
    }
}
