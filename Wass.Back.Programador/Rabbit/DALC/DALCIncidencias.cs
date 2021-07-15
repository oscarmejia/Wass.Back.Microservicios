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
    public class DALCIncidencias
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<Incidencias> _transact;
        public DALCIncidencias(ProgramadorContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<Incidencias>(context);
        }


        public async Task<Incidencias> getAsync(long idIncidencias)
        {
            return await _context.Incidencias.Where(x => x.idIncidencias == idIncidencias)
                .Include(x => x.ArchivosAdjuntos)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Incidencias>> getTodasAsync()
        {
            return await _context.Incidencias.Include(x => x.ArchivosAdjuntos).ToListAsync();
        }


        public async Task<Incidencias> setAsync(Incidencias incidencias, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transact.Crear(incidencias);
                case Transaction.Update:
                    return await _transact.Actualizar(incidencias);
                default:
                    return incidencias;
            }
        }
    }
}
