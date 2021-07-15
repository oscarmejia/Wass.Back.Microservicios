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
    public class AlmacenController : ControllerBase
    {
        private readonly BOAlmacen _bussines;

        public AlmacenController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOAlmacen(dataBase);
        }

        /// <summary>
        /// Consulta un Almacen en especifico
        /// </summary>
        /// <param name="idAlmacen"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idAlmacen}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Almacen>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idAlmacen)
        {
            var datos = await _bussines.GetPorId(idAlmacen);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Almacenes
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Almacen>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un Almacen
        /// </summary>
        /// <param name="Almacen"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Almacen>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearAlmacen([FromBody] Almacen datos)
        {
            var datos_actualizados = await _bussines.guardarAlmacen(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de un Almacen
        /// </summary>
        /// <param name="Almacen"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Almacen>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Almacen>> actualizarAlmacen([FromBody] Almacen datos)
        {
            return await _bussines.guardarAlmacen(datos, Transaction.Update);
        }

        /// <summary>
        /// Consulta todos los almacenes de una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Almacen>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Almacen>>> getPorEmpresa(long idEmpresa)
        {
            return await _bussines.GetPorEmpresaAsync(idEmpresa);
        }

        /// <summary>
        /// Consulta todos los almacenes por sede
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Almacen>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Almacen>>> getTodasPorSede(long idSede)
        {
            return await _bussines.GetTodasPorSede(idSede);
        }

        /// <summary>
        /// Consulta todos los almacenes por tipo
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Tipo/{tipo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Almacen>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Almacen>>> getTodasPorTipo(long tipo)
        {
            return await _bussines.GetTodasPorTipo(tipo);
        }

        /// <summary>
        /// Consulta todos los almacenes por cuadrilla
        /// </summary>
        /// <param name="idCuadrilla"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Cuadrilla/{idCuadrilla}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Almacen>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Almacen>>> getTodasPorCuadrilla(long idCuadrilla)
        {
            return await _bussines.GetTodasPorCuadrilla(idCuadrilla);
        }

        /// <summary>
        /// Consulta todos los almacenes por tipo y Empresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Empresa/{idEmpresa}/TipoAlmacen/{tipo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Almacen>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Almacen>>> getTodasPorTipoEmpresa(long idEmpresa, long tipo)
        {
            return await _bussines.getTodasPorTipoEmpresa(idEmpresa, tipo);
        }

    }
}
