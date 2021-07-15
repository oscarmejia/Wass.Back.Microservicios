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
    public class DALCPaises
    {
        private readonly EmpresaContext _context;

        public DALCPaises(EmpresaContext context)
        {
            this._context = context;
        }

        public async Task<Paises> GetAsync(long id)
        {
            return await _context.Paises.Where(x => x.idPais == id && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<Paises>> GetAllAsync()
        {
            return await _context.Paises.Where(x => !x.eliminado).ToListAsync();
        }
    }
}
