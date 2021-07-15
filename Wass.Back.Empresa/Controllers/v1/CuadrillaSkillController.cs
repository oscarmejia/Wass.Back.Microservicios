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
    public class CuadrillaSkillController : ControllerBase
    {
        private readonly BOCuadrillaSkill _bussines;
        public CuadrillaSkillController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOCuadrillaSkill(dataBase);
        }

        [HttpGet]
        [Route("{idCuadrillaSkill}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillaSkill>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuadrillaSkill>> get(long idCuadrillaSkill)
        {
            return await _bussines.get(idCuadrillaSkill);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaSkill>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuadrillaSkill>>> getTodos()
        {
            return await _bussines.getTodos();
        }

        [HttpGet]
        [Route("cuadrilla/{idCuadrilla}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaSkill>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuadrillaSkill>>> getTodosporCuadrilla(long idCuadrilla)
        {
            return await _bussines.getTodospoCuadrilla(idCuadrilla);
        }

        [HttpGet]
        [Route("skill/{idSkill}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaSkill>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuadrillaSkill>>> getTodosporSkill(long idSkill)
        {
            return await _bussines.getTodosporSkill(idSkill);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaSkill>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuadrillaSkill>> crear([FromBody] CuadrillaSkill cuadrillaSkill)
        {
            return await _bussines.guardar(cuadrillaSkill, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaSkill>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuadrillaSkill>> editar([FromBody] CuadrillaSkill cuadrillaSkill)
        {
            return await _bussines.guardar(cuadrillaSkill, Transaction.Update);
        }

    }
}
