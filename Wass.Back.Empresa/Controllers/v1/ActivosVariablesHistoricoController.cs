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
    public class ActivosVariablesHistoricoController : ControllerBase
    {
        private readonly BOActivosVariablesHistorico _bussines;
        private readonly IConfiguration _configuration;

        public ActivosVariablesHistoricoController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOActivosVariablesHistorico(dataBase);
        }

        /// <summary>
        /// Consulta en el historci el los cambios que ha tenido una variable en el historico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("{id}")]
        //[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosVariablesHistorico>>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> Get(long id)
        //{
        //    var datos = await _bussines.GetAsync(id);
        //    return StatusCode(datos.codigo, datos);
        //}
        /// <summary>
        /// Consulta todos el historico de la apliación
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("")]
        //[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosVariablesHistorico>>>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> getTodasAsync()
        //{
        //    var datos = await _bussines.GetAllAsync();
        //    return StatusCode(datos.codigo, datos);
        //}

        /// <summary>
        /// Consulta todos el historico de la apliación segun el el activo variable a consultar.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("variable/{idActivoClasificacionVariable}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetHistoricoPorIdVariableAsync(long idActivoClasificacionVariable)
        {
            var datos = await _bussines.GetHistoricoPorIdVariableAsync(idActivoClasificacionVariable);
            return StatusCode(datos.codigo, datos);
        }

    }
}
