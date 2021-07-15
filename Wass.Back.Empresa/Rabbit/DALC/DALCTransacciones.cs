using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Rabbit.DALC
{
    public class DALCTransacciones<T>
    {
        private readonly EmpresaContext _context;
        public DALCTransacciones(EmpresaContext context)
        {
            this._context = context;
        }

        public async Task<T> Actualizar(T objeto)
        {
            _ = _context.Update(objeto);
            _ = await _context.SaveChangesAsync();
            return objeto;
        }

        public async Task<List<T>> ActualizarRango(List<T> objeto)
        {
            foreach (var item in objeto)
            {
                _ = _context.Update(item);
            }
            _ = await _context.SaveChangesAsync();
            return objeto;
        }

        public async Task<T> Crear(T objeto)
        {
            _ = _context.Add(objeto);
            _ = await _context.SaveChangesAsync();
            return objeto;
        }

        public async Task<List<T>> CrearRango(List<T> objeto)
        {
            foreach (var item in objeto)
            {
                _context.Add(item);
            }
            _ = await _context.SaveChangesAsync();
            return objeto;
        }

        public async Task<T> Borrar(T objeto)
        {
            _ = _context.Remove(objeto);
            _ = await _context.SaveChangesAsync();
            return objeto;
        }

        public async Task<List<T>> BorrarRango(List<T> objeto)
        {
            foreach (var item in objeto)
            {
                _context.Remove(item);
            }
            _ = await _context.SaveChangesAsync();
            return objeto;
        }
    }
}
