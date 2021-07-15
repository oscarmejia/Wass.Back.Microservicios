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
using Wass.Back.Empresa.Models.Peticiones.v1.RecepcionRepuestos;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RecepcionRepuestosController : ControllerBase
    {
        private readonly BORecepcionRepuestos _bussines;

        public RecepcionRepuestosController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORecepcionRepuestos(dataBase);
        }

        /// <summary>
        /// Consulta una Recepcion de Repuestos
        /// </summary>
        /// <param name="idRecepcionRepuestos"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idRecepcionRepuestos}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RecepcionRepuestosRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idRecepcionRepuestos)
        {
            var datos = await _bussines.GetPorId(idRecepcionRepuestos);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las Recepciones de Repuestos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RecepcionRepuestosRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un registro de Recepcion de Repuestos
        /// </summary>
        /// <param name="RecepcionRepuestos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RecepcionRepuestosRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearRecepcionRepuestos([FromBody] RecepcionRepuestosRequest datos)
        {
            var datos_actualizados = await _bussines.guardarRecepcionRepuestos(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de una Recepcion de Repuestos
        /// </summary>
        /// <param name="OrdenEntregaAlmacen"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RecepcionRepuestosRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RecepcionRepuestosRequest>> actualizarOrdenEntregaAlmacen([FromBody] RecepcionRepuestosRequest datos)
        {
            return await _bussines.guardarRecepcionRepuestos(datos, Transaction.Update);
        }

        /// <summary>
        /// Consulta el promedio de costo unitario de un repuesto en la Recepcion de Repuestos
        /// </summary>
        /// <param name="idRepuesto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PromedioCostoUnitario/Repuesto/{idRepuesto}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RecepcionRepuestosRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPromedioUltimosSeisMesesCostoUnitario(long idRepuesto)
        {
            var datos = await _bussines.GetPromedioUltimosSeisMesesCostoUnitario(idRepuesto);
            return StatusCode(datos.codigo, datos);
        }
    }
}
