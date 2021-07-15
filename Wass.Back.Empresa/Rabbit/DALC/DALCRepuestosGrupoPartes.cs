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
    public class DALCRepuestosGrupoPartes
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<RepuestosGrupoPartes> _DALCTransaccion;
        public DALCRepuestosGrupoPartes(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<RepuestosGrupoPartes>(context);
        }

        public async Task<List<RepuestosGrupoPartes>> GetTodas()
        {
            return await _context.RepuestosGrupoPartes.Where(x => x.eliminado == false).ToListAsync();
        }

        public async Task<List<RepuestosGrupoPartes>> GetTodasByRepuesto(long idRepestos)
        {
            return await _context.RepuestosGrupoPartes.Where(x => x.idRepuestos == idRepestos && x.eliminado == false).ToListAsync();
        }

        public async Task<List<RepuestosGrupoPartes>> GetTodasByGrupoPartes(long idGrupo)
        {
            return await _context.RepuestosGrupoPartes.Where(x => x.idGrupoPartes == idGrupo && x.eliminado == false)
                .Include(x => x.Repuestos)
                .ToListAsync();
        }

        public async Task<RepuestosGrupoPartes> GetRepuestoGrupoParte(int idRepuestosGrupoPartes)
        {
            return await _context.RepuestosGrupoPartes.Where(x => x.idRepuestosGrupoPartes == idRepuestosGrupoPartes).FirstOrDefaultAsync();
        }





        public async Task<RepuestosGrupoPartes> Set(RepuestosGrupoPartes repuestosDiagnostico, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(repuestosDiagnostico);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(repuestosDiagnostico);

                default:
                    return repuestosDiagnostico;
            }
        }
    }
}
