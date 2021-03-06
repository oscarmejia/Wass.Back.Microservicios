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
    public class AjustesAlmacenesController : ControllerBase
    {
        private readonly BOAjustesAlmacenes _bussines;

        public AjustesAlmacenesController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOAjustesAlmacenes(dataBase);
        }

        /// <summary>
        /// Consulta un Ajuste de Almacen en especifico
        /// </summary>
        /// <param name="idAjustesAlmacenes"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idRepuestosAlmacen}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<AjustesAlmacenes>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idAjustesAlmacenes)
        {
            var datos = await _bussines.GetPorId(idAjustesAlmacenes);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Ajustes de Almacenes
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<AjustesAlmacenes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un Ajuste de Almacen
        /// </summary>
        /// <param name="AjustesAlmacenes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<AjustesAlmacenes>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearAjustesAlmacenes([FromBody] AjustesAlmacenes datos)
        {
            var datos_actualizados = await _bussines.guardarAjustesAlmacenes(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de un Ajuste de Almacen
        /// </summary>
        /// <param name="AjustesAlmacenes"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<AjustesAlmacenes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<AjustesAlmacenes>> actualizarAjustesAlmacenes([FromBody] AjustesAlmacenes datos)
        {
            return await _bussines.guardarAjustesAlmacenes(datos, Transaction.Update);
        }
    }
}
