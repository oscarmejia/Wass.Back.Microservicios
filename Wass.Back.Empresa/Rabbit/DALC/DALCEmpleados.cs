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
    public class DALCEmpleados
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<Empleados> _transact;

        public DALCEmpleados(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<Empleados>(context);
        }

        public async Task<Empleados> GetAsync(long id)
        {
            return await _context.Empleados.Where(x => x.idEmpleado == id)
                //.Include(x => x.conversaciones)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Empleados>> GetAllAsync()
        {
            return await _context.Empleados
                //.Include(x => x.conversaciones)
                .ToListAsync();
        }

        public async Task<List<Empleados>> GetPorSedeAsync(long idSede)
        {
            return await _context.Empleados.Where(x => x.idSede == idSede)
                //.Include(x => x.conversaciones)
                .ToListAsync();
        }

        public async Task<List<Empleados>> GetPorSedeCargoAsync(long idSede, int idCargo)
        {
            return await _context.Empleados.Where(x => x.idSede == idSede && x.idCargo == idCargo)
                //.Include(x => x.conversaciones)
                .ToListAsync();
        }

        public async Task<Empleados> GetPorNumDocumentoAsync(int idTipoDocumento, string numDocumento)
        {
            return await _context.Empleados.Where(x => x.idTipoDocumento == idTipoDocumento && x.numDocumento == numDocumento)
                //.Include(x => x.conversaciones)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Empleados>> GetPorCargoAsync(int idCargo)
        {
            return await _context.Empleados.Where(x => x.idCargo == idCargo)
                //.Include(x => x.conversaciones)
                .ToListAsync();
        }

        public async Task<List<Empleados>> GetPorEstadoAsync(int idEstadoEmpleado)
        {
            return await _context.Empleados.Where(x => x.idEstadoEmpleado == idEstadoEmpleado).ToListAsync();
        }

        public async Task<List<Empleados>> GetPorEmpresaAsync(int idEmpresa)
        {
            var sql = (from empleado in _context.Empleados
                       join sede in _context.Sedes on empleado.idSede equals sede.idSede
                       where sede.idEmpresa == idEmpresa
                       select empleado
                       ).AsQueryable();

            return await sql
                .Include(x => x.sede)
                .ToListAsync();
        }

        public async Task<Empleados> SetAsync(Empleados objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    return await _transact.Crear(objeto);
                case Transaction.Update:
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
