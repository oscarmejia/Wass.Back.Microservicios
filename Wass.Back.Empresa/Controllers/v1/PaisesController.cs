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
    public class PaisesController : ControllerBase
    {
        private readonly BOPaises _bussines;
        private readonly IConfiguration _configuration;

        public PaisesController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOPaises(dataBase);
        }

        /// <summary>
        /// Consulta un pais en especifico
        /// </summary>
        /// <param name="idPais"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idPais}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Paises>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Paises>> Get(long idPais)
        {
            return await _bussines.GetAsync(idPais);
        }


        /// <summary>
        /// Consulta todos los paises disponibles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Paises>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Paises>>> getTodos()
        {
            return await _bussines.GetAllAsync();
        }

    }
}
