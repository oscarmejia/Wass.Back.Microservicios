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
    public class HerramientasController : ControllerBase
    {

        private readonly BOHerramientas _bussines;


        public HerramientasController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOHerramientas(dataBase);
        }

        [HttpGet]
        [Route("{idHerramienta}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Herramientas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Herramientas>> GetPorId(long idHerramienta)
        {
            return await _bussines.GetPorId(idHerramienta);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Herramientas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Herramientas>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpGet]
        [Route("sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Herramientas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Herramientas>>> GetTodasPorSede(long idSede)
        {
            return await _bussines.GetTodasPorSede(idSede);
        }

        /// <summary>
        /// Consulta Herramientas por la Empresa a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cuadrillas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorEmpresaAsync(long idEmpresa)
        {
            var datos = await _bussines.GetPorEmpresaAsync(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Herramientas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Herramientas>> CrearHerramienta([FromBody] Herramientas herramienta)
        {
            return await _bussines.SaveHerramienta(herramienta, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Herramientas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Herramientas>> EditarHerramienta([FromBody] Herramientas herramienta)
        {
            return await _bussines.SaveHerramienta(herramienta, Transaction.Update);
        }

        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Herramientas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Herramientas>> EliminarHerramienta([FromBody] Herramientas herramienta)
        {
            return await _bussines.EliminarHerramienta(herramienta);
        }
    }
}
