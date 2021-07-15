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
    public class TipoActivoController : ControllerBase
    {
        private readonly BOTipoActivo _bussines;

        public TipoActivoController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOTipoActivo(dataBase);
        }

        /// <summary>
        /// Consulta un Tipo Activo  en especifico
        /// </summary>
        /// <param name="idTipoActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idTipoActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TipoActivo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idTipoActivo)
        {
            var datos = await _bussines.GetPorId(idTipoActivo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Tipos Activo 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TipoActivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Crea un Tipo Activo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TipoActivo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] TipoActivo datos)
        {
            var datos_actualizados = await _bussines.guardar(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de un Tipo Activo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TipoActivo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<TipoActivo>> actualizar([FromBody] TipoActivo datos)
        {
            return await _bussines.guardar(datos, Transaction.Update);
        }

    }
}
