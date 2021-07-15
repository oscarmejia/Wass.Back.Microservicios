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
    public class DLACCotizaciones 
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<Cotizaciones> _DLACTransaccion;
        private readonly DALCTransacciones<Licitacion> _DLACTransaccion2;

        public DLACCotizaciones(ProgramadorContext context)
        {
            this._context = context;
            _DLACTransaccion = new DALCTransacciones<Cotizaciones>(context);
            _DLACTransaccion2 = new DALCTransacciones<Licitacion>(context);
        }
        //Estados cotizacion: Creada = 1, Enviada = 2, Ganada Total = 3, Rechazada = 4, Cancelada = 5, Ganada Parcial = 6
        public async Task<Cotizaciones> getAsync(long idCotizacion)
        {
            return await _context.Cotizaciones.Where(x => x.idCotizacion == idCotizacion && !x.eliminada && x.estado != 5)
            .Include(x => x.ArchivosAdjuntos).FirstOrDefaultAsync();
        }

        public async Task<List<Cotizaciones>> getTodasAsync()
        {
            return await _context.Cotizaciones.Where(x => !x.eliminada)
                .Include(x => x.ArchivosAdjuntos).ToListAsync();
        }

        public async Task<List<Cotizaciones>> getCotizacionesByState(long idEmpresa, long estadoCotizacion, long estadoLicitacion)
        {
            return await _context.Cotizaciones
                .Where(
                        x => x.idEmpresa == idEmpresa &&
                            x.estado == estadoCotizacion &&
                            x.Licitacion.estado  == estadoLicitacion &&
                            x.Licitacion.OrdenTrabajo.idProveedorAsignado == idEmpresa
                       ).OrderByDescending(x => x.fechaCreacion)
                .Include(x => x.Licitacion.OrdenTrabajo)
                .ToListAsync();
        }


        public async Task<List<Cotizaciones>> getTodasPorSedeAsync(long idSede)
        {
            return await _context.Cotizaciones.Where(x => !x.eliminada && x.idSede == idSede)
                .Include(x => x.ArchivosAdjuntos).ToListAsync();
        }

        public async Task<List<Cotizaciones>> getTodasPorEmpresaAsync(long idEmpresa)
        {
            return await _context.Cotizaciones.Where(x => !x.eliminada && x.idEmpresa == idEmpresa).OrderByDescending(x => x.idCotizacion)
                .Include(x => x.Licitacion)
                .Include(x => x.ArchivosAdjuntos).ToListAsync();
        }
        public async Task<List<Cotizaciones>> GetIdEmpresaPago(long idEmpresa)
        {


            return await _context.Cotizaciones.Where(x => x.idEmpresa == idEmpresa &&
            x.estado == 180 && x.IdOrdenPago == 0 && x.Licitacion.estado==175)
                 .Include(x => x.Licitacion).ToListAsync();
        }

        public async Task<List<Cotizaciones>> GetIdOrdenPago(long idOrdenPago)
        {
            return await _context.Cotizaciones.Where(x => x.IdOrdenPago== idOrdenPago &&  x.estado == 180 &&
               x.IdOrdenPago != 0 && x.Licitacion.estado == 175) 
           .Include(x => x.Licitacion).ToListAsync();
        }

        public async Task<List<Cotizaciones>> GetPorLicitacion(long idLicitacion)
        {
            return await _context.Cotizaciones.Where(x => x.idLicitacion == idLicitacion && !x.eliminada).ToListAsync();
        }

        public async Task<List<Cotizaciones>> GetPorLicitacionEmpresa(long idLicitacion, long idEmpresa)
        {
            return await _context.Cotizaciones.Where(x => x.idLicitacion == idLicitacion && !x.eliminada && x.idEmpresa == idEmpresa).ToListAsync();
        }

        public async Task<List<Cotizaciones>> GetPorLicitacionOrdenada(long idLicitacion, long idEmpresa)
        {
            //estado 177 Adjudicada
            return await _context.Cotizaciones.Where(x => x.idLicitacion == idLicitacion && !x.eliminada && x.idEmpresa == idEmpresa && x.estado == 177).OrderBy(x => x.fechaPropuestaServicioCotizacion).ToListAsync();
        }

        public async Task<List<Cotizaciones>> GetPorLicitacionUltimoAnio(long idLicitacion)
        {
            //Estado 177 Adjudicada
            var anioActual = DateTime.Now.Year;
            return await _context.Cotizaciones.Where(x => x.idLicitacion == idLicitacion && !x.eliminada && x.fechaPropuestaServicioCotizacion.Year == anioActual && x.estado == 177).ToListAsync();
        }

        public async Task<Cotizaciones> setAsync(Cotizaciones cotizacion, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    return await _DLACTransaccion.Crear(cotizacion);
                case Transaction.Update:
                    //return await _DLACTransaccion.Actualizar(cotizacion);
                    var entry = _context.Cotizaciones.First(x => x.idCotizacion == cotizacion.idCotizacion);
                    _context.Entry(entry).CurrentValues.SetValues(cotizacion);
                    _context.SaveChanges();
                    return entry;
                case Transaction.Delete:
                    cotizacion.eliminada = true;
                    return await _DLACTransaccion.Actualizar(cotizacion);
                default:
                    return cotizacion;
            }
        }

       

        public async Task<Cotizaciones> EliminarCotizacion(long idCotizacion)
        {
            var get = await _context.Cotizaciones.FirstOrDefaultAsync(x => x.idCotizacion == idCotizacion);
            get.eliminada = true;
            _context.Update(get);
            await _context.SaveChangesAsync();

            return get;
        }

    }

}
