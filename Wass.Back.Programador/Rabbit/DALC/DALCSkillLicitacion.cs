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
    public class DALCSkillLicitacion
    {
        private readonly ProgramadorContext _context;
        public DALCSkillLicitacion(ProgramadorContext context)
        {
            _context = context;
        }

        public async Task<SkillLicitacion> Get( long idSkillLicitacion)
        {
            return await _context.SkillLicitacion.Where(x => x.idSkillLicitacion == idSkillLicitacion).FirstOrDefaultAsync();
        }

        public async Task<List<SkillLicitacion>> GetTodas ()
        {
            return await _context.SkillLicitacion.ToListAsync();
        }

        public async Task<SkillLicitacion> GetPorLicitacion(long idLicitacion)
        {
            return await _context.SkillLicitacion.Where(x => x.idLicitacion == idLicitacion).FirstOrDefaultAsync();
        }

        public async Task<SkillLicitacion> Set (SkillLicitacion skillLicitacion, Transaction trasaction)
        {
            switch (trasaction)
            {
                case Transaction.Insert:
                    return await Crear(skillLicitacion);
                case Transaction.Update:
                    return await Editar(skillLicitacion);
                default:
                    return skillLicitacion;
            }
        }

        public async Task<SkillLicitacion> Crear(SkillLicitacion skill)
        {
            _ = _context.Add(skill);
            _ = await _context.SaveChangesAsync();

            return skill;
        }

        public async Task<SkillLicitacion> Editar(SkillLicitacion skill)
        {
            _ = _context.Update(skill);
            _ = await _context.SaveChangesAsync();

            return skill;
        }
    }
}
