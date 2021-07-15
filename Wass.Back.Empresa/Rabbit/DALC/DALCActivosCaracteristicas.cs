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
    public class DALCActivosCaracteristicas : IDALCCrudGuid<ActivosCaracteristicas>
    {

        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<ActivosCaracteristicas> _transact;

        public DALCActivosCaracteristicas(EmpresaContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<ActivosCaracteristicas>(context);
        }

        public async Task<ActivosCaracteristicas> GetAsync(Guid id)
        {
            return await _context.ActivosCaracteristicas.Where(x => x.idActivoCaracteristica == id && !x.Eliminado).FirstOrDefaultAsync();
        }

        public async Task<List<ActivosCaracteristicas>> GetAllAsync()
        {
            return await _context.ActivosCaracteristicas.Where(x => !x.Eliminado).ToListAsync();
        }
        public async Task<List<ActivosCaracteristicas>> GetPorEquipoAsync(Guid id)
        {
            return await _context.ActivosCaracteristicas.Where(x => x.idActivoEquipo == id && !x.Eliminado).ToListAsync();
        }

        public async Task<List<ActivosCaracteristicas>> GetPorFlotaAsync(Guid id)
        {
            return await _context.ActivosCaracteristicas.Where(x => x.idActivoFlota == id && !x.Eliminado).ToListAsync();
        }
        public async Task<ActivosCaracteristicas> SetAsync(ActivosCaracteristicas objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.idActivoCaracteristica = Guid.NewGuid();
                    objeto.Eliminado = false;
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.Eliminado = true;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    objeto.Eliminado = false;
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
        public async Task<List<ActivosCaracteristicas>> SetListasAsync(List<ActivosCaracteristicas> listaCaracteristicas, Transaction transaccion)
        {
            var listarta = new List<ActivosCaracteristicas>();
            switch (transaccion)
            {
                case Transaction.InsertMasive:
                    foreach (var item in listaCaracteristicas)
                    {
                        item.idActivoCaracteristica = Guid.NewGuid();
                        item.Eliminado = false;
                        listarta.Add(await _transact.Crear(item));
                    }
                    return listarta;
                case Transaction.UpdateMasive:
                    foreach (var item in listaCaracteristicas)
                    {
                        item.Eliminado = false;
                        listarta.Add(await _transact.Actualizar(item));
                    }
                    return listarta;
                default:
                    return listaCaracteristicas;
            }
        }
    }
}
