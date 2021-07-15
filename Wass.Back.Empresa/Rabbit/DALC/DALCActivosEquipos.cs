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
    public class DALCActivosEquipos : IDALCCrudGuid<ActivosEquipos>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosEquipos> _transact;

        public DALCActivosEquipos(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosEquipos>(context);
        }

        public async Task<ActivosEquipos> GetAsync(Guid id)
        {
            return await _context.ActivosEquipos.Where(x => x.idActivo == id && !x.Eliminado)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.marca)
                .Include(x => x.adquisicion)
                .Include(x => x.caracteristicas)
                .Include(x => x.ubicacion)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ActivosEquipos>> GetAllAsync()
        {
            return await _context.ActivosEquipos.Where(x => !x.Eliminado)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.marca)
                .Include(x => x.adquisicion)
                .Include(x => x.caracteristicas)
                .Include(x => x.ubicacion)
                .ToListAsync();
        }

        public async Task<List<ActivosEquipos>> GetPorSedeAsync(long idSede)
        {
            return await _context.ActivosEquipos.Where(x => x.idSedeResponsable == idSede && !x.Eliminado).ToListAsync();
        }

        public async Task<List<ActivosEquipos>> GetPorMarcaAsync(long idMarca)
        {
            return await _context.ActivosEquipos.Where(x => x.idMarca == idMarca && !x.Eliminado)
                .Include(x => x.marca)
                .ToListAsync();
        }

        public async Task<List<ActivosEquipos>> GetPorEmpresaAsync(long idEmpresa)
        {
            var sql = (from equipo in _context.ActivosEquipos
                       join sede in _context.Sedes on equipo.idSedeResponsable equals sede.idSede
                       where sede.idEmpresa == idEmpresa
                       select equipo
                       ).AsQueryable();

            return await sql
                .Include(x => x.marca)
                .Include(x => x.adquisicion)
                .Include(x => x.caracteristicas)
                .Include(x => x.ubicacion)
                .ToListAsync();
        }


        public async Task<List<ActivosEquipos>> ObtenerEquiposCategoriaClasificacionSubClasificacionSedeMarca(long idCategoria, long idClasificacion1, long idSedeResponsable, long idMarca, long? idClasificacion2 = null)
        {
            return await _context.ActivosEquipos.Where(x => !x.Eliminado && x.idCategoria == idCategoria && x.idClasificacion1 == idClasificacion1 && x.idClasificacion2 == idClasificacion2 && x.idSedeResponsable == idSedeResponsable && x.idMarca == idMarca)
                .Include(x => x.ArchivosAdjuntos).ToListAsync();
        }
        public async Task<ActivosEquipos> SetAsync(ActivosEquipos objeto, Transaction transaccion)
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
