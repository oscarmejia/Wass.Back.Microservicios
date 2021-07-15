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
    public class ActivosUbicacionController : ControllerBase
    {
        private readonly BOActivosUbicacion _bussines;
        private readonly IConfiguration _configuration;

        public ActivosUbicacionController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOActivosUbicacion(dataBase);
        }

        /// <summary>
        /// Consulta la ubiación especifica de un activo por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosUbicacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las ubiaciones registradas
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosUbicacion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta las ubicacion registradas de un activo equipo 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("equipo/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosUbicacion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorEquipoAsync(Guid id)
        {
            var datos = await _bussines.GetPorEquipoAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta las ubicacion registradas de un activo flota 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("flota/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosUbicacion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorFlotaAsync(Guid id)
        {
            var datos = await _bussines.GetPorFlotaAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta las ubicacion registradas de un activo equipo por si tipo de ubicación
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("equipo/{id}/ubicacion/{ubicacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosUbicacion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorEquipoUbicacionAsync(Guid id, TiposUbicacion ubicacion)
        {
            var datos = await _bussines.GetPorEquipoUbicacionAsync(id, ubicacion);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta las ubicacion registradas de un activo equipo por si tipo de ubicación
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("flota/{id}/ubicacion/{ubicacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosUbicacion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorFlotaUbicacionAsync(Guid id, TiposUbicacion ubicacion)
        {
            var datos = await _bussines.GetPorFlotaUbicacionAsync(id, ubicacion);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Asocia la un activo su ubicación
        /// </summary>
        /// <param name="Activos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosUbicacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosUbicacion dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza un Activo Equipos
        /// </summary>
        /// <param name="Equipos"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosUbicacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] ActivosUbicacion dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Elimina de manera lógica activo Equipos
        /// </summary>
        /// <param name="Equipo"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosUbicacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> eliminar([FromBody] ActivosUbicacion dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Delete);
            return StatusCode(datos.codigo, datos);
        }
    }
}
