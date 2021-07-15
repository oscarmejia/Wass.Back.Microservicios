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
    public class DALCRespuestaAccionesPlanMantenimientoPreventivo
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<RespuestaAccionesPlanMantenimientoPreventivo> _DALCTransaccion;

        public DALCRespuestaAccionesPlanMantenimientoPreventivo(ProgramadorContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<RespuestaAccionesPlanMantenimientoPreventivo>(context);
        }

        public async Task<RespuestaAccionesPlanMantenimientoPreventivo> Get(long idRespuesta)
        {
            return await _context.RespuestaAccionesPlanMantenimientoPreventivo.Where(x => x.idRespuesta == idRespuesta && x.estado).FirstOrDefaultAsync();
        }

        public async Task<List<RespuestaAccionesPlanMantenimientoPreventivo>> GetPorMantenimientoPreventivo(long idMantenimientoPreventivo)
        {
            return await _context.RespuestaAccionesPlanMantenimientoPreventivo.Where(x => x.idMantenimientoPreventivo == idMantenimientoPreventivo && x.estado).ToListAsync();
        }

        public async Task<List<RespuestaAccionesPlanMantenimientoPreventivo>> GetPorMantenimientoPreventivoyParte(long idMantenimientoPreventivo, Guid idParte)
        {
            return await _context.RespuestaAccionesPlanMantenimientoPreventivo.Where(x => x.idMantenimientoPreventivo == idMantenimientoPreventivo && x.idParte == idParte && x.estado).ToListAsync();
        }



        public async Task<List<RespuestaAccionesPlanMantenimientoPreventivo>> GetTodas()
        {
            return await _context.RespuestaAccionesPlanMantenimientoPreventivo.Where(x => x.estado).ToListAsync();
        }

        public async Task<RespuestaAccionesPlanMantenimientoPreventivo> Set(RespuestaAccionesPlanMantenimientoPreventivo respuestas, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(respuestas);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(respuestas);

                default:
                    return respuestas;
            }
        }
    }
}
