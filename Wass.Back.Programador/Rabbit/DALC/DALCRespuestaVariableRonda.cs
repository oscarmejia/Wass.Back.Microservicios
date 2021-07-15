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
    public class DALCRespuestaVariableRonda
    {

        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<RespuestaVariableRonda> _transact;

        public DALCRespuestaVariableRonda(ProgramadorContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<RespuestaVariableRonda>(context);
        }

        public async Task<RespuestaVariableRonda> Get(long idRespuestaVariableRonda)
        {
            return await _context.RespuestaVariableRonda.Where(x => x.idRespuestaVariableRonda == idRespuestaVariableRonda).FirstOrDefaultAsync();
        }

        public async Task<List<RespuestaVariableRonda>> GetTodas()
        {
            return await _context.RespuestaVariableRonda.ToListAsync();
        }

        public async Task<List<RespuestaVariableRonda>> GetTodasPorRonda(long idRonda)
        {
            return await _context.RespuestaVariableRonda.Where(x => x.idRonda == idRonda).ToListAsync();
        }

        public async Task<List<RespuestaVariableRonda>> GetTodasPorVariable(long idVariable)
        {
            return await _context.RespuestaVariableRonda.Where(x => x.idVariable == idVariable).ToListAsync();
        }

        public async Task<RespuestaVariableRonda> Set(RespuestaVariableRonda respuesta, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transact.Crear(respuesta);
                case Transaction.Update:
                    return await _transact.Actualizar(respuesta);
                default:
                    return respuesta;
            }
        }
    }
}
