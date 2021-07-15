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
using Wass.Back.Empresa.Models.Peticiones.v1.Calificacion;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        private readonly BOCalificacion _bussines;

        public CalificacionController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOCalificacion(dataBase);
        }

        /// <summary>
        /// Consulta una Calificacion en especifico
        /// </summary>
        /// <param name="idCalificacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCalificacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CalificacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idCalificacion)
        {
            var datos = await _bussines.GetPorId(idCalificacion);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos las Calificaciones
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CalificacionRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea una Calificacion
        /// </summary>
        /// <param name="Calificacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CalificacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearCalificacion([FromBody] CalificacionRequest datos)
        {
            var datos_actualizados = await _bussines.guardarCalificacion(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de una Calificacion
        /// </summary>
        /// <param name="Calificacion"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CalificacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CalificacionRequest>> actualizarComentario([FromBody] CalificacionRequest datos)
        {
            return await _bussines.guardarCalificacion(datos, Transaction.Update);
        }



        /// <summary>
        /// Obtiene todas las Calificaciones asociados a una sede
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CalificacionRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CalificacionRequest>>> GetAllPorSede(long idSede)
        {

            return await _bussines.getTodasPorSedeAsync(idSede);
        }

        /// <summary>
        /// Obtiene todas las Calificaciones asociados a una orden de trabajo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("OrdenTrabajo/{idOrdenTrabajo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CalificacionRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CalificacionRequest>>> GetAllPorOrdenTrabajo(long idOrdenTrabajo)
        {

            return await _bussines.getTodasPorOrdenTrabajoAsync(idOrdenTrabajo);
        }

        /// <summary>
        /// Obtiene todas las Calificaciones asociadas a una empresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CalificacionRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CalificacionRequest>>> GetAllPorEmpresa(long idEmpresa)
        {

            return await _bussines.getTodasPorEmpresaAsync(idEmpresa);
        }

        /// <summary>
        /// Obtiene todas las Calificaciones asociadas a un proveedor
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("proveedor/{idProveedor}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CalificacionRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CalificacionRequest>>> GetAllPorProveedor(long idProveedor)
        {

            return await _bussines.getTodasPorProveedorAsync(idProveedor);
        }

        /// <summary>
        /// Obtiene el promedio de todas las Calificaciones asociadas a un proveedor
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("promedio/{idProveedor}/{idSede}/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CalificacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CalificacionRequest>> GetPromedio(long idProveedor, long idSede, long idEmpresa)
        {

            return await _bussines.GetPromedioCalificacion(idProveedor, idSede, idEmpresa);
        }
    }
}
