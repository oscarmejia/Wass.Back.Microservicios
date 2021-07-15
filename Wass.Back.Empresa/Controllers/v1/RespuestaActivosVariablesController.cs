using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wass.Back.Empresa.Kiwi.Bussines;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.RespuestaActivosVariables;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RespuestaActivosVariablesController : ControllerBase
    {
        private readonly BORespuestaActivosVariables _bussines;

        public RespuestaActivosVariablesController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORespuestaActivosVariables(dataBase);
        }


        [HttpGet]
        [Route("{idRespuestaActivosVariables}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaActivosVariablesResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaActivosVariablesResponse>> GetId(long idRespuestaActivosVariables)
        {
            return await _bussines.Get(idRespuestaActivosVariables);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RespuestaActivosVariablesResponse>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaActivosVariablesResponse>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaActivosVariablesResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaActivosVariablesResponse>> CrearRespuestaCuestionario([FromBody] RespuestaActivosVariablesResponse RespuestaCuestionario)
        {
            return await _bussines.GuardarRespuesta(RespuestaCuestionario, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaActivosVariablesResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RespuestaActivosVariablesResponse>> EditarRespuestaCuestionario([FromBody] RespuestaActivosVariablesResponse RespuestaCuestionario)
        {
            return await _bussines.GuardarRespuesta(RespuestaCuestionario, Transaction.Update);
        }

        [HttpGet]
        [Route("{idClasificacion}/{idCategorizacion}/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RespuestaActivosVariablesResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RespuestaActivosVariablesResponse>>> GetPorCategoriaClasificacionActivo(long idClasificacion, long idCategorizacion, Guid idActivo)
        {
            return await _bussines.GetPorCategoriaClasificacionActivo(idClasificacion, idCategorizacion, idActivo);
        }
    }
}
