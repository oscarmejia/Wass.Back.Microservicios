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
    public class ActivosEquiposController : ControllerBase
    {
        private readonly BOActivosEquipos _bussines;
        private readonly IConfiguration _configuration;

        public ActivosEquiposController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOActivosEquipos(dataBase);
        }

        /// <summary>
        /// Consulta un Activo Equipo por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta activos equipos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosEquipos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta activos equipos por la sede a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosEquipos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorSedeAsync(long id)
        {
            var datos = await _bussines.GetPorSedeAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta activos equipos por la marca a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("marca/{idMarca}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosEquipos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorMarcaAsync(long idMarca)
        {
            var datos = await _bussines.GetPorMarcaAsync(idMarca);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta activos equipos por la Empresa a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosEquipos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorEmpresaAsync(long idEmpresa)
        {
            var datos = await _bussines.GetPorEmpresaAsync(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crear un activo equipo
        /// </summary>
        /// <param name="Activos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosEquipos dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza un Activo Equipos
        /// </summary>
        /// <param name="Equipos"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] ActivosEquipos dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Elimina de manera lógica activo Equipos
        /// </summary>
        /// <param name="Equipo"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> eliminar([FromBody] ActivosEquipos dato)
        {
            var datos = await _bussines.SetAsync(dato, Transaction.Delete);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Activos Equipo por categoria,clasificacion, subclasificacion,sede,marca
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("categoria/{idCategoria}/clasificacion/{idClasificacion}/sede/{idSedeResponsable}/marca/{idMarca}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosEquipos>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerEquiposCategoriaClasificacionSubClasificacionSedeMarca(long idCategoria, long idClasificacion, long idSedeResponsable, long idMarca, long? clasificacion2 = null)
        {
            var datos = await _bussines.ObtenerEquiposCategoriaClasificacionSubClasificacionSedeMarca(idCategoria, idClasificacion, idSedeResponsable, idMarca, clasificacion2);
            return StatusCode(datos.codigo, datos);
        }

    }
}
