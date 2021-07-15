using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Bussines;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Models.Peticiones.Licitacion;

namespace Wass.Back.Programador.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LicitacionController : ControllerBase
    {

        private readonly BOLicitacion _bussines;
        private readonly IConfiguration _configuration;
        public LicitacionController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOLicitacion(dataBase);
        }
        // GET: api/values

        [HttpGet]
        [Route("{idLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<LicitacionRequest>> Get(long idLicitacion)
        {
            return await _bussines.Get(idLicitacion);
        }

        [HttpGet]
        [Route("SumaCostosUltimoAnio/empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionSumaUltimoAnio>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<LicitacionSumaUltimoAnio>> GetSumaUltimoAnio(long idEmpresa)
        {
            return await _bussines.GetSumaUltimoAnio(idEmpresa);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<LicitacionRequest>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpGet]
        [Route("invitacion/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<LicitacionRequest>>> GetAllByInvitation(long idEmpresa)
        {
            return await _bussines.GetAllByInvitation(idEmpresa);
        }

        [HttpGet]
        [Route("skill/{idSkill}/sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<LicitacionRequest>>> GetAllSkillSedePais(long idSkill, long idSede)
        {
            return await _bussines.GetAllBySkillSedePais(idSkill, idSede);
        }


        [HttpGet]
        [Route("orden/{idOrden}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<LicitacionRequest>> GetPorOrden(long idOrden)
        {
            return await _bussines.GetIdOrden(idOrden);
        }

        [HttpGet]
        [Route("sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<LicitacionRequest>>> GetPorSede(long idSede)
        {
            return await _bussines.GetTodasPorSede(idSede);
        }

        [HttpGet]
        [Route("ActivosEnOrdenTrabajoAsociada/sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivoEnOrdenTrabajo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ActivoEnOrdenTrabajo>>> GetActivosEnOrdenTrabajoSede(long idSede)
        {
            return await _bussines.GetActivosEnOrdenTrabajoSede(idSede);
        }

        [HttpGet]
        [Route("ActivosEnOrdenTrabajoAsociada/licitacion/{idLicitacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivoEnOrdenTrabajo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ActivoEnOrdenTrabajo>> GetActivosEnOrdenTrabajoLicitacion(long idLicitacion)
        {
            return await _bussines.GetActivosEnOrdenTrabajoLicitacion(idLicitacion);
        }

        [HttpGet]
        [Route("Compras/ComprasPorSede/sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ComprasPorSede>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<ComprasPorSede>> GetComprasPorSede(long idSede)
        {
            return await _bussines.GetComprasPorSede(idSede);
        }

        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<LicitacionRequest>>> GetPorEmpresa(long idEmpresa)
        {
            return await _bussines.GetTodasPorEmpresa(idEmpresa);
        }

      

        [HttpGet]
        [Route("SumaCostoMesAMes/empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionSuma>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<LicitacionSuma>>> GetSumaMesAMes(long idEmpresa)
        {
            return await _bussines.GetSumaMesAMesPorAnio(idEmpresa);
        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<LicitacionRequest>> CrearLicitacion([FromBody] LicitacionRequest licitacion)
        {
            return await _bussines.guardarLicitacion(licitacion, Transaction.Insert);
        }


        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<LicitacionRequest>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<LicitacionRequest>> EditarLicitacion([FromBody] LicitacionRequest licitacion)
        {
            return await _bussines.guardarLicitacion(licitacion, Transaction.Update);
        }

       
       
    }
}
