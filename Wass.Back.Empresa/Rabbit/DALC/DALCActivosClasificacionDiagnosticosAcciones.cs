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
    public class DALCActivosClasificacionDiagnosticosAcciones : IDALCCrud<ActivosClasificacionDiagnosticosAcciones>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosClasificacionDiagnosticosAcciones> _transact;

        public DALCActivosClasificacionDiagnosticosAcciones(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosClasificacionDiagnosticosAcciones>(context);
        }

        public async Task<ActivosClasificacionDiagnosticosAcciones> GetAsync(long id)
        {
            return await _context.ActivosClasificacionDiagnosticosAcciones.Where(x => x.idDiagnosticosAcciones == id && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<ActivosClasificacionDiagnosticosAcciones> GetPorAccionDiagnosticoAsync(long idDiagnostico, long idAccion)
        {
            return await _context.ActivosClasificacionDiagnosticosAcciones.Where(x => x.idDiagnostico == idDiagnostico && x.idAccion == idAccion && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosClasificacionDiagnosticosAcciones>> GetAllAsync()
        {
            return await _context.ActivosClasificacionDiagnosticosAcciones.Where(x => !x.eliminado)
                .Include(x => x.ActivosClasificacionAcciones)
                .ToListAsync();
        }

        public async Task<List<ActivosClasificacionDiagnosticosAcciones>> GetPorDiagnosticoAsync(long idDiagnostico)
        {
            return await _context.ActivosClasificacionDiagnosticosAcciones.Where(x => x.idDiagnostico == idDiagnostico && !x.eliminado)
                .Include(x => x.ActivosClasificacionAcciones)
                .ToListAsync();
        }

        public async Task<List<ActivosClasificacionDiagnosticosAcciones>> GetPorAccionAsync(long idAccion)
        {
            return await _context.ActivosClasificacionDiagnosticosAcciones.Where(x => x.idAccion == idAccion && !x.eliminado).ToListAsync();
        }

        public async Task<ActivosClasificacionDiagnosticosAcciones> SetAsync(ActivosClasificacionDiagnosticosAcciones objeto, Transaction transaccion)
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
