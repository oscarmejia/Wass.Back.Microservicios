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
    public class EmpresaDocumentosController : ControllerBase
    {
        private readonly BOEmpresaSoportes _bussines;

        public EmpresaDocumentosController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOEmpresaSoportes(dataBase);
        }

        /// <summary>
        /// COnsulta el primer documetno almacenado de una empresa
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("EmpresaSoportes/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<EmpresaSoportes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<EmpresaSoportes>> ObtenerInformacion(long id)
        {
            return await _bussines.GetAsync(id);
        }

        /// <summary>
        /// Consulta todos los sooportes almacenados para todas las empresas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DocumentosEmpresas")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<EmpresaSoportes>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<EmpresaSoportes>>> ObtenerListas()
        {
            return await _bussines.GetAllAsync();
        }

        /// <summary>
        /// Consulta todos los documentos asociados a una empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DocumentoEmpresa/Empresa/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<EmpresaSoportes>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<EmpresaSoportes>>> ObtenerListas(long id)
        {
            return await _bussines.GetPorEmpresaAsync(id);
        }

        /// <summary>
        /// Consulta los documentos asociados a un activo equipo especifico
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DocumentoEmpresa/ActivoEquipo/Guid/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<EmpresaSoportes>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<EmpresaSoportes>>> ObtenerActivosEquipoListas(Guid id)
        {
            return await _bussines.GetPorActivoEquipoAsync(id);
        }

        /// <summary>
        /// Consulta los documentos asociados a un activo equipo especifico
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DocumentoEmpresa/ActivoFlota/Guid/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<EmpresaSoportes>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<EmpresaSoportes>>> ObtenerActivosFlotaListas(Guid id)
        {
            return await _bussines.GetPorActivoFlotaAsync(id);
        }

        /// <summary>
        /// Consulta el doucmento por su Guid
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DocumentoEmpresa/Guid/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<EmpresaSoportes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<EmpresaSoportes>> ObtenerListas(Guid id)
        {
            return await _bussines.GetAsync(id);
        }

        /// <summary>
        /// Crea un documento para asociarlo a la empresa
        /// </summary>
        /// <param name="RequestSede"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<EmpresaSoportes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<EmpresaSoportes>> crear([FromBody] EmpresaSoportes objeto)
        {
            return await _bussines.SetAsync(objeto, Transaction.Insert);
        }

        /// <summary>
        /// Actualiza los datos de un documento atado a una empresa
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<EmpresaSoportes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<EmpresaSoportes>> actualizar([FromBody] EmpresaSoportes objeto)
        {
            return await _bussines.SetAsync(objeto, Transaction.Update);
        }

        /// <summary>
        /// Elimina de manera lógica un documento atado a una empresa
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<EmpresaSoportes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<EmpresaSoportes>> eliminar([FromBody] EmpresaSoportes sede)
        {
            return await _bussines.SetAsync(sede, Transaction.Delete);
        }
    }
}
