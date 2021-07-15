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
using Wass.Back.Empresa.Models.Peticiones.v1.RepuestosAlmacen;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RepuestosAlmacenController : ControllerBase
    {
        private readonly BORepuestosAlmacen _bussines;

        public RepuestosAlmacenController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORepuestosAlmacen(dataBase);
        }

        /// <summary>
        /// Consulta un RepuestosAlmacen en especifico
        /// </summary>
        /// <param name="idRepuestosAlmacen"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idRepuestosAlmacen}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RepuestosAlmacen>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idRepuestosAlmacen)
        {
            var datos = await _bussines.GetPorId(idRepuestosAlmacen);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta un repuesto en un almacen en especifico
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RepuestoEnAlmacen/Almacen/{idAlmacen}/Repuesto/{idRepuesto}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RepuestosAlmacen>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RepuestosEnAlmacen(long idAlmacen, long idRepuesto)
        {
            var datos = await _bussines.RepuestosEnAlmacen(idAlmacen, idRepuesto);
            return StatusCode(datos.codigo, datos);
        }



        /// <summary>
        /// Consulta todos los RepuestosAlmacen
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosAlmacen>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los RepuestosAlmacen en donde su cantidad actual esta por debajo de la Optima
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("debajoCantidadMinima")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosAlmacen>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasDebajoMinima()
        {
            var datos = await _bussines.GetTodasDebajoMinima();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los RepuestosAlmacen en donde su cantidad actual esta por debajo de la Optima
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("debajoCantidadOptima")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosAlmacen>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasDebajoOptima()
        {
            var datos = await _bussines.GetTodasDebajoOptima();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Repuestos por idAlmacen
        /// </summary>
        /// <param name="idAlmacen"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RepuestosPorAlmacen/{idAlmacen}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosAlmacen>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRepuestosPorAlmacen(long idAlmacen)
        {
            var datos = await _bussines.GetRepuestosPorAlmacen(idAlmacen);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Almacenes por idRepuesto
        /// </summary>
        /// <param name="idRepuesto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("AlmacenesPorRepuesto/{idRepuesto}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosAlmacen>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAlmacenesPorRepuesto(long idRepuesto)
        {
            var datos = await _bussines.GetAlmacenesPorRepuesto(idRepuesto);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>

        /// Crea un RepuestosAlmacen
        /// </summary>
        /// <param name="RepuestosAlmacen"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RepuestosAlmacen>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearRepuestosAlmacen([FromBody] RepuestosAlmacen datos)
        {
            var datos_actualizados = await _bussines.guardarRepuestosAlmacen(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Consulta la cantidad de días que un Repuestos tarda en estar por debajo de su cantidad minima en el almacen
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DiasPorDebajoCantidadMinima/Repuesto/{idRepuesto}/fechaActual/{fechaActual}/Almacen/{idAlmacen}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RepuestosAlmacenRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCantidadDiasParaEstarPorDebajoCantidadMinima(long idRepuesto, DateTime fechaActual, long idAlmacen)
        {
            var datos = await _bussines.GetCantidadDiasParaEstarPorDebajoCantidadMinima(idRepuesto, fechaActual, idAlmacen);
            return StatusCode(datos.codigo, datos);
        }

    }
}
