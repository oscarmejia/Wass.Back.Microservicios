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
    public class DALCOrdenEntregaAlmacen
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<OrdenEntregaAlmacen> _DALCTransaccion;

        public DALCOrdenEntregaAlmacen(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<OrdenEntregaAlmacen>(context);
        }

        public async Task<OrdenEntregaAlmacen> Get(long idOrdenEntregaAlmacen)
        {
            return await _context.OrdenEntregaAlmacen.Where(x => x.idOrdenEntregaAlmacen == idOrdenEntregaAlmacen).FirstOrDefaultAsync();
        }
        public async Task<OrdenEntregaAlmacen> GetPorIdOrdenTrabajo(long idOrden)
        {
            return await _context.OrdenEntregaAlmacen.Where(x => x.idOrdenTrabajo == idOrden).FirstOrDefaultAsync();
        }


        public async Task<List<OrdenEntregaAlmacen>> GetTodas()
        {
            return await _context.OrdenEntregaAlmacen.ToListAsync();
        }

        public async Task<List<OrdenEntregaAlmacen>> GetPorFecha(long idSede)
        {

            return await _context.OrdenEntregaAlmacen.Where(x => x.idSede == idSede).OrderByDescending(x => x.fechaHora).ToListAsync();

        }
        public async Task<List<OrdenEntregaAlmacen>> GetPorFechaAlmacen(long idAlmacen)
        {

            return await _context.OrdenEntregaAlmacen.Where(x => x.idAlmacen == idAlmacen).OrderByDescending(x => x.fechaHora).ToListAsync();

        }

        public async Task<OrdenEntregaAlmacen> GetPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.OrdenEntregaAlmacen.Where(x => x.idAlmacen == idAlmacen && x.repuestos.Contains(Convert.ToString(idRepuesto))).FirstOrDefaultAsync();
        }

        public async Task<List<OrdenEntregaAlmacen>> GetTodasPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.OrdenEntregaAlmacen.Where(x => x.idAlmacen == idAlmacen && x.repuestos.Contains(Convert.ToString(idRepuesto))).OrderByDescending(x => x.fechaHora).ToListAsync();
        }

        public async Task<OrdenEntregaAlmacen> Set(OrdenEntregaAlmacen ordenEntregaAlmacen, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(ordenEntregaAlmacen);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(ordenEntregaAlmacen);

                default:
                    return ordenEntregaAlmacen;
            }
        }
    }
}
