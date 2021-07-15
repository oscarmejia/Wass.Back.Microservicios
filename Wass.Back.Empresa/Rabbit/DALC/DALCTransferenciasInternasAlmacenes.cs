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
    public class DALCTransferenciasInternasAlmacenes
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<TransferenciasInternasAlmacenes> _DALCTransaccion;

        public DALCTransferenciasInternasAlmacenes(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<TransferenciasInternasAlmacenes>(context);
        }

        public async Task<TransferenciasInternasAlmacenes> Get(long idTransferenciasInternasAlmacenes)
        {
            return await _context.TransferenciasInternasAlmacenes.Where(x => x.idTransferenciasInternasAlmacenes == idTransferenciasInternasAlmacenes).FirstOrDefaultAsync();
        }

        public async Task<TransferenciasInternasAlmacenes> actualizarEstado(long idTransferenciasInternasAlmacenes, long estado)
        {
            var transferencia = await _context.TransferenciasInternasAlmacenes.Where(x => x.idTransferenciasInternasAlmacenes == idTransferenciasInternasAlmacenes).FirstOrDefaultAsync();
            transferencia.estado = estado;
            _context.TransferenciasInternasAlmacenes.Update(transferencia);
            await _context.SaveChangesAsync();

            return transferencia;


        }
        public async Task<List<TransferenciasInternasAlmacenes>> GetTodas()
        {
            return await _context.TransferenciasInternasAlmacenes.ToListAsync();
        }

        public async Task<List<TransferenciasInternasAlmacenes>> GetTodasPorEmpresa(long idEmpresa)
        {
            return await _context.TransferenciasInternasAlmacenes.Where(x => x.idEmpresa == idEmpresa).ToListAsync();
        }


        public async Task<TransferenciasInternasAlmacenes> GetPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.TransferenciasInternasAlmacenes.Where(x => x.idAlmacenEmisor == idAlmacen && x.repuestos.Contains(Convert.ToString(idRepuesto))).FirstOrDefaultAsync();
        }
        public async Task<List<TransferenciasInternasAlmacenes>> GetTodasPorAlmacenRepuesto(long idAlmacen, long idRepuesto)
        {
            return await _context.TransferenciasInternasAlmacenes.Where(x => x.idAlmacenEmisor == idAlmacen && x.repuestos.Contains(Convert.ToString(idRepuesto))).OrderByDescending(x => x.fechaHora).ToListAsync();
        }

        public async Task<List<TransferenciasInternasAlmacenes>> GetTodasPorAlmacenRepuestoMobile(long idAlmacen, long idRepuesto)
        {
            return await _context.TransferenciasInternasAlmacenes.Where(x => x.idAlmacenEmisor == idAlmacen || x.idAlmacenReceptor == idAlmacen && x.repuestos.Contains(Convert.ToString(idRepuesto))).OrderByDescending(x => x.fechaHora).ToListAsync();
        }

        public async Task<TransferenciasInternasAlmacenes> Set(TransferenciasInternasAlmacenes transferenciasInternasAlmacenes, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(transferenciasInternasAlmacenes);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(transferenciasInternasAlmacenes);

                default:
                    return transferenciasInternasAlmacenes;
            }
        }
    }
}
