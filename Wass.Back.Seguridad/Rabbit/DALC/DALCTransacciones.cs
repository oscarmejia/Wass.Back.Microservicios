using System;
using System.Threading.Tasks;
using Wass.Back.Seguridad.Rabbit.Context;

namespace Wass.Back.Seguridad.Rabbit.DALC
{
    public class DALCTransacciones<T>
    {
        private readonly SeguridadContext _context;

        public DALCTransacciones(SeguridadContext context)
        {
            this._context = context;
        }
        public async Task<T> Actualizar(T objeto)
        {
            _ = _context.Update(objeto);
            _ = await _context.SaveChangesAsync();
            return objeto;
        }

        public async Task<T> Crear(T objeto)
        {
            _ = _context.Add(objeto);
            _ = await _context.SaveChangesAsync();
            return objeto;
        }
    }
}
