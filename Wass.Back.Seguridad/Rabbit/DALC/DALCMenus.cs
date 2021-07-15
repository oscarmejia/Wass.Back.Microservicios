using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Seguridad.Models.Entity;
using Wass.Back.Seguridad.Models.Enum;
using Wass.Back.Seguridad.Rabbit.Context;
using Wass.Back.Seguridad.Rabbit.Interface;

namespace Wass.Back.Seguridad.Rabbit.DALC
{
    public class DALCMenus : IDALCCrud<Menus>
    {
        private readonly SeguridadContext _context;
        private readonly DALCTransacciones<Menus> _transact;

        public DALCMenus(SeguridadContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<Menus>(context);
        }

        public async Task<Menus> Get(long id)
        {
            return await _context.Menus.Where(x => x.idMenu == id && x.activo).FirstOrDefaultAsync();
        }

        public async Task<List<Menus>> GetPadres()
        {
            return await _context.Menus.Where(x => x.idPadre == null).ToListAsync();
        }

        public async Task<List<Menus>> GetHijos(long idPadre)
        {
            return await _context.Menus.Where(x => x.idPadre == idPadre && x.activo).ToListAsync();
        }

        public async Task<List<Menus>> GetAll()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menus> Set(Menus objeto, Transaction transaccion)
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
