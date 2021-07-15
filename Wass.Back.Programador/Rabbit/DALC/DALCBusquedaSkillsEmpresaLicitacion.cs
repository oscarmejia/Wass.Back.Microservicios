using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.Interface;
using WASS.Back.Programador.core.rabbit.DALC;

namespace Wass.Back.Programador.Rabbit.DALC
{
    public class DALCBusquedaSkillsEmpresaLicitacion
    {
        private readonly ProgramadorContext _context;
        
        public DALCBusquedaSkillsEmpresaLicitacion(ProgramadorContext context)
        {
            _context = context;
            
        }

        public async Task<List<Licitacion>> Get(string buscar)
        {
            return await _context.Licitacion.Where(x => x.skills.skills.Contains(buscar))
                .Include(x => x.cronograma)
                .Include(x => x.soportes)
                .Include(x => x.cotizaciones)
                .Include(x => x.skills)
                .ToListAsync();
        }

        
    }
}
