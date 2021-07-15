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
    public class DALCPreguntas
    {
        private readonly EmpresaContext _context;

        public DALCPreguntas(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<Preguntas> Get(long idPregunta)
        {
            return await _context.Preguntas.Where(x => x.idPregunta == idPregunta).FirstOrDefaultAsync();
        }

        public async Task<List<Preguntas>> GetTodas()
        {
            return await _context.Preguntas.Where(x => x.activo != false).ToListAsync();
        }

        public async Task<List<Preguntas>> GetPorEmpresa(long idEmpresa)
        {
            return await _context.Preguntas.Where(x => x.idEmpresa == idEmpresa && x.activo != false).ToListAsync();
        }

        public async Task<Preguntas> Set(Preguntas preguntas, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await Crear(preguntas);

                case Transaction.Update:
                    return await Editar(preguntas);

                default:
                    return preguntas;
            }
        }


        public async Task<Preguntas> Crear(Preguntas preguntas)
        {
            _ = _context.Add(preguntas);
            _ = await _context.SaveChangesAsync();

            return preguntas;
        }

        public async Task<Preguntas> Editar(Preguntas preguntas)
        {
            _ = _context.Update(preguntas);
            _ = await _context.SaveChangesAsync();

            return preguntas;
        }


        public async Task<ResponseTransaction> Eliminar(Preguntas preguntas)
        {
            try
            {
                var pregunta = _context.Preguntas.Where(x => x.idPregunta == preguntas.idPregunta).FirstOrDefault();
                pregunta.activo = false;
                _ = _context.Update(pregunta);
                _ = await _context.SaveChangesAsync();

                return new ResponseTransaction()
                {
                    estado = true,
                    mensaje = $"Pregunta eliminada con exito."
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
