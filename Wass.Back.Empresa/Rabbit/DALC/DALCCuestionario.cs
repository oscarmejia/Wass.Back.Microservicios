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
    public class DALCCuestionario
    {
        private readonly EmpresaContext _context;

        public DALCCuestionario(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<Cuestionario> Get(long idCuestionario)
        {
            return await _context.Cuestionario.Where(x => x.idCuestionario == idCuestionario).FirstOrDefaultAsync();
        }

        public async Task<List<Cuestionario>> GetTodas()
        {
            return await _context.Cuestionario.Where(x => x.activo != false).ToListAsync();
        }

        public async Task<List<Cuestionario>> GetPorEmpresa(long idEmpresa)
        {
            return await _context.Cuestionario.Where(x => x.idEmpresa == idEmpresa).ToListAsync();
        }

        public async Task<Cuestionario> Set(Cuestionario cuestionario, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await Crear(cuestionario);
                case Transaction.Update:
                    return await Editar(cuestionario);
                default:
                    return cuestionario;
            }
        }

        public async Task<Cuestionario> Crear(Cuestionario cuestionario)
        {
            _ = _context.Add(cuestionario);
            _ = await _context.SaveChangesAsync();

            return cuestionario;
        }

        public async Task<Cuestionario> Editar(Cuestionario cuestionario)
        {
            _ = _context.Update(cuestionario);
            _ = await _context.SaveChangesAsync();

            return cuestionario;
        }

        public async Task<ResponseTransaction> Eliminar(Cuestionario cuestionario)
        {
            try
            {
                var dataCuestionario = _context.Cuestionario.Where(x => x.idCuestionario == cuestionario.idCuestionario).FirstOrDefault();
                dataCuestionario.activo = false;
                _ = _context.Update(dataCuestionario);
                _ = await _context.SaveChangesAsync();

                return new ResponseTransaction()
                {
                    estado = true,
                    mensaje = $"Cuestionario eliminado con exito."
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
