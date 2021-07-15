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
    public class DALCRepuestos
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<Repuestos> _DALCTransaccion;

        public DALCRepuestos(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<Repuestos>(context);
        }

        public async Task<Repuestos> Get(long idRepuestos)
        {
            return await _context.Repuestos.Where(x => x.idRepuestos == idRepuestos)
                .Include(x => x.RepuestosAlmacen)
                .Include(x => x.RepuestosDiagnostico)
                .FirstOrDefaultAsync();
        }

        public async Task<Repuestos> GetID(long idRepuestos)
        {
            return await _context.Repuestos.Where(x => x.idRepuestos == idRepuestos).FirstOrDefaultAsync();
        }


        public async Task<List<Repuestos>> GetTodas()
        {
            return await _context.Repuestos
                .Include(x => x.RepuestosAlmacen)
                .Include(x => x.RepuestosDiagnostico)
                .ToListAsync();
        }

        public async Task<List<Repuestos>> GetTodasPorCategoria(long idCategoria)
        {
            return await _context.Repuestos.Where(x => x.idCategoria == idCategoria)
                .Include(x => x.RepuestosAlmacen)
                .Include(x => x.RepuestosDiagnostico)
                .ToListAsync();
        }

        public async Task<List<Repuestos>> GetTodasPorClasificacion(long idClasificacion)
        {
            return await _context.Repuestos.Where(x => x.idClasificacion == idClasificacion)
                .Include(x => x.RepuestosAlmacen)
                .Include(x => x.RepuestosDiagnostico)
                .ToListAsync();
        }

        public async Task<Repuestos> Set(Repuestos repuestos, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(repuestos);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(repuestos);

                default:
                    return repuestos;
            }
        }
    }
}
