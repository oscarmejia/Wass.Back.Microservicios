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
    public class EmpresaChecksController : ControllerBase
    {
        private readonly BOEmpresaChecks _bussines;

        public EmpresaChecksController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOEmpresaChecks(dataBase);
        }

        /// <summary>
        /// Consultar check por id
        /// </summary>
        /// <param name="idCheck"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCheck}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<EmpresaChecks>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCheckPorId(long idCheck)
        {
            var datos = await _bussines.GetAsync(idCheck);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta checks que una empresa a registrado
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<EmpresaChecks>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCheckPorEmpresaAsync(long idEmpresa)
        {
            var datos = await _bussines.GetPorEmpresaAsync(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los checks que se han registrado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<EmpresaChecks>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TodosChecks()
        {
            var datos = await _bussines.GetTodasAsync();
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Registra checks a la empesa seleccionada
        /// </summary>
        /// <param name="_datos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<EmpresaChecks>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CrearChecks([FromBody] List<EmpresaChecks> _datos)
        {
            var datos = await _bussines.SetAsync(_datos, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualizar checks a la empesa seleccionada
        /// </summary>
        /// <param name="_datos"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<EmpresaChecks>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActualizarChecks([FromBody] List<EmpresaChecks> _datos)
        {
            var datos = await _bussines.SetAsync(_datos, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Activar/inactivar checks a la empesa seleccionada
        /// </summary>
        /// <param name="_datos"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{idCheck}/{estado}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<EmpresaChecks>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActivarInactivarChecks(long idCheck, bool estado)
        {
            var datos = await _bussines.ActivarInactivarCheckAsync(idCheck, estado);
            return StatusCode(datos.codigo, datos);
        }
    }
}
