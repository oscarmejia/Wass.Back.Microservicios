using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class UsuarioRolesController : ControllerBase
    {
        private readonly BORoles _bussines;

        public UsuarioRolesController(SeguridadContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORoles(dataBase);
        }

        /// <summary>
        /// Consulta los roles de un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<UsuariosRoles>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<UsuariosRoles>>> ObtenerRolesUsuario(long idUsuario)
        {
            return await _bussines.Get(idUsuario);
        }

        /// <summary>
        /// Agregar roles a usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("crear")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<UsuariosRoles>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<UsuariosRoles>>> CrearUsuario([FromBody] RequestRoles datos)
        {
            return await _bussines.Set(datos, Transaction.Insert);
        }

        /// <summary>
        /// Agregar roles a usuario
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<UsuariosRoles>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<UsuariosRoles>> editarUsuario([FromBody] UsuariosRoles datos)
        {
            return await _bussines.Editar(datos);
        }
    }
}