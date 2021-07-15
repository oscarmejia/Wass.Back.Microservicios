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
    public class DALCMantenimientoAviso : IDALCCrud<MantenimientoAviso>
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<MantenimientoAviso> _transact;

        public DALCMantenimientoAviso(ProgramadorContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<MantenimientoAviso>(context);
        }

        public async Task<MantenimientoAviso> Get(long id)
        {
            return await _context.MantenimientoAviso.Where(x => x.idAviso == id).FirstOrDefaultAsync();
        }

        public async Task<MantenimientoAviso> GetPorOrdenAsync(long idOrden)
        {
            return await _context.MantenimientoAviso.Where(x => x.idOrden == idOrden).FirstOrDefaultAsync();
        }

        public async Task<List<MantenimientoAviso>> GetAll()
        {
            return await _context.MantenimientoAviso.ToListAsync();
        }

        public async Task<List<MantenimientoAviso>> GetAllPorCondicion(long idCondicionesVariables)
        {
            return await _context.MantenimientoAviso.Where(x => x.idCondicionesVariables == idCondicionesVariables)
                .Include(x => x.orden)
                .ToListAsync();
        }

        public async Task<List<MantenimientoAviso>> GetAllPorDiagnostico(long idDiagnostico)
        {
            return await _context.MantenimientoAviso.Where(x => x.idDiagnostico == idDiagnostico).ToListAsync();
        }

        public async Task<MantenimientoAviso> Set(MantenimientoAviso objeto, Transaction transaccion)
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
