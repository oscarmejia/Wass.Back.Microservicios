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
    public class DALCMunicipios
    {
        /// <summary>
        /// Contexto de Base de Datos
        /// </summary>
        private readonly EmpresaContext _context;

        public DALCMunicipios(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<Municipios> GetAsync(long id)
        {
            return await _context.Municipios.Where(x => x.idMunicipio == id && !x.eliminado)
                .Include(x => x.departamento)
                .Include(x => x.departamento.pais)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Municipios>> GetAllAsync()
        {
            return await _context.Municipios.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<List<Municipios>> GetPorDepartamentoAsync(long idDepto)
        {
            return await _context.Municipios.Where(x => x.idDepto == idDepto && !x.eliminado).ToListAsync();
        }
    }
}
