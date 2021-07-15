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
using Wass.Back.Empresa.Models.Peticiones.v1.SolicitudPedido;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SolicitudPedidoController : ControllerBase
    {
        private readonly BOSolicitudPedido _bussines;

        public SolicitudPedidoController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOSolicitudPedido(dataBase);
        }

        /// <summary>
        /// Consulta una Solicitud de Pedido en especifico
        /// </summary>
        /// <param name="idSolicitudPedido"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idSolicitudPedido}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SolicitudPedidoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idSolicitudPedido)
        {
            var datos = await _bussines.GetPorId(idSolicitudPedido);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos las Solicitudes de Pedido
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<SolicitudPedidoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea una Solicitud de Pedido
        /// </summary>
        /// <param name="SolicitudPedido"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SolicitudPedidoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearSolicitudPedido([FromBody] SolicitudPedidoRequest datos)
        {
            var datos_actualizados = await _bussines.guardarSolicitudPedido(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de una Solicitud de Pedido
        /// </summary>
        /// <param name="SolicitudPedido"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<SolicitudPedidoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<SolicitudPedidoRequest>> actualizarComentario([FromBody] SolicitudPedidoRequest datos)
        {
            return await _bussines.guardarSolicitudPedido(datos, Transaction.Update);
        }


    }
}
