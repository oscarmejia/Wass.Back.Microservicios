using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.Interface;
using WASS.Back.Programador.core.rabbit.DALC;

namespace Wass.Back.Programador.Rabbit.DALC
{
    public class DALCMantenimientoRondas
    {

        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<MantenimientoRondas> _transact;

        public DALCMantenimientoRondas(ProgramadorContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<MantenimientoRondas>(context);
        }

        public async Task<MantenimientoRondas> Get(long idRonda)
        {
            return await _context.MantenimientoRondas.Where(x => x.idRonda == idRonda && x.estado)
                .Include(x => x.respuestasVariableRondas)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MantenimientoRondas>> GetAll()
        {
            return await _context.MantenimientoRondas.Where(x => x.estado)
                .Include(x => x.respuestasVariableRondas)
                .ToListAsync();
        }

        public async Task<List<MantenimientoRondas>> GetAllPorGrupo(long idGrupo)
        {
            return await _context.MantenimientoRondas.Where(x => x.idGrupo == idGrupo)
                .Include(x => x.respuestasVariableRondas)
                .ToListAsync();
        }

        public async Task<List<MantenimientoRondas>> GetAllPorPlan(long idPlan)
        {
            return await _context.MantenimientoRondas.Where(x => x.idPlan == idPlan)
                .Include(x => x.respuestasVariableRondas)
                .Include(x => x.orden)
                .ToListAsync();
        }

        public async Task<MantenimientoRondas> GetPorOrdenAsync(long idOrden)
        {
            return await _context.MantenimientoRondas.Where(x => x.idOrden == idOrden && x.estado)
                .Include(x => x.respuestasVariableRondas)
                .FirstOrDefaultAsync();
        }

        public async Task<MantenimientoRondas> Set(MantenimientoRondas objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
