using System.Threading.Tasks;
using Wass.Back.Programador.Rabbit.Context;

namespace WASS.Back.Programador.core.rabbit.DALC
{
    public class DALCTransacciones<T>
    {
        private readonly ProgramadorContext _context;

        public DALCTransacciones(ProgramadorContext context)
        {
            _context = context;
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

        public async Task<T> Eliminar(T objeto)
        {
            _ = _context.Remove(objeto);
            _ = await _context.SaveChangesAsync();
            return objeto;
        }
    }
}
