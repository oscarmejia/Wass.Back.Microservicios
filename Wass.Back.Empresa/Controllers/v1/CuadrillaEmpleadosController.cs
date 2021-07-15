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
    public class CuadrillaEmpleadosController : ControllerBase
    {
        private readonly BOCuadrillaEmpleados _bussines;
        private readonly IConfiguration _configuration;

        public CuadrillaEmpleadosController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOCuadrillaEmpleados(dataBase);
        }

        /// <summary>
        /// Consulta la cuadrilla a la que pertenece un empleado
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Empleado/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillaEmpleados>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmpleado(long id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta una cuadrilla en especifico
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillaEmpleados>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta los empleados de una cuadrilla
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Empleados/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaEmpleados>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorCuadrillaAsync(long id)
        {
            var datos = await _bussines.GetPorCuadrillaAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta los empleados de una cuadrilla
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CuadrillaEmpleados>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Asociar un empleado a una cuadrilla
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Cuadrillas>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] CuadrillaEmpleados cuadrilla)
        {
            var datos = await _bussines.SetAsync(cuadrilla, Transaction.Insert);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Actualiza un empleado de una cuadrilla
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillaEmpleados>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] CuadrillaEmpleados cuadrilla)
        {
            var datos = await _bussines.SetAsync(cuadrilla, Transaction.Update);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Elimina de manera lógica en empleado asociado a una cuadrilla
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<CuadrillaEmpleados>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> eliminar([FromBody] CuadrillaEmpleados cuadrilla)
        {
            var datos = await _bussines.SetAsync(cuadrilla, Transaction.Delete);
            return StatusCode(datos.codigo, datos);
        }
    }
}
