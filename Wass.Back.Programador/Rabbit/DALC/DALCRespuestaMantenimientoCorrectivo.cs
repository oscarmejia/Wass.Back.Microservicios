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
    public class DALCRespuestaMantenimientoCorrectivo
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<RespuestaMantenimientoCorrectivo> _DALCTransaccion;

        public DALCRespuestaMantenimientoCorrectivo(ProgramadorContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<RespuestaMantenimientoCorrectivo>(context);
        }

        public async Task<RespuestaMantenimientoCorrectivo> Get(long idRespuestaMantenimientoCorrectivo)
        {
            return await _context.RespuestaMantenimientoCorrectivo.Where(x => x.idRespuestaMantenimientoCorrectivo == idRespuestaMantenimientoCorrectivo).FirstOrDefaultAsync();
        }

        public async Task<List<RespuestaMantenimientoCorrectivo>> GetTodas()
        {
            return await _context.RespuestaMantenimientoCorrectivo.ToListAsync();
        }

        public async Task<RespuestaMantenimientoCorrectivo> Set(RespuestaMantenimientoCorrectivo respuestas, Transaction transaction)
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

        public async Task<List<RespuestaMantenimientoCorrectivo>> GetPorDiagnostico(long idDiagnostico)
        {
            return await _context.RespuestaMantenimientoCorrectivo.Where(x => x.idDiagnostico == idDiagnostico).ToListAsync();
        }

        public async Task<List<RespuestaMantenimientoCorrectivo>> GetPorMantenimientoCorrectivo(long idMantenimientoCorrectivo)
        {
            return await _context.RespuestaMantenimientoCorrectivo.Where(x => x.idMantenimientoCorrectivo == idMantenimientoCorrectivo).ToListAsync();
        }
    }
}
