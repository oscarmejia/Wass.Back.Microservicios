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
using Wass.Back.Empresa.Models.Peticiones.v1.CuadrillaSkillsEmpresa;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CuadrillaSkillsEmpresaController : ControllerBase
    {
        private readonly BOCuadrillaSkillsEmpresa _bussines;
        private readonly IConfiguration _configuration;

        public CuadrillaSkillsEmpresaController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOCuadrillaSkillsEmpresa(dataBase);
        }

        /// <summary>
        /// Consulta una CuadrillaSkillsEmpresa especifica
        /// </summary>
        /// <param name="idCuadrillaSkillsEmpresa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCuadrillaSkillsEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillaSkillsEmpresaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuadrillaSkillsEmpresaRequest>> Get(long idCuadrillaSkillsEmpresa)
        {
            return await _bussines.GetAsync(idCuadrillaSkillsEmpresa);
        }


        /// <summary>
        /// Consulta una CuadrillaSkillsEmpresa por idSkill e idCuadrilla
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("skill/{idSkill}/cuadrilla/{idCuadrilla}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillaSkillsEmpresaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuadrillaSkillsEmpresaRequest>> GetPorSkillsCuadrillaAsync(long idSkill, long idCuadrilla)
        {
            return await _bussines.GetPorSkillsCuadrillaAsync(idSkill, idCuadrilla);
        }

        /// <summary>
        /// Consulta las CuadrillaSkillsEmpresa que estan atadas a una cuadrilla
        /// </summary>
        /// <param name="idCuadrilla"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Cuadrilla/{idCuadrilla}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaSkillsEmpresaRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuadrillaSkillsEmpresaRequest>>> GetPorCuadrillaAsync(long idCuadrilla)
        {
            return await _bussines.GetPorCuadrillaAsync(idCuadrilla);
        }

        /// <summary>
        /// Consulta CuadrillaSkillsEmpresa por el Skill al que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Skill/{idSkill}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaSkillsEmpresaRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorSkillAsync(long idSkill)
        {
            var datos = await _bussines.GetPorSkillAsync(idSkill);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las CuadrillaSkillsEmpresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaSkillsEmpresaRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuadrillaSkillsEmpresaRequest>>> getTodasAsync()
        {
            return await _bussines.GetAllAsync();
        }

        /// <summary>
        /// Crear una CuadrillaSkillsEmpresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillaSkillsEmpresaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuadrillaSkillsEmpresaRequest>> crear([FromBody] CuadrillaSkillsEmpresaRequest cuadrillaSkillsEmpresa)
        {
            return await _bussines.SetAsync(cuadrillaSkillsEmpresa, Transaction.Insert);
        }

        /// <summary>
        /// Actualiza una CuadrillaSkillsEmpresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillaSkillsEmpresaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuadrillaSkillsEmpresaRequest>> actualizar([FromBody] CuadrillaSkillsEmpresaRequest cuadrillaSkillsEmpresa)
        {
            return await _bussines.SetAsync(cuadrillaSkillsEmpresa, Transaction.Update);
        }
    }
}
