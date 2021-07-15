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
    public class DALCActivosClasificacionAcciones : IDALCCrud<ActivosClasificacionAcciones>
    {

        private readonly EmpresaContext _context;

        private readonly DALCTransacciones<ActivosClasificacionAcciones> _transact;


        public DALCActivosClasificacionAcciones(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosClasificacionAcciones>(context);
        }

        public async Task<ActivosClasificacionAcciones> GetAsync(long id)
        {
            return await _context.ActivosClasificacionAcciones.Where(x => x.idAccion == id && !x.eliminado).Include(x => x.ActivosClasificacionDiagnosticosAcciones).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosClasificacionAcciones>> GetAllAsync()
        {
            return await _context.ActivosClasificacionAcciones.Where(x => !x.eliminado).Include(x => x.ActivosClasificacionDiagnosticosAcciones).ToListAsync();
        }

        public async Task<List<ActivosClasificacionAcciones>> GetPorClasifiacionAsync(long idClasificacion)
        {
            return await _context.ActivosClasificacionAcciones.Where(x => x.idClasificacion == idClasificacion && !x.eliminado).Include(x => x.ActivosClasificacionDiagnosticosAcciones).ToListAsync();
        }

        public async Task<ActivosClasificacionAcciones> SetAsync(ActivosClasificacionAcciones objeto, Transaction transaccion)
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
