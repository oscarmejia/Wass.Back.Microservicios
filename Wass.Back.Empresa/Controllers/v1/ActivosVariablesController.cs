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
    public class ActivosVariablesController : ControllerBase
    {
        private readonly BOActivosVariables _bussines;
        private readonly BOActivosVariablesHistorico _bussinesHistorial;
        private readonly IConfiguration _configuration;

        public ActivosVariablesController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOActivosVariables(dataBase);
            _bussinesHistorial = new BOActivosVariablesHistorico(dataBase);
        }

        /// <summary>
        /// Consulta un valor de una variable en especifico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosVariables>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(long id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta un valor de una variable en especifico segun su clasificacion variable.
        /// </summary>
        /// <param name="idActivoClasificacionVariable"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("valor/{idActivoClasificacionVariable}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosVariables>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorCalsificacionActivoAsync(long idActivoClasificacionVariable)
        {
            var datos = await _bussines.GetAsync(idActivoClasificacionVariable);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los valores de las variables asociadas a las clasificaciones (puede ser un alto volumen de datos).
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los valores de las variables asociadas a las clasificaciones (puede ser un alto volumen de datos) para los activos de tipo flota.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("flota/{idActivoFlota}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorFlotaAsync(Guid idActivoFlota)
        {
            var datos = await _bussines.GetPorFlotaAsync(idActivoFlota);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los valores de las variables asociadas a las clasificaciones (puede ser un alto volumen de datos) para los activos de tipo flota.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("equipo/{idActivoEquipo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorEquipoAsync(Guid idActivoEquipo)
        {
            var datos = await _bussines.GetPorEquipoAsync(idActivoEquipo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los valores de las variables asociadas a un equipo ordenadas por fecha ultimos 3 meses
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("variablesFechaEquipos/{idActivoEquipo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorOrdenFechaEquipoAsync(Guid idActivoEquipo)
        {
            var datos = await _bussines.GetPorOrdenFechaEquipoAsync(idActivoEquipo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los valores de las variables asociadas a una flota ordenadas por fecha ultimos 3 meses
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("variablesFechaFlotas/{idActivoFlota}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorOrdenFechaFlotaAsync(Guid idActivoFlota)
        {
            var datos = await _bussines.GetPorOrdenFechaFlotaAsync(idActivoFlota);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los ultimos valores de las variables asociadas a un equipo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("variablesUltimoEquipo/{idActivoEquipo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorUltimoEquipoAsync(Guid idActivoEquipo)
        {
            var datos = await _bussines.GetPorUltimoEquipoAsync(idActivoEquipo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los ultimos valores de las variables asociadas a una flota
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("variablesUltimoFlota/{idActivoFlota}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorUltimoFlotaAsync(Guid idActivoFlota)
        {
            var datos = await _bussines.GetPorUltimoFlotaAsync(idActivoFlota);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crear un valor a una variable este valor es unico por cada asociación a un activo.
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosVariables>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosVariables dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza un valor a una variable este valor es unico por cada asociación a un activo.
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosVariables>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] ActivosVariables dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Elimina de manera lógica un valor a una variable este valor es unico por cada asociación a un activo.
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosVariables>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> eliminar([FromBody] ActivosVariables dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Delete);
            return StatusCode(datos.codigo, datos);
        }
    }
}
