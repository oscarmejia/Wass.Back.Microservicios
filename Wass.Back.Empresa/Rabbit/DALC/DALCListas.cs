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
    public class DALCListas : IDALCLectura<Listas>
    {
        /// <summary>
        /// Contexto de Base de Datos
        /// </summary>
        private readonly EmpresaContext _context;

        public DALCListas(EmpresaContext context)
        {
            this._context = context;
        }
        public async Task<Listas> GetAsync(long id)
        {
            return await _context.Listas.Where(x => x.idLista == id).Include(x => x.listasValores).FirstOrDefaultAsync();
        }

        public async Task<List<Listas>> GetAllAsync()
        {
            return await _context.Listas.Where(x => x.activo).Include(x => x.listasValores).ToListAsync();
        }
    }
}
