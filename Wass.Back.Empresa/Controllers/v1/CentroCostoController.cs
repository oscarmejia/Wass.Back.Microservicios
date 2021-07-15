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
using Wass.Back.Empresa.Models.Peticiones.v1.CentroCosto;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CentroCostoController : ControllerBase
    {
        private readonly BOCentroCosto _bussines;

        public CentroCostoController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOCentroCosto(dataBase);
        }

        /// <summary>
        /// Consulta un Centro de Costo en especifico
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCentroCosto}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CentroCostoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idCentroCosto)
        {
            var datos = await _bussines.GetPorId(idCentroCosto);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Centros de Costo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CentroCostoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Centros de Costo por empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CentroCostoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasPorEmpresa(long idEmpresa)
        {
            var datos = await _bussines.GetTodasPorEmpresa(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Consulta todos los Hijos (Centros de Costo) de un Centro de Costo
        /// </summary>
        /// <param name="idCentroCostoPadre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CentroCostoPadre/{idCentroCostoPadre}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CentroCostoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorCentroCostoPadre(string idCentroCostoPadre)
        {
            var datos = await _bussines.GetPorCentroCostoPadre(idCentroCostoPadre);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un Centro de Costo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CentroCosto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearCentroCosto([FromBody] CentroCosto datos)
        {
            var datos_actualizados = await _bussines.guardarCentroCosto(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de un Centro de Costo
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CentroCosto>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CentroCosto>> actualizarComentario([FromBody] CentroCosto datos)
        {
            return await _bussines.guardarCentroCosto(datos, Transaction.Update);
        }

        /// <summary>
        /// Elimina de manera lógica un Centro de Costo
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idCentroCosto}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CentroCosto>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CentroCosto>> eliminarComentario(long idCentroCosto)
        {
            return await _bussines.EliminarCentroCosto(idCentroCosto);
        }
    }
}
