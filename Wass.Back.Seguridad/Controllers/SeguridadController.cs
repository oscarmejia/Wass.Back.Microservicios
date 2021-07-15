using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wass.Back.Seguridad.Kiwi.Bussines;
using Wass.Back.Seguridad.Models.Entity;
using Wass.Back.Seguridad.Models.Enum;
using Wass.Back.Seguridad.Models.Peticiones.Base;
using Wass.Back.Seguridad.Models.Peticiones.Usuario;
using Wass.Back.Seguridad.Rabbit.Context;

namespace Wass.Back.Seguridad.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        private readonly BOUsuario _bussines;

        public SeguridadController(SeguridadContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOUsuario(dataBase, configuration);
        }

        /// <summary>
        /// Consulta el usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        ///
        [HttpGet]
        [Route("usuario/{idUsuario}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Usuarios>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Usuarios>> ObtenerInformacionUsuarioNuevo(long idUsuario)
        {
            return await _bussines.GetUsuarioId(idUsuario);
        }

        [HttpGet]
        [Route("usuarioProspecto/empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Usuarios>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Usuarios>> ObtenerInformacionUsuarioNuevoPorEmpresa(long idEmpresa)
        {
            return await _bussines.GetUsuarioProspectoIdEmpresa(idEmpresa);
        }

        [HttpGet]
        [Route("usuario/empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Usuarios>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Usuarios>>> ObtenerInformacionUsuarioPorEmpresa(long idEmpresa)
        {
            return await _bussines.GetUsuarioIdEmpresa(idEmpresa);
        }



        /// <summary>
        /// Consulta la autenticidad de un usuario con usuario y contraseña
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("usuario")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseUsuario>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ResponseUsuario>> ObtenerInformacion([FromBody] RequestAutentica datos)
        {
            return await _bussines.GetAutentica(datos);
        }

        /// <summary>
        /// Consulta la autenticidad de un usuario con usuario y contraseña
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("validarToken/{token}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseUsuario>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ResponseValidarToken>> ValidarToken(string token)
        {
            return await _bussines.ValidarToken(token);
        }


        /// <summary>
        /// Consulta la autenticidad de un usuario con usuario y contraseña
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ForgotPassword/{email}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Usuarios>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Usuarios>> forgotPassword(string email)
        {
            return await _bussines.sendEmailForPassword(email);
        }

        /// <summary>
        /// Crear un usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("crear")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseUsuario>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Usuarios>> CrearUsuario([FromBody] Usuarios datos)
        {
            return await _bussines.CrearUsuario(datos, Transaction.Insert);
        }

        /// <summary>
        /// Crear un usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("asociar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseUsuario>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Usuarios>> AsociarUsuarioEmpresa([FromBody] RequestAsociar datos)
        {
            return await _bussines.AsociarUsuario(datos);
        }

        /// <summary>
        /// Cambiar contraseña del usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("contrasena")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseUsuario>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Usuarios>> CambiarContrasena([FromBody] RequestContrasena datos)
        {
            return await _bussines.CambiarContrasena(datos);
        }
    }
}