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
    public class CronogramaLicitacionController : ControllerBase
    {

        private readonly BOCronogramaLicitacion _bussines;
        private readonly IConfiguration _configuration;
        public CronogramaLicitacionController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOCronogramaLicitacion(dataBase);
        }

        [HttpGet]
        [Route("{idCronogramaLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CronogramaLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CronogramaLicitacion>> Get(long idCronogramaLicitacion)
        {
            return await _bussines.Get(idCronogramaLicitacion);
        }

        [HttpGet]
        [Route("Licitacion/{idLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CronogramaLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CronogramaLicitacion>>> GetPorLicitacion(long idLicitacion)
        {
            return await _bussines.GetTodasPorLicitacion(idLicitacion);
        }


        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CronogramaLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CronogramaLicitacion>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CronogramaLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CronogramaLicitacion>> CrearCronograma([FromBody] CronogramaLicitacion cronograma)
        {
            return await _bussines.guardaCronograma(cronograma, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CronogramaLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CronogramaLicitacion>> EditarCronograma([FromBody] CronogramaLicitacion cronograma)
        {
            return await _bussines.guardaCronograma(cronograma, Transaction.Update);
        }
    }
}
