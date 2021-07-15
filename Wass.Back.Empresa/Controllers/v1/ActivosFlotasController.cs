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
    public class ActivosFlotasController : ControllerBase
    {

        private readonly BOActivosFlotas _bussines;

        public ActivosFlotasController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOActivosFlotas(dataBase);
        }

        /// <summary>
        /// Consulta un Activo Flota en especifico
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(Guid id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Activos Flota en especifico
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosFlotas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Consulta todos los Activos Flota por categoria,clasificacion,sede,marca,subclasificacion
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Categoria/{idCategoria}/Clasificacion/{idClasificacion1}/Sede/{idSedeResponsable}/Marca/{marca}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosFlotas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerFlotasCategoriaClasificacionSedeMarca(long idCategoria, long idClasificacion1, long idSedeResponsable, string marca, long? idClasificacion2 = null)
        {
            var datos = await _bussines.ObtenerFlotasCategoriaClasificacionSubClasificacionSedeMarca(idCategoria, idClasificacion1, idSedeResponsable, marca, idClasificacion2);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Activos Flota de una sede en especifico
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosFlotas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorSedeAsync(long id)
        {
            var datos = await _bussines.GetPorSedeAsync(id);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Consulta activos Flotas por la Empresa a la que esta asociada
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosFlotas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorEmpresaAsync(long idEmpresa)
        {
            var datos = await _bussines.GetPorEmpresaAsync(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Crea un Activo Flota
        /// </summary>
        /// <param name="Flota"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosFlotas datos)
        {
            var datos_actualizados = await _bussines.SetAsync(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de una flota
        /// </summary>
        /// <param name="Flota"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ActivosFlotas>> actualizar([FromBody] ActivosFlotas datos)
        {
            return await _bussines.SetAsync(datos, Transaction.Update);
        }

        /// <summary>
        /// Elimina de manera lógica un activo Flota
        /// </summary>
        /// <param name="Flota"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ActivosFlotas>> eliminar([FromBody] ActivosFlotas datos)
        {
            return await _bussines.SetAsync(datos, Transaction.Delete);
        }


    }
}
