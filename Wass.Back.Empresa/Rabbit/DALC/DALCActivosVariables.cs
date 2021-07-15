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
    public class DALCActivosVariables : IDALCCrud<ActivosVariables>
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosVariables> _transact;

        public DALCActivosVariables(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosVariables>(context);
        }

        public async Task<ActivosVariables> GetAsync(long id)
        {
            return await _context.ActivosVariables.Where(x => x.idActivoVariable == id && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<ActivosVariables> GetPorCalsificacionActivoAsync(long idActivoClasificacionVariable)
        {
            return await _context.ActivosVariables.Where(x => x.idActivoClasificacionVariable == idActivoClasificacionVariable && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosVariables>> GetAllAsync()
        {
            return await _context.ActivosVariables.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosVariables>> GetPorFlotaAsync(Guid idActivoFlota)
        {
            return await _context.ActivosVariables.Where(x => x.idActivoFlota.Equals(idActivoFlota) && !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosVariables>> GetPorEquipoAsync(Guid idActivoEquipo)
        {
            return await _context.ActivosVariables.Where(x => x.idActivoEquipo.Equals(idActivoEquipo) && !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosVariables>> GetPorOrdenFechaEquipoAsync(Guid idActivoEquipo)
        {
            return await _context.ActivosVariables.Where(x => x.idActivoEquipo.Equals(idActivoEquipo) && !x.eliminado).OrderByDescending(x => x.fechaCreacion).ToListAsync();

        }

        public async Task<ActivosVariables> GetPorUltimoEquipoAsync(Guid idActivoEquipo, long idActivoClasificacionVariable)
        {
            return await _context.ActivosVariables.Where(x => x.idActivoEquipo.Equals(idActivoEquipo) && x.idActivoClasificacionVariable == idActivoClasificacionVariable && !x.eliminado).OrderByDescending(x => x.fechaCreacion).FirstOrDefaultAsync();

        }

        public async Task<ActivosVariables> GetPorUltimoFlotaAsync(Guid idActivoFlota, long idActivoClasificacionVariable)
        {
            return await _context.ActivosVariables.Where(x => x.idActivoFlota.Equals(idActivoFlota) && x.idActivoClasificacionVariable == idActivoClasificacionVariable && !x.eliminado).OrderByDescending(x => x.fechaCreacion).FirstOrDefaultAsync();

        }

        public async Task<List<ActivosVariables>> GetPorOrdenFechaFlotaAsync(Guid idActivoFlota)
        {
            return await _context.ActivosVariables.Where(x => x.idActivoFlota.Equals(idActivoFlota) && !x.eliminado).OrderByDescending(x => x.fechaCreacion).ToListAsync();

        }

        public async Task<ActivosVariables> SetAsync(ActivosVariables objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.eliminado = false;
                    objeto.fechaEdicion = DateTime.Now;
                    objeto.fechaCreacion = DateTime.Now;
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.eliminado = true;
                    objeto.fechaEdicion = DateTime.Now;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    objeto.fechaEdicion = DateTime.Now;
                    objeto.eliminado = false;
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
