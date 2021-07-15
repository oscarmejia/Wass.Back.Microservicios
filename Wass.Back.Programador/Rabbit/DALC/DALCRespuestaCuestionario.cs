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
    public class DALCRespuestaCuestionario
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<RespuestaCuestionario> _DALCTransaccion;

        public DALCRespuestaCuestionario(ProgramadorContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<RespuestaCuestionario>(context);
        }

        public async Task<RespuestaCuestionario> Get(long idRespuestaCuestionario)
        {
            return await _context.RespuestaCuestionario.Where(x => x.idRespuestaCuestionario == idRespuestaCuestionario).FirstOrDefaultAsync();
        }

        public async Task<List<RespuestaCuestionario>> GetTodas()
        {
            return await _context.RespuestaCuestionario.Where(x => x.activo != false).ToListAsync();
        }

        public async Task<RespuestaCuestionario> Set(RespuestaCuestionario respuestas, Transaction transaction)
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


        public async Task<List<RespuestaCuestionario>> GetPorCuestionarioCotizacionLicitacion(long idCuestionario, long idCotizacion, long idLicitacion)
        {
            return await _context.RespuestaCuestionario.Where(x => x.idCuestionario == idCuestionario && x.idCotizacion == idCotizacion && x.idLicitacion == idLicitacion && x.activo != false).ToListAsync();
        }
        public async Task<List<RespuestaCuestionario>> GetPorCotizacion(long idCotizacion)
        {
            return await _context.RespuestaCuestionario.Where(x => x.idCotizacion == idCotizacion && x.activo != false).ToListAsync();
        }
    }
}

