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
    public class DALCKardexRepuesto
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<KardexRepuesto> _DALCTransaccion;

        public DALCKardexRepuesto(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<KardexRepuesto>(context);
        }

        public async Task<KardexRepuesto> Set(KardexRepuesto consultaFechasInventario, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(consultaFechasInventario);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(consultaFechasInventario);

                default:
                    return consultaFechasInventario;
            }
        }
    }
}
