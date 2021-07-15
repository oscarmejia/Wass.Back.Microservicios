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
	[Route("api/v1/[controller]")]
	[ApiController]
	public class PlanMantenimientoRondasController : ControllerBase
	{
        private readonly BOPlanMantenimientoRondas _BO;
        private readonly IConfiguration _configuration;

        public PlanMantenimientoRondasController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _BO = new BOPlanMantenimientoRondas(dataBase);
        }


        //-----------
        //  RONDAS
        //-----------
        [HttpGet]
        [Route("plan/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PlanesRondas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(long id)
        {
            var datos = await _BO.Get(id);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("plan/categoria/{idCategoria}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanesRondas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorCategoria(long idCategoria)
        {
            var datos = await _BO.GetPorCategoria(idCategoria);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("plan/todos")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanesRondas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var datos = await _BO.GetAll();
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("plan/todosPorEmpresa/Empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanesRondas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPorEmpresa(long idEmpresa)
        {
            var datos = await _BO.GetAllPorEmpresa(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("plan/todosPorEmpresaTipoPlan/Empresa/{idEmpresa}/tipoPlan/{tipoPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanesRondas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPorEmpresaTipoPlan(long idEmpresa, long tipoPlan)
        {
            var datos = await _BO.GetAllPorEmpresaTipoPlan(idEmpresa, tipoPlan);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("plan/{idCategoria}/{idClasificacion1}/{idClasificacion2}/{idSede}/{marca}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<PlanesRondas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPorParametros(long idCategoria, long idClasificacion1, long idClasificacion2, long idSede, string marca)
        {
            var datos = await _BO.GetPorParametros(idCategoria, idClasificacion1, idClasificacion2, idSede, marca);
            return StatusCode(datos.codigo, datos);
        }


        

        [HttpPost]
        [Route("plan/crear")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PlanesRondas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Set([FromBody] PlanesRondas objeto)
        {
            var datos = await _BO.Set(objeto, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }


        //---------------------
        //  GRUPOS VARIABLES
        //---------------------
        [HttpGet]
        [Route("variables/grupo/todos")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarGruposVariables()
        {
            var datos = await _BO.ConsultarGruposVariables();
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("variables/grupo/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarGrupoId(long idGrupo)
        {
            var datos = await _BO.ConsultarGrupoId(idGrupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPost]
        [Route("variables/grupo/crear")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Grupos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CrearGrupoVariables([FromBody] Grupos grupo)
        {
            var datos = await _BO.CrearGrupoVariables(grupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPut]
        [Route("variables/grupo/actualizar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Grupos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActualizarGrupo([FromBody] Grupos grupo)
        {
            var datos = await _BO.ActualizarGrupo(grupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("variables/grupos/quitar/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Grupos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> QuitarGrupo(long idGrupo)
        {
            var datos = await _BO.QuitarGrupo(idGrupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPost]
        [Route("variables/agregar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Variables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AgregarVariables(AgregarVariablesRondasRequest data)
        {
            var datos = await _BO.AgregarVariables(data.idGrupo, data.idPlan, data.variables);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("variables/quitar/{idGrupo}/{idPlan}/{idVariable}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Variables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> QuitarVariables(long idGrupo, long idPlan, long idVariable)
        {
            var datos = await _BO.QuitarVariables(idGrupo, idPlan, idVariable);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("variables/consultar/por/grupo/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Variables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarVariablesGrupoId(long idGrupo)
        {
            var datos = await _BO.ConsultarVariablesGrupoId(idGrupo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("variables/consultar/por/clasificacion{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Variables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarVariablesIdClasificacion(long idClasificacion)
        {
            var datos = await _BO.ConsultarVariablesIdClasificacion(idClasificacion);
            return StatusCode(datos.codigo, datos);
        }


        //---------------------
        //  ACTIVOS
        //---------------------
        [HttpPost]
        [Route("activos/asociar/{idPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AsociarActivosPlan(long idPlan, [FromBody] AsociarActivosRondasRequest grupoActivos)
        {
            var datos = await _BO.AsociarActivosPlan(idPlan, grupoActivos.activos);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("activos/desasociar/{idPlan}/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DesasociarActivosPlan(long idPlan, Guid idActivo)
        {
            var datos = await _BO.DesasociarActivosPlan(idPlan, idActivo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPost]
        [Route("activos/asociar/{idPlan}/activo/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AsociarActivoIdConGrupoIdAPlan(long idPlan, Guid idActivo)
        {
            var datos = await _BO.AsociarActivoIdConGrupoIdAPlan(idPlan, idActivo);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("activos/plan/{idPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarActivosIdPlan(long idPlan)
        {
            var datos = await _BO.ConsultarActivosIdPlan(idPlan);
            return StatusCode(datos.codigo, datos);
        }

        //[HttpGet]
        //[Route("activos/plan/{idPlan}")]
        //[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivos>>>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> ConsultarActivosIdGrupoConIdPlan(long idPlan)
        //{
        //    var datos = await _BO.ConsultarActivosIdGrupoConIdPlan(idPlan);
        //    return StatusCode(datos.codigo, datos);
        //}

        [HttpGet]
        [Route("activos/plan/{idPlan}/activo/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarActivosIdActivoConIdPlan(Guid idActivo, long idPlan)
        {
            var datos = await _BO.ConsultarActivosIdActivoConIdPlan(idActivo, idPlan);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPut]
        [Route("rondas/actualizar/activos")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<GruposActivos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActualizarRondasConActivosGrupos([FromBody] ActualizarRondasRequest data)
        {
            var datos = await _BO.ActualizarRondasConActivosGrupos(data);
            return StatusCode(datos.codigo, datos);
        }

        [HttpPut]
        [Route("rondas/actualizar/variables/nombreVariable/{nombreVariable}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RespuestaActivosVariables>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActualizarRondasConVariablesGrupos([FromBody] ActualizarRondasVarsRequest data,string nombreVariable)
        {
            var datos = await _BO.ActualizarRondasConVariablesGrupos(data, nombreVariable);
            return StatusCode(datos.codigo, datos);
        }

        //---------------------
        //  MANTENIMIENTO PREVENTIVO
        //---------------------

        //Crear mantenimiento preventivo
        [HttpPost]
        [Route("mantenimientoRondas/crearPlanMantenimientoRondas")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<PlanMantenimientoPreventivo>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SetPlanMantenimientoRondas([FromBody] ActualizarRondasRequest objeto)
        {
            var datos = await _BO.SetPlanMantenimientoRondas(objeto, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("mantenimientoRondas/plan/{idPlan}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<MantenimientoRondas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMantenimientoRondasPorPlan(long idPlan)
        {
            var datos = await _BO.GetMantenimientoRondasPorPlan(idPlan);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("mantenimientoRondas/plan/{idPlan}/activo/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<MantenimientoRondas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMantenimientoRondasPorPlanYActivo(long idPlan, Guid idActivo)
        {
            var datos = await _BO.GetMantenimientoRondasPorPlanYActivo(idPlan, idActivo);
            return StatusCode(datos.codigo, datos);
        }


    }
}
