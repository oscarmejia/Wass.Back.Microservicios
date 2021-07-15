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
    public class RecomendacionesController : ControllerBase
    {

        private readonly BORecomendaciones _bussines;

        public RecomendacionesController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORecomendaciones(dataBase);
        }

        [HttpGet]
        [Route("{idRecomendacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Recomendaciones>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Recomendaciones>> GetPorId(int idRecomendacion)
        {
            return await _bussines.GetRecomendacionAsyn(idRecomendacion);
        }


        [HttpGet]
        [Route("empresa/recomendada/{idEmpresaRecomendada}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Recomendaciones>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Recomendaciones>>> GetPorProvedor(long idEmpresaRecomendada)
        {
            return await _bussines.GetTodasPorEmpresaRecomenda(idEmpresaRecomendada);
        }


        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Recomendaciones>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Recomendaciones>>> GetTodas()
        {
            return await _bussines.GetRecomendacionesAsync();
        }




        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Recomendaciones>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Recomendaciones>> Crear([FromBody] Recomendaciones recomendaciones)
        {
            return await _bussines.SaveRecomendacionAync(recomendaciones, Transaction.Insert);
        }



    }
}
