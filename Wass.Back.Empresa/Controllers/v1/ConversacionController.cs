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
    public class ConversacionController : ControllerBase
    {
        private readonly BOConversacion _bussines;

        public ConversacionController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOConversacion(dataBase);
        }

        /// <summary>
        /// Consulta un Conversacion en especifico
        /// </summary>
        /// <param name="idConversacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idConversacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Conversacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idConversacion)
        {
            var datos = await _bussines.GetPorId(idConversacion);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos las Conversaciones
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Conversacion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos las Conversaciones de un empleado
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empleado/{idEmpleado}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Conversacion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodoPorEmpleado(long idEmpleado)
        {
            var datos = await _bussines.GetTodasPorEmpleado(idEmpleado);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea una Conversacion
        /// </summary>
        /// <param name="Conversacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Conversacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearConversacion([FromBody] Conversacion datos)
        {
            var datos_actualizados = await _bussines.guardarConversacion(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de una Conversacion
        /// </summary>
        /// <param name="Conversacion"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Conversacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Conversacion>> actualizarConversacion([FromBody] Conversacion datos)
        {
            return await _bussines.guardarConversacion(datos, Transaction.Update);
        }

        /// <summary>
        /// Elimina de manera lógica una Conversacion
        /// </summary>
        /// <param name="idConversacion"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idConversacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Conversacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Conversacion>> eliminarConversacion(long idConversacion)
        {
            return await _bussines.EliminarConversacion(idConversacion);
        }
    }
}
