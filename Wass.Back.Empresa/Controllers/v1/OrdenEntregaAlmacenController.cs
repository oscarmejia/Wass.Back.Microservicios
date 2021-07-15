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
using Wass.Back.Empresa.Models.Peticiones.v1.OrdenEntregaAlmacen;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdenEntregaAlmacenController : ControllerBase
    {
        private readonly BOOrdenEntregaAlmacen _bussines;

        public OrdenEntregaAlmacenController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOOrdenEntregaAlmacen(dataBase);
        }

        /// <summary>
        /// Consulta una Orden de Entrega en especifico
        /// </summary>
        /// <param name="idOrdenEntregaAlmacen"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idOrdenEntregaAlmacen}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenEntregaAlmacenRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idOrdenEntregaAlmacen)
        {
            var datos = await _bussines.GetPorId(idOrdenEntregaAlmacen);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta una Orden de Entrega por idOrdenTrabajo
        /// </summary>
        /// <param name="idOrdenTrabajo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("OrdenTrabajo/{idOrdenTrabajo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenEntregaAlmacenRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorIdOrdenTrabajo(long idOrdenTrabajo)
        {
            var datos = await _bussines.GetPorIdOrdenTrabajo(idOrdenTrabajo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las Ordenes de Entrega
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenEntregaAlmacenRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un registro de Orden de Entrega
        /// </summary>
        /// <param name="OrdenEntregaAlmacen"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenEntregaAlmacenRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearOrdenEntregaAlmacen([FromBody] OrdenEntregaAlmacenRequest datos)
        {
            var datos_actualizados = await _bussines.guardarOrdenEntregaAlmacen(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de una Orden de Entrega
        /// </summary>
        /// <param name="OrdenEntregaAlmacen"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenEntregaAlmacenRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<OrdenEntregaAlmacenRequest>> actualizarOrdenEntregaAlmacen([FromBody] OrdenEntregaAlmacenRequest datos)
        {
            return await _bussines.guardarOrdenEntregaAlmacen(datos, Transaction.Update);
        }

        /// <summary>
        /// Consulta el promedio de Ordenes de entrega de los jultimos 6 meses por Sede
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Promedio/{idRepuesto}/fecha/{fechaActual}/Sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenEntregaAlmacenPromedioRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPromedioMensual(long idRepuesto, DateTime fechaActual, long idSede)
        {
            var datos = await _bussines.GetPromedioUltimosSeisMeses(idRepuesto, fechaActual, idSede);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el promedio de Ordenes de entrega de los jultimos 6 meses por Almacen
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Promedio/{idRepuesto}/fecha/{fechaActual}/Almacen/{idAlmacen}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenEntregaAlmacenPromedioRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPromedioUltimosSeisMesesPorAlmacen(long idRepuesto, DateTime fechaActual, long idAlmacen)
        {
            var datos = await _bussines.GetPromedioUltimosSeisMesesPorAlmacen(idRepuesto, fechaActual, idAlmacen);
            return StatusCode(datos.codigo, datos);
        }

    }
}
