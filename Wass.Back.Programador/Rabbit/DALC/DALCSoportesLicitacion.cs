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
    public class DALCSoportesLicitacion
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<SoportesLicitacion> _transac;

        public DALCSoportesLicitacion(ProgramadorContext context)
        {
            _context = context;
            _transac = new DALCTransacciones<SoportesLicitacion>(context);
        }

        public async Task<SoportesLicitacion> Get (long idSoporteLicitacion)
        {
            return await _context.SoportesLicitacion.Where(x => x.idSoporteLicitacion == idSoporteLicitacion).FirstOrDefaultAsync();
        }

        public async Task<List<SoportesLicitacion>> GetIdLicitacion ( long idLicitacion)
        {
            return await _context.SoportesLicitacion.Where(x => x.idLicitacion == idLicitacion).ToListAsync();
        }

        public async Task<List<SoportesLicitacion>> GetTodas()
        {
            return await _context.SoportesLicitacion.ToListAsync();
        }

        public async Task<SoportesLicitacion> Set (SoportesLicitacion soportesLicitacion, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transac.Crear(soportesLicitacion);
                case Transaction.Update:
                    return await _transac.Actualizar(soportesLicitacion);
                default:
                    return soportesLicitacion;
            }
        }
    }
}
