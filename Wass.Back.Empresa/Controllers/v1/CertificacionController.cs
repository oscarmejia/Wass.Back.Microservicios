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
using Wass.Back.Empresa.Models.Peticiones.v1.Certificacion;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CertificacionController : ControllerBase
    {

        private readonly BOCertificacion _bussines;

        public CertificacionController(EmpresaContext context)
        {
            var database = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOCertificacion(database);
        }



        [HttpGet]
        [Route("{idCertificacion}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Certificacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Certificacion>> GetPotId(int idCertificacion)
        {
            return await _bussines.Get(idCertificacion);
        }


        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Certificacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Certificacion>>> GetTodas()
        {
            return await _bussines.GetTodas();
        }

        [HttpGet]
        [Route("empresa/idEmpresa")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Certificacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Certificacion>>> GetTodasPorEmpresa(long idEmpresa)
        {
            return await _bussines.GetTodasPorEmpresa(idEmpresa);
        }

        [HttpGet]
        [Route("empleado/idEmpleado")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Certificacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<Certificacion>>> GetTodasPorEmpleado(long idEmpleado)
        {
            return await _bussines.GetTodasPorEmpleado(idEmpleado);
        }

        [HttpGet]
        [Route("certificados/empresa/idEmpresa")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ResponseCertificacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<List<ResponseCertificacion>>> GetTodasPorGeneral(long idEmpresa)
        {
            return await _bussines.GetTodasGeneralEmpresa(idEmpresa);
        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Certificacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Certificacion>> CrearCertificacion([FromBody] Certificacion certificacion)
        {
            return await _bussines.SaveCertificacion(certificacion, Transaction.Insert);
        }


        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Certificacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Certificacion>> ActualizarCertificacion([FromBody] Certificacion certificacion)
        {
            return await _bussines.SaveCertificacion(certificacion, Transaction.Update);
        }

        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<Certificacion>>), (int)HttpStatusCode.OK)]
        public async Task<ResponseBase<Certificacion>> EliminarCertificacion([FromBody] Certificacion certificacion)
        {
            return await _bussines.EliminarCertificado(certificacion);
        }


    }
}
