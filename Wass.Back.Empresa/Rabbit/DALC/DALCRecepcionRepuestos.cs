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
    public class DALCRecepcionRepuestos
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<RecepcionRepuestos> _DALCTransaccion;

        public DALCRecepcionRepuestos(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<RecepcionRepuestos>(context);
        }

        public async Task<RecepcionRepuestos> Get(long idRecepcionRepuestos)
        {
            return await _context.RecepcionRepuestos.Where(x => x.idRecepcionRepuestos == idRecepcionRepuestos).FirstOrDefaultAsync();
        }

        public async Task<RecepcionRepuestos> GetPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.RecepcionRepuestos.Where(x => x.idAlmacen == idAlmacen && x.repuestos.Contains(Convert.ToString(idRepuesto))).FirstOrDefaultAsync();
        }

        public async Task<List<RecepcionRepuestos>> GetTodasPorRepuesto(long idRepuesto)
        {
            return await _context.RecepcionRepuestos.Where(x => x.repuestos.Contains(Convert.ToString(idRepuesto))).OrderByDescending(x => x.fechaHora).ToListAsync();
        }
        public async Task<List<RecepcionRepuestos>> GetTodas()
        {
            return await _context.RecepcionRepuestos.ToListAsync();
        }

        public async Task<List<RecepcionRepuestos>> GetTodasPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.RecepcionRepuestos.Where(x => x.idAlmacen == idAlmacen && x.repuestos.Contains(Convert.ToString(idRepuesto))).OrderByDescending(x => x.fechaHora).ToListAsync();
        }
        public async Task<RecepcionRepuestos> Set(RecepcionRepuestos recepcionRepuestos, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(recepcionRepuestos);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(recepcionRepuestos);

                default:
                    return recepcionRepuestos;
            }
        }
    }
}
