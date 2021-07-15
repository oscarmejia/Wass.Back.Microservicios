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
    public class CargosController : ControllerBase
    {
        private readonly BOCargos _bussines;
        private readonly IConfiguration _configuration;

        public CargosController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOCargos(dataBase);
        }

        /// <summary>
        /// Consulta un cargo en especifico
        /// </summary>
        /// <param name="idPais"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCargo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cargos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Cargos>> Get(long idCargo)
        {
            return await _bussines.GetAsync(idCargo);
        }


        /// <summary>
        /// Consulta todos los cargos disponibles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cargos>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cargos>>> getTodos()
        {
            return await _bussines.GetAllAsync();
        }

    }
}
