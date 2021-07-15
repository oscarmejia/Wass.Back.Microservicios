using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Bussines;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Agenda;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArchivosAdjuntosCotizacionController : ControllerBase
    {
        private readonly BOArchivosAdjuntosCotizacion _bussines;
        private readonly IConfiguration _configuration;

        public ArchivosAdjuntosCotizacionController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOArchivosAdjuntosCotizacion(dataBase);
        }


        [HttpGet]
        [Route("{idArchivoAdjuntoCotizacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosCotizacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ArchivosAdjuntosCotizacion>> Get(long idArchivoAdjuntoCotizacion)
        {
            return await _bussines.Get(idArchivoAdjuntoCotizacion);
        }

        [HttpGet]
        [Route("Cotizaciones/{idCotizacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosCotizacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ArchivosAdjuntosCotizacion>>> GetPorCotizacion(long idCotizacion)
        {
            return await _bussines.GetTodasPorCotizacion(idCotizacion);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosCotizacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ArchivosAdjuntosCotizacion>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosCotizacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ArchivosAdjuntosCotizacion>> CrearArchivo([FromBody] ArchivosAdjuntosCotizacion archivos)
        {
            return await _bussines.guardarArchivo(archivos, Transaction.Insert);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosCotizacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ArchivosAdjuntosCotizacion>> EditarSoporte([FromBody] ArchivosAdjuntosCotizacion archivos)
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
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ArchivosAdjuntosCotizacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Eliminar(long idArchivo)
        {
            var datos = await _bussines.EliminarArchivo(idArchivo);
            return StatusCode(datos.codigo, datos);
        }
    }
}
