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
    public class DALCActivosAdquisicion : IDALCCrudGuid<ActivosAdquisicion>
    {

        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosAdquisicion> _transact;

        public DALCActivosAdquisicion(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosAdquisicion>(context);
        }

        public async Task<ActivosAdquisicion> GetAsync(Guid id)
        {
            return await _context.ActivosAdquisicion.Where(x => x.idActivosAdquisicion == id && !x.Eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosAdquisicion>> GetAllAsync()
        {
            return await _context.ActivosAdquisicion.Where(x => !x.Eliminado).ToListAsync();
        }

        public async Task<List<ActivosAdquisicion>> GetPorEquipoAsync(Guid id)
        {
            return await _context.ActivosAdquisicion.Where(x => x.idActivosEquipos == id && !x.Eliminado).ToListAsync();
        }

        public async Task<List<ActivosAdquisicion>> GetPorFlotaAsync(Guid id)
        {
            return await _context.ActivosAdquisicion.Where(x => x.idActivosFlotas == id && !x.Eliminado).ToListAsync();
        }

        public async Task<ActivosAdquisicion> SetAsync(ActivosAdquisicion objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.idActivosAdquisicion = Guid.NewGuid();
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
