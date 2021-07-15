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
    public class ActivosClasificacionDiagnosticosSkillsController : ControllerBase
    {

        private readonly BOActivosClasificacionDiagnosticosSkills _bussines;

        public ActivosClasificacionDiagnosticosSkillsController(EmpresaContext context)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _bussines = new BOActivosClasificacionDiagnosticosSkills(dataBase);
        }

        /// <summary>
        /// Consulta una diagnostico y skill asociado a una clasificación
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticosSkills>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> get(long id)
        {
            var datos = await _bussines.GetAsync(id);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta una diagnostico y skill especifica a esta convinación
        /// </summary>
        /// <param name="idActivo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("diagnostico/{idDiagnostico}/skill/idSkill")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticosSkills>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorSkillDiagnosticoAsync(long idDiagnostico, long idSkill)
        {
            var datos = await _bussines.GetPorSkillDiagnosticoAsync(idDiagnostico, idSkill);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta los skills que tiene atados los diagnocticos y estan asociadas a todas las calsifiaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getTodasAsync()
        {
            var datos = await _bussines.GetAllAsync();
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta los skills que tiene un diagnostico asociado y esta atado a todas las calsifiaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("diagnostico/idDiagnostico")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorDiagnosticoAsync(long idDiagnostico)
        {
            var datos = await _bussines.GetPorDiagnosticoAsync(idDiagnostico);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Consulta los skills que tiene un diagnostico asociado y esta atado a todas las calsifiaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("skill/idSkill")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<ActivosClasificacionDiagnosticosSkills>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getPorSkillAsync(long idSkill)
        {
            var datos = await _bussines.GetPorSkillAsync(idSkill);
            return StatusCode(datos.codigo, datos);
        }

        /// <summary>
        /// Crea un skill / diagnostico y asociarndolo a una clasificación
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticosSkills>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> crear([FromBody] ActivosClasificacionDiagnosticosSkills datos)
        {
            var datos_actualizados = await _bussines.SetAsync(datos, Transaction.Insert);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// CActualiza un diagnostico / skill especifico.
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticosSkills>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> actualizar([FromBody] ActivosClasificacionDiagnosticosSkills datos)
        {
            var datos_actualizados = await _bussines.SetAsync(datos, Transaction.Update);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);
        }

        /// <summary>
        /// Elimina de manera lógica un diagnostico / skill
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<ActivosClasificacionDiagnosticosSkills>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> eliminar([FromBody] ActivosClasificacionDiagnosticosSkills datos)
        {
            var datos_actualizados = await _bussines.SetAsync(datos, Transaction.Delete);
            return StatusCode(datos_actualizados.codigo, datos_actualizados);

        }
    }
}
