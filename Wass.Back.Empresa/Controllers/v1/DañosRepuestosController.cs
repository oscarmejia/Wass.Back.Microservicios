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
    public class DañosRepuestosController : ControllerBase
    {
        private readonly BODañosRepuestos _bussines;

        public DañosRepuestosController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BODañosRepuestos(dataBase);
        }

        /// <summary>
        /// Consulta un Daño en Repuesto en especifico
        /// </summary>
        /// <param name="idDañosRepuestos"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idDañosRepuestos}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<DañosRepuestos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idDañosRepuestos)
        {
            var datos = await _bussines.GetPorId(idDañosRepuestos);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Daños en Repuestos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<DañosRepuestos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un registro de Daño en Repuesto
        /// </summary>
        /// <param name="DañosRepuestos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<DañosRepuestos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearDañosRepuestos([FromBody] DañosRepuestos datos)
        {
            var datos_actualizados = await _bussines.guardarDañosRepuestos(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de un Daño en Repuesto
        /// </summary>
        /// <param name="DañosRepuestos"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<DañosRepuestos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<DañosRepuestos>> actualizarDañosRepuestos([FromBody] DañosRepuestos datos)
        {
            return await _bussines.guardarDañosRepuestos(datos, Transaction.Update);
        }
    }
}
