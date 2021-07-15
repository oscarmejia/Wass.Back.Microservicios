using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Bussines;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Agenda;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Models.Peticiones.BusquedaSkillsEmpresaLicitacion;
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BusquedaSkillsEmpresaLicitacionController : ControllerBase
    {
        private readonly BOBusquedaSkillsEmpresaLicitacion _BO;
        private readonly IConfiguration _configuration;

        public BusquedaSkillsEmpresaLicitacionController(ProgramadorContext context, IConfiguration configuration)
        {
            var dataBase = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _BO = new BOBusquedaSkillsEmpresaLicitacion(dataBase);
        }

        /// <summary>
        /// Buscar en Licitacion y Empresa
        /// </summary>
        /// <param name="buscar"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("buscar")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerator<ResponseBase<List<Licitacion>>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(BusquedaSkillsEmpresaLicitacionRequest buscar)
        {
            var datos = await _BO.Get(buscar);
            return StatusCode(datos.codigo, datos);
        }

        
    }
}