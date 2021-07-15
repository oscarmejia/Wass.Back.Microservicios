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
    public class ActivosPartesController : ControllerBase
    {
        private readonly BOActivosPartes _bussines;
        private readonly IConfiguration _configuration;

        public ActivosPartesController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOActivosPartes(dataBase);
        }

        /// <summary>
        /// Consulta  una parte especifica asociada a un activo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosPartes>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las partes creadas para todos los activos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las partes creadas para todos los activo segun su clasificación
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("clasificacion/{idClasificacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorClasificacionAsync(long idClasificacion)
        {
            var datos = await _bussines.GetPorClasificacionAsync(idClasificacion);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las partes creadas para todos los activo segun sus partes asocadas.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("subparte/{idParte}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorSubParteAsync(Guid idParte)
        {
            var datos = await _bussines.GetPorSubParteAsync(idParte);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// crea una parte para sociar a clasificación de un activo
        /// </summary>
        /// <param name="caracteristicas"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosPartes>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosPartes dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza una parte para sociar a clasificación de un activo
        /// </summary>
        /// <param name="caracteristicas"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosPartes>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] ActivosPartes dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Elimina de manera lógica una parte para sociar a clasificación de un activo
        /// </summary>
        /// <param name="Caracteristicas"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosPartes>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> eliminar([FromBody] ActivosPartes dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Delete);
            return StatusCode(datos.codigo, datos);
        }

    }
}
