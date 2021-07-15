using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.Interface;
using WASS.Back.Programador.core.rabbit.DALC;

namespace Wass.Back.Programador.Rabbit.DALC
{
    public class DALCLicitacion
    {

        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<Licitacion> _transact;
        public DALCLicitacion(ProgramadorContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<Licitacion>(context); 
        }


        public async Task<Licitacion> Get (long idLicitacion)
        {
            return await _context.Licitacion.Where(x => x.idLicitacion == idLicitacion && x.estado != 4)
                .Include(x => x.cronograma)
                .Include(x => x.soportes)
                .Include(x => x.cotizaciones)
                .Include(x => x.skills)
                .Include(x => x.ArchivosAdjuntos)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Licitacion>> getLicitacionBySkillPaisSede(long idSkill, long idSede)
        {
            return await _context.Licitacion.Where(x => x.idSede == idSede && x.skills.idSkillLicitacion == idSkill)
                .Include(x => x.skills)
                .ToListAsync();
        }

        public async Task<List<Licitacion>> getLicitacionByPaisSkill(long idSkill)
        {
            return await _context.Licitacion.Where(x => x.skills.idSkillLicitacion == idSkill)
                .Include(x => x.OrdenTrabajo)
                .ToListAsync();
        }

        public async Task<List<Licitacion>> getLicitacionPorInvitacion(long idEmpresa)
        {
            return await _context.Licitacion.Where(x => x.tipoLicitacion == 2)
                .Include(x => x.cronograma)
                .Include(x => x.soportes)
                .Include(x => x.cotizaciones)
                .Include(x => x.OrdenTrabajo)
                .Include(x => x.skills)
                .Include(x => x.ArchivosAdjuntos)
                .ToListAsync();
        }

        public async Task<List<Licitacion>> GetTodas ()
        {
            return await _context.Licitacion.Where(x => x.estado != 4)
                .Include(x => x.cronograma)
                .Include(x => x.soportes)
                .Include(x => x.cotizaciones)
                .Include(x => x.skills)
                .Include(x => x.ArchivosAdjuntos)
                .ToListAsync();
            
        }

        public async Task<Licitacion> GetIdOrden (long idOrden)
        {
            return await _context.Licitacion.Where(x => x.idOrden == idOrden).FirstOrDefaultAsync();
        }

        public async Task<List<Licitacion>> GetIdSede(long idSede)
        {
            return await _context.Licitacion.Where(x => x.idSede == idSede && x.estado != 4)
                .Include(x => x.cronograma)
                .Include(x => x.soportes)
                .Include(x => x.cotizaciones)
                .Include(x => x.skills)
                .Include(x => x.ArchivosAdjuntos)
                .ToListAsync();
        }

        public async Task<List<Licitacion>> GetIdEmpresa(long idEmpresa)
        {
            return await _context.Licitacion.Where(x => x.idEmpresa == idEmpresa && x.estado != 4)
                .Include(x => x.cronograma)
                .Include(x => x.soportes)
                .Include(x => x.cotizaciones)
                .Include(x => x.skills)
                .Include(x => x.ArchivosAdjuntos)
                .ToListAsync();
            
        }
          

        public async Task<List<Licitacion>> GetIdEmpresaAdjudicadas(long idEmpresa)
        {
            //estado = 61 Adjudicada
            return await _context.Licitacion.Where(x => x.idEmpresa == idEmpresa && x.estado == 61)
                .Include(x => x.cronograma)
                .Include(x => x.soportes)
                .Include(x => x.cotizaciones)
                .Include(x => x.skills)
                .Include(x => x.ArchivosAdjuntos)
                .ToListAsync();
        }

        public async Task<List<Licitacion>> GetIdEmpresaAdjudicadasPorOrdenTrabajo(long idEmpresa, long idOrden)
        {
            //estado = 61 Adjudicada
            return await _context.Licitacion.Where(x => x.idEmpresa == idEmpresa && x.estado == 61 && x.idOrden == idOrden)
                .Include(x => x.cronograma)
                .Include(x => x.soportes)
                .Include(x => x.cotizaciones)
                .Include(x => x.skills)
                .Include(x => x.ArchivosAdjuntos)
                .ToListAsync();
        }

        public async Task<Licitacion> Set (Licitacion licitacion, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _transact.Crear(licitacion);
                case Transaction.Update:
                    return await _transact.Actualizar(licitacion);
                default:
                    return licitacion;
            }
        }
    }
}
