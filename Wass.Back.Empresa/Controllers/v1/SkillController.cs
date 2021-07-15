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
    public class SkillController : ControllerBase
    {
        private readonly BOSkill _bussines;

        public SkillController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOSkill(dataBase);
        }

        [HttpGet]
        [Route("{idSkill}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Skill>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Skill>> get(long idSkill)
        {
            return await _bussines.get(idSkill);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Skill>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Skill>>> getTodos()
        {
            return await _bussines.getTodos();
        }

        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Skill>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Skill>>> getTodosEmpresa(long idEmpresa)
        {
            return await _bussines.getTodosPorEmpresa(idEmpresa);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Skill>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Skill>> crear([FromBody] Skill skill)
        {
            return await _bussines.guardar(skill, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Skill>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Skill>> editar([FromBody] Skill skill)
        {
            return await _bussines.guardar(skill, Transaction.Update);
        }
    }
}
