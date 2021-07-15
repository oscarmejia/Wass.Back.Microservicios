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
    public class RepuestosController : ControllerBase
    {
        private readonly BORepuestos _bussines;

        public RepuestosController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORepuestos(dataBase);
        }

        /// <summary>
        /// Consulta un Repuesto en especifico
        /// </summary>
        /// <param name="idRepuesto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idRepuesto}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Almacen>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idRepuesto)
        {
            var datos = await _bussines.GetPorId(idRepuesto);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Repuestos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Repuestos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Repuestos por Categoria
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("categoria/{idCategoria}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Repuestos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodoPorCategoria(long idCategoria)
        {
            var datos = await _bussines.GetTodasPorCategoria(idCategoria);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Repuestos por Clasificacion
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Clasificacion/{idClasificacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Repuestos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodoPorClasificacion(long idClasificacion)
        {
            var datos = await _bussines.GetTodasPorClasificacion(idClasificacion);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un Repuesto
        /// </summary>
        /// <param name="Repuestos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Repuestos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearRepuestos([FromBody] Repuestos datos)
        {
            var datos_actualizados = await _bussines.guardarRepuestos(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de un Repuesto
        /// </summary>
        /// <param name="Repuesto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Repuestos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Repuestos>> actualizarRepuesto([FromBody] Repuestos datos)
        {
            return await _bussines.guardarRepuestos(datos, Transaction.Update);
        }


    }
}
