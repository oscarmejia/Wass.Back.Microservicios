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
    public class DALCActivosParada
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosParada> _transact;


        public DALCActivosParada(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosParada>(context);
        }

        public async Task<ActivosParada> GetAsync(long idActivosParada)
        {
            return await _context.ActivosParada.Where(x => x.idActivosParada == idActivosParada)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ActivosParada>> GetPorSedeAsync(long idSede)
        {
            return await _context.ActivosParada.Where(x => x.idSede == idSede)
                .ToListAsync();
        }
        public async Task<List<ActivosParada>> GetPorActivoAsync(Guid idActivo)
        {
            return await _context.ActivosParada.Where(x => x.idActivo == idActivo).ToListAsync();
        }

        public async Task<List<ActivosParada>> GetPorEmpresaAsync(long idEmpresa)
        {
            return await _context.ActivosParada.Where(x => x.idEmpresa == idEmpresa).ToListAsync();
        }

        public async Task<List<ActivosParada>> GetPorCuadrillaAsync(long idCuadrilla)
        {
            return await _context.ActivosParada.Where(x => x.idCuadrilla == idCuadrilla).ToListAsync();
        }

        public async Task<List<ActivosParada>> GetAllAsync()
        {
            return await _context.ActivosParada.ToListAsync();
        }

        public async Task<List<ActivosParada>> GetAllParadosAsync(DateTime fechaActual)
        {
            return await _context.ActivosParada.Where(x => x.fechaHoraReactivacion == null && x.fechaHoraParada.Date == fechaActual.Date).ToListAsync();
        }

        public async Task<ActivosParada> GetPorActivosParadaYActivoAsync(Guid idActivo, long idActivosParada)
        {
            return await _context.ActivosParada.Where(x => x.idActivo == idActivo && x.idActivosParada == idActivosParada).FirstOrDefaultAsync();
        }

        public async Task<ActivosParada> SetAsync(ActivosParada objeto, Transaction transaccion)
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
