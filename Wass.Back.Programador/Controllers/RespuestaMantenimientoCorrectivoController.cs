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
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RespuestaMantenimientoCorrectivoController : ControllerBase
    {
        private readonly BORespuestaMantenimientoCorrectivo _bussines;

        public RespuestaMantenimientoCorrectivoController(ProgramadorContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORespuestaMantenimientoCorrectivo(dataBase);
        }


        [HttpGet]
        [Route("{idRespuestaMantenimientoCorrectivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CorrectivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CorrectivoRequest>> GetId(long idRespuestaMantenimientoCorrectivo)
        {
            return await _bussines.Get(idRespuestaMantenimientoCorrectivo);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CorrectivoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CorrectivoRequest>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CorrectivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CorrectivoRequest>> CrearRespuestaMantenimientoCorrectivo([FromBody] CorrectivoRequest correctivoRequest)
        {
            return await _bussines.GuardarRespuestaMantenimientoCorrectivo(correctivoRequest, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CorrectivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CorrectivoRequest>> EditarRespuestaMantenimientoCorrectivo([FromBody] CorrectivoRequest correctivoRequest)
        {
            return await _bussines.GuardarRespuestaMantenimientoCorrectivo(correctivoRequest, Transaction.Update);
        }


        [HttpGet]
        [Route("Diagnostico/{idDiagnostico}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CorrectivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CorrectivoRequest>>> GetPorDiagnostico(long idDiagnostico)
        {
            return await _bussines.GetPorDiagnostico(idDiagnostico);
        }

        [HttpGet]
        [Route("MantenimientoCorrectivo/{idMantenimientoCorrectivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CorrectivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CorrectivoRequest>>> GetPorMantenimientoCorrectivo(long idMantenimientoCorrectivo)
        {
            return await _bussines.GetPorMantenimientoCorrectivo(idMantenimientoCorrectivo);
        }
    }
}
