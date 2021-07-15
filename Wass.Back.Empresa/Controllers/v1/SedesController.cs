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
using Wass.Back.Empresa.Models.Peticiones.v1.Empresa;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SedesController : ControllerBase
    {
        private readonly BOSedes _bussines;

        public SedesController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOSedes(dataBase);
        }

        /// <summary>
        /// Consulta una sede en especifico
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Sedes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Sedes>> ObtenerInformacion(long idSede)
        {
            return await _bussines.GetSedeAsync(idSede);
        }

        /// <summary>
        /// Consulta las sedes de una empresa
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Sedes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Sedes>>> ObtenerSedes(long idEmpresa)
        {
            return await _bussines.GetSedesAsync(idEmpresa);
        }


        /// <summary>
        /// Crea una sede
        /// </summary>
        /// <param name="RequestSede"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Sedes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Sedes>> crear([FromBody] RequestSede sede)
        {
            return await _bussines.SaveAsync(sede, Transaction.Insert);
        }

        /// <summary>
        /// Actualiza los datos de una sede
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Sedes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Sedes>> actualizar([FromBody] RequestSede sede)
        {
            return await _bussines.SaveAsync(sede, Transaction.Update);
        }

        /// <summary>
        /// Elimina de manera lógica una sede
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Sedes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Sedes>> eliminar([FromBody] RequestSede sede)
        {
            return await _bussines.SaveAsync(sede, Transaction.Delete);
        }
    }
}
