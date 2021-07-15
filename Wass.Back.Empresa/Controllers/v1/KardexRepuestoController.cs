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
    public class KardexRepuestoController : ControllerBase
    {
        private readonly BOKardexRepuesto _bussines;

        public KardexRepuestoController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOKardexRepuesto(dataBase);
        }


        /// <summary>
        /// Consulta Trazabilidad de un almacen fisico
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TrazabilidadAlmacenFisico/{idAlmacen}/Repuesto/{idRepuesto}/fechaInicio/{fechaInicio}/fechaFin/{fechaFin}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<KardexRepuesto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> trazabilidadRepuestoAlmacenFisico(long idAlmacen, long idRepuesto, DateTime fechaInicio, DateTime fechaFin)
        {
            var datos = await _bussines.trazabilidadRepuestoAlmacenFisico(idAlmacen, idRepuesto, fechaInicio, fechaFin);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta trazabilidad de un almacen virtual
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TrazabilidadAlmacenVirtual/{idAlmacen}/Repuesto/{idRepuesto}/fechaInicio/{fechaInicio}/fechaFin/{fechaFin}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<KardexRepuesto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> trazabilidadRepuestoAlmacenVirtual(long idAlmacen, long idRepuesto, DateTime fechaInicio, DateTime fechaFin)
        {
            var datos = await _bussines.trazabilidadRepuestoAlmacenVirtual(idAlmacen, idRepuesto, fechaInicio, fechaFin);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta trazabilidad de un almacen virtual en mobile
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TrazabilidadAlmacenVirtualMobile/{idAlmacen}/fechaActual/{fechaActual}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<KardexRepuesto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> trazabilidadRepuestoAlmacenVirtualMobile(long idAlmacen, DateTime fechaActual)
        {
            var datos = await _bussines.trazabilidadRepuestoAlmacenVirtualMobile(idAlmacen, fechaActual);
            return StatusCode(datos.codigo, datos);
        }
    }
}
