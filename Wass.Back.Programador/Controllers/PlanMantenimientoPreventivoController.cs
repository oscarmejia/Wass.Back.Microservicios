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
using WASS.Back.Programador.core.models.Peticiones.v1.Mantenimientos.Request;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlanMantenimientoPreventivoController : ControllerBase
    {
        private readonly BOPlanMantenimientoPreventivo _BO;
        private readonly IConfiguration _configuration;

        public PlanMantenimientoPreventivoController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _BO = new BOPlanMantenimientoPreventivo(dataBase);
        }


        //-----------
        //  PLAN
        //-----------
        [HttpGet]
        [Route("plan/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PlanesMantenimientoPreventivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(long id)
        {
            var datos = await _BO.Get(id);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("plan/categoria/{idCategoria}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorCategoria(long idCategoria)
        {
            var datos = await _BO.GetPorCategoria(idCategoria);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("plan/todos")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var datos = await _BO.GetAll();
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("plan/TodosPorEmpresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPorEmpresa(long idEmpresa)
        {
            var datos = await _BO.GetAllPorEmpresa(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("plan/{idCategoria}/{idClasificacion1}/{idClasificacion2}/{idSede}/{marca}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorParametros(long idCategoria, long idClasificacion1, long idClasificacion2, long idSede, string marca)
        {
            var datos = await _BO.GetPorParametros(idCategoria, idClasificacion1, idClasificacion2, idSede, marca);
            return StatusCode(datos.codigo, datos);
        }

        

        [HttpPost]
        [Route("plan/crearPlan")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PlanesMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Set([FromBody] PlanesMantenimientoPreventivo objeto)
        {
            var datos = await _BO.Set(objeto, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }


        //---------------------
        //  GRUPOS PARTES
        //---------------------
        [HttpGet]
        [Route("partes/grupo/todos")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarGruposPartes()
        {
            var datos = await _BO.ConsultarGruposPartes();
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("partes/grupo/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarGrupoId(long idGrupo)
        {
            var datos = await _BO.ConsultarGrupoId(idGrupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPost]
        [Route("partes/grupo/crearGrupoPartes")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<GruposMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CrearGrupoPartes([FromBody] GruposMantenimientoPreventivo grupo)
        {
            var datos = await _BO.CrearGrupoPartes(grupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("partes/grupos/quitarGrupo/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> QuitarGrupo(long idGrupo)
        {
            var datos = await _BO.QuitarGrupo(idGrupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPost]
        [Route("partes/agregar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AgregarPartes(AgregarPartesMantenimientoPreventivoRequest data)
        {
            var datos = await _BO.AgregarPartes(data.idGrupo, data.idPlan, data.partes);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("partes/quitarPartes/{idGrupo}/{idPlan}/{idParte}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> QuitarPartes(long idGrupo, long idPlan, Guid idParte)
        {
            var datos = await _BO.QuitarPartes(idGrupo, idPlan, idParte);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("partes/consultar/por/grupo/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarPartesGrupoId(long idGrupo)
        {
            var datos = await _BO.ConsultarPartesGrupoId(idGrupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("partes/consultar/por/clasificacion/{idClasificacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarPartesIdClasificacion(long idClasificacion)
        {
            var datos = await _BO.ConsultarPartesIdClasificacion(idClasificacion);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPut]
        [Route("partes/actualizarParada/{idGrupo}/plan/{idPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizarParada(long idGrupo, long idPlan)
        {
            var datos = await _BO.actualizarParada(idGrupo, idPlan);
            return StatusCode(datos.codigo, datos);
        }

        //---------------------
        //  GRUPOS ACCIONES
        //---------------------
        [HttpGet]
        [Route("acciones/todos")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposAcciones>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarGruposAcciones()
        {
            var datos = await _BO.ConsultarGruposAcciones();
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("acciones/grupo/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposAcciones>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarAccionesGrupoId(long idGrupo)
        {
            var datos = await _BO.ConsultarAccionesGrupoId(idGrupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPost]
        [Route("acciones/asociar/{idPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposAcciones>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AsociarAccionesPlan(long idPlan, [FromBody] AgregarAccionesMantenimientoPreventivoRequest grupoAcciones)
        {
            var datos = await _BO.AsociarAccionesPlan(idPlan, grupoAcciones.idGrupo, grupoAcciones.acciones);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPost]
        [Route("acciones/agregar/{idPlan}/grupo/{idGrupo}/accion/{idAccion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposAcciones>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AgregarAccionIdConGrupoIdAPlan(long idPlan, long idGrupo, long idAccion)
        {
            var datos = await _BO.AgregarAccionIdConGrupoIdAPlan(idPlan, idGrupo, idAccion);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("acciones/quitarAcciones/{idGrupo}/{idPlan}/{idAccion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposAcciones>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> QuitarAcciones(long idGrupo, long idPlan, long idAccion)
        {
            var datos = await _BO.QuitarAcciones(idGrupo, idPlan, idAccion);
            return StatusCode(datos.codigo, datos);
        }


        //---------------------
        //  ACTIVOS
        //---------------------
        [HttpPost]
        [Route("activos/asociar/{idPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AsociarActivosPlan(long idPlan, [FromBody] AsociarActivosMantenimientoPreventivoRequest grupoActivos)
        {
            var datos = await _BO.AsociarActivosPlan(idPlan, grupoActivos.activos);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("activos/desasociar/{idPlan}/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DesasociarActivosPlan(long idPlan, string idActivo)
        {
            var datos = await _BO.DesasociarActivosPlan(idPlan, idActivo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPost]
        [Route("activos/asociar/{idPlan}/activo/")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<GruposActivosMantenimientoPreventivoRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AsociarActivoIdConGrupoIdAPlan(long idPlan, [FromBody] detalleActivosRequest idActivo)
        {
            var datos = await _BO.AsociarActivoIdConGrupoIdAPlan(idPlan, idActivo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("activos/plan/{idPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarActivosIdPlan(long idPlan)
        {
            var datos = await _BO.ConsultarActivosIdPlan(idPlan);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("activos/plan/{idPlan}/activo/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivosMantenimientoPreventivoRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarActivosIdActivoConIdPlan(string idActivo, long idPlan)
        {
            var datos = await _BO.ConsultarActivosIdActivoConIdPlan(idActivo, idPlan);
            return StatusCode(datos.codigo, datos);
        }
        //Crear mantenimiento preventivo
        [HttpPost]
        [Route("mantenimientoPreventivo/crearPlanMantenimientoPreventivo")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PlanMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SetPlanMantenimientoPreventivo([FromBody] PlanMantenimientoPreventivo objeto)
        {
            var datos = await _BO.SetPlanMantenimientoPreventivo(objeto, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPut]
        [Route("mantenimientoPreventivo/actualizar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivosMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActualizarMantenimientoPreventivoConActivosGrupos([FromBody] ActualizarMantenimientoPreventivoRequest data)
        {
            var datos = await _BO.ActualizarMantenimientoPreventivoConActivosGrupos(data.idPlanMantenimientoPreventivo, data.idPlan, data.idGrupo, data.idActivo, data.fechaUltimoMantenimientoPreventivo);
            return StatusCode(datos.codigo, datos);
        }


        //---------------------
        //  MANTENIMIENTO PREVENTIVO
        //---------------------
        [HttpGet]
        [Route("mantenimientoPreventivo/plan/{idPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMantenimientoPreventivoPorPlan(long idPlan)
        {
            var datos = await _BO.GetMantenimientoPreventivoPorPlan(idPlan);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("mantenimientoPreventivo/plan/{idPlan}/activo/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMantenimientoPreventivoPorPlanYActivo(long idPlan,string idActivo)
        {
            var datos = await _BO.GetMantenimientoPreventivoPorPlanYActivo(idPlan,idActivo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("mantenimientoPreventivo/todos")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanMantenimientoPreventivo>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodosMantenimientoPreventivo()
        {
            var datos = await _BO.GetTodosMantenimientoPreventivo();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idPlanMantenimientoPreventivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PlanMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Eliminar(long idPlanMantenimientoPreventivo)
        {
            var datos = await _BO.EliminarPlanMantenimientoPreventivo(idPlanMantenimientoPreventivo);
            return StatusCode(datos.codigo, datos);
        }

    }
}
