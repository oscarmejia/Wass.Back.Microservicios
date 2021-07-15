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
    public class DALCDepartamentos : IDALCLectura<Departamentos>
    {
        /// <summary>
        /// Contexto de Base de Datos
        /// </summary>
        private readonly EmpresaContext _context;

        public DALCDepartamentos(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<Departamentos> GetAsync(long id)
        {
            return await _context.Departamentos.Where(x => x.idDepto == id && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<Departamentos>> GetAllAsync()
        {
            return await _context.Departamentos.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<List<Departamentos>> GetPorPaisAsync(long idPais)
        {
            return await _context.Departamentos.Where(x => !x.eliminado && x.idPais == idPais).ToListAsync();
        }
    }
}
