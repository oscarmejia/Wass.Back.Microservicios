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
    public class DALCArchivosAdjuntosActivosEquipos
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ArchivosAdjuntosActivosEquipos> _transac;

        public DALCArchivosAdjuntosActivosEquipos(EmpresaContext context)
        {
            _context = context;
            _transac = new DALCTransacciones<ArchivosAdjuntosActivosEquipos>(context);
        }

        public async Task<ArchivosAdjuntosActivosEquipos> Get(long idArchivosAdjuntosActivosEquipos)
        {
            return await _context.ArchivosAdjuntosActivosEquipos.Where(x => x.idArchivoAdjuntoActivosEquipos == idArchivosAdjuntosActivosEquipos && !x.eliminada).FirstOrDefaultAsync();
        }

        public async Task<List<ArchivosAdjuntosActivosEquipos>> GetIdActivosEquipo(Guid idActivo)
        {
            return await _context.ArchivosAdjuntosActivosEquipos.Where(x => x.idActivo == idActivo && !x.eliminada).ToListAsync();
        }

        public async Task<List<ArchivosAdjuntosActivosEquipos>> GetTodas()
        {
            return await _context.ArchivosAdjuntosActivosEquipos.Where(x => !x.eliminada).ToListAsync();
        }

        public async Task<ArchivosAdjuntosActivosEquipos> Set(ArchivosAdjuntosActivosEquipos archivosAdjuntosActivosEquipos, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transac.Crear(archivosAdjuntosActivosEquipos);
                case Transaction.Update:
                    return await _transac.Actualizar(archivosAdjuntosActivosEquipos);
                case Transaction.Delete:
                    archivosAdjuntosActivosEquipos.eliminada = true;
                    return await _transac.Actualizar(archivosAdjuntosActivosEquipos);
                default:
                    return archivosAdjuntosActivosEquipos;
            }
        }

        public async Task<ArchivosAdjuntosActivosEquipos> EliminarArchivo(long idArchivo)
        {
            var get = await _context.ArchivosAdjuntosActivosEquipos.FirstOrDefaultAsync(x => x.idArchivoAdjuntoActivosEquipos == idArchivo);
            get.eliminada = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }
    }
}
