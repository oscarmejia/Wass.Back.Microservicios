using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.Interface;
using WASS.Back.Programador.core.rabbit.DALC;

namespace Wass.Back.Programador.Rabbit.DALC
{
    public class DALCOrdenesTrabajo : IDALCCrud<OrdenesTrabajo>
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<OrdenesTrabajo> _transact;

        public DALCOrdenesTrabajo(ProgramadorContext context)
        {
            _context = context;
            _transact = new DALCTransacciones<OrdenesTrabajo>(context);
        }


        public async Task<OrdenesTrabajo> GetPrueba(long id)
        {
            return await _context.OrdenesTrabajo.Where(x => x.idOrden == id && !x.eliminada)
                .Include(x => x.mantenimientoAviso)
                .Include(x => x.mantenimientoCorrectivo)
                .Include(x => x.mantenimientoPreventivo)
                .Include(x => x.mantenimientoRondas)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .FirstOrDefaultAsync();
        }

        public async Task<List<OrdenesTrabajo>> GetAllPrueba()
        {
            return await _context.OrdenesTrabajo.Where(x => !x.eliminada)
                .Include(x => x.mantenimientoAviso)
                .Include(x => x.mantenimientoCorrectivo)
                .Include(x => x.mantenimientoPreventivo)
                .Include(x => x.mantenimientoRondas)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .ToListAsync();
        }

        public async Task<OrdenesTrabajo> Get(long id)
        {
            return await _context.OrdenesTrabajo.Where(x => x.idOrden == id && !x.eliminada)
                .Include(x => x.mantenimientoAviso)
                .Include(x => x.mantenimientoCorrectivo)
                .Include(x => x.mantenimientoPreventivo)
                .Include(x => x.mantenimientoRondas)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .FirstOrDefaultAsync();
        }

        public async Task<List<OrdenesTrabajo>> GetAll()
        {
            return await _context.OrdenesTrabajo.Where(x => !x.eliminada)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .ToListAsync();
        }

        public async Task<List<OrdenesTrabajo>> GetTodasPorCuadrillaAsync(long idCuadrilla)
        {
            return await _context.OrdenesTrabajo.Where(x => !x.eliminada && x.idCuadrilla == idCuadrilla)
                .Include(x => x.mantenimientoAviso)
                .Include(x => x.mantenimientoCorrectivo)
                .Include(x => x.mantenimientoPreventivo)
                .Include(x => x.mantenimientoRondas)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .ToListAsync();
        }

        public async Task<List<OrdenesTrabajo>> GetAllPorFechaCierreLlena()
        {
            return await _context.OrdenesTrabajo.Where(x => !x.eliminada && x.fechaCierre != null)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .ToListAsync();
        }


        public async Task<List<OrdenesTrabajo>> GetHistoricoActivoAsync(Guid idActivo)
        {
            
            return await _context.OrdenesTrabajo.Where(x => !x.eliminada && x.datosActivos.Contains(Convert.ToString(idActivo)))
                .Include(x => x.mantenimientoAviso)
                .Include(x => x.mantenimientoCorrectivo)
                .Include(x => x.mantenimientoPreventivo)
                .Include(x => x.mantenimientoRondas)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .OrderByDescending(x => x.fechaCreacion)
                .ToListAsync();
        }
        

        public async Task<List<OrdenesTrabajo>> GetAllPorSede(long idSede)
        {
            return await _context.OrdenesTrabajo.Where(x => x.idSede == idSede)
                .Include(x => x.mantenimientoAviso)
                .Include(x => x.mantenimientoCorrectivo)
                .Include(x => x.mantenimientoPreventivo)
                .Include(x => x.mantenimientoRondas)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .ToListAsync();
        }

        public async Task<List<OrdenesTrabajo>> GetAllPorEmpresa(long idEmpresa)
        {
            return await _context.OrdenesTrabajo.Where(x => x.idEmpresa == idEmpresa)
                .Include(x => x.mantenimientoAviso)
                .Include(x => x.mantenimientoCorrectivo)
                .Include(x => x.mantenimientoPreventivo)
                .Include(x => x.mantenimientoRondas)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .ToListAsync();
        }

        public async Task<List<OrdenesTrabajo>> GetAllPorEmpresaPorTercerizar(long idEmpresa)
        {
            //x.idEstadoOrden == 62 Por tercerizar
            return await _context.OrdenesTrabajo.Where(x => x.idEmpresa == idEmpresa && x.idEstadoOrden == 62)
                .Include(x => x.mantenimientoAviso)
                .Include(x => x.mantenimientoCorrectivo)
                .Include(x => x.mantenimientoPreventivo)
                .Include(x => x.mantenimientoRondas)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .ToListAsync();
        }

        public async Task<List<OrdenesTrabajo>> GetAllSinCerrar(long idEmpresa)
        {
            return await _context.OrdenesTrabajo.Where(x => x.idEmpresa == idEmpresa && x.fechaCierre == null)
                .Include(x => x.mantenimientoAviso)
                .Include(x => x.mantenimientoCorrectivo)
                .Include(x => x.mantenimientoPreventivo)
                .Include(x => x.mantenimientoRondas)
                .Include(x => x.ArchivosAdjuntos)
                .Include(x => x.Incidencias)
                .ToListAsync();
        }

        public async Task<List<OrdenesTrabajo>> GetAllPorActivo(Guid idActivo)
        {
            return await _context.OrdenesTrabajo.Where(x => x.datosActivos.Contains(Convert.ToString(idActivo)) && !x.eliminada).Include(x => x.ArchivosAdjuntos).ToListAsync();
        }

        public async Task<List<OrdenesTrabajo>> GetHistoricoActivoVariable(Guid idActivo)
        {
            return await _context.OrdenesTrabajo.Where(x => x.datosActivos.Contains(Convert.ToString(idActivo)) && !x.eliminada && x.mantenimientoRondas != null).Include(x => x.mantenimientoRondas).OrderByDescending(x => x.fechaCreacion).ToListAsync();
        }
        public async Task<List<OrdenesTrabajo>> GetPorBusquedaAsync(BusquedasOrdenesRequest mensaje)
        {
            return await _context.OrdenesTrabajo.Where(x => !x.eliminada
                    && mensaje.estados.Contains(x.idEstadoOrden)
                    && (mensaje.idEmpresa != null ? x.idEmpresa == mensaje.idEmpresa : x.idEmpresa == x.idEmpresa)
                    && (mensaje.idSede != null ? x.idSede == mensaje.idSede : x.idSede == x.idSede)
                    && (mensaje.idOrden != null ? x.idOrden == mensaje.idOrden : x.idOrden == x.idOrden)
                    && x.idServicio.Equals((int)mensaje.servicio)
                    && (mensaje.aprobador != null ? x.aprobador == mensaje.aprobador : x.aprobador == x.aprobador)
                    && (mensaje.idProveedorAsignado != null ? x.idProveedorAsignado == mensaje.idProveedorAsignado : x.idProveedorAsignado == x.idProveedorAsignado)
                    && (mensaje.aprobador != null ? x.aprobador == mensaje.aprobador : x.aprobador == x.aprobador)
                  ).ToListAsync();
        }

        public async Task<OrdenesTrabajo> Set(OrdenesTrabajo objeto, Transaction transaccion)
        {
            switch (transaccion)
            {
                case Transaction.Insert:
                    objeto.eliminada = false;
                    objeto.facturada = false;
                    return await _transact.Crear(objeto);
                case Transaction.Delete:
                    objeto.eliminada = true;
                    objeto.facturada = false;
                    return await _transact.Actualizar(objeto);
                case Transaction.Update:
                    objeto.eliminada = false;
                    objeto.facturada = false;
                    return await _transact.Actualizar(objeto);
                default:
                    return objeto;
            }
        }
    }
}
