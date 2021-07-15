using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wass.Back.Empresa.Kiwi.Bussines;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.ActivoParada;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ActivosParadaController : ControllerBase
    {
        private readonly BOActivosParada _bussines;
        private readonly IConfiguration _configuration;

        public ActivosParadaController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOActivosParada(dataBase);
        }

        /// <summary>
        /// Consulta una Parada de Activo por ID
        /// </summary>
        /// <param name="idActivosParada"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idActivosParada}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(long idActivosParada)
        {
            var datos = await _bussines.GetAsync(idActivosParada);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta parada de activos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosParadaRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta activos que estan parados en la fecha actual
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ConsultarActivosParados/FechaActual/{fechaActual}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosParada>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllParadosAsync(DateTime fechaActual)
        {
            var datos = await _bussines.GetAllParadosAsync(fechaActual);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Consulta todas las paradas de un activo por la sede a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{idSede}/FechaInicio/{fechaInicio}/FechaFinal/{fechaFinal}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaSedeRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorSedeAsync(long idSede, DateTime fechaInicio, DateTime fechaFinal)
        {
            var datos = await _bussines.GetPorSedeAsync(idSede, fechaInicio, fechaFinal);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las paradas de un activo en un rango de fechas a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Activo/{idActivo}/FechaInicio/{fechaInicio}/FechaFinal/{fechaFinal}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaActivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorActivoAsync(Guid idActivo, DateTime fechaInicio, DateTime fechaFinal)
        {
            var datos = await _bussines.GetPorActivoAsync(idActivo, fechaInicio, fechaFinal);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las paradas de un activo por la Empresa a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Empresa/{idEmpresa}/FechaInicio/{fechaInicio}/FechaFinal/{fechaFinal}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaEmpresaRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorEmpresaAsync(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal)
        {
            var datos = await _bussines.GetPorEmpresaAsync(idEmpresa, fechaInicio, fechaFinal);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta Promedio de horas de todas las paradas de un activo por la sede a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PromedioHoras/sede/{idSede}/FechaInicio/{fechaInicio}/FechaFinal/{fechaFinal}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaSedePromedioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPromedioPorSedeAsync(long idSede, DateTime fechaInicio, DateTime fechaFinal)
        {
            var datos = await _bussines.GetPromedioPorSedeAsync(idSede, fechaInicio, fechaFinal);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta Promedio de horas de todas las paradas de un activo en un rango de fechas a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PromedioHoras/Activo/{idActivo}/FechaInicio/{fechaInicio}/FechaFinal/{fechaFinal}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaActivoPromedioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPromedioPorActivoAsync(Guid idActivo, DateTime fechaInicio, DateTime fechaFinal)
        {
            var datos = await _bussines.GetPromedioPorActivoAsync(idActivo, fechaInicio, fechaFinal);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta Promedio de horas de todas las paradas de un activo por la Empresa a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PromedioHoras/Empresa/{idEmpresa}/FechaInicio/{fechaInicio}/FechaFinal/{fechaFinal}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaEmpresaPromedioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPromedioPorEmpresaAsync(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal)
        {
            var datos = await _bussines.GetPromedioPorEmpresaAsync(idEmpresa, fechaInicio, fechaFinal);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta Promedio de horas de todas las paradas de un activo por la sede a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PorcentajeHorasDisponible/sede/{idSede}/FechaInicio/{fechaInicio}/FechaFinal/{fechaFinal}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaSedeDisponibleRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTiempoDisponiblePorSedeAsync(long idSede, DateTime fechaInicio, DateTime fechaFinal)
        {
            var datos = await _bussines.GetTiempoDisponiblePorSedeAsync(idSede, fechaInicio, fechaFinal);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta Promedio de horas de todas las paradas de un activo en un rango de fechas a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PorcentajeHorasDisponible/Activo/{idActivo}/FechaInicio/{fechaInicio}/FechaFinal/{fechaFinal}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaActivoDisponibleRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTiempoDisponiblePorActivoAsync(Guid idActivo, DateTime fechaInicio, DateTime fechaFinal)
        {
            var datos = await _bussines.GetTiempoDisponiblePorActivoAsync(idActivo, fechaInicio, fechaFinal);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta Promedio de horas de todas las paradas de un activo por la Empresa a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PorcentajeHorasDisponible/Empresa/{idEmpresa}/FechaInicio/{fechaInicio}/FechaFinal/{fechaFinal}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParadaEmpresaDisponibleRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTiempoDisponiblePorEmpresaAsync(long idEmpresa, DateTime fechaInicio, DateTime fechaFinal)
        {
            var datos = await _bussines.GetTiempoDisponiblePorEmpresaAsync(idEmpresa, fechaInicio, fechaFinal);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta parada de activos por la cuadrilla a la que esta asociada
        /// </summary>
        /// <param name="idCuadrilla"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Cuadrilla/{idCuadrilla}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosParada>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorCuadrillaAsync(long idCuadrilla)
        {
            var datos = await _bussines.GetPorCuadrillaAsync(idCuadrilla);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crear una parada de activo 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParada>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosParada dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza una parada de Activo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParada>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] ActivosParada dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza la fecha de reactivacion de una parada de Activo
        /// </summary>
        /// <param name="Reactivacion"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Reactivacion/{fechaHoraReactivacion}/Activo/{idActivo}/ActivoParada/{idActivosParada}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosParada>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizarFechaReactivacion(DateTime fechaHoraReactivacion, Guid idActivo, long idActivosParada)
        {
            var datos = await _bussines.actualizarFechaReactivacion(fechaHoraReactivacion, idActivo, idActivosParada);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta una Parada de Activo por IdActivo que ya haya sido reactivado
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Activo/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosParada>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasPorActivoAsync(Guid idActivo)
        {
            var datos = await _bussines.GetTodasPorActivoAsync(idActivo);
            return StatusCode(datos.codigo, datos);
        }
    }
}
