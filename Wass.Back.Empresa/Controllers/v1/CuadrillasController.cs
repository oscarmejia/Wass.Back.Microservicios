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
using Wass.Back.Empresa.Models.Peticiones.v1.Cuadrilla;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CuadrillasController : ControllerBase
    {
        private readonly BOCuadrillas _bussines;
        private readonly IConfiguration _configuration;

        public CuadrillasController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOCuadrillas(dataBase);
        }

        /// <summary>
        /// Consulta una cuadrilla especifica
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cuadrillas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Cuadrillas>> Get(long id)
        {
            return await _bussines.GetAsync(id);
        }

        /// <summary>
        /// Consulta una cuadrilla especifica
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ubicacion/{idCuadrilla}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillasRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CuadrillasRequest>> GetUbicacion(long idCuadrilla)
        {
            return await _bussines.GetUbicacionAsync(idCuadrilla);
        }

        /// <summary>
        /// Consulta las cuadrillas que estan atadas a una sede
        /// </summary>
        /// <param name="idEstado"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cuadrillas>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cuadrillas>>> getPorSedeAsync(long idSede)
        {
            return await _bussines.GetPorSedeAsync(idSede);
        }

        /// <summary>
        /// Consulta Cuadrillas por la Empresa a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cuadrillas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorEmpresaAsync(long idEmpresa)
        {
            var datos = await _bussines.GetPorEmpresaAsync(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta las cuadrillas que estan atadas a una sede
        /// </summary>
        /// <param name="idEstado"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cuadrillas>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cuadrillas>>> getTodasAsync()
        {
            return await _bussines.GetAllAsync();
        }

        /// <summary>
        /// Crear una cuadrilla
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cuadrillas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Cuadrillas>> crear([FromBody] Cuadrillas cuadrilla)
        {
            return await _bussines.SetAsync(cuadrilla, Transaction.Insert);
        }

        /// <summary>
        /// Actualiza una cuadrilla
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empleados>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Cuadrillas>> actualizar([FromBody] Cuadrillas cuadrilla)
        {
            return await _bussines.SetAsync(cuadrilla, Transaction.Update);
        }
    }
}
