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
    public class DALCAlmacen
    {

        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<Almacen> _DALCTransaccion;

        public DALCAlmacen(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<Almacen>(context);
        }

        public async Task<Almacen> Get(long idAlmacen)
        {
            return await _context.Almacen.Where(x => x.idAlmacen == idAlmacen)
                .Include(x => x.RepuestosAlmacen)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Almacen>> GetTodasPorCuadrilla(long idCuadrilla)
        {
            return await _context.Almacen.Where(x => x.idCuadrilla == idCuadrilla)
                .Include(x => x.RepuestosAlmacen)
                .ToListAsync();
        }

        public async Task<List<Almacen>> GetTodasPorSede(long idSede)
        {
            return await _context.Almacen.Where(x => x.idSede == idSede)
                .Include(x => x.RepuestosAlmacen)
                .ToListAsync();
        }

        public async Task<List<Almacen>> GetTodasPorTipo(long tipo)
        {
            return await _context.Almacen.Where(x => x.tipo == tipo)
                .Include(x => x.RepuestosAlmacen)
                .ToListAsync();
        }

        public async Task<List<Almacen>> getTodasPorTipoEmpresaSede(long idEmpresa, long tipo)
        {
            var sql = (from almacen in _context.Almacen
                       join sede in _context.Sedes on almacen.idSede equals sede.idSede
                       where sede.idEmpresa == idEmpresa && tipo == 1
                       select almacen
                       ).AsQueryable();

            return await sql.Include(x => x.RepuestosAlmacen).ToListAsync();
        }

        public async Task<List<Almacen>> getTodasPorTipoEmpresaCuadrilla(long idEmpresa, long tipo)
        {
            var sql = (from almacen in _context.Almacen
                       join cuadrilla in _context.Cuadrillas on almacen.idCuadrilla equals cuadrilla.idCuadrilla
                       join sede in _context.Sedes on cuadrilla.idSede equals sede.idSede
                       where sede.idEmpresa == idEmpresa && tipo == 2
                       select almacen
                       ).AsQueryable();

            return await sql.Include(x => x.RepuestosAlmacen).ToListAsync();
        }

        public async Task<List<Almacen>> GetTodas()
        {
            return await _context.Almacen
                .Include(x => x.RepuestosAlmacen)
                .ToListAsync();
        }

        public async Task<Almacen> Set(Almacen almacen, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(almacen);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(almacen);

                default:
                    return almacen;
            }
        }
    }
}
