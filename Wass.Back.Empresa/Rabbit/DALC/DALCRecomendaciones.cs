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
    public class DALCRecomendaciones
    {
        private readonly EmpresaContext _context;

        public DALCRecomendaciones(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<Recomendaciones> Get(int idRecomendaciones)
        {
            return await _context.Recomendaciones.Where(x => x.idRecomendacion == idRecomendaciones).FirstOrDefaultAsync();
        }

        public async Task<List<Recomendaciones>> GetTodas()
        {
            return await _context.Recomendaciones.ToListAsync();
        }

        public async Task<List<Recomendaciones>> GetTodasPorIdRecomendado(long idEmpresaRecomendada)
        {
            return await _context.Recomendaciones.Where(x => x.idEmpresaRecomendada == idEmpresaRecomendada).ToListAsync();
        }

        public async Task<Recomendaciones> GetRecomendacionPorEmisorYReceptor(long idEmpresaRecomienda, long idEmpresaRecomendada)
        {
            return await _context.Recomendaciones.Where(x => x.idEmpresaRecomienda.Equals(idEmpresaRecomienda) && x.idEmpresaRecomendada.Equals(idEmpresaRecomendada)).FirstOrDefaultAsync();
        }


        public async Task<Recomendaciones> Set(Recomendaciones recomendaciones, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await Crear(recomendaciones);
                default:
                    return recomendaciones;
            }
        }

        public async Task<Recomendaciones> Crear(Recomendaciones recomendacion)
        {
            _ = _context.Add(recomendacion);
            _ = await _context.SaveChangesAsync();

            return recomendacion;
        }
    }
}
