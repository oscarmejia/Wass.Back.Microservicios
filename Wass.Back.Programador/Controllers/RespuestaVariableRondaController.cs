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
    public class RespuestaVariableRondaController : ControllerBase
    {

        private readonly BORespuestaVariableRonda _bussines;
        private readonly IConfiguration _configuration;

        public RespuestaVariableRondaController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BORespuestaVariableRonda(dataBase);
        }


        [HttpGet]
        [Route("{idRespuestaVariableRonda}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaVariableRonda>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaVariableRonda>> Get(long idRespuestaVariableRonda)
        {
            return await _bussines.Get(idRespuestaVariableRonda);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RespuestaVariableRonda>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaVariableRonda>>> GetTodas()
        {
            return await _bussines.getTodas();
        }

        [HttpGet]
        [Route("ronda/{idRonda}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RespuestaVariableRonda>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaVariableRonda>>> GetTodasPorRonda(long idRonda)
        {
            return await _bussines.getTodasPorRonda(idRonda);
        }

        [HttpGet]
        [Route("variable/{idVariable}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RespuestaVariableRonda>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaVariableRonda>>> GetTodasPorVariable(long idVariable)
        {
            return await _bussines.getTodasPorVariable(idVariable);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaVariableRonda>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaVariableRonda>> GuadarRespuesta(RespuestaVariableRonda respuesta) 
        {
            return await _bussines.guardarRespuestaVariableRonda(respuesta, Transaction.Insert);
        }


        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaVariableRonda>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaVariableRonda>> EditarRespuesta(RespuestaVariableRonda respuesta)
        {
            return await _bussines.guardarRespuestaVariableRonda(respuesta, Transaction.Update);
        }
    }
}
