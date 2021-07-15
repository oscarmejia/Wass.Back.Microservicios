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
using Wass.Back.Programador.Models.Peticiones.SkillLicitacion;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SkillLicitacionController : ControllerBase
    {

        private readonly BOSkillLicitacion _bussines;
        private readonly IConfiguration _configuration;

        public SkillLicitacionController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOSkillLicitacion(dataBase);
        }


        [HttpGet]
        [Route("{idSkillLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SkillResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<SkillResponse>> Get(long idSkillLicitacion)
        {
            return await _bussines.Get(idSkillLicitacion);
        }

        [HttpGet]
        [Route("licitacion/{idLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SkillResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<SkillResponse>> GetPorLicitacion(long idLicitacion)
        {
            return await _bussines.GetPorLicitacion(idLicitacion);
        }


        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<SkillResponse>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<SkillResponse>>> GetTodas(long idSkillLicitacion)
        {
            return await _bussines.GetTodas();
        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SkillResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<SkillResponse>> Crear([FromBody] SkillRequest skill)
        {
            return await _bussines.GuardarSkills(skill, Transaction.Insert);
        }


        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SkillResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<SkillResponse>> Actulizar([FromBody] SkillRequest skill)
        {
            return await _bussines.GuardarSkills(skill, Transaction.Update);
        }
    }
}
