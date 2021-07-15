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
    public class DALCRespuestaActivosVariables
    {
        private readonly EmpresaContext _context;
        private readonly DALCTransacciones<RespuestaActivosVariables> _DALCTransaccion;

        public DALCRespuestaActivosVariables(EmpresaContext context)
        {
            _context = context;
            _DALCTransaccion = new DALCTransacciones<RespuestaActivosVariables>(context);
        }

        public async Task<RespuestaActivosVariables> Get(long idRespuestaActivosVariables)
        {
            return await _context.RespuestaActivosVariables.Where(x => x.idRespuestaActivosVariables == idRespuestaActivosVariables).FirstOrDefaultAsync();
        }

        public async Task<List<RespuestaActivosVariables>> GetTodas()
        {
            return await _context.RespuestaActivosVariables.ToListAsync();
        }

        public async Task<RespuestaActivosVariables> Set(RespuestaActivosVariables respuestas, Transaction transaction)
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


        public async Task<List<RespuestaActivosVariables>> GetPorCategoriaClasificacionActivo(long idClasificacion, long idCategorizacion, Guid idActivo)
        {
            return await _context.RespuestaActivosVariables.Where(x => x.idClasificacion == idClasificacion && x.idCategorizacion == idCategorizacion && (x.idActivoFlota == idActivo || x.idActivoEquipo == idActivo)).ToListAsync();
        }
    }
}
