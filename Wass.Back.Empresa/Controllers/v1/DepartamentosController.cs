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
    public class DepartamentosController : ControllerBase
    {
        private readonly BODepartamentos _bussines;
        private readonly IConfiguration _configuration;

        public DepartamentosController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BODepartamentos(dataBase);
        }

        /// <summary>
        /// Consulta un departamento especifico
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idDepto}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Departamentos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Departamentos>> Get(long idDepto)
        {
            return await _bussines.GetAsync(idDepto);
        }


        /// <summary>
        /// Consulta todos los departamentos disponibles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Departamentos>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Departamentos>>> getTodos()
        {
            return await _bussines.GetAllAsync();
        }

        /// <summary>
        /// Consulta todos los departamentos de un pais
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("pais/{idPais}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Departamentos>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Departamentos>>> getPorPaisAsync(long idPais)
        {
            return await _bussines.GetPorPaisAsync(idPais);
        }
    }
}
