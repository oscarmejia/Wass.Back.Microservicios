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
    public class ArchivosAdjuntosActivosEquiposController : ControllerBase
    {
        private readonly BOArchivosAdjuntosActivosEquipos _bussines;
        private readonly IConfiguration _configuration;

        public ArchivosAdjuntosActivosEquiposController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOArchivosAdjuntosActivosEquipos(dataBase);
        }


        [HttpGet]
        [Route("{idArchivosAdjuntosActivosEquipos}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ArchivosAdjuntosActivosEquipos>> Get(long idArchivosAdjuntosActivosEquipos)
        {
            return await _bussines.Get(idArchivosAdjuntosActivosEquipos);
        }

        [HttpGet]
        [Route("ActivosEquipos/{idActivo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ArchivosAdjuntosActivosEquipos>>> GetPorActivoEquipo(Guid idActivo)
        {
            return await _bussines.GetTodasPorActivosEquipos(idActivo);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ArchivosAdjuntosActivosEquipos>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ArchivosAdjuntosActivosEquipos>> CrearArchivo([FromBody] ArchivosAdjuntosActivosEquipos archivos)
        {
            return await _bussines.guardarArchivo(archivos, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ArchivosAdjuntosActivosEquipos>> EditarArchivo([FromBody] ArchivosAdjuntosActivosEquipos archivos)
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
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosActivosEquipos>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Eliminar(long idArchivo)
        {
            var datos = await _bussines.EliminarArchivo(idArchivo);
            return StatusCode(datos.codigo, datos);
        }
    }
}
