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
    public class MenusController : ControllerBase
    {
        private readonly BOMenu _bussines;

        public MenusController(SeguridadContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOMenu(dataBase);
        }

        /// <summary>
        /// Consulta un menú especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("menu/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Menus>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Menus>> get(long id)
        {
            return await _bussines.GetAsync(id);
        }


        /// <summary>
        /// Consulta todos los menús disponibles sin importar su estado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("menus")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Menus>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Menus>>> ObtenerMenus()
        {
            return await _bussines.GetTodoAsync();
        }

        /// <summary>
        /// Consulta todos los menus padres
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("menus/padres")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Menus>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Menus>>> ObtenerPadres()
        {
            return await _bussines.GetPadresAsync();
        }

        /// <summary>
        /// Consulta los hijos de un menú padre
        /// </summary>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("menus/hijo/{idPadre}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Menus>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Menus>>> ObtenerHijos(long idPadre)
        {
            return await _bussines.GetHijosAsync(idPadre);
        }


        /// <summary>
        /// Crear una opción de menú
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Menus>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Menus>> crear([FromBody] RequestMenus menu)
        {
            return await _bussines.save(menu, Transaction.Insert);
        }

        /// <summary>
        /// Actualiza los datos de un menú
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Menus>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Menus>> actualizar([FromBody] RequestMenus menu)
        {
            return await _bussines.save(menu, Transaction.Update);
        }
    }
}