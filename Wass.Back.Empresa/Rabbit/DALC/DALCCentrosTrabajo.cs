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
    public class DALCCentrosTrabajo : IDALCCrud<CentrosTrabajo>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<CentrosTrabajo> _transact;

        public DALCCentrosTrabajo(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<CentrosTrabajo>(context);
        }

        public async Task<CentrosTrabajo> GetAsync(long id)
        {
            return await _context.CentrosTrabajo.Where(x => x.idCentroTrabajo == id).FirstOrDefaultAsync();
        }

        public async Task<List<CentrosTrabajo>> GetAllAsync()
        {
            return await _context.CentrosTrabajo.ToListAsync();
        }

        public async Task<List<CentrosTrabajo>> GetPorSedeAsync(long id)
        {
            return await _context.CentrosTrabajo.Where(x => x.idSede == id).ToListAsync();
        }

        public async Task<List<CentrosTrabajo>> GetPorSedeActivaAsync(long id)
        {
            return await _context.CentrosTrabajo.Where(x => x.idSede == id && x.activo).ToListAsync();
        }

        public async Task<CentrosTrabajo> SetAsync(CentrosTrabajo objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    return await _transact.Crear(objeto);
                case Transaction.Update:
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
