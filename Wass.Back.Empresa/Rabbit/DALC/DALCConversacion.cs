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
    public class DALCConversacion
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<Conversacion> _DALCTransaccion;

        public DALCConversacion(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<Conversacion>(context);
        }

        public async Task<Conversacion> Get(long idConversacion)
        {
            return await _context.Conversacion.Where(x => x.idConversacion == idConversacion && !x.eliminado)
                .Include(x => x.Mensajes)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Conversacion>> GetTodas()
        {
            return await _context.Conversacion.Where(x => !x.eliminado)
                .Include(x => x.Mensajes)
                .ToListAsync();
        }


        public async Task<List<Conversacion>> GetTodasporEmpleados(long idEmpleado)
        {
            return await _context.Conversacion.Where(x => !x.eliminado && x.usuario1 == idEmpleado || x.usuario2 == idEmpleado)
                .Include(x => x.Mensajes)
                .ToListAsync();
        }

        public async Task<Conversacion> EliminarConversacion(long idConversacion)
        {
            var get = await _context.Conversacion.FirstOrDefaultAsync(x => x.idConversacion == idConversacion);
            get.eliminado = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<Conversacion> Set(Conversacion conversacion, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(conversacion);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(conversacion);

                default:
                    return conversacion;
            }
        }
    }
}
