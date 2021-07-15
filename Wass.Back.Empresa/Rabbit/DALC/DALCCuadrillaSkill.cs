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
    public class DALCCuadrillaSkill
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<CuadrillaSkill> _DALCTransaccion;
        public DALCCuadrillaSkill(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<CuadrillaSkill> get(long idCuadrillaSkill)
        {
            return await _context.CuadrillaSkill.Where(x => x.idCuadrillaSkill == idCuadrillaSkill).FirstOrDefaultAsync();
        }

        public async Task<List<CuadrillaSkill>> getTodos()
        {
            return await _context.CuadrillaSkill.ToListAsync();
        }

        public async Task<List<CuadrillaSkill>> getTodosporCuadrilla(long idCuadrilla)
        {
            return await _context.CuadrillaSkill.Where(x => x.idCuadrilla == idCuadrilla).ToListAsync();
        }

        public async Task<List<CuadrillaSkill>> getTodosporSkill(long idSkill)
        {
            return await _context.CuadrillaSkill.Where(x => x.idSkill == idSkill).ToListAsync();
        }

        public async Task<CuadrillaSkill> set(CuadrillaSkill cuadrillaSkill, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(cuadrillaSkill);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(cuadrillaSkill);

                default:
                    return cuadrillaSkill;
            }
        }
    }
}
