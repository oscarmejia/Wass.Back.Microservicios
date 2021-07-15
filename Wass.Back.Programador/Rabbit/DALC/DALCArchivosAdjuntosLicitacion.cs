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
    public class DALCArchivosAdjuntosLicitacion
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<ArchivosAdjuntosLicitacion> _transac;

        public DALCArchivosAdjuntosLicitacion(ProgramadorContext context)
        {
            _context = context;
            _transac = new DALCTransacciones<ArchivosAdjuntosLicitacion>(context);
        }

        public async Task<ArchivosAdjuntosLicitacion> Get(long idArchivoAdjuntoLicitacion)
        {
            return await _context.ArchivosAdjuntosLicitacion.Where(x => x.idArchivoAdjuntoLicitacion == idArchivoAdjuntoLicitacion && !x.eliminada).FirstOrDefaultAsync();
        }

        public async Task<List<ArchivosAdjuntosLicitacion>> GetIdLicitacion(long idLicitacion)
        {
            return await _context.ArchivosAdjuntosLicitacion.Where(x => x.idLicitacion == idLicitacion && !x.eliminada).ToListAsync();
        }

        public async Task<List<ArchivosAdjuntosLicitacion>> GetTodas()
        {
            return await _context.ArchivosAdjuntosLicitacion.Where(x => !x.eliminada).ToListAsync();
        }

        public async Task<ArchivosAdjuntosLicitacion> Set(ArchivosAdjuntosLicitacion archivosAdjuntosLicitacion, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transac.Crear(archivosAdjuntosLicitacion);
                case Transaction.Update:
                    return await _transac.Actualizar(archivosAdjuntosLicitacion);
                case Transaction.Delete:
                    archivosAdjuntosLicitacion.eliminada = true;
                    return await _transac.Actualizar(archivosAdjuntosLicitacion);
                default:
                    return archivosAdjuntosLicitacion;
            }
        }

        public async Task<ArchivosAdjuntosLicitacion> EliminarArchivo(long idArchivo)
        {
            var get = await _context.ArchivosAdjuntosLicitacion.FirstOrDefaultAsync(x => x.idArchivoAdjuntoLicitacion == idArchivo);
            get.eliminada = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }
    }
}
