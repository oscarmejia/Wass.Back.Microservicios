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
    public class DALCActivosClasificacion : IDALCCrud<ActivosClasificacion>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosClasificacion> _transact;
        public DALCActivosClasificacion(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosClasificacion>(context);
        }

        public async Task<ActivosClasificacion> GetAsync(long id)
        {
            return await _context.ActivosClasificacion.Where(x => x.idClasificacion == id && !x.eliminado).Include(x => x.ListaPartes).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosClasificacion>> GetAllAsync()
        {
            return await _context.ActivosClasificacion.Where(x => !x.eliminado).Include(x => x.ListaPartes).ToListAsync();
        }

        public async Task<List<ActivosClasificacion>> GetPorCategorizacionAsync(long idCategorizacion)
        {
            return await _context.ActivosClasificacion.Where(x => x.idCategorizacion == idCategorizacion && !x.eliminado).Include(x => x.ListaPartes).ToListAsync();
        }

        public async Task<List<ActivosClasificacion>> GetPorClasificaionAsync(long idClasificacion)
        {
            return await _context.ActivosClasificacion.Where(x => x.idSubClasificaicon == idClasificacion && !x.eliminado).Include(x => x.ListaPartes).ToListAsync();
        }

        public async Task<ActivosClasificacion> SetAsync(ActivosClasificacion objeto, Transaction transaccion)
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
