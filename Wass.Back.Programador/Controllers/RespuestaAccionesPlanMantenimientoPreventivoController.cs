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
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RespuestaAccionesPlanMantenimientoPreventivoController : ControllerBase
    {
        private readonly BORespuestaAccionesPlanMantenimientoPreventivo _bussines;

        public RespuestaAccionesPlanMantenimientoPreventivoController(ProgramadorContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORespuestaAccionesPlanMantenimientoPreventivo(dataBase);
        }


        [HttpGet]
        [Route("{idRespuesta}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>> GetId(long idRespuesta)
        {
            return await _bussines.Get(idRespuesta);
        }

        [HttpGet]
        [Route("MantenimientoPrventivo/{idMantenimientoPreventivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>> GetIdMantenimiento(long idMantenimientoPreventivo)
        {
            return await _bussines.GetPorMantenimiento(idMantenimientoPreventivo);
        }

        [HttpGet]
        [Route("MantenimientoPrventivo/{idMantenimientoPreventivo}/parte/{idParte}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>> GetIdMantenimientoyParte(long idMantenimientoPreventivo, Guid idParte)
        {
            return await _bussines.GetPorMantenimientoyParte(idMantenimientoPreventivo, idParte);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaAccionesPlanMantenimientoPreventivo>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>> CrearRespuesta([FromBody] RespuestaAccionesPlanMantenimientoPreventivo respuesta)
        {
            return await _bussines.GuardarRespuesta(respuesta, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaAccionesPlanMantenimientoPreventivo>> EditarRespuestaCuestionario([FromBody] RespuestaAccionesPlanMantenimientoPreventivo respuesta)
        {
            return await _bussines.GuardarRespuesta(respuesta, Transaction.Update);
        }
    }
}
