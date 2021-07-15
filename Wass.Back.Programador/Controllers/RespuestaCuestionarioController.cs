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
using Wass.Back.Programador.Models.Peticiones.RespuestaCuestionario;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RespuestaCuestionarioController : ControllerBase
    {

        private readonly BORespuestaCuestionario _bussines;

        public RespuestaCuestionarioController(ProgramadorContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORespuestaCuestionario(dataBase);
        }


        [HttpGet]
        [Route("{idRespuestaCuestionario}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaCuestionarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaCuestionarioRequest>> GetId(long idRespuestaCuestionario)
        {
            return await _bussines.Get(idRespuestaCuestionario);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RespuestaCuestionarioRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaCuestionarioRequest>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaCuestionarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaCuestionarioRequest>> CrearRespuestaCuestionario([FromBody] RespuestaCuestionarioRequest RespuestaCuestionario)
        {
            return await _bussines.GuardarRespuestaCuestionario(RespuestaCuestionario, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaCuestionarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaCuestionarioRequest>> EditarRespuestaCuestionario([FromBody] RespuestaCuestionarioRequest RespuestaCuestionario)
        {
            return await _bussines.GuardarRespuestaCuestionario(RespuestaCuestionario, Transaction.Update);
        }

        [HttpGet]
        [Route("{idCuestionario}/{idCotizacion}/{idLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaCuestionarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaCuestionarioRequest>>> GetPorCuestionarioCotizacionLicitacion(long idCuestionario, long idCotizacion, long idLicitacion)
        {
            return await _bussines.GetPorCuestionarioCotizacionLicitacion(idCuestionario, idCotizacion, idLicitacion);
        }

        [HttpGet]
        [Route("{idCotizacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaCuestionarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaCuestionarioRequest>>> GetPorCotizacion(long idCotizacion)
        {
            return await _bussines.GetPorCotizacion(idCotizacion);
        }

    }
}

