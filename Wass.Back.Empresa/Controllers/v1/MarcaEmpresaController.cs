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
    public class MarcaEmpresaController : ControllerBase
    {
        private readonly BOMarcaEmpresa _bussines;
        public MarcaEmpresaController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOMarcaEmpresa(dataBase);
        }

        [HttpGet]
        [Route("{idMarcaEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MarcaEmpresa>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<MarcaEmpresa>> Get(long idMarcaEmpresa)
        {
            return await _bussines.get(idMarcaEmpresa);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MarcaEmpresa>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<MarcaEmpresa>>> GetTodas()
        {
            return await _bussines.getTodas();
        }

        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MarcaEmpresa>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<MarcaEmpresa>>> GetTodasPorEmpresa(long idEmpresa)
        {
            return await _bussines.getTodasPorEmpresa(idEmpresa);
        }

        [HttpGet]
        [Route("marca/{idMarca}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MarcaEmpresa>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<MarcaEmpresa>>> GetTodasPorMarca(long idMarca)
        {
            return await _bussines.getTodasPorMarca(idMarca);
        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MarcaEmpresa>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<MarcaEmpresa>> Crear([FromBody] MarcaEmpresa marcaEmpresa)
        {
            return await _bussines.set(marcaEmpresa, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<MarcaEmpresa>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<MarcaEmpresa>> Editar([FromBody] MarcaEmpresa marcaEmpresa)
        {
            return await _bussines.set(marcaEmpresa, Transaction.Insert);
        }

    }
}
