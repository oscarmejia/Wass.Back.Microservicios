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
using Wass.Back.Programador.Models.Peticiones.RespuestaSolicitudPedido;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RespuestaSolicitudPedidoController : ControllerBase
    {
        private readonly BORespuestaSolicitudPedido _bussines;
        private readonly IConfiguration _configuration;

        public RespuestaSolicitudPedidoController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BORespuestaSolicitudPedido(dataBase);
        }


        [HttpGet]
        [Route("{idRespuestaSolicitudPedido}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaSolicitudPedidoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaSolicitudPedidoRequest>> Get(long idRespuestaSolicitudPedido)
        {
            return await _bussines.GetPorId(idRespuestaSolicitudPedido);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaSolicitudPedidoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaSolicitudPedidoRequest>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaSolicitudPedidoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaSolicitudPedidoRequest>> CrearRespuestaSolicitudPedido([FromBody] RespuestaSolicitudPedidoRequest respuestaSolicitudPedido)
        {
            return await _bussines.guardarRespuestaSolicitudPedido(respuestaSolicitudPedido, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaSolicitudPedidoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaSolicitudPedidoRequest>> GuardarRespuestaSolicitudPedido([FromBody] RespuestaSolicitudPedidoRequest RespuestaCuestionario)
        {
            return await _bussines.guardarRespuestaSolicitudPedido(RespuestaCuestionario, Transaction.Update);
        }
    }
}
