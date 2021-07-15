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
    public class IncidenciasController : ControllerBase
    {
        private readonly BOIncidencias _bussines;
        private readonly IConfiguration _configuration;
        public IncidenciasController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOIncidencias(dataBase);
        }
        // GET: api/values

        [HttpGet]
        [Route("{idIncidencias}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Incidencias>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Incidencias>> Get(long idIncidencias)
        {
            return await _bussines.getAsync(idIncidencias);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Incidencias>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Incidencias>>> GetTodas()
        {
            return await _bussines.getTodasAsync();
        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Incidencias>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Incidencias>> GuardarIncidencias([FromBody] Incidencias incidencias)
        {
            return await _bussines.setAsync(incidencias, Transaction.Insert);
        }


        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Incidencias>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Incidencias>> EditarIncidencias([FromBody] Incidencias incidencias)
        {
            return await _bussines.setAsync(incidencias, Transaction.Update);
        }
    }
}
