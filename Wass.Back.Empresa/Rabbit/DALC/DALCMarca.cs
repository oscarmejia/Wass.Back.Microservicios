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
    public class DALCMarca
    {

        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<Marca> _DALCTransaccion;

        public DALCMarca(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<Marca>(context);
        }

        public async Task<Marca> get(long idMarca)
        {
            return await _context.Marca.Where(x => x.idMarca == idMarca).FirstOrDefaultAsync();
        }

        public async Task<Marca> getSub(long idSubMarca)
        {
            return await _context.Marca.Where(x => x.idSubMarca == idSubMarca).FirstOrDefaultAsync();
        }

        public async Task<List<Marca>> getTodas()
        {
            return await _context.Marca.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<Marca> Set(Marca marca, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(marca);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(marca);

                default:
                    return marca;
            }
        }
    }
}
