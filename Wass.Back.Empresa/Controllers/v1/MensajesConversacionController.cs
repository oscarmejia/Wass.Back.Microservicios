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
    public class MensajesConversacionController : ControllerBase
    {
        private readonly BOMensajesConversacion _bussines;

        public MensajesConversacionController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOMensajesConversacion(dataBase);
        }

        /// <summary>
        /// Consulta un Mensaje de Conversacion en especifico
        /// </summary>
        /// <param name="idMensajesConversacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idMensajesConversacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MensajesConversacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idMensajesConversacion)
        {
            var datos = await _bussines.GetPorId(idMensajesConversacion);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Mensajes de la Conversacion
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<MensajesConversacion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un Mensaje 
        /// </summary>
        /// <param name="MensajesConversacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MensajesConversacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearMensajesConversacion([FromBody] MensajesConversacion datos)
        {
            var datos_actualizados = await _bussines.guardarMensajesConversacion(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de un Mensaje
        /// </summary>
        /// <param name="MensajesConversacion"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MensajesConversacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<MensajesConversacion>> actualizarConversacion([FromBody] MensajesConversacion datos)
        {
            return await _bussines.guardarMensajesConversacion(datos, Transaction.Update);
        }

        /// <summary>
        /// Elimina de manera lógica un Mensaje
        /// </summary>
        /// <param name="idMensajesConversacion"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idMensajesConversacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MensajesConversacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<MensajesConversacion>> eliminarMensajesConversacion(long idMensajesConversacion)
        {
            return await _bussines.EliminarMensajesConversacion(idMensajesConversacion);
        }
    }
}
