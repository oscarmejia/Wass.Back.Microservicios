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

    public class ActivosClasificacionDiagnosticosController : ControllerBase
    {
        private readonly BOActivosClasificacionDiagnosticos _bussines;

        public ActivosClasificacionDiagnosticosController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOActivosClasificacionDiagnosticos(dataBase);
        }

        /// <summary>
        /// Consulta una diagnostico asociado a una clasificación
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> get(long id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta los diagnosticps que estan asociadas a todas las calsifiaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosClasificacionDiagnosticos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta los diagnosticos por que estan asociadas a una calsifiación especifica.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("clasificacion/id")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosClasificacionDiagnosticos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorClasificacionAsync(long id)
        {
            var datos = await _bussines.GetPorClasificacionAsync(id);
            return StatusCode(datos.codigo, datos);
        }
        /// <summary>
        /// Crea un diagnostico y asociarndolo a una clasificación
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosClasificacionDiagnosticos datos)
        {
            var datos_actualizados = await _bussines.SetAsync(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// CActualiza un diagnostico especifica.
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] ActivosClasificacionDiagnosticos datos)
        {
            var datos_actualizados = await _bussines.SetAsync(datos, Transaction.Update);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza el estado de parada de un diagnostico especifico.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ActualizarParada/{idDiagnostico}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizarParada(long idDiagnostico)
        {
            var datos_actualizados = await _bussines.actualizarParada(idDiagnostico);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Elimina de manera lógica un diagnostico
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> eliminar([FromBody] ActivosClasificacionDiagnosticos datos)
        {
            var datos_actualizados = await _bussines.SetAsync(datos, Transaction.Delete);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);

        }
    }
}
