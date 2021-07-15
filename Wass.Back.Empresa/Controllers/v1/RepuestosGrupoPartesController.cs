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
    public class RepuestosGrupoPartesController : ControllerBase
    {

        private readonly BORepuestosGrupoPartes _bussines;

        public RepuestosGrupoPartesController(EmpresaContext context)
        {

            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORepuestosGrupoPartes(dataBase);
        }

        /// <summary>
        /// Consulta todos los Repuestos por grupo de partes
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosGrupoPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RepuestosGrupoPartes>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        /// <summary>
        /// Consulta todos los Repuestos por grupo de partes
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idRepuestosGrupoPartes}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosGrupoPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RepuestosGrupoPartes>> GetRepuestosGrupoParte(int idRepuestosGrupoPartes)
        {
            return await _bussines.GetRepuestosGrupoParte(idRepuestosGrupoPartes);
        }

        /// <summary>
        /// Consulta todos los Repuestos por grupo de partes por el idRepuestos
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("repuestos/{idRepuestos}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosGrupoPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RepuestosGrupoPartes>>> GetTodasByRepuesto(long idRepuestos)
        {
            return await _bussines.GetTodasByRepuestos(idRepuestos);
        }

        /// <summary>
        /// Consulta todos los Repuestos por grupo de partes por el idGrupo
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("grupo/parte/{idGrupo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosGrupoPartes>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RepuestosGrupoPartes>>> GetTodasByGrupoPartes(long idGrupo)
        {
            return await _bussines.GetTodasByGrupoPartes(idGrupo);
        }


        /// <summary>

        /// Crea una relacion entre repuestos y grupo de partes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RepuestosGrupoPartes>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RepuestosGrupoPartes>> crearRepuestosDiagnostico([FromBody] RepuestosGrupoPartes datos)
        {
            return await _bussines.guardarRepuestosGrupoPartes(datos, Transaction.Insert);

        }
    }
}
