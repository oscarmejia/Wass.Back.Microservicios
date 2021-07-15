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
    public class DALCArchivosAdjuntosCotizacion
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<ArchivosAdjuntosCotizacion> _transac;

        public DALCArchivosAdjuntosCotizacion(ProgramadorContext context)
        {
            _context = context;
            _transac = new DALCTransacciones<ArchivosAdjuntosCotizacion>(context);
        }

        public async Task<ArchivosAdjuntosCotizacion> Get(long idArchivoAdjuntoCotizacion)
        {
            return await _context.ArchivosAdjuntosCotizacion.Where(x => x.idArchivoAdjuntoCotizacion == idArchivoAdjuntoCotizacion && !x.eliminada).FirstOrDefaultAsync();
        }

        public async Task<List<ArchivosAdjuntosCotizacion>> GetIdCotizacion(long idCotizacion)
        {
            return await _context.ArchivosAdjuntosCotizacion.Where(x => x.idCotizacion == idCotizacion && !x.eliminada).ToListAsync();
        }

        public async Task<List<ArchivosAdjuntosCotizacion>> GetTodas()
        {
            return await _context.ArchivosAdjuntosCotizacion.Where(x => !x.eliminada).ToListAsync();
        }

        public async Task<ArchivosAdjuntosCotizacion> Set(ArchivosAdjuntosCotizacion archivosAdjuntosCotizacion, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transac.Crear(archivosAdjuntosCotizacion);
                case Transaction.Update:
                    return await _transac.Actualizar(archivosAdjuntosCotizacion);
                case Transaction.Delete:
                    archivosAdjuntosCotizacion.eliminada = true;
                    return await _transac.Actualizar(archivosAdjuntosCotizacion);
                default:
                    return archivosAdjuntosCotizacion;
            }
        }

        public async Task<ArchivosAdjuntosCotizacion> EliminarArchivo(long idArchivo)
        {
            var get = await _context.ArchivosAdjuntosCotizacion.FirstOrDefaultAsync(x => x.idArchivoAdjuntoCotizacion == idArchivo);
            get.eliminada = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }
    }
}
