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
    public class DALCActivosClasificacionDiagnosticosSkills : IDALCCrud<ActivosClasificacionDiagnosticosSkills>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosClasificacionDiagnosticosSkills> _transact;

        public DALCActivosClasificacionDiagnosticosSkills(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosClasificacionDiagnosticosSkills>(context);
        }

        public async Task<ActivosClasificacionDiagnosticosSkills> GetAsync(long id)
        {
            return await _context.ActivosClasificacionDiagnosticosSkills.Where(x => x.idDiagnosticosSkills == id && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<ActivosClasificacionDiagnosticosSkills> GetPorSkillDiagnosticoAsync(long idDiagnostico, long idSkill)
        {
            return await _context.ActivosClasificacionDiagnosticosSkills.Where(x => x.idDiagnostico == idDiagnostico && x.idSkill == idSkill && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosClasificacionDiagnosticosSkills>> GetAllAsync()
        {
            return await _context.ActivosClasificacionDiagnosticosSkills.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosClasificacionDiagnosticosSkills>> GetPorDiagnosticoAsync(long idDiagnostico)
        {
            return await _context.ActivosClasificacionDiagnosticosSkills.Where(x => x.idDiagnostico == idDiagnostico && !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosClasificacionDiagnosticosSkills>> GetPorSkillAsync(long idSkill)
        {
            return await _context.ActivosClasificacionDiagnosticosSkills.Where(x => x.idSkill == idSkill && !x.eliminado).ToListAsync();
        }

        public async Task<ActivosClasificacionDiagnosticosSkills> SetAsync(ActivosClasificacionDiagnosticosSkills objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.eliminado = false;
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.eliminado = true;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    objeto.eliminado = false;
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
