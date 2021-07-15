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
    public class DALCComentarioActivosClasificacionDiagnosticoAcciones
    {
        private readonly ProgramadorContext _context;
        private readonly DALCTransacciones<ComentarioActivosClasificacionDiagnosticoAcciones> _DALCTransaccion;

        public DALCComentarioActivosClasificacionDiagnosticoAcciones(ProgramadorContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<ComentarioActivosClasificacionDiagnosticoAcciones>(context);
        }

        public async Task<ComentarioActivosClasificacionDiagnosticoAcciones> Get(long idComentarioDiagnosticoAcciones)
        {
            return await _context.ComentarioActivosClasificacionDiagnosticoAcciones.Where(x => x.idComentarioDiagnosticosAcciones == idComentarioDiagnosticoAcciones).FirstOrDefaultAsync();
        }

        public async Task<ComentarioActivosClasificacionDiagnosticoAcciones> GetPorDiagnosticoMantenimientoCorrectivoClasificacion(long idDiagnostico, long idMantenimientoCorrectivo, long idClasificacion)
        {
            return await _context.ComentarioActivosClasificacionDiagnosticoAcciones.Where(x => x.idDiagnostico == idDiagnostico && x.idMantenimientoCorrectivo == idMantenimientoCorrectivo && x.idClasificacion == idClasificacion).FirstOrDefaultAsync();
        }

        public async Task<List<ComentarioActivosClasificacionDiagnosticoAcciones>> GetTodas()
        {
            return await _context.ComentarioActivosClasificacionDiagnosticoAcciones.ToListAsync();
        }

        public async Task<List<ComentarioActivosClasificacionDiagnosticoAcciones>> GetPorClasificacion(long idClasificacion)
        {
            return await _context.ComentarioActivosClasificacionDiagnosticoAcciones.Where(x => x.idClasificacion == idClasificacion).ToListAsync();
        }

        public async Task<ComentarioActivosClasificacionDiagnosticoAcciones> Set(ComentarioActivosClasificacionDiagnosticoAcciones respuestas, Transaction transaction)
        {
            switch (transaction)
            {
                case Transaction.Insert:
                    return await _DALCTransaccion.Crear(respuestas);

                case Transaction.Update:
                    return await _DALCTransaccion.Actualizar(respuestas);

                default:
                    return respuestas;
            }
        }


    }
}
