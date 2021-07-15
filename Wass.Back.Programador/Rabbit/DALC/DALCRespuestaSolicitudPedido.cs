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
    public class DALCRespuestaSolicitudPedido
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<RespuestaSolicitudPedido> _DALCTransaccion;

        public DALCRespuestaSolicitudPedido(ProgramadorContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<RespuestaSolicitudPedido>(context);
        }

        public async Task<RespuestaSolicitudPedido> Get(long idRespuestaSolicitudPedido)
        {
            return await _context.RespuestaSolicitudPedido.Where(x => x.idRespuestaSolicitudPedido == idRespuestaSolicitudPedido)
                .FirstOrDefaultAsync();
        }

        public async Task<List<RespuestaSolicitudPedido>> GetTodas()
        {
            return await _context.RespuestaSolicitudPedido.ToListAsync();
        }

        public async Task<RespuestaSolicitudPedido> Set(RespuestaSolicitudPedido respuestaSolicitudPedido, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(respuestaSolicitudPedido);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(respuestaSolicitudPedido);

                default:
                    return respuestaSolicitudPedido;
            }
        }
    }
}
