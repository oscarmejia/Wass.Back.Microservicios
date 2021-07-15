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
    public class DALCDiagnosticoSkillsEmpresa
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<DiagnosticoSkillsEmpresa> _transaction;

        public DALCDiagnosticoSkillsEmpresa(EmpresaContext context)
        {
            _context = context;
            _transaction = new DALCTransacciones<DiagnosticoSkillsEmpresa>(context);
        }

        public async Task<DiagnosticoSkillsEmpresa> GetAsync(long idDiagnosticoSkillsEmpresa)
        {
            return await _context.DiagnosticoSkillsEmpresa.Where(x => x.idDiagnosticoSkillsEmpresa == idDiagnosticoSkillsEmpresa).FirstOrDefaultAsync();
        }

        public async Task<DiagnosticoSkillsEmpresa> GetPorSkillDiagnosticoAsync(long idSkill, long idDiagnostico)
        {
            return await _context.DiagnosticoSkillsEmpresa.Where(x => x.idSkill == idSkill && x.idDiagnostico == idDiagnostico).FirstOrDefaultAsync();
        }

        public async Task<List<DiagnosticoSkillsEmpresa>> GetPorDiagnosticoAsync(long idDiagnostico)
        {
            return await _context.DiagnosticoSkillsEmpresa.Where(x => x.idDiagnostico == idDiagnostico).Include(x => x.empresaSkills).ToListAsync();
        }

        public async Task<List<DiagnosticoSkillsEmpresa>> GetPorSkillAsync(long idSkill)
        {
            return await _context.DiagnosticoSkillsEmpresa.Where(x => x.idSkill == idSkill).Include(x => x.ActivosClasificacionDiagnosticos).ToListAsync();
        }

        public async Task<List<DiagnosticoSkillsEmpresa>> GetAllAsync()
        {
            return await _context.DiagnosticoSkillsEmpresa.ToListAsync();
        }

        public async Task<DiagnosticoSkillsEmpresa> SetAsync(DiagnosticoSkillsEmpresa objeto, Transaction transaccion)
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
