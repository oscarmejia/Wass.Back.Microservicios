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
    public class MarcaActivoController : ControllerBase
    {
        private readonly BOMarcaActivo _bussines;

        public MarcaActivoController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOMarcaActivo(dataBase);
        }

        /// <summary>
        /// Consulta un Marca Activo  en especifico
        /// </summary>
        /// <param name="idMarcaActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idMarcaActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MarcaActivo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idMarcaActivo)
        {
            var datos = await _bussines.GetPorId(idMarcaActivo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos las Marcas Activo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<MarcaActivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Crea una Marca Activo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MarcaActivo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] MarcaActivo datos)
        {
            var datos_actualizados = await _bussines.guardar(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de una Marca Activo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MarcaActivo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<MarcaActivo>> actualizar([FromBody] MarcaActivo datos)
        {
            return await _bussines.guardar(datos, Transaction.Update);
        }
    }
}
