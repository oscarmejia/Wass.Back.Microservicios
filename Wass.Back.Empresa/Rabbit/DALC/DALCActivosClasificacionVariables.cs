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
    public class DALCActivosClasificacionVariables : IDALCCrud<ActivosClasificacionVariables>
    {

        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosClasificacionVariables> _transact;

        public DALCActivosClasificacionVariables(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosClasificacionVariables>(context);
        }

        public async Task<ActivosClasificacionVariables> GetAsync(long id)
        {
            return await _context.ActivosClasificacionVariables.Where(x => x.idVarible == id && !x.eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosClasificacionVariables>> GetAllAsync()
        {
            return await _context.ActivosClasificacionVariables.Where(x => !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosClasificacionVariables>> GetPorClasificacionAsync(long idClasificacion)
        {
            return await _context.ActivosClasificacionVariables.Where(x => x.idClasificacion == idClasificacion && !x.eliminado).ToListAsync();
        }

        public async Task<List<ActivosClasificacionVariables>> GetPorUnidadMedidaAsync(long idUnidadMedida)
        {
            return await _context.ActivosClasificacionVariables.Where(x => x.idUnidadMedida == idUnidadMedida && !x.eliminado).ToListAsync();
        }

        public async Task<ActivosClasificacionVariables> SetAsync(ActivosClasificacionVariables objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.eliminado = false;
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.eliminado = true;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    objeto.eliminado = false;
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
