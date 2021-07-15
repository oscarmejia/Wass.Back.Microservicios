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
    public class MunicipiosController : ControllerBase
    {
        private readonly BOMunicipios _bussines;
        private readonly IConfiguration _configuration;

        public MunicipiosController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOMunicipios(dataBase);
        }

        /// <summary>
        /// Consulta un departamento especifico
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idMunicipio}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Municipios>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Municipios>> Get(long idMunicipio)
        {
            return await _bussines.GetAsync(idMunicipio);
        }


        /// <summary>
        /// Consulta todos los departamentos disponibles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Municipios>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Municipios>>> getTodos()
        {
            return await _bussines.GetAllAsync();
        }

        /// <summary>
        /// Consulta todos los departamentos de un pais
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("depto/{idDepto}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Municipios>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Municipios>>> getPorDepartamentoAsync(long idDepto)
        {
            return await _bussines.GetPorDepartamentoAsync(idDepto);
        }
    }
}
