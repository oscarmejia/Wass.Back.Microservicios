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
    public class DALCComentario
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<Comentario> _transac;

        public DALCComentario(EmpresaContext context)
        {
            _context = context;
            _transac = new DALCTransacciones<Comentario>(context);
        }

        public async Task<Comentario> GetPorId(long idComentario)
        {
            return await _context.Comentario.Where(x => x.idComentario == idComentario && !x.eliminado)
                .Include(x => x.Empleados)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Comentario>> GetPorIdTopicoAComentar(long idTopicoAComentar)
        {
            return await _context.Comentario.Where(x => x.idTopicoAComentar == idTopicoAComentar && !x.eliminado)
                .Include(x => x.Empleados)
                .ToListAsync();
        }

        public async Task<List<Comentario>> GetTodas()
        {
            return await _context.Comentario.Where(x => !x.eliminado)
                .Include(x => x.Empleados)
                .ToListAsync();
        }

        public async Task<List<Comentario>> getTodasPorFechaAsync()
        {
            return await _context.Comentario.OrderByDescending(x => x.fechaHoraComentario).ToListAsync();
        }

        public async Task<Comentario> EliminarComentario(long idComentario)
        {
            var get = await _context.Comentario.FirstOrDefaultAsync(x => x.idComentario == idComentario);
            get.eliminado = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<Comentario> ActualizarLikeComentario(long idComentario)
        {
            var get = await _context.Comentario.FirstOrDefaultAsync(x => x.idComentario == idComentario);
            get.like = get.like + 1;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<Comentario> Set(Comentario replicaComentario, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transac.Crear(replicaComentario);
                case Transaction.Update:
                    return await _transac.Actualizar(replicaComentario);
                default:
                    return replicaComentario;
            }
        }
    }
}
