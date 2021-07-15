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
    public class DALCActivosCategorizacion : IDALCCrud<ActivosCategorizacion>
    {

        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosCategorizacion> _transact;
        public DALCActivosCategorizacion(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosCategorizacion>(context);
        }

        public async Task<ActivosCategorizacion> GetAsync(long id)
        {
            return await _context.ActivosCategorizacion.Where(x => x.idCategorizacion == id && !x.eliminado).Include(x => x.ListaClasificaciones).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosCategorizacion>> GetAllAsync()
        {
            return await _context.ActivosCategorizacion.Where(x => !x.eliminado).Include(x => x.ListaClasificaciones).ToListAsync();
        }

        public async Task<List<ActivosCategorizacion>> GetPorEmpresaAsync(long idEmpresa)
        {
            return await _context.ActivosCategorizacion.Where(x => x.idEmpresa == idEmpresa && !x.eliminado).Include(x => x.ListaClasificaciones).ToListAsync();
        }

        public async Task<ActivosCategorizacion> SetAsync(ActivosCategorizacion objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
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
