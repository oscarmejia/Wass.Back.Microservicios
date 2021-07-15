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
    public class DALCEmpresaSkills : IDALCCrud<EmpresaSkills>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<EmpresaSkills> _transaction;

        public DALCEmpresaSkills(EmpresaContext context)
        {
            _context = context;
            _transaction = new DALCTransacciones<EmpresaSkills>(context);
        }

        public async Task<EmpresaSkills> GetAsync(long id)
        {
            return await _context.EmpresaSkills.Where(x => x.idSkill == id)
                .Include(x => x.cuadrillaSkillsEmpresa)
                .Include(x => x.diagnosticoSkillsEmpresa)
                .FirstOrDefaultAsync();
        }

        public async Task<EmpresaSkills> GetPorEmpresaAsync(long idEmpresa)
        {
            return await _context.EmpresaSkills.Where(x => x.idEmpresa == idEmpresa)
                .Include(x => x.cuadrillaSkillsEmpresa)
                .Include(x => x.diagnosticoSkillsEmpresa)
                .FirstOrDefaultAsync();
        }

        public async Task<List<EmpresaSkills>> GetAllAsync()
        {
            return await _context.EmpresaSkills
                .Include(x => x.cuadrillaSkillsEmpresa)
                .Include(x => x.diagnosticoSkillsEmpresa)
                .ToListAsync();
        }

        public async Task<EmpresaSkills> SetAsync(EmpresaSkills objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    return await _transaction.Crear(objeto);
                case Transaction.Delete:
                    return await _transaction.Actualizar(objeto);
                case Transaction.Update:
                    return await _transaction.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
