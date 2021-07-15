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
    public class MantenimientoAvisoController : ControllerBase
    {
        private readonly BOMantenimientoAviso _BO;
        private readonly IConfiguration _configuration;

        public MantenimientoAvisoController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _BO = new BOMantenimientoAviso(dataBase);
        }

        /// <summary>
        /// Consulta una orden de aviso especifica con todos sus detalles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MantenimientoAviso>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(long id)
        {
            var datos = await _BO.Get(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta una orden de aviso asociada a un orden de trabajo especifica con todos sus detalles
        /// </summary>
        /// <param name="idOrden">Orden que esta asociada a aviso de mantenimieto</param>
        /// <returns></returns>
        [HttpGet]
        [Route("orden/{idOrden}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MantenimientoAviso>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorOrdenAsync(long idOrden)
        {
            var datos = await _BO.GetPorOrdenAsync(idOrden);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el 100% de las orden de aviso creadas en la plataforma (puede tubar el microservicio)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<MantenimientoAviso>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasAsync()
        {
            var datos = await _BO.GetAll();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el 100% de las orden de aviso creadas en la plataforma (puede tubar el microservicio)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("condicion/{idCondicion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<MantenimientoAviso>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasPorCondicionAsync(long idCondicion)
        {
            var datos = await _BO.GetAllPorCondicion(idCondicion);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Crear un aviso de mantenimiento.
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MantenimientoAviso>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Crear([FromBody]  MantenimientoAviso dato)
        {
            var datos = await _BO.Set(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza un aviso de mantenimiento.
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MantenimientoAviso>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Actualizar([FromBody]  MantenimientoAviso dato)
        {
            var datos = await _BO.Set(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

    }
}