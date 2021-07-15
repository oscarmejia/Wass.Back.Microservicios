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
    public class CentrosTrabajoController : Controller
    {

        private readonly BOCentrosTrabajo _bussines;


        public CentrosTrabajoController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOCentrosTrabajo(dataBase);
        }

        /// <summary>
        /// Consulta un empleado especifico
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CentrosTrabajo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CentrosTrabajo>> Get(long id)
        {
            return await _bussines.GetAsync(id);
        }


        /// <summary>
        /// Consulta todos los centros de trabajo disponibles sin importar su estado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CentrosTrabajo>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CentrosTrabajo>>> getTodos()
        {
            return await _bussines.GetAllAsync();
        }


        /// <summary>
        /// Consulta todos los centros de trabajo disponibles de una sede
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CentrosTrabajo>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CentrosTrabajo>>> getPorSedeAsync(long id)
        {
            return await _bussines.GetPorSedeAsync(id);
        }


        /// <summary>
        /// Consulta todos los centros de trabajo disponibles de una sede pero que estan activas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/activa/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CentrosTrabajo>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<CentrosTrabajo>>> getPorSedeActivaAsync(long id)
        {
            return await _bussines.GetPorSedeActivaAsync(id);
        }

        /// <summary>
        /// Crear un empleado
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CentrosTrabajo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CentrosTrabajo>> crear([FromBody] CentrosTrabajo datos)
        {
            return await _bussines.SetAsync(datos, Transaction.Insert);
        }

        /// <summary>
        /// Actualiza un empleado
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CentrosTrabajo>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<CentrosTrabajo>> actualizar([FromBody] CentrosTrabajo datos)
        {
            return await _bussines.SetAsync(datos, Transaction.Update);
        }

    }
}
