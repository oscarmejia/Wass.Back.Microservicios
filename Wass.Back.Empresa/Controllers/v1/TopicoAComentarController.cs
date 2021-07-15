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
    public class TopicoAComentarController : ControllerBase
    {
        private readonly BOTopicoAComentar _bussines;

        public TopicoAComentarController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOTopicoAComentar(dataBase);
        }

        /// <summary>
        /// Consulta un Topico A Comentar en especifico
        /// </summary>
        /// <param name="idTopicoAComentar"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idComentario}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TopicoAComentar>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idTopicoAComentar)
        {
            var datos = await _bussines.GetPorId(idTopicoAComentar);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Topicos 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TopicoAComentar>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerTodo()
        {
            var datos = await _bussines.GetTodas();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todos los Comentarios ordenados de mas reciente a mas viejo excepto los tipo Licitacion
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObtenerTodasOrdenadasPorFecha")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TopicoAComentar>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrdenadasPorFecha()
        {
            var datos = await _bussines.GetOrdenadasPorFecha();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un Topico A Comentar
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TopicoAComentar>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crearComentario([FromBody] TopicoAComentar datos)
        {
            var datos_actualizados = await _bussines.guardarComentario(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Actualiza los datos de un  Topico A Comentar
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TopicoAComentar>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<TopicoAComentar>> actualizarComentario([FromBody] TopicoAComentar datos)
        {
            return await _bussines.guardarComentario(datos, Transaction.Update);
        }

        /// <summary>
        /// Elimina de manera lógica un Topico A Comentar
        /// </summary>
        /// <param name="idComentario"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idTopicoAComentar}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TopicoAComentar>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<TopicoAComentar>> eliminarComentario(long idTopicoAComentar)
        {
            return await _bussines.EliminarComentario(idTopicoAComentar);
        }



        /// <summary>
        /// Obtiene todas los Topicos asociados a una sede
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TopicoAComentar>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<TopicoAComentar>>> GetAllPorSede(long idSede)
        {

            return await _bussines.getTodasPorSedeAsync(idSede);
        }

        /// <summary>
        /// Obtiene todas los Topicos asociados a una empresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TopicoAComentar>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<TopicoAComentar>>> GetAllPorEmpresa(long idEmpresa)
        {

            return await _bussines.getTodasPorEmpresaAsync(idEmpresa);
        }

        /// <summary>
        /// Obtiene un Topico a comentar por TipoTopico e idTopico
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TipoTopico/{tipoTopico}/idTopico/{idTopico}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<TopicoAComentar>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<TopicoAComentar>> GetPorTipoTopicoIdTopico(long tipoTopico, string idTopico)
        {
            return await _bussines.GetPorTipoTopicoIdTopico(tipoTopico, idTopico);
        }

        /// <summary>
        /// Obtiene todas los Topico A Comentar ordenados por fecha
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Feed")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<TopicoAComentar>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<TopicoAComentar>>> GetFeed()
        {

            return await _bussines.GetFeed();
        }
    }
}
