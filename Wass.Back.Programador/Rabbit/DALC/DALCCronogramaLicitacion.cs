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
    public class DALCCronogramaLicitacion
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<CronogramaLicitacion> _transac;

        public DALCCronogramaLicitacion(ProgramadorContext context)
        {
            _context = context;
            _transac = new DALCTransacciones<CronogramaLicitacion>(context);
        }

        public async Task<CronogramaLicitacion> Get (long idCronogramaLicitacion)
        {
            return await _context.CronogramaLicitacion.Where(x => x.idCronogramaLicitacion == idCronogramaLicitacion).FirstOrDefaultAsync();
        }

        public async Task<List<CronogramaLicitacion>> GetIdLicitacion (long idLicitacion)
        {
            return await _context.CronogramaLicitacion.Where(x => x.idLicitacion == idLicitacion).ToListAsync();
        }

        public async Task<List<CronogramaLicitacion>> GetTodas()
        {
            return await _context.CronogramaLicitacion.ToListAsync();
        }

        public async Task<CronogramaLicitacion> Set (CronogramaLicitacion cronogramaLicitacion, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transac.Crear(cronogramaLicitacion);
                case Transaction.Update:
                    return await _transac.Actualizar(cronogramaLicitacion);
                default:
                    return cronogramaLicitacion;
            }
        }


    }
}
