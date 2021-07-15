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
using Wass.Back.Empresa.Models.Peticiones.v1.Preguntas;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        private readonly BOPreguntas _bussines;

        public PreguntasController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOPreguntas(dataBase);
        }


        [HttpGet]
        [Route("{idPregunta}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PreguntasRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<PreguntasRequest>> GetId(long idPregunta)
        {
            return await _bussines.Get(idPregunta);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PreguntasRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<PreguntasRequest>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PreguntasRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<PreguntasRequest>>> GetTodasPorEmpresa(long idEmpresa)
        {
            return await _bussines.GetPorEmpresa(idEmpresa);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PreguntasRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<PreguntasRequest>> Guardar([FromBody] PreguntasRequest preguntas)
        {
            return await _bussines.guardarPregunta(preguntas, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PreguntasRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<PreguntasRequest>> Actualizar([FromBody] PreguntasRequest preguntas)
        {
            return await _bussines.guardarPregunta(preguntas, Transaction.Update);
        }

        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Preguntas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Preguntas>> Eliminar([FromBody] Preguntas preguntas)
        {
            return await _bussines.Eliminar(preguntas);
        }


    }
}
