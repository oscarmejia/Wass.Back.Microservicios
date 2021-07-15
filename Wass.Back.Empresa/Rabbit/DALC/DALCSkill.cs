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
    public class DALCSkill
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<Skill> _DALCTransaccion;

        public DALCSkill(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<Skill>(context);
        }

        public async Task<Skill> get(long idSkills)
        {
            return await _context.Skills.Where(x => x.idSkill == idSkills).FirstOrDefaultAsync();
        }

        public async Task<List<Skill>> getTodos()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<List<Skill>> getTodosPorEmpresa(long idEmpresa)
        {
            return await _context.Skills.Where(x => x.idEmpresa == idEmpresa).ToListAsync();
        }

        public async Task<Skill> set(Skill skill, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(skill);
                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(skill);
                default:
                    return skill;
            }
        }
    }
}
