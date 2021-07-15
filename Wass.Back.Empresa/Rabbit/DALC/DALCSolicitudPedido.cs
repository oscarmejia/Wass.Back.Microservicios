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
    public class DALCSolicitudPedido
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<SolicitudPedido> _DALCTransaccion;

        public DALCSolicitudPedido(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<SolicitudPedido>(context);
        }

        public async Task<SolicitudPedido> Get(long idSolicitudPedido)
        {
            return await _context.SolicitudPedido.Where(x => x.idSolicitudPedido == idSolicitudPedido && x.estado != 3)
                .FirstOrDefaultAsync();
        }

        public async Task<List<SolicitudPedido>> GetTodas()
        {
            return await _context.SolicitudPedido.Where(x => x.estado != 3)
                .ToListAsync();
        }



        public async Task<SolicitudPedido> Set(SolicitudPedido solicitudPedido, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(solicitudPedido);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(solicitudPedido);

                default:
                    return solicitudPedido;
            }
        }
    }
}
