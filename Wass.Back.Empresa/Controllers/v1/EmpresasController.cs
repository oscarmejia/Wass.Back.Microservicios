using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class EmpresasController : ControllerBase
    {

        private readonly BOEmpresa _bussines;
        private readonly IConfiguration _configuration;

        public EmpresasController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOEmpresa(dataBase, configuration);
        }

        /// <summary>
        /// Consulta una empresa en especifico
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empresas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObtenerInformacion(long idEmpresa)
        {
            var datos = await _bussines.GetEmpresaAsync(idEmpresa);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta todas las empresas creadas en WASS
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("todas")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Empresas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Todas()
        {
            var datos = await _bussines.GetEmpresasAsync();
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Consulta todas las empresas creadas en WASS
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("porTipo")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Empresas>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTipoAfiliacion(TipoAfiliacion tipoAfiliacion)
        {
            var datos = await _bussines.GetPorTipoAfiliacionAsync(tipoAfiliacion);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Crea una empresa
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empresas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] RequestEmpresa empresa)
        {
            var datos = await _bussines.SaveAsync(empresa, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }


        /// <summary>
        /// Actualiza los datos de una empresa
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empresas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] RequestEmpresa empresa)
        {
            var datos = await _bussines.EditarEmpresa(empresa, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Elimina de manera lógica una empresa
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empresas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> eliminar([FromBody] RequestEmpresa empresa)
        {
            var datos = await _bussines.EliminarEmpresa(empresa);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Enviar correo de solicitud de información
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("correo/solicitarinfo")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<(bool, string)>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CorreoSolicitarInfo([FromBody] RequestSolicitarInfo info)
        {
            var datos = await _bussines.CorreoSolicitarInfoAsync(info);
            return StatusCode(datos.codigo, datos);
        }
    }
}