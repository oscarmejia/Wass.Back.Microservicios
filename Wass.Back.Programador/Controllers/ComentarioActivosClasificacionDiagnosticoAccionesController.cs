using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Bussines;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Agenda;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.ComentarioActivosClasificacionDiagnosticoAcciones;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComentarioActivosClasificacionDiagnosticoAccionesController : ControllerBase
    {
        private readonly BOComentarioActivosClasificacionDiagnosticoAcciones _bussines;

        public ComentarioActivosClasificacionDiagnosticoAccionesController(ProgramadorContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOComentarioActivosClasificacionDiagnosticoAcciones(dataBase);
        }


        [HttpGet]
        [Route("{idComentarioDiagnosticoAcciones}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>> GetId(long idComentarioDiagnosticoAcciones)
        {
            return await _bussines.Get(idComentarioDiagnosticoAcciones);
        }

        [HttpGet]
        [Route("Diagnostico/{idDiagnostico}/MantenimientoCorrectivo/{idMantenimientoCorrectivo}/Clasificacion/{idClasificacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>> GetPorDiagnosticoMantenimientoCorrectivoClasificacion(long idDiagnostico,long idMantenimientoCorrectivo, long idClasificacion)
        {
            return await _bussines.GetPorDiagnosticoMantenimientoCorrectivoClasificacion(idDiagnostico, idMantenimientoCorrectivo, idClasificacion);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>> CrearRespuestaCuestionario([FromBody] ComentarioActivosClasificacionDiagnosticoAccionesRequest respuesta)
        {
            return await _bussines.GuardarComentario(respuesta, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>> EditarRespuestaCuestionario([FromBody] ComentarioActivosClasificacionDiagnosticoAccionesRequest respuesta)
        {
            return await _bussines.GuardarComentario(respuesta, Transaction.Update);
        }

        [HttpGet]
        [Route("Clasificacion/{idClasificacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ComentarioActivosClasificacionDiagnosticoAccionesRequest>>> GetPorClasificacion(long idClasificacion)
        {
            return await _bussines.GetPorClasificacion(idClasificacion);
        }
    }
}
