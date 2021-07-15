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
    public class DALCMantenimientoCorrectivo : IDALCCrud<MantenimientoCorrectivo>
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<MantenimientoCorrectivo> _transact;

        public DALCMantenimientoCorrectivo(ProgramadorContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<MantenimientoCorrectivo>(context);
        }

        public async Task<MantenimientoCorrectivo> Get(long id)
        {
            return await _context.MantenimientoCorrectivo.Where(x => x.idMantenimientoCorrectivo == id).FirstOrDefaultAsync();
        }

        public async Task<List<MantenimientoCorrectivo>> GetAll()
        {
            return await _context.MantenimientoCorrectivo.ToListAsync();
        }

        public async Task<List<MantenimientoCorrectivo>> GetAllPorDiagnostico(long idDiagnostico)
        {
            return await _context.MantenimientoCorrectivo.Where(x => x.idDiagnostico == idDiagnostico).ToListAsync();
        }

        public async Task<MantenimientoCorrectivo> GetPorOrdenAsync(long idOrden)
        {
            return await _context.MantenimientoCorrectivo.Where(x => x.idOrden == idOrden).FirstOrDefaultAsync();
        }

        public async Task<List<MantenimientoCorrectivo>> GetPorOrdenAvisoAsync(long idOrdenAviso)
        {
            return await _context.MantenimientoCorrectivo.Where(x => x.idOrdenAviso == idOrdenAviso).ToListAsync();
        }

        public async Task<MantenimientoCorrectivo> Set(MantenimientoCorrectivo objeto, Transaction transaccion)
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
