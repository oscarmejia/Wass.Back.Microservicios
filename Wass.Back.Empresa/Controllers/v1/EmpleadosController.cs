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
    public class EmpleadosController : ControllerBase
    {
        private readonly BOEmpleados _bussines;
        private readonly IConfiguration _configuration;

        public EmpleadosController(EmpresaContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _bussines = new BOEmpleados(dataBase);
        }

        /// <summary>
        /// Consulta un empleado especifico
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("idEmpleado/{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empleados>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Empleados>> Get(long id)
        {
            return await _bussines.GetAsync(id);
        }


        /// <summary>
        /// Consulta todos los empleados disponibles sin importar su estado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Empleados>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Empleados>>> getTodos()
        {
            return await _bussines.GetAllAsync();
        }

        /// <summary>
        /// Consulta todos los empleados en un cargo especifico
        /// </summary>
        /// <param name="idCargo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("cargo/{idCargo}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Empleados>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Empleados>>> getPorCargo(int idCargo)
        {
            return await _bussines.GetPorCargoAsync(idCargo);
        }


        /// <summary>
        /// Consulta todos los emplado en un estado especifico
        /// </summary>
        /// <param name="idEstado"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("estado/{idEstado}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Empleados>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Empleados>>> getPorEstado(int idEstado)
        {
            return await _bussines.GetPorEstadoAsync(idEstado);
        }

        /// <summary>
        /// Consulta todos los emplados de una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("empresa/{idEmpresa}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Empleados>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Empleados>>> getPorEmpresa(int idEmpresa)
        {
            return await _bussines.GetPorEmpresaAsync(idEmpresa);
        }

        /// <summary>
        /// Consulta todos los emplado de una sede especifica
        /// </summary>
        /// <param name="idEstado"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Empleados>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Empleados>>> getPorSede(long idSede)
        {
            return await _bussines.GetPorSedeAsync(idSede);
        }

        /// <summary>
        /// Empleados por numero de documento
        /// </summary>
        /// <param name="idTipoDocumento"></param>
        /// /// <param name="numDocumento"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("documento/{idTipoDocumento}/{numDocumento}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empleados>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Empleados>> getPorNumDocumento(int idTipoDocumento, string numDocumento)
        {
            return await _bussines.GetPorNumDocumentoAsync(idTipoDocumento, numDocumento);
        }

        /// <summary>
        /// Empleados en una sede y cargo especificos
        /// </summary>
        /// <param name="idTipoDocumento"></param>
        /// /// <param name="numDocumento"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("cargo/{idCargo}/sede/{idSede}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empleados>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Empleados>>> getPorSedeCargo(long idSede, int idCargo)
        {
            return await _bussines.GetPorSedeCargoAsync(idSede, idCargo);
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
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empleados>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Empleados>> crear([FromBody] Empleados empleado)
        {
            return await _bussines.SetAsync(empleado, Transaction.Insert);
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
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Empleados>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Empleados>> actualizar([FromBody] Empleados empleado)
        {
            return await _bussines.SetAsync(empleado, Transaction.Update);
        }


    }
}
