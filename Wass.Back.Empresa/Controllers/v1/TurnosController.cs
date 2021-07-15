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
using Wass.Back.Empresa.Models.Peticiones.v1.Turno;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly BOTurnos _bussines;
        private readonly IConfiguration _configuration;

        public TurnosController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOTurnos(dataBase);
        }

        /// <summary>
        /// Consulta un turno especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Turnos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Turnos>> Get(long id)
        {
            return await _bussines.GetAsync(id);
        }

        /// <summary>
        /// Consulta todos los turnos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Turnos>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Turnos>>> GetAll()
        {
            return await _bussines.GetAllAsync();
        }

        /// <summary>
        /// Crear un turno
        /// </summary>
        /// <param name="turno"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Turnos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Turnos>> crear([FromBody] Turnos turno)
        {
            return await _bussines.SetAsync(turno, Transaction.Insert);
        }

        /// <summary>
        /// Actualiza un turno
        /// </summary>
        /// <param name="turno"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Turnos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Turnos>> actualizar([FromBody] Turnos turno)
        {
            return await _bussines.SetAsync(turno, Transaction.Update);
        }

        /// <summary>
        /// Consulta un turno especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("cuadrilla/turnos/{idCuadrilla}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Turnos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Turnos>>> GetPorCuadrilla(long idCuadrilla)
        {
            return await _bussines.GetPorCuadrillaAsync(idCuadrilla);
        }

        /// <summary>
        /// Consulta un turno especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/turnos/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Turnos>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Turnos>>> GetgetPoEmpresa(long idEmpresa)
        {
            return await _bussines.GetPorEmpresaAsync(idEmpresa);
        }

        /// <summary>
        /// Asignar turnos a cuadrilla
        /// </summary>
        /// <param name="turno"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("cuadrilla/asignar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillasTurnos>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuadrillasTurnos>>> asignar([FromBody] RequestTurnosCuadrilla turnos)
        {
            return await _bussines.SetTurnosToCuadrillaAsync(turnos, Transaction.Insert);
        }

        /// <summary>
        /// Quitar turnos a cuadrilla
        /// </summary>
        /// <param name="turno"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("cuadrilla/quitar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillasTurnos>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CuadrillasTurnos>>> quitar([FromBody] RequestTurnosCuadrilla turnos)
        {
            return await _bussines.SetTurnosToCuadrillaAsync(turnos, Transaction.Delete);
        }
    }
}
