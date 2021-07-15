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
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MantenimientoPreventivoController : ControllerBase
    {
        private readonly BOMantenimientoPreventivo _BO;
        private readonly IConfiguration _configuration;

        public MantenimientoPreventivoController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _BO = new BOMantenimientoPreventivo(dataBase);
        }

        /// <summary>
        /// Consulta una orden de mantenimiento preventivo
        /// </summary>
        /// <param name="idMantenimientoPreventivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idMantenimientoPreventivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MantenimientoPreventivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(long idMantenimientoPreventivo)
        {
            var datos = await _BO.Get(idMantenimientoPreventivo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta una orden de mantenimiento preventivo
        /// </summary>
        /// <param name="idOrden"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("orden/{idOrden}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MantenimientoPreventivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorOrdenAsync(long idOrden)
        {
            var datos = await _BO.GetPorOrdenAsync(idOrden);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el 100% de las orden de trabajos de mantenimientos correpreventivoctivos creadas en la plataforma (puede tubar el microservicio)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<MantenimientoPreventivoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasAsync()
        {
            var datos = await _BO.GetAll();
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Consulta el 100% de las orden de trabajos de mantenimientos correpreventivoctivos creadas en la plataforma (puede tubar el microservicio)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("plan/{idPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<MantenimientoPreventivoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasPorPlanAsync(long idPlan)
        {
            var datos = await _BO.GetAllPorPlan(idPlan);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Crea un mantenimiento preventivo
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MantenimientoPreventivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Crear([FromBody] MantenimientoPreventivoRequest dato)
        {
            var datos = await _BO.Set(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }
        /// <summary>
        /// Actualiza un mantenimiento preventivo asociandolo a una orden de aviso de mantenimiento
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenesTrabajo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Actualizar([FromBody] MantenimientoPreventivoRequest dato)
        {
            var datos = await _BO.Set(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }
    }
}
