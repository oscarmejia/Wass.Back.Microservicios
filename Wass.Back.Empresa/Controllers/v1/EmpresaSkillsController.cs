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
using Wass.Back.Empresa.Models.Peticiones.v1.Skill;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmpresaSkillsController : ControllerBase
    {
        private readonly BOEmpresaSkills _bussines;

        public EmpresaSkillsController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOEmpresaSkills(dataBase);
        }

        /// <summary>
        /// Consulta la habilidad de manera directa sin importar a cual empresa pertenece
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("habilidad/{idHabilidades}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseSkills>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idHabilidades)
        {
            var datos = await _bussines.GetAsync(idHabilidades);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta las habilidades que una empresa a registrado
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseSkills>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorEmpresaAsync(long idEmpresa)
        {
            var datos = await _bussines.GetPorEmpresaAsync(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las habilidades que se han registrado en el sistema (clientes y proveedores)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("todas")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ResponseSkills>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Todas()
        {
            var datos = await _bussines.GetTodasAsync();
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Registra habilidades a la empesa seleccionada
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseSkills>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] RequestSkills _datos)
        {
            var datos = await _bussines.SetAsync(_datos, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Actualiza los datos de una empresa
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseSkills>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] RequestSkills _datos)
        {
            var datos = await _bussines.SetAsync(_datos, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }


    }
}
