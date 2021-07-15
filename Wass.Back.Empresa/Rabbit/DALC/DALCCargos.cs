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
    public class DALCCargos : IDALCLectura<Cargos>
    {
        /// <summary>
        /// Contexto de Base de Datos
        /// </summary>
        private readonly EmpresaContext _context;

        public DALCCargos(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<Cargos> GetAsync(long id)
        {
            return await _context.Cargos.Where(x => x.idCargo == id).FirstOrDefaultAsync();
        }

        public async Task<List<Cargos>> GetAllAsync()
        {
            return await _context.Cargos.ToListAsync();
        }
    }
}
