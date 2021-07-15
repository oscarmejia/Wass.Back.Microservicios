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
    public class DALCTipoActivo
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<TipoActivo> _DALCTransaccion;

        public DALCTipoActivo(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<TipoActivo>(context);
        }

        public async Task<TipoActivo> Get(long idTipoActivo)
        {
            return await _context.TipoActivo.Where(x => x.idTipoActivo == idTipoActivo).FirstOrDefaultAsync();
        }
        public async Task<List<TipoActivo>> GetTodas()
        {
            return await _context.TipoActivo.ToListAsync();
        }

        public async Task<TipoActivo> Set(TipoActivo tipoActivo, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(tipoActivo);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(tipoActivo);

                default:
                    return tipoActivo;
            }
        }
    }
}
