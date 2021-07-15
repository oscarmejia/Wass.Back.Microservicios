using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class ActivosAdquisicionController : ControllerBase
    {
        private readonly BOActivosAdquisicion _bussines;
        private readonly IConfiguration _configuration;

        public ActivosAdquisicionController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOActivosAdquisicion(dataBase);
        }

        /// <summary>
        /// Consulta un Activo adquisicion Equipo por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosAdquisicion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta activos adquisicion
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosAdquisicion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta activos adquisicion para activos de tipo Equipos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("equipos/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosAdquisicion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorEquiposAsync(Guid id)
        {
            var datos = await _bussines.GetPorEquiposAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta activos adquisicion para activos de tipo Equipos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("flotas/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosAdquisicion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorFlotasAsync(Guid id)
        {
            var datos = await _bussines.GetPorFlotaAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crear un activo adquisicion
        /// </summary>
        /// <param name="ActivosAdquisicion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosAdquisicion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosAdquisicion dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza un Activo Adquisicion
        /// </summary>
        /// <param name="ActivoAdquisicion"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosAdquisicion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] ActivosAdquisicion dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Elimina de manera lógica activo Adquisicion
        /// </summary>
        /// <param name="ActivoAdquisicion"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosAdquisicion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> eliminar([FromBody] ActivosAdquisicion dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Delete);
            return StatusCode(datos.codigo, datos);
        }
    }
}