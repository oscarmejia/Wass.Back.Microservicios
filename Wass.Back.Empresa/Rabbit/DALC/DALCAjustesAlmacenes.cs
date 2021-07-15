using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Rabbit.Context;
namespace Wass.Back.Empresa.Rabbit.DALC
{
    public class DALCAjustesAlmacenes
    {

        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<AjustesAlmacenes> _DALCTransaccion;

        public DALCAjustesAlmacenes(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<AjustesAlmacenes>(context);
        }

        public async Task<AjustesAlmacenes> Get(long idAjustesAlmacenes)
        {
            return await _context.AjustesAlmacenes.Where(x => x.idAjustesAlmacenes == idAjustesAlmacenes).FirstOrDefaultAsync();
        }

        public async Task<List<AjustesAlmacenes>> GetPorFechaAlmacen(long idAlmacen)
        {

            return await _context.AjustesAlmacenes.Where(x => x.idAlmacen == idAlmacen).OrderByDescending(x => x.fechaHora).ToListAsync();

        }
        public async Task<List<AjustesAlmacenes>> GetTodas()
        {
            return await _context.AjustesAlmacenes.ToListAsync();
        }

        public async Task<AjustesAlmacenes> GetPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.AjustesAlmacenes.Where(x => x.idAlmacen == idAlmacen && x.idRepuesto == idRepuesto).FirstOrDefaultAsync();
        }

        public async Task<List<AjustesAlmacenes>> GetTodasPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.AjustesAlmacenes.Where(x => x.idAlmacen == idAlmacen && x.idRepuesto == idRepuesto).OrderByDescending(x => x.fechaHora).ToListAsync();
        }
        public async Task<AjustesAlmacenes> Set(AjustesAlmacenes ajustesAlmacenes, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(ajustesAlmacenes);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(ajustesAlmacenes);

                default:
                    return ajustesAlmacenes;
            }
        }
    }
}
