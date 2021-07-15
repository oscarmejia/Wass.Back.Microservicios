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
    public class CuestionarioPreguntasController : ControllerBase
    {
        private readonly BOCuestionarioPreguntas _bussines;

        public CuestionarioPreguntasController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOCuestionarioPreguntas(dataBase);
        }

        [HttpGet]
        [Route("{idCuestionarioPreguntas}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuestionarioPreguntas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuestionarioPreguntas>> GetId(long idCuestionarioPreguntas)
        {
            return await _bussines.Get(idCuestionarioPreguntas);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuestionarioPreguntas>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuestionarioPreguntas>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpGet]
        [Route("cuestionario/{idCuestionario}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuestionarioPreguntas>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuestionarioPreguntas>>> GetTodasPorCuestionario(long idCuestionario)
        {
            return await _bussines.GetTodasPorCuestionario(idCuestionario);
        }

        [HttpGet]
        [Route("preguntas/{idPregunta}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuestionarioPreguntas>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuestionarioPreguntas>>> GetTodasPorPregunta(long idPregunta)
        {
            return await _bussines.GetTodasCuestionarioPorPregunta(idPregunta);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuestionarioPreguntas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuestionarioPreguntas>> CrearCuestionarioPreguntas([FromBody] CuestionarioPreguntas cuestionarioPreguntas)
        {
            return await _bussines.Set(cuestionarioPreguntas, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuestionarioPreguntas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuestionarioPreguntas>> EditarCuestionarioPreguntas([FromBody] CuestionarioPreguntas cuestionarioPreguntas)
        {
            return await _bussines.Set(cuestionarioPreguntas, Transaction.Update);
        }


        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuestionarioPreguntas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuestionarioPreguntas>> EliminarCuestionarioPreguntas([FromBody] CuestionarioPreguntas cuestionarioPreguntas)
        {
            return await _bussines.Eliminar(cuestionarioPreguntas);
        }

    }
}
