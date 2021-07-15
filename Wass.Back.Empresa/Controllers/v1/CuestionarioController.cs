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
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CuestionarioController : ControllerBase
    {

        private readonly BOCuestionario _bussines;

        public CuestionarioController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOCuestionario(dataBase);
        }


        [HttpGet]
        [Route("{idCuestionario}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cuestionario>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Cuestionario>> GetId(long idCuestionario)
        {
            return await _bussines.Get(idCuestionario);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cuestionario>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cuestionario>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cuestionario>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cuestionario>>> GetTodasPorEmpresa(long idEmpresa)
        {
            return await _bussines.GetTodasPorEmpresa(idEmpresa);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cuestionario>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Cuestionario>> CrearCuestionario([FromBody] Cuestionario cuestionario)
        {
            return await _bussines.GuardarCuestionario(cuestionario, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cuestionario>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Cuestionario>> EditarCuestionario([FromBody] Cuestionario cuestionario)
        {
            return await _bussines.GuardarCuestionario(cuestionario, Transaction.Update);
        }


        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cuestionario>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Cuestionario>> EliminarCuestionario([FromBody] Cuestionario cuestionario)
        {
            return await _bussines.Eliminar(cuestionario);
        }
    }
}
