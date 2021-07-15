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
    public class DALCActivosUbicacion : IDALCCrudGuid<ActivosUbicacion>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosUbicacion> _transact;

        public DALCActivosUbicacion(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosUbicacion>(context);
        }

        public async Task<ActivosUbicacion> GetAsync(Guid id)
        {
            return await _context.ActivosUbicacion.Where(x => x.idUbicacion == id && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosUbicacion>> GetAllAsync()
        {
            return await _context.ActivosUbicacion.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosUbicacion>> GetPorEquipoAsync(Guid id)
        {
            return await _context.ActivosUbicacion.Where(x => x.idActivosEquipos == id && !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosUbicacion>> GetPorFlotaAsync(Guid id)
        {
            return await _context.ActivosUbicacion.Where(x => x.idActivoFlota == id && !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosUbicacion>> GetPorEquipoUbicacionAsync(Guid id, TiposUbicacion ubicacion)
        {
            return await _context.ActivosUbicacion.Where(x => x.idActivosEquipos == id && x.idTipoUbicacion == (int)ubicacion && !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosUbicacion>> GetPorFlotaUbicacionAsync(Guid id, TiposUbicacion ubicacion)
        {
            return await _context.ActivosUbicacion.Where(x => x.idActivoFlota == id && x.idTipoUbicacion == (int)ubicacion && !x.eliminado).ToListAsync();
        }

        public async Task<ActivosUbicacion> SetAsync(ActivosUbicacion objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.idUbicacion = Guid.NewGuid();
                    objeto.eliminado = false;
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.eliminado = true;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    objeto.eliminado = false;
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
