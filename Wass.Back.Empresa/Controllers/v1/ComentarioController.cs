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
using Wass.Back.Empresa.Models.Peticiones.v1.Comentario;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly BOComentario _bussines;
        private readonly IConfiguration _configuration;

        public ComentarioController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOComentario(dataBase);
        }


        [HttpGet]
        [Route("{idComentario}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ComentarioRequest>> Get(long idComentario)
        {
            return await _bussines.GetPorId(idComentario);
        }

        [HttpGet]
        [Route("TodasPorTopicoAComentar/{idTopicoAComentar}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ComentarioRequest>>> GetTodasPorIdTopicoAComentar(long idTopicoAComentar)
        {
            return await _bussines.GetPorIdTopicoAComentar(idTopicoAComentar);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ComentarioRequest>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ComentarioRequest>> CrearComentario([FromBody] ComentarioRequest comentario)
        {
            return await _bussines.guardarComentario(comentario, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ComentarioRequest>> EditarSoporte([FromBody] ComentarioRequest comentario)
        {
            return await _bussines.guardarComentario(comentario, Transaction.Update);
        }

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="idComentario"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("EliminarComentario/{idComentario}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Eliminar(long idComentario)
        {
            var datos = await _bussines.EliminarComentario(idComentario);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza los likes de un Comentario
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Comentario/{idComentario}/UsuarioLike/{idUsuarioLike}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComentarioRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ComentarioRequest>> ActualizarLikeComentario(long idComentario, string idUsuarioLike)
        {
            return await _bussines.ActualizarLikeComentario(idComentario, idUsuarioLike);
        }

        /// <summary>
        /// Obtiene todas los comentarios ordenados por fecha del mas reciente al mas viejo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("OrdenaRPorFecha")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ComentarioRequest>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ComentarioRequest>>> GetAllPorFecha()
        {

            return await _bussines.getTodasPorFechaAsync();
        }
    }
}
