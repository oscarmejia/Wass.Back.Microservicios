using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Rabbit.DALC
{
    public class DALCArchivosAdjuntosActivosFlotas
    {

        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ArchivosAdjuntosActivosFlotas> _transac;

        public DALCArchivosAdjuntosActivosFlotas(EmpresaContext context)
        {
            _context = context;
            _transac = new DALCTransacciones<ArchivosAdjuntosActivosFlotas>(context);
        }

        public async Task<ArchivosAdjuntosActivosFlotas> Get(long idArchivosAdjuntosActivosFlotas)
        {
            return await _context.ArchivosAdjuntosActivosFlotas.Where(x => x.idArchivoAdjuntoActivosFlotas == idArchivosAdjuntosActivosFlotas && !x.eliminada).FirstOrDefaultAsync();
        }

        public async Task<List<ArchivosAdjuntosActivosFlotas>> GetIdActivosFlotas(Guid idActivo)
        {
            return await _context.ArchivosAdjuntosActivosFlotas.Where(x => x.idActivo == idActivo && !x.eliminada).ToListAsync();
        }

        public async Task<List<ArchivosAdjuntosActivosFlotas>> GetTodas()
        {
            return await _context.ArchivosAdjuntosActivosFlotas.Where(x => !x.eliminada).ToListAsync();
        }

        public async Task<ArchivosAdjuntosActivosFlotas> Set(ArchivosAdjuntosActivosFlotas archivosAdjuntosActivosFlotas, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transac.Crear(archivosAdjuntosActivosFlotas);
                case Transaction.Update:
                    return await _transac.Actualizar(archivosAdjuntosActivosFlotas);
                case Transaction.Delete:
                    archivosAdjuntosActivosFlotas.eliminada = true;
                    return await _transac.Actualizar(archivosAdjuntosActivosFlotas);
                default:
                    return archivosAdjuntosActivosFlotas;
            }
        }

        public async Task<ArchivosAdjuntosActivosFlotas> EliminarArchivo(long idArchivo)
        {
            var get = await _context.ArchivosAdjuntosActivosFlotas.FirstOrDefaultAsync(x => x.idArchivoAdjuntoActivosFlotas == idArchivo);
            get.eliminada = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }
    }
}
