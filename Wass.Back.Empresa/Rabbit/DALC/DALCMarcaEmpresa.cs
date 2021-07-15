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
    public class DALCMarcaEmpresa
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<MarcaEmpresa> _DALCTransaccion;

        public DALCMarcaEmpresa(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<MarcaEmpresa>(context);
        }

        public async Task<MarcaEmpresa> get(long idMarcaEmpresa)
        {
            return await _context.MarcaEmpresa.Where(x => x.idMarcaEmpresa == idMarcaEmpresa).FirstOrDefaultAsync();
        }

        public async Task<List<MarcaEmpresa>> GetTodas()
        {
            return await _context.MarcaEmpresa.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<List<MarcaEmpresa>> GetTodasPorEmpresa(long idEmpresa)
        {
            return await _context.MarcaEmpresa.Where(x => x.idEmpresa == idEmpresa && !x.eliminado)
                .Include(x => x.Marca)
                .ToListAsync();
        }

        public async Task<List<MarcaEmpresa>> GetTodasPorMarca(long idMarca)
        {
            return await _context.MarcaEmpresa.Where(x => x.idMarca == idMarca && !x.eliminado)
                .Include(x => x.Empresa)
                .ToListAsync();
        }

        public async Task<MarcaEmpresa> Set(MarcaEmpresa marcaEmpresa, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(marcaEmpresa);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(marcaEmpresa);

                default:
                    return marcaEmpresa;
            }
        }
    }
}
