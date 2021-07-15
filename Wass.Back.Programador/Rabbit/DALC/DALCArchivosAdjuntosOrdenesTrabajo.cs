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
    public class DALCArchivosAdjuntosOrdenesTrabajo
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<ArchivosAdjuntosOrdenesTrabajo> _transac;

        public DALCArchivosAdjuntosOrdenesTrabajo(ProgramadorContext context)
        {
            _context = context;
            _transac = new DALCTransacciones<ArchivosAdjuntosOrdenesTrabajo>(context);
        }

        public async Task<ArchivosAdjuntosOrdenesTrabajo> Get(long idArchivosAdjuntosOrdenesTrabajo)
        {
            return await _context.ArchivosAdjuntosOrdenesTrabajo.Where(x => x.idArchivosAdjuntosOrdenesTrabajo == idArchivosAdjuntosOrdenesTrabajo && !x.eliminada).FirstOrDefaultAsync();
        }

        public async Task<List<ArchivosAdjuntosOrdenesTrabajo>> GetIdOrdenTrabajo(long idOrdenesTrabajo)
        {
            return await _context.ArchivosAdjuntosOrdenesTrabajo.Where(x => x.idOrdenesTrabajo == idOrdenesTrabajo && !x.eliminada).ToListAsync();
        }

        public async Task<List<ArchivosAdjuntosOrdenesTrabajo>> GetTodas()
        {
            return await _context.ArchivosAdjuntosOrdenesTrabajo.Where(x => !x.eliminada).ToListAsync();
        }

        public async Task<ArchivosAdjuntosOrdenesTrabajo> Set(ArchivosAdjuntosOrdenesTrabajo archivosAdjuntosOrdenesTrabajo, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transac.Crear(archivosAdjuntosOrdenesTrabajo);
                case Transaction.Update:
                    return await _transac.Actualizar(archivosAdjuntosOrdenesTrabajo);
                default:
                    return archivosAdjuntosOrdenesTrabajo;
            }
        }

        public async Task<ArchivosAdjuntosOrdenesTrabajo> EliminarArchivo(long idArchivo)
        {
            var get = await _context.ArchivosAdjuntosOrdenesTrabajo.FirstOrDefaultAsync(x => x.idArchivosAdjuntosOrdenesTrabajo == idArchivo);
            get.eliminada = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }
    }
}
