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
    public class ArchivosAdjuntosActivosFlotasController : ControllerBase
    {
        private readonly BOArchivosAdjuntosActivosFlotas _bussines;
        private readonly IConfiguration _configuration;

        public ArchivosAdjuntosActivosFlotasController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOArchivosAdjuntosActivosFlotas(dataBase);
        }


        [HttpGet]
        [Route("{idArchivosAdjuntosActivosFlotas}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ArchivosAdjuntosActivosFlotas>> Get(long idArchivosAdjuntosActivosFlotas)
        {
            return await _bussines.Get(idArchivosAdjuntosActivosFlotas);
        }

        [HttpGet]
        [Route("ActivosFlotas/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ArchivosAdjuntosActivosFlotas>>> GetPorActivoFlota(Guid idActivo)
        {
            return await _bussines.GetTodasPorActivosFlotas(idActivo);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ArchivosAdjuntosActivosFlotas>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ArchivosAdjuntosActivosFlotas>> CrearArchivo([FromBody] ArchivosAdjuntosActivosFlotas archivos)
        {
            return await _bussines.guardarArchivo(archivos, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ArchivosAdjuntosActivosFlotas>> EditarArchivo([FromBody] ArchivosAdjuntosActivosFlotas archivos)
        {
            return await _bussines.guardarArchivo(archivos, Transaction.Update);
        }

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="idArchivo"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idArchivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosFlotas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Eliminar(long idArchivo)
        {
            var datos = await _bussines.EliminarArchivo(idArchivo);
            return StatusCode(datos.codigo, datos);
        }
    }
}
