using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Rabbit.Context;
namespace Wass.Back.Empresa.Rabbit.DALC
{
    public class DALCActivosVariablesHistorico
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosVariablesHistorico> _transact;

        public DALCActivosVariablesHistorico(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosVariablesHistorico>(context);
        }

        public async Task<List<ActivosVariables>> GetHistoricoPorIdVariableAsync(long idActivoClasificacionVariable)
        {
            return await _context.ActivosVariables.Where(x => x.idActivoClasificacionVariable == idActivoClasificacionVariable && !x.eliminado).OrderByDescending(z => z.fechaCreacion).ToListAsync();
        }
    }
}
