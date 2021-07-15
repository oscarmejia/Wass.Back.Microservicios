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
    public class DALCCuadrillaEmpleados : IDALCCrud<CuadrillaEmpleados>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<CuadrillaEmpleados> _transact;

        public DALCCuadrillaEmpleados(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<CuadrillaEmpleados>(context);
        }

        public async Task<CuadrillaEmpleados> GetAsync(long id)
        {
            return await _context.CuadrillaEmpleados.Where(x => x.idEmpleado == id).Include(x => x.empleado).Include(x => x.Cuadrillas).FirstOrDefaultAsync();
        }

        public async Task<CuadrillaEmpleados> GetAsync(Guid id)
        {
            return await _context.CuadrillaEmpleados.Where(x => x.idEmpleadoCuadrilla == id).FirstOrDefaultAsync();
        }

        public async Task<CuadrillaEmpleados> GetLiderCuadrilla(long idCuadrilla)
        {
            return await _context.CuadrillaEmpleados.Where(x => x.idCuadrilla == idCuadrilla && !x.eliminado && x.estado && x.lider).FirstOrDefaultAsync();
        }

        public async Task<CuadrillaEmpleados> GetEmpleadoCuadrilla(long idEmpleado, long idCuadrilla)
        {
            return await _context.CuadrillaEmpleados.Where(x => x.idEmpleado == idEmpleado && x.idCuadrilla == idCuadrilla && !x.eliminado && x.estado).FirstOrDefaultAsync();
        }

        public async Task<CuadrillaEmpleados> GetUnicidadCuadrilla(long idEmpleado, long idCuadrilla)
        {
            return await _context.CuadrillaEmpleados.Where(x => x.idEmpleado == idEmpleado && !x.eliminado && x.estado && x.idCuadrilla != idCuadrilla).FirstOrDefaultAsync();
        }

        public async Task<List<CuadrillaEmpleados>> GetPorCuadrillaAsync(long id)
        {
            return await _context.CuadrillaEmpleados.Where(x => x.idCuadrilla == id).Include(x => x.empleado).Include(x => x.Cuadrillas).ToListAsync();
        }

        public async Task<List<CuadrillaEmpleados>> GetAllAsync()
        {
            return await _context.CuadrillaEmpleados.Include(x => x.empleado).Include(x => x.Cuadrillas).ToListAsync();
        }

        public async Task<CuadrillaEmpleados> SetAsync(CuadrillaEmpleados objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.idEmpleadoCuadrilla = Guid.NewGuid();
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.eliminado = true;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
