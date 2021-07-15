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
    public class DALCMarcaActivo
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<MarcaActivo> _DALCTransaccion;

        public DALCMarcaActivo(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<MarcaActivo>(context);
        }

        public async Task<MarcaActivo> Get(long idMarcaActivo)
        {
            return await _context.MarcaActivo.Where(x => x.idMarcaActivo == idMarcaActivo).FirstOrDefaultAsync();
        }
        public async Task<List<MarcaActivo>> GetTodas()
        {
            return await _context.MarcaActivo.ToListAsync();
        }

        public async Task<MarcaActivo> Set(MarcaActivo marcaActivo, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(marcaActivo);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(marcaActivo);

                default:
                    return marcaActivo;
            }
        }
    }
}
