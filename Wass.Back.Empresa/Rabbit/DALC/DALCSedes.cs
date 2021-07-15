using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.Interface;
namespace Wass.Back.Empresa.Rabbit.DALC
{
    public class DALCSedes
    {
        /// <summary>
        /// Contexto de Base de Datos
        /// </summary>
        private readonly EmpresaContext _context;

        public DALCSedes(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<Sedes> Get(long idSede)
        {
            return await _context.Sedes.Where(x => x.idSede == idSede && !x.eliminado).Include(x => x.ListaCuadrillas).Include(x => x.ListaCentrosTrabajo).FirstOrDefaultAsync();
        }

        public async Task<List<Sedes>> GetByEmpresa(long idEmpresa)
        {
            return await _context.Sedes.Where(x => x.idEmpresa == idEmpresa && !x.eliminado).Include(x => x.ListaCuadrillas).Include(x => x.ListaCentrosTrabajo).ToListAsync();
        }

        public async Task<Sedes> Set(Sedes sede, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    return await Crear(sede);
                case Transaction.Update:
                    return await Actualizar(sede);
                default:
                    return sede;
            }
        }

        private async Task<Sedes> Actualizar(Sedes sede)
        {
            _ = _context.Update(sede);
            _ = await _context.SaveChangesAsync();
            return sede;
        }


        private async Task<Sedes> Crear(Sedes empresa)
        {
            _ = _context.Add(empresa);
            _ = await _context.SaveChangesAsync();
            return empresa;
        }


        public async Task<ResponseTransaction> Eliminar(long idSede)
        {
            try
            {
                var sede = _context.Sedes.Where(x => x.idSede == idSede).FirstOrDefault();
                sede.eliminado = true;
                _ = _context.Update(sede);
                _ = await _context.SaveChangesAsync();
                return new ResponseTransaction()
                {
                    estado = true,
                    mensaje = $"Empresa eliminada con exito."
                };
            }
            catch (Exception ex)
            {
                return new ResponseTransaction()
                {
                    estado = false,
                    mensaje = $"Error: {ex.Message}"
                };
            }
        }
    }
}
