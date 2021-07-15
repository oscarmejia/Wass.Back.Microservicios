using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wass.Back.Empresa.Kiwi.Bussines;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.DiagnosticoSkillsEmpresa;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiagnosticoSkillsEmpresaController : ControllerBase
    {
        private readonly BODiagnosticoSkillsEmpresa _bussines;
        private readonly IConfiguration _configuration;

        public DiagnosticoSkillsEmpresaController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BODiagnosticoSkillsEmpresa(dataBase);
        }

        /// <summary>
        /// Consulta una DiagnosticoSkillsEmpresa especifica
        /// </summary>
        /// <param name="idDiagnosticoSkillsEmpresa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idDiagnosticoSkillsEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<DiagnosticoSkillsEmpresaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<DiagnosticoSkillsEmpresaRequest>> Get(long idDiagnosticoSkillsEmpresa)
        {
            return await _bussines.GetAsync(idDiagnosticoSkillsEmpresa);
        }

        /// <summary>
        /// Consulta una DiagnosticoSkillsEmpresa por idSkill e idDiagnostico
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("skill/{idSkill}/diagnostico/{idDiagnostico}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<DiagnosticoSkillsEmpresaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<DiagnosticoSkillsEmpresaRequest>> GetPorSkillDiagnosticoAsync(long idSkill, long idDiagnostico)
        {
            return await _bussines.GetPorSkillDiagnosticoAsync(idSkill, idDiagnostico);
        }


        /// <summary>
        /// Consulta las DiagnosticoSkillsEmpresa que estan atadas a una cuadrilla
        /// </summary>
        /// <param name="idDiagnostico"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Diagnostico/{idDiagnostico}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>> GetPorDiagnosticoAsync(long idDiagnostico)
        {
            return await _bussines.GetPorDiagnosticoAsync(idDiagnostico);
        }

        /// <summary>
        /// Consulta DiagnosticoSkillsEmpresa por el Skill al que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Skill/{idSkill}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorSkillAsync(long idSkill)
        {
            var datos = await _bussines.GetPorSkillAsync(idSkill);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las DiagnosticoSkillsEmpresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>> getTodasAsync()
        {
            return await _bussines.GetAllAsync();
        }

        /// <summary>
        /// Crear un DiagnosticoSkillsEmpresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<DiagnosticoSkillsEmpresaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<DiagnosticoSkillsEmpresaRequest>> crear([FromBody] DiagnosticoSkillsEmpresaRequest diagnosticoSkillsEmpresa)
        {
            return await _bussines.SetAsync(diagnosticoSkillsEmpresa, Transaction.Insert);
        }

        /// <summary>
        /// Actualiza una DiagnosticoSkillsEmpresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<DiagnosticoSkillsEmpresaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<DiagnosticoSkillsEmpresaRequest>> actualizar([FromBody] DiagnosticoSkillsEmpresaRequest diagnosticoSkillsEmpresa)
        {
            return await _bussines.SetAsync(diagnosticoSkillsEmpresa, Transaction.Update);
        }
    }
}
