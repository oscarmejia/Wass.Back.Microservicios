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
    public class DALCActivosClasificacionDiagnosticos : IDALCCrud<ActivosClasificacionDiagnosticos>
    {

        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosClasificacionDiagnosticos> _transact;

        public DALCActivosClasificacionDiagnosticos(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosClasificacionDiagnosticos>(context);
        }

        public async Task<ActivosClasificacionDiagnosticos> GetAsync(long id)
        {
            return await _context.ActivosClasificacionDiagnosticos.Where(x => x.idDiagnostico == id && !x.eliminado)
                .Include(x => x.ActivosClasificacionDiagnosticosAcciones)
                .Include(x => x.RepuestosDiagnostico)
                .Include(x => x.DiagnosticoSkillsEmpresa)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ActivosClasificacionDiagnosticos>> GetAllAsync()
        {
            return await _context.ActivosClasificacionDiagnosticos.Where(x => !x.eliminado)
                .Include(x => x.ActivosClasificacionDiagnosticosAcciones)
                .Include(x => x.RepuestosDiagnostico)
                .Include(x => x.DiagnosticoSkillsEmpresa)
                .ToListAsync();
        }

        public async Task<List<ActivosClasificacionDiagnosticos>> GetPorClasificacionAsync(long idClasificacion)
        {
            return await _context.ActivosClasificacionDiagnosticos.Where(x => x.idClasificacion == idClasificacion && !x.eliminado)
                .Include(x => x.ActivosClasificacionDiagnosticosAcciones)
                .Include(x => x.RepuestosDiagnostico)
                .Include(x => x.DiagnosticoSkillsEmpresa)
                .ToListAsync();
        }

        public async Task<ActivosClasificacionDiagnosticos> actualizarParada(long idDiagnostico)
        {
            var get = await _context.ActivosClasificacionDiagnosticos.FirstOrDefaultAsync(x => x.idDiagnostico == idDiagnostico);
            if (get.parada == false)
            {
                get.parada = true;
                _context.Update(get);
                await _context.SaveChangesAsync();
            }
            else
            {
                get.parada = false;
                _context.Update(get);
                await _context.SaveChangesAsync();
            }

            return get;
        }

        public async Task<ActivosClasificacionDiagnosticos> SetAsync(ActivosClasificacionDiagnosticos objeto, Transaction transaccion)
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
