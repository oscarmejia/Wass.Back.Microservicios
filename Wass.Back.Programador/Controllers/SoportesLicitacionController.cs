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
    public class SoportesLicitacionController : ControllerBase
    {
        private readonly BOSoportesLicitacion _bussines;
        private readonly IConfiguration _configuration;

        public SoportesLicitacionController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOSoportesLicitacion(dataBase);
        }


        [HttpGet]
        [Route("{idSoporteLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SoportesLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<SoportesLicitacion>> Get (long idSoporteLicitacion)
        {
            return await _bussines.Get(idSoporteLicitacion);
        }

        [HttpGet]
        [Route("Licitacion/{idLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SoportesLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<SoportesLicitacion>>> GetPorLicitacion(long idLicitacion)
        {
            return await _bussines.GetTodasPorlicitacion(idLicitacion);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SoportesLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<SoportesLicitacion>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SoportesLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<SoportesLicitacion>> CrearSoporte([FromBody] SoportesLicitacion soportes)
        {
            return await _bussines.guardarSoporte(soportes, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SoportesLicitacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<SoportesLicitacion>> EditarSoporte([FromBody] SoportesLicitacion soportes)
        {
            return await _bussines.guardarSoporte(soportes, Transaction.Update);
        }
    }
}
