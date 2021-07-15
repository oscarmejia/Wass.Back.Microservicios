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
    public class DALCMensajesConversacion
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<MensajesConversacion> _DALCTransaccion;

        public DALCMensajesConversacion(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<MensajesConversacion>(context);
        }

        public async Task<MensajesConversacion> Get(long idMensajesConversacion)
        {
            return await _context.MensajesConversacion.Where(x => x.idMensajesConversacion == idMensajesConversacion && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<MensajesConversacion>> GetTodas()
        {
            return await _context.MensajesConversacion.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<MensajesConversacion> EliminarMensajesConversacion(long idMensajesConversacion)
        {
            var get = await _context.MensajesConversacion.FirstOrDefaultAsync(x => x.idMensajesConversacion == idMensajesConversacion);
            get.eliminado = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<MensajesConversacion> Set(MensajesConversacion mensajesConversacion, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(mensajesConversacion);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(mensajesConversacion);

                default:
                    return mensajesConversacion;
            }
        }

    }
}
