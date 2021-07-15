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
    public class DALCTurnos
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<Turnos> _transact;
        private readonly DALCTransacciones<CuadrillasTurnos> _transactCuad;

        public DALCTurnos(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<Turnos>(context);
            _transactCuad = new DALCTransacciones<CuadrillasTurnos>(context);
        }
        public async Task<Turnos> GetAsync(long id)
        {
            return await _context.Turnos.Where(x => x.idTurno == id && !x.eliminado && x.estado == true).FirstOrDefaultAsync();
        }

        public async Task<List<Turnos>> GetAllAsync()
        {
            return await _context.Turnos.Where(x => !x.eliminado && x.estado == true).ToListAsync();
        }

        public async Task<List<Turnos>> GetPorEmpresaAsync(long idEmpresa)
        {
            return await _context.Turnos.Where(x => x.idEmpresa == idEmpresa && x.estado == true).AsNoTracking().ToListAsync();
        }

        public async Task<List<Turnos>> GetPorCuadrillaAsync(long idCuadrilla)
        {
            var turnos = await
                            (from turno in _context.Turnos
                             join relacion in _context.CuadrillasTurnos on turno.idTurno equals relacion.idTurno
                             where relacion.idCuadrilla == idCuadrilla
                             select turno)
                          .ToListAsync();

            return turnos;
        }

        public async Task<List<CuadrillasTurnos>> GetPorCuadrillaRelacionAsync(long idCuadrilla)
        {
            return await _context.CuadrillasTurnos.Where(x => x.idCuadrilla == idCuadrilla).AsNoTracking().ToListAsync();
        }

        public async Task<Turnos> SetAsync(Turnos objeto, Transaction transaccion)
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

        public async Task<List<CuadrillasTurnos>> SetTurnosToCuadrillaAsync(List<CuadrillasTurnos> turnos, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    return await _transactCuad.CrearRango(turnos);

                case Transaction.Delete:
                    return await _transactCuad.BorrarRango(turnos);

                default:
                    return turnos;
            }
        }
    }
}
