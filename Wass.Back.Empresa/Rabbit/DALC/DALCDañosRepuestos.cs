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
    public class DALCDañosRepuestos
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<DañosRepuestos> _DALCTransaccion;

        public DALCDañosRepuestos(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<DañosRepuestos>(context);
        }

        public async Task<DañosRepuestos> Get(long idDañosRepuestos)
        {
            return await _context.DañosRepuestos.Where(x => x.idDañosRepuestos == idDañosRepuestos).FirstOrDefaultAsync();
        }

        public async Task<DañosRepuestos> GetPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.DañosRepuestos.Where(x => x.idAlmacen == idAlmacen && x.idRepuesto == idRepuesto).FirstOrDefaultAsync();
        }

        public async Task<List<DañosRepuestos>> GetTodasPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.DañosRepuestos.Where(x => x.idAlmacen == idAlmacen && x.idRepuesto == idRepuesto).OrderByDescending(x => x.fechaHora).ToListAsync();
        }

        public async Task<List<DañosRepuestos>> GetTodas()
        {
            return await _context.DañosRepuestos.ToListAsync();
        }

        public async Task<DañosRepuestos> Set(DañosRepuestos dañosRepuestos, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(dañosRepuestos);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(dañosRepuestos);

                default:
                    return dañosRepuestos;
            }
        }
    }
}
