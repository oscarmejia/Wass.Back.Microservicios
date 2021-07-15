using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wass.Back.Empresa.Kiwi.Bussines;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.TransferenciasInternasAlmacenes;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferenciasInternasAlmacenesController : ControllerBase
    {
        private readonly BOTransferenciasInternasAlmacenes _bussines;

        public TransferenciasInternasAlmacenesController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOTransferenciasInternasAlmacenes(dataBase);
        }

        /// <summary>
        /// Consulta una Transferencia Interna
        /// </summary>
        /// <param name="idTransferenciasInternasAlmacenes"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idRecepcionRepuestos}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TransferenciasInternasAlmacenesRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idTransferenciasInternasAlmacenes)
        {
            var datos = await _bussines.GetPorId(idTransferenciasInternasAlmacenes);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las Transferencias Internas
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TransferenciasInternasAlmacenesRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las Transferencias Internas por empresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TransferenciasInternasAlmacenesRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodoPorEmpresa(long idEmpresa)
        {
            var datos = await _bussines.GetTodasPorEmpresa(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Crea un registro de Transferencias Internas
        /// </summary>
        /// <param name="TransferenciasInternasAlmacenes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TransferenciasInternasAlmacenesRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearTransferenciasInternasAlmacenes([FromBody] TransferenciasInternasAlmacenesRequest datos)
        {
            var datos_actualizados = await _bussines.guardarTransferenciasInternasAlmacenes(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza el estado de una Transferencia Interna y hace una transferencia para el estado = 2
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ActualizarEstadoTransferencia/{idTransferenciaInterna}/estado/{estado}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TransferenciasInternasAlmacenesRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizarTransferenciasInternasAlmacenes(long idTransferenciaInterna, long estado)
        {
            var datos_actualizados = await _bussines.actualizarTransferenciasInternasAlmacenes(idTransferenciaInterna, estado);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

    }
}
