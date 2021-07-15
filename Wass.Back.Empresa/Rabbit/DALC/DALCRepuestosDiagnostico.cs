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
    public class DALCRepuestosDiagnostico
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<RepuestosDiagnostico> _DALCTransaccion;

        public DALCRepuestosDiagnostico(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<RepuestosDiagnostico>(context);
        }

        public async Task<RepuestosDiagnostico> Get(long idRepuestosDiagnostico)
        {
            return await _context.RepuestosDiagnostico.Where(x => x.idRepuestosDiagnostico == idRepuestosDiagnostico && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<RepuestosDiagnostico>> GetPorIdRepuestosDiagnostico(long idRepuesto)
        {
            return await _context.RepuestosDiagnostico.Where(x => x.idRepuestos == idRepuesto && !x.eliminado)
                .Include(x => x.ActivosClasificacionDiagnosticos)
                .ToListAsync();
        }

        public async Task<List<RepuestosDiagnostico>> GetPorIdDiagnosticoRepuesto(long idDiagnostico)
        {
            return await _context.RepuestosDiagnostico.Where(x => x.idDiagnostico == idDiagnostico && !x.eliminado)
                .Include(x => x.Repuestos)
                .ToListAsync();
        }

        public async Task<List<RepuestosDiagnostico>> GetTodas()
        {
            return await _context.RepuestosDiagnostico.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<RepuestosDiagnostico> Set(RepuestosDiagnostico repuestosDiagnostico, Transaction transaction)
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

        public async Task<RepuestosDiagnostico> EliminarRepuestosDiagnostico(long idRepuestosDiagnostico)
        {
            var get = await _context.RepuestosDiagnostico.FirstOrDefaultAsync(x => x.idRepuestosDiagnostico == idRepuestosDiagnostico);
            get.eliminado = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }
    }
}
