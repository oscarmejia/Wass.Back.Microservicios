using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Bussines;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Agenda;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly BOAgenda _BO;
        private readonly IConfiguration _configuration;

        public AgendaController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _BO = new BOAgenda(dataBase);
        }

        /// <summary>
        /// Consulta agenda por Id
        /// </summary>
        /// <param name="idAgenda"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idAgenda}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Agenda>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(long idAgenda)
        {
            var datos = await _BO.Get(idAgenda);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consultar tipos de recursos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("recursos/tipos")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<(int, string)>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTipos()
        {
            var datos = await _BO.GetTipos();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta agenda de un recurso por Id
        /// </summary>
        /// <param name="idRecurso"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("recurso/{idRecurso}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Agenda>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIdRecurso(long idRecurso)
        {
            var datos = await _BO.GetIdRecurso(idRecurso, DateTime.Now, DateTime.Now);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta turnos de un recurso por Id
        /// </summary>
        /// <param name="idRecurso"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("recurso/turnos/{idRecurso}/{tipo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Turnos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIdRecursoTurnos(long idRecurso, int tipo = 1)
        {
            var datos = await _BO.GetRecursoTurnos(idRecurso, tipo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta agenda de un recurso por Id y rangos
        /// </summary>
        /// <param name="rango"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recurso/rango")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Agenda>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIdRecursoRango([FromBody] AgendaRango rango)
        {
            var datos = await _BO.GetIdRecurso(rango.idRecurso, rango.fechaInicial, rango.fechaFinal, true);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta agenda de un recurso por Orden de trabajo
        /// </summary>
        /// <param name="idOrden"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("orden/{idOrden}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Agenda>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIdOrdenTrabajo(long idOrden)
        {
            var datos = await _BO.GetIdOrdenTrabajo(idOrden);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Agregar una agenda
        /// </summary>
        /// <param name="agenda"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("asignar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Agenda>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Set([FromBody] Agenda agenda)
        {
            var datos = await _BO.Set(agenda, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Agregar una agenda
        /// </summary>
        /// <param name="agenda"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("cancelar/{idAgenda}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Agenda>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CancelAgenda(long idAgenda)
        {
            var datos = await _BO.CancelAgenda(idAgenda);
            return StatusCode(datos.codigo, datos);
        }

        ///// <summary>
        ///// Consulta agenda de un recurso por Id y fecha
        ///// </summary>
        ///// <param name="idRecurso"></param>
        ///// <param name="fecha"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("recurso/{idRecurso}/{fecha}")]
        //[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Agenda>>>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetDisponibilidadIdRecurso(long idRecurso, DateTime fecha)
        //{
        //    var datos = await _BO.GetDisponibilidadIdRecurso(idRecurso, fecha);
        //    return StatusCode(datos.codigo, datos);
        //}

        ///// <summary>
        ///// Consultar agenda de un recurso por fecha y horas de inicio y fin
        ///// </summary>
        ///// <param name="idRecurso"></param>
        ///// <param name="fecha"></param>
        ///// <param name="hinicial"></param>
        ///// <param name="hfinal"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("recurso/{idRecurso}/{fecha}/{hinicial}/{hfinal}")]
        //[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Agenda>>>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetDisponibilidadHoras(long idRecurso, DateTime fecha, string hinicial, string hfinal)
        //{
        //    var datos = await _BO.GetDisponibilidadHoras(idRecurso, fecha, hinicial, hfinal);
        //    return StatusCode(datos.codigo, datos);
        //}
    }
}