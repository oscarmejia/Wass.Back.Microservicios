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
    public class RepuestosDiagnosticoController : ControllerBase
    {
        private readonly BORepuestosDiagnostico _bussines;

        public RepuestosDiagnosticoController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BORepuestosDiagnostico(dataBase);
        }

        /// <summary>
        /// Consulta un RepuestosDiagnostico en especifico
        /// </summary>
        /// <param name="idRepuestosDiagnostico"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idRepuestosDiagnostico}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RepuestosDiagnostico>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RepuestosDiagnostico>> ObtenerInformacion(long idRepuestosDiagnostico)
        {
            return await _bussines.GetPorId(idRepuestosDiagnostico);
        }

        /// <summary>
        /// Consulta todos los RepuestosDiagnostico
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosDiagnostico>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RepuestosDiagnostico>>> ObtenerTodo()
        {
            return await _bussines.GetTodas();
        }


        /// <summary>
        /// Consulta todos los Diagnosticos asociados a un repuesto
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Repuesto/{idRepuesto}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosDiagnostico>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RepuestosDiagnostico>>> GetPorIdRepuestosDiagnostico(long idRepuesto)
        {
            return await _bussines.GetPorIdRepuestosDiagnostico(idRepuesto);
        }

        /// <summary>
        /// Consulta todos los Repuestos asociados a un Diagnostico
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Diagnostico/{idDiagnostico}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<RepuestosDiagnostico>>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<RepuestosDiagnostico>>> GetPorIdDiagnosticoRepuesto(long idDiagnostico)
        {
            return await _bussines.GetPorIdDiagnosticoRepuesto(idDiagnostico);
        }

        /// <summary>

        /// Crea un RepuestosDiagnostico
        /// </summary>
        /// <param name="RepuestosAlmacen"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RepuestosDiagnostico>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RepuestosDiagnostico>> crearRepuestosDiagnostico([FromBody] RepuestosDiagnostico datos)
        {
            return await _bussines.guardarRepuestosDiagnostico(datos, Transaction.Insert);

        }

        /// <summary>
        /// Actualiza los datos de un RepuestosDiagnostico
        /// </summary>
        /// <param name="RepuestosAlmacen"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RepuestosDiagnostico>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RepuestosDiagnostico>> actualizarRepuestosDiagnostico([FromBody] RepuestosDiagnostico datos)
        {
            return await _bussines.guardarRepuestosDiagnostico(datos, Transaction.Update);
        }

        /// <summary>
        /// Elimina de manera lógica un RepuestosDiagnostico
        /// </summary>
        /// <param name="idRepuestosDiagnostico"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idRepuestosDiagnostico}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<RepuestosDiagnostico>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<RepuestosDiagnostico>> eliminarRepuestosDiagnostico(long idRepuestosDiagnostico)
        {
            return await _bussines.EliminarRepuestosDiagnostico(idRepuestosDiagnostico);
        }
    }
}
