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
    public class DALCCuestionarioPreguntas
    {
        private readonly EmpresaContext _context;

        public DALCCuestionarioPreguntas(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<CuestionarioPreguntas> Get(long idCuestionarioPregunta)
        {
            return await _context.CuestionarioPreguntas.Where(x => x.idCuestionarioPregunta == idCuestionarioPregunta)
                .Include(x => x.Preguntas)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CuestionarioPreguntas>> GetTodas()
        {
            return await _context.CuestionarioPreguntas.Where(x => x.activo != false)
                .Include(x => x.Preguntas)
                .ToListAsync();
        }

        public async Task<List<CuestionarioPreguntas>> GetTodasPreguntasPorCuestionario(long idCuestionario)
        {
            return await _context.CuestionarioPreguntas.Where(x => x.idCuestionario == idCuestionario)
                .Include(x => x.Preguntas)
                .ToListAsync();
        }

        public async Task<List<CuestionarioPreguntas>> GetTodasPreguntasEnCuestionario(long idPregunta)
        {
            return await _context.CuestionarioPreguntas.Where(x => x.idPregunta == idPregunta)
                .Include(x => x.Preguntas)
                .ToListAsync();
        }

        public async Task<CuestionarioPreguntas> Set(CuestionarioPreguntas cuestionarioPreguntas, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await Crear(cuestionarioPreguntas);

                case Transaction.Update:
                    return await Editar(cuestionarioPreguntas);
                default:
                    return cuestionarioPreguntas;
            }
        }

        public async Task<CuestionarioPreguntas> Crear(CuestionarioPreguntas cuestionarioPreguntas)
        {
            _ = _context.Add(cuestionarioPreguntas);
            _ = await _context.SaveChangesAsync();

            return cuestionarioPreguntas;
        }


        public async Task<CuestionarioPreguntas> Editar(CuestionarioPreguntas cuestionarioPreguntas)
        {
            _ = _context.Update(cuestionarioPreguntas);
            _ = await _context.SaveChangesAsync();

            return cuestionarioPreguntas;
        }

        public async Task<ResponseTransaction> Eliminar(CuestionarioPreguntas cuestionario)
        {
            try
            {
                var dataCuestionario = _context.CuestionarioPreguntas.Where(x => x.idCuestionarioPregunta == cuestionario.idCuestionarioPregunta).FirstOrDefault();
                dataCuestionario.activo = false;
                _ = _context.Update(dataCuestionario);
                _ = await _context.SaveChangesAsync();

                return new ResponseTransaction()
                {
                    estado = true,
                    mensaje = $"Pregunta eliminada del Cuestionario con exito."
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
