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
    public class DALCRepuestosAlmacen
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<RepuestosAlmacen> _DALCTransaccion;

        public DALCRepuestosAlmacen(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<RepuestosAlmacen>(context);
        }

        public async Task<RepuestosAlmacen> Get(long idRepuestosAlmacen)
        {
            return await _context.RepuestosAlmacen.Where(x => x.idRepuestosAlmacen == idRepuestosAlmacen).Include(x => x.Repuestos).FirstOrDefaultAsync();
        }

        public async Task<RepuestosAlmacen> RepuestosEnAlmacen(long idAlmacen, long idRepuesto)
        {
            return await _context.RepuestosAlmacen.Where(x => x.idAlmacen == idAlmacen && x.idRepuestos == idRepuesto).Include(x => x.Repuestos).FirstOrDefaultAsync();
        }

        public async Task<RepuestosAlmacen> GetPorIdRepuestoAlmacen(long idRepuesto, long idAlmacen)
        {
            return await _context.RepuestosAlmacen.Where(x => x.idRepuestos == idRepuesto && x.idAlmacen == idAlmacen).Include(x => x.Repuestos).FirstOrDefaultAsync();
        }

        public async Task<List<RepuestosAlmacen>> GetRepuestosPorAlmacen(long idAlmacen)
        {
            return await _context.RepuestosAlmacen.Where(x => x.idAlmacen == idAlmacen).Include(x => x.Repuestos).ToListAsync();
        }

        public async Task<List<RepuestosAlmacen>> GetAlmacenesPorRepuesto(long idRepuesto)
        {
            return await _context.RepuestosAlmacen.Where(x => x.idRepuestos == idRepuesto).Include(x => x.Repuestos).ToListAsync();
        }
        public async Task<RepuestosAlmacen> ActualizarCantidadRepuestosAlmacen(long idAlmacen, long idRepuesto, long cantidad, long tipo)
        {
            var get = await _context.RepuestosAlmacen.FirstOrDefaultAsync(x => x.idAlmacen == idAlmacen && x.idRepuestos == idRepuesto);
            //tipo == 1 Fisico, tipo == 2 Virtual
            if (tipo == 1)
            {
                get.cantidadActual = get.cantidadActual + cantidad;
            }
            else if (tipo == 2)
            {
                get.cantidadActual = get.cantidadActual - cantidad;
            }
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<RepuestosAlmacen> AgregarCantidadRepuestosAlmacen(long idAlmacen, long idRepuesto, long cantidad)
        {
            var get = await _context.RepuestosAlmacen.FirstOrDefaultAsync(x => x.idAlmacen == idAlmacen && x.idRepuestos == idRepuesto);
            get.cantidadActual = get.cantidadActual + cantidad;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<RepuestosAlmacen> TransferirRepuestosAlmacen(long idAlmacen, long idRepuesto, long cantidad, long almacen)
        {
            var get = await _context.RepuestosAlmacen.FirstOrDefaultAsync(x => x.idAlmacen == idAlmacen && x.idRepuestos == idRepuesto);
            if (almacen == 1)
            {
                get.cantidadActual = get.cantidadActual - cantidad;
            }
            else if (almacen == 2)
            {
                get.cantidadActual = get.cantidadActual + cantidad;
            }
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<RepuestosAlmacen> DañosRepuestosAlmacen(long idAlmacen, long idRepuesto, long cantidad)
        {
            var get = await _context.RepuestosAlmacen.FirstOrDefaultAsync(x => x.idAlmacen == idAlmacen && x.idRepuestos == idRepuesto);
            get.cantidadActual = get.cantidadActual - cantidad;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<RepuestosAlmacen> AjustarCantidadRepuestosAlmacen(long idAlmacen, long idRepuesto, long nuevaCantidad)
        {
            var get = await _context.RepuestosAlmacen.FirstOrDefaultAsync(x => x.idAlmacen == idAlmacen && x.idRepuestos == idRepuesto);
            get.cantidadActual = nuevaCantidad;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<List<RepuestosAlmacen>> GetTodas()
        {
            return await _context.RepuestosAlmacen.Include(x => x.Repuestos).ToListAsync();
        }

        public async Task<RepuestosAlmacen> Set(RepuestosAlmacen repuestosAlmacen, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(repuestosAlmacen);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(repuestosAlmacen);

                default:
                    return repuestosAlmacen;
            }
        }
    }
}
