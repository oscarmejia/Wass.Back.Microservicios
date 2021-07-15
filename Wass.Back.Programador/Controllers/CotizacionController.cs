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
using Wass.Back.Programador.Models.Peticiones.Cotizaciones;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{

    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        private readonly BOCotizaciones _BO;
        private readonly IConfiguration _configuration;

        public CotizacionController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _BO = new BOCotizaciones(dataBase);
        }

        /// <summary>
        /// Consulta una cotizacion especifica con todos sus detalles
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCotizacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cotizaciones>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Cotizaciones>> Get(long idCotizacion)
        {
            
            return await _BO.getAsync(idCotizacion);
        }


        /// <summary>
        /// Obtiene todas las cotizaciones creadas
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cotizaciones>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cotizaciones>>> GetAll()
        {
            
            return await _BO.getTodasAsync();
        }

        

        /// <summary>
        /// Obtiene todas las cotizaciones asociadas a una sede
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cotizaciones>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cotizaciones>>> GetAllPorSede(long idSede)
        {

            return await _BO.getTodasPorSedeAsync(idSede);
        }

        /// <summary>
        /// Obtiene todas las cotizaciones asociadas a una empresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cotizaciones>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cotizaciones>>> GetAllPorEmpresa(long idEmpresa)
        {

            return await _BO.getTodasPorEmpresaAsync(idEmpresa);
        }

        /// <summary>
        /// Obtiene todas las cotizaciones asociadas a una empresa
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}/estadoCotizacion/{sC}/estadoLicitacion/{sL}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cotizaciones>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cotizaciones>>> GetByEmpresaYEstado(long idEmpresa, long sC, long sL)
        {
            return await _BO.getByInterpriseAndState(idEmpresa, sC, sL);
        }

        [HttpGet]
        [Route("empresa/ordenpago/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cotizaciones>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cotizaciones>>> GetAllPorEmpresaPago(long idEmpresa)
        {

            return await _BO.getTodasPorEmpresaPago(idEmpresa);
        }
        [HttpGet]
        [Route("ordenpago/{idOrdenPago}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cotizaciones>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cotizaciones>>> GetAllPorOrdenago(long idOrdenPago)
        {

            return await _BO.getTodasPago(idOrdenPago);
        }

        /// <summary>
        /// Crear Cotizacion
        /// </summary>
        /// 
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cotizaciones>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Crear([FromBody] Cotizaciones dato)
        {

            var datos = await _BO.setAsync(dato, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        [HttpGet]
        [Route("Licitacion/{idLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Cotizaciones>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Cotizaciones>>> GetTodasPorLicitacion(long idLicitacion)
        {
            return await _BO.GetPorLicitacion(idLicitacion);
        }

        [HttpGet]
        [Route("SumaCostoMesAMes/Licitacion/{idLicitacion}/empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CotizacionesResponse>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CotizacionesResponse>>> GetSumaMesAMes(long idLicitacion, long idEmpresa)
        {
            return await _BO.GetSumaMesAMesPorAnio(idLicitacion, idEmpresa);
        }


        /// <summary>
        /// Actualiza una cotizacion
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cotizaciones>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Actualizar([FromBody] Cotizaciones dato)
        {
            var datos = await _BO.setAsync(dato, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }
               /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idCotizacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cotizaciones>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Eliminar(long idCotizacion)
        {
            var datos = await _BO.EliminarCotizacion(idCotizacion);
            return StatusCode(datos.codigo, datos);
        }
    }
}