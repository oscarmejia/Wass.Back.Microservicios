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
    public class DALCEmpresaSoportes : IDALCCrud<EmpresaSoportes>
    {
        /// <summary>
        /// Contexto de Base de Datos
        /// </summary>
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<EmpresaSoportes> _transact;

        public DALCEmpresaSoportes(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<EmpresaSoportes>(context);
        }
        public async Task<EmpresaSoportes> GetAsync(long id)
        {
            return await _context.EmpresaSoportes.Where(x => x.idEmpresa == id).Include(x => x.empresas).FirstOrDefaultAsync();
        }

        public async Task<EmpresaSoportes> GetAsync(Guid id)
        {
            return await _context.EmpresaSoportes.Where(x => x.idSoporte == id).Include(x => x.empresas).FirstOrDefaultAsync();
        }

        public async Task<List<EmpresaSoportes>> GetPorEmpresaAsync(long id)
        {
            return await _context.EmpresaSoportes.Where(x => x.idEmpresa == id).Include(x => x.empresas).ToListAsync();
        }

        public async Task<List<EmpresaSoportes>> GetPorActivoEquipoAsync(Guid idActivoEquipo)
        {
            return await _context.EmpresaSoportes.Where(x => x.idActivosEquipos == idActivoEquipo).Include(x => x.empresas).ToListAsync();
        }

        public async Task<List<EmpresaSoportes>> GetPorActivoFlotaAsync(Guid idActivoFlota)
        {
            return await _context.EmpresaSoportes.Where(x => x.idActivosFlotas == idActivoFlota).Include(x => x.empresas).ToListAsync();
        }

        public async Task<List<EmpresaSoportes>> GetAllAsync()
        {
            return await _context.EmpresaSoportes.Where(x => !x.eliminado).Include(x => x.empresas).ToListAsync();
        }

        public async Task<EmpresaSoportes> SetAsync(EmpresaSoportes objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.idSoporte = Guid.NewGuid();
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.eliminado = true;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
