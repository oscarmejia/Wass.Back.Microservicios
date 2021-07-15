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
    public class DALCCuadrillaSkillsEmpresa
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<CuadrillaSkillsEmpresa> _transaction;

        public DALCCuadrillaSkillsEmpresa(EmpresaContext context)
        {
            _context = context;
            _transaction = new DALCTransacciones<CuadrillaSkillsEmpresa>(context);
        }

        public async Task<CuadrillaSkillsEmpresa> GetAsync(long idCuadrillaSkillsEmpresa)
        {
            return await _context.CuadrillaSkillsEmpresa.Where(x => x.idCuadrillaSkillsEmpresa == idCuadrillaSkillsEmpresa).FirstOrDefaultAsync();
        }
        public async Task<CuadrillaSkillsEmpresa> GetPorSkillsCuadrillaAsync(long idSkill, long idCuadrilla)
        {
            return await _context.CuadrillaSkillsEmpresa.Where(x => x.idSkill == idSkill && x.idCuadrilla == idCuadrilla).FirstOrDefaultAsync();
        }
        public async Task<List<CuadrillaSkillsEmpresa>> GetPorCuadrillaAsync(long idCuadrilla)
        {
            return await _context.CuadrillaSkillsEmpresa.Where(x => x.idCuadrilla == idCuadrilla).Include(x => x.empresaSkills).ToListAsync();
        }

        public async Task<List<CuadrillaSkillsEmpresa>> GetPorSkillAsync(long idSkill)
        {
            return await _context.CuadrillaSkillsEmpresa.Where(x => x.idSkill == idSkill).Include(x => x.cuadrilla).ToListAsync();
        }

        public async Task<List<CuadrillaSkillsEmpresa>> GetAllAsync()
        {
            return await _context.CuadrillaSkillsEmpresa.ToListAsync();
        }

        public async Task<CuadrillaSkillsEmpresa> SetAsync(CuadrillaSkillsEmpresa objeto, Transaction transaccion)
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
