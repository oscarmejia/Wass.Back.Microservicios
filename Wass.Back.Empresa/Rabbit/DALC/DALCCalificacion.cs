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
    public class DALCCalificacion
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<Calificacion> _DALCTransaccion;

        public DALCCalificacion(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<Calificacion>(context);
        }

        public async Task<Calificacion> Get(long idCalificacion)
        {
            return await _context.Calificacion.Where(x => x.idCalificacion == idCalificacion).FirstOrDefaultAsync();
        }

        public async Task<Calificacion> GetPorProveedorSedeEmpresa(long idProveedor, long idSede, long idEmpresa)
        {
            return await _context.Calificacion.Where(x => x.idProveedor == idProveedor && x.idSede == idSede && x.idEmpresa == idEmpresa).FirstOrDefaultAsync();
        }

        public async Task<List<Calificacion>> GetTodas()
        {
            return await _context.Calificacion.ToListAsync();
        }

        public async Task<List<Calificacion>> getTodasPorSedeAsync(long idSede)
        {
            return await _context.Calificacion.Where(x => x.idSede == idSede).ToListAsync();
        }

        public async Task<List<Calificacion>> getTodasPorOrdenTrabajoAsync(long idOrdenTrabajo)
        {
            return await _context.Calificacion.Where(x => x.idOrdenTrabajo == idOrdenTrabajo).ToListAsync();
        }

        public async Task<List<Calificacion>> getTodasPorEmpresaAsync(long idEmpresa)
        {
            return await _context.Calificacion.Where(x => x.idEmpresa == idEmpresa).ToListAsync();
        }

        public async Task<List<Calificacion>> getTodasPorProveedorAsync(long idProveedor)
        {
            return await _context.Calificacion.Where(x => x.idProveedor == idProveedor).ToListAsync();
        }
        public async Task<Calificacion> Set(Calificacion calificacion, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(calificacion);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(calificacion);

                default:
                    return calificacion;
            }
        }
    }
}
