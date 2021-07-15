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
    public class DALCTopicoAComentar
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<TopicoAComentar> _DALCTransaccion;

        public DALCTopicoAComentar(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<TopicoAComentar>(context);
        }

        public async Task<TopicoAComentar> Get(long idTopicoAComentar)
        {
            return await _context.TopicoAComentar.Where(x => x.idTopicoAComentar == idTopicoAComentar && !x.eliminado)
                .Include(x => x.comentario)
                .FirstOrDefaultAsync();
        }

        public async Task<TopicoAComentar> GetPorIdTopico(string idTopico)
        {
            return await _context.TopicoAComentar.Where(x => x.idTopico == idTopico && !x.eliminado)
                .Include(x => x.comentario)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TopicoAComentar>> GetTodas()
        {
            return await _context.TopicoAComentar.Where(x => !x.eliminado)
                .Include(x => x.comentario)
                .ToListAsync();
        }

        public async Task<List<TopicoAComentar>> GetOrdenadasPorFecha()
        {
            return await _context.TopicoAComentar.Where(x => !x.eliminado && x.tipoTopico != 4).OrderByDescending(x => x.fechaHora)
                .Include(x => x.comentario)
                .ToListAsync();
        }

        public async Task<TopicoAComentar> EliminarComentario(long idTopicoAComentar)
        {
            var get = await _context.TopicoAComentar.FirstOrDefaultAsync(x => x.idTopicoAComentar == idTopicoAComentar);
            get.eliminado = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

        public async Task<List<TopicoAComentar>> getTodasPorSedeAsync(long idSede)
        {
            return await _context.TopicoAComentar.Where(x => !x.eliminado && x.idSede == idSede)
                .Include(x => x.comentario).ToListAsync();
        }

        public async Task<List<TopicoAComentar>> getTodasPorEmpresaAsync(long idEmpresa)
        {
            return await _context.TopicoAComentar.Where(x => !x.eliminado && x.idEmpresa == idEmpresa)
                .Include(x => x.comentario).ToListAsync();
        }

        public async Task<List<TopicoAComentar>> GetFeed()
        {
            return await _context.TopicoAComentar.Where(x => !x.eliminado && x.tipoTopico != 4).OrderByDescending(x => x.fechaHora)
                .Include(x => x.comentario).ToListAsync();
        }

        //TipoTopico : 1 = ActivoEquipo, 2 = ActivoFlota, 3 = Orden de trabajo, 4 = Licitacion 
        public async Task<TopicoAComentar> GetPorTipoTopicoIdTopico(long tipoTopico, string idTopico)
        {
            return await _context.TopicoAComentar.Where(x => x.tipoTopico == tipoTopico && x.idTopico == idTopico && !x.eliminado)
                .Include(x => x.comentario)
                .FirstOrDefaultAsync();
        }

        public async Task<TopicoAComentar> Set(TopicoAComentar comentario, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    comentario.fechaHora = DateTime.Now;
                    return await _DALCTransaccion.Crear(comentario);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(comentario);

                default:
                    return comentario;
            }
        }
    }
}
