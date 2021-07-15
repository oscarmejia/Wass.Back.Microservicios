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
    public class MarcaController : ControllerBase
    {

        private readonly BOMarca _bussines;
        public MarcaController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOMarca(dataBase);
        }

        [HttpGet]
        [Route("{idMarca}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Marca>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Marca>> Get(long idMarca)
        {
            return await _bussines.getMarca(idMarca);
        }

        [HttpGet]
        [Route("{idSubMarca}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Marca>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Marca>> GetSub(long idSubMarca)
        {
            return await _bussines.getSubMarca(idSubMarca);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Marca>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Marca>>> GetTodas()
        {
            return await _bussines.getMarcas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Marca>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Marca>> Crear([FromBody] Marca marca)
        {
            return await _bussines.Guardar(marca, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Marca>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Marca>> Editar([FromBody] Marca marca)
        {
            return await _bussines.Guardar(marca, Transaction.Update);
        }
    }
}
