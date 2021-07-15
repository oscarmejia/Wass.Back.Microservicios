using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Bussines;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Agenda;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.Mantenimientos;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdenesTrabajoController : ControllerBase
    {
        private readonly BOOrdenesTrabajo _BO;
        private readonly IConfiguration _configuration;

        public OrdenesTrabajoController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _BO = new BOOrdenesTrabajo(dataBase);
        }

        /// <summary>
        /// Consulta una orden de trabajo especifica con todos sus detalles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("prueba/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenTrabajoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPruebaAsync(long id)
        {
            var datos = await _BO.GetPrueba(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el 100% de las orden de trabajos creadas en la plataforma (puede tubar el microservicio)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("prueba")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasPruebaAsync()
        {
            var datos = await _BO.GetAllPrueba();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta una orden de trabajo especifica con todos sus detalles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenTrabajoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(long id)
        {
            var datos = await _BO.Get(id);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Consulta el 100% de las orden de trabajos creadas en la plataforma (puede tubar el microservicio)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasAsync()
        {
            var datos = await _BO.GetAll();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el 100% de las orden de trabajos creadas en la plataforma (puede tubar el microservicio)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TodasPorCuadrilla/{idCuadrilla}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasPorCuadrillaAsync(long idCuadrilla)
        {
            var datos = await _BO.GetTodasPorCuadrillaAsync(idCuadrilla);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el tiempo promedio que toma realizar la cantidad de trabajos introducidas en una orden de Mantenimiento Correctivo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MantenimientoCorrectivo/TiempoPromedioDeTrabajo/cantidadTrabajos/{cantidadTrabajos}/Diagnostico/{idDiagnostico}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TiempoPromedioRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPorFechaCierreLlenaMantenimientoCorrectivo(long cantidadTrabajos, long idDiagnostico)
        {
            var datos = await _BO.GetAllPorFechaCierreLlenaMantenimientoCorrectivo(cantidadTrabajos, idDiagnostico);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el tiempo promedio que toma realizar la cantidad de trabajos introducidas en una orden de Mantenimiento Preventivo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MantenimientoPreventivo/TiempoPromedioDeTrabajo/cantidadTrabajos/{cantidadTrabajos}/GrupoPartes/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TiempoPromedioRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPorFechaCierreLlenaMantenimientoPreventivo(long cantidadTrabajos, long idGrupo)
        {
            var datos = await _BO.GetAllPorFechaCierreLlenaMantenimientoPreventivo(cantidadTrabajos, idGrupo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el tiempo promedio que toma realizar la cantidad de trabajos introducidas en una orden de Mantenimiento Rondas
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MantenimientoRondas/TiempoPromedioDeTrabajo/cantidadTrabajos/{cantidadTrabajos}/GrupoVariables/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TiempoPromedioRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPorFechaCierreLlenaMantenimientoRondas(long cantidadTrabajos, long idGrupo)
        {
            var datos = await _BO.GetAllPorFechaCierreLlenaMantenimientoRondas(cantidadTrabajos, idGrupo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las ordenes asociadas a una sede
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasPorSedeAsync(long idSede)
        {
            var datos = await _BO.GetAllPorSede(idSede);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las ordenes asociadas a un activo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Activo/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPorActivo(Guid idActivo)
        {
            var datos = await _BO.GetAllPorActivo(idActivo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las ordenes de rondas asociadas a un activo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("HistoricoVariables/MantenimintoRondas/Variables/Activo/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetHistoricoActivoVariable(Guid idActivo)
        {
            var datos = await _BO.GetHistoricoActivoVariable(idActivo);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las ordenes asociadas a una empresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodasPorEmpresaAsync(long idEmpresa)
        {
            var datos = await _BO.GetAllPorEmpresa(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las ordenes por tercerizar asociadas a una empresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TodasPorTercerizar/empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPorEmpresaPorTercerizar(long idEmpresa)
        {
            var datos = await _BO.GetAllPorEmpresaPorTercerizar(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las ordenes en ejecucion asociadas a una empresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TodasEnEjecucion/empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPorEmpresaEnEjecucion(long idEmpresa)
        {
            var datos = await _BO.GetAllPorEmpresaEnEjecucion(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta segun los parametros ingresados entre las ordenes de trabajo 
        /// Creada = 1,
        /// Aprobada = 2,
        /// Programada = 3,
        /// Ejecucion = 4,
        /// Evaluacion = 5,
        /// Terminada = 6
        /// 
        /// </summary>
        /// <param name="mensaje">Mensaje enviado segpun criterios de busqueda</param>
        /// <returns></returns>
        [HttpPost]
        [Route("busquedas")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorBusquedaAsync( [FromBody] BusquedasOrdenesRequest mensaje)
        {
            var datos = await _BO.GetPorBusquedaAsync(mensaje);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crear 
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenTrabajoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Crear([FromBody]  OrdenTrabajoRequest dato)
        {

            var datos = await _BO.Set(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza una orden de trabajo
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenTrabajoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Actualizar([FromBody]  OrdenTrabajoRequest dato)
        {
            var datos = await _BO.Set(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Elimina de manera lógica activo Equipos
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<OrdenTrabajoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Eliminar([FromBody]  OrdenTrabajoRequest dato)
        {
            var datos = await _BO.Set(dato, Transaction.Delete);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta el Historico de una orden de trabajo por idActivo
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ActivoHistorico/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<OrdenTrabajoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetHistoricoActivoAsync(Guid idActivo)
        {
            var datos = await _BO.GetHistoricoActivoAsync(idActivo);
            return StatusCode(datos.codigo, datos);
        }
    }
}