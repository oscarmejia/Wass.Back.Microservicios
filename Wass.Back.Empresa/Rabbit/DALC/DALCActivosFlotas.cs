using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.Interface;
namespace Wass.Back.Empresa.Rabbit.DALC
{
    public class DALCActivosFlotas : IDALCCrudGuid<ActivosFlotas>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosFlotas> _transact;

        public DALCActivosFlotas(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosFlotas>(context);
        }

        public async Task<ActivosFlotas> GetAsync(Guid id)
        {
            return await _context.ActivosFlotas.Where(x => x.idActivo == id && !x.Eliminado)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.adquisicion)
                .Include(x => x.caracteristicas)
                .Include(x => x.ubicacion)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ActivosFlotas>> GetAllAsync()
        {
            return await _context.ActivosFlotas.Where(x => !x.Eliminado)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.adquisicion)
                .Include(x => x.caracteristicas)
                .Include(x => x.ubicacion)
                .ToListAsync();
        }

        public async Task<List<ActivosFlotas>> ObtenerFlotasCategoriaClasificacionSubClasificacionSedeMarca(long idCategoria, long idClasificacion1, long idSedeResponsable, string marca, long? idClasificacion2 = null)
        {
            return await _context.ActivosFlotas.Where(x => !x.Eliminado && x.idCategoria == idCategoria && x.idClasificacion1 == idClasificacion1 && x.idClasificacion2 == idClasificacion2 && x.idSedeResponsable == idSedeResponsable && x.Marca == marca)
                .Include(x => x.ArchivosAdjuntos).ToListAsync();
        }

        public async Task<List<ActivosFlotas>> GetPorSedeAsync(long idSede)
        {
            return await _context.ActivosFlotas.Where(x => x.idSedeResponsable == idSede && !x.Eliminado).ToListAsync();
        }

        public async Task<List<ActivosFlotas>> GetPorEmpresaAsync(long idEmpresa)
        {
            var sql = (from flota in _context.ActivosFlotas
                       join sede in _context.Sedes on flota.idSedeResponsable equals sede.idSede
                       where sede.idEmpresa == idEmpresa
                       select flota
                       ).AsQueryable();

            return await sql
                .Include(x => x.adquisicion)
                .Include(x => x.caracteristicas)
                .Include(x => x.ubicacion)
                .ToListAsync();
        }

        public async Task<ActivosFlotas> SetAsync(ActivosFlotas objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.idActivo = Guid.NewGuid();
                    objeto.Eliminado = false;
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.Eliminado = true;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    objeto.Eliminado = false;
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
