using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    public class ActivosClasificacionAccionesController : ControllerBase
    {
        private readonly BOActivosClasificacionAcciones _bussines;

        public ActivosClasificacionAccionesController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOActivosClasificacionAcciones(dataBase);
        }

        /// <summary>
        /// Consulta una accion que esta asociada en una categorización 
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idAccion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionAcciones>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> get(long idAccion)
        {
            var datos = await _bussines.GetAsync(idAccion);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta las acciones que estan asociadas segun una clasifiacion/sub-clasificacion
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("clasifiacion/{idClasificacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosClasificacionAcciones>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorClasifiacionAsync(long idClasificacion)
        {
            var datos = await _bussines.GetPorClasifiacionAsync(idClasificacion);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta las acciones que estan asociadas a todas las calsifiaciones
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosClasificacionAcciones>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea una accion a una clasifiacion ya registrada
        /// </summary>
        /// <param name="Flota"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionAcciones>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosClasificacionAcciones datos)
        {
            var datos_actualizados = await _bussines.SetAsync(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// CActualiza una clasifiacion especifica.
        /// </summary>
        /// <param name="Flota"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionAcciones>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ActivosClasificacionAcciones>> actualizar([FromBody] ActivosClasificacionAcciones datos)
        {
            return await _bussines.SetAsync(datos, Transaction.Update);
        }

        /// <summary>
        /// Elimina de manera lógica un activo Flota
        /// </summary>
        /// <param name="Flota"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionAcciones>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ActivosClasificacionAcciones>> eliminar([FromBody] ActivosClasificacionAcciones datos)
        {
            return await _bussines.SetAsync(datos, Transaction.Delete);
        }

    }
}