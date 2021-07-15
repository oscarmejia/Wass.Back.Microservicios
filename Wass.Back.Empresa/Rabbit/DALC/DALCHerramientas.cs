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
    public class DALCHerramientas
    {

        private readonly EmpresaContext _context;

        public DALCHerramientas(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<List<Herramientas>> GetTodas()
        {
            return await _context.Herramientas.Where(x => x.estado != 4).ToListAsync();
        }

        public async Task<Herramientas> GetPorId(long idHerramienta)
        {
            return await _context.Herramientas.Where(x => x.idHerramienta == idHerramienta).FirstOrDefaultAsync();
        }

        public async Task<List<Herramientas>> GetPorSede(long idSede)
        {
            return await _context.Herramientas.Where(x => x.idSede == idSede).ToListAsync();
        }


        public async Task<List<Herramientas>> GetPorEmpresaAsync(long idEmpresa)
        {
            var sql = (from herramienta in _context.Herramientas
                       join sede in _context.Sedes on herramienta.idSede equals sede.idSede
                       where sede.idEmpresa == idEmpresa
                       select herramienta
                       ).AsQueryable();

            return await sql.ToListAsync();
        }


        public async Task<Herramientas> Set(Herramientas herramienta, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await Crear(herramienta);

                case Transaction.Update:
                    return await Actualizar(herramienta);
                default:
                    return herramienta;
            }
        }

        public async Task<Herramientas> Crear(Herramientas herramienta)
        {
            _ = _context.Add(herramienta);
            _ = await _context.SaveChangesAsync();

            return herramienta;
        }

        public async Task<Herramientas> Actualizar(Herramientas herramienta)
        {
            _ = _context.Update(herramienta);
            _ = await _context.SaveChangesAsync();

            return herramienta;
        }

        public async Task<ResponseTransaction> Eliminar(long idHerramienta)
        {
            try
            {
                var herramienta = _context.Herramientas.Where(x => x.idHerramienta == idHerramienta).FirstOrDefault();
                herramienta.eliminado = true;
                _ = _context.Update(herramienta);
                _ = await _context.SaveChangesAsync();
                return new ResponseTransaction()
                {
                    estado = true,
                    mensaje = $"Herramienta eliminada con exito."
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
