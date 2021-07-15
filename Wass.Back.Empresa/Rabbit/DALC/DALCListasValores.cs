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
    public class DALCListasValores : IDALCLectura<ListasValores>
    {
        /// <summary>
        /// Contexto de Base de Datos
        /// </summary>
        private readonly EmpresaContext _context;

        public DALCListasValores(EmpresaContext context)
        {
            _context = context;
        }
        public async Task<ListasValores> GetAsync(long id)
        {
            return await _context.ListasValores.Where(x => x.idValor == id).Include(x => x.Lista).FirstOrDefaultAsync();
        }

        public async Task<List<ListasValores>> GetAllAsync()
        {
            return await _context.ListasValores.Where(x => x.activo).Include(x => x.Lista).ToListAsync();
        }

        public async Task<List<ListasValores>> GetListaPorIdAsync(long id)
        {
            return await _context.ListasValores.Where(x => x.activo && x.idLista == id).Include(x => x.Lista).ToListAsync();
        }
    }
}
