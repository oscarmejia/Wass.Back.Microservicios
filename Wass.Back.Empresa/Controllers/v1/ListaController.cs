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
    public class ListaController : ControllerBase
    {

        private readonly BOListas _bussines;

        public ListaController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOListas(dataBase);
        }

        /// <summary>
        /// COnsulta una lista especifica
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("lista/{idLista}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Listas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Listas>> ObtenerInformacion(long idLista)
        {
            return await _bussines.GetAsync(idLista);
        }

        /// <summary>
        /// Consulta las sedes de una empresa
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listas")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Listas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Listas>>> ObtenerListas()
        {
            return await _bussines.GetAllAsync();
        }



    }
}
