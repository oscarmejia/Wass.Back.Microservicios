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
    public class DALCArchivosAdjuntosIncidencias
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<ArchivosAdjuntosIncidencias> _transac;

        public DALCArchivosAdjuntosIncidencias(ProgramadorContext context)
        {
            _context = context;
            _transac = new DALCTransacciones<ArchivosAdjuntosIncidencias>(context);
        }

        public async Task<ArchivosAdjuntosIncidencias> Get(long idArchivosAdjuntosIncidencias)
        {
            return await _context.ArchivosAdjuntosIncidencias.Where(x => x.idArchivosAdjuntosIncidencias == idArchivosAdjuntosIncidencias && !x.eliminada).FirstOrDefaultAsync();
        }

        public async Task<List<ArchivosAdjuntosIncidencias>> GetIdIncidencia(long idIncidencia)
        {
            return await _context.ArchivosAdjuntosIncidencias.Where(x => x.idIncidencias == idIncidencia && !x.eliminada).ToListAsync();
        }

        public async Task<List<ArchivosAdjuntosIncidencias>> GetTodas()
        {
            return await _context.ArchivosAdjuntosIncidencias.Where(x => !x.eliminada).ToListAsync();
        }

        public async Task<ArchivosAdjuntosIncidencias> Set(ArchivosAdjuntosIncidencias archivosAdjuntosIncidencias, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transac.Crear(archivosAdjuntosIncidencias);
                case Transaction.Update:
                    return await _transac.Actualizar(archivosAdjuntosIncidencias);
                default:
                    return archivosAdjuntosIncidencias;
            }
        }

        public async Task<ArchivosAdjuntosIncidencias> EliminarArchivo(long idArchivo)
        {
            var get = await _context.ArchivosAdjuntosIncidencias.FirstOrDefaultAsync(x => x.idArchivosAdjuntosIncidencias == idArchivo);
            get.eliminada = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }
    }
}
