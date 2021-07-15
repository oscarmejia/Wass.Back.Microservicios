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
using Wass.Back.Programador.Rabbit.Context;

namespace Wass.Back.Programador.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class MantenimientoCondicionesController : ControllerBase
	{
		private readonly BOMantenimientoCondiciones _BO;
		private readonly IConfiguration _configuration;

		public MantenimientoCondicionesController(ProgramadorContext context, IConfiguration configuration)
		{
			var dataBase = context ?? throw new ArgumentNullException(nameof(context));
			_configuration = configuration;
			_BO = new BOMantenimientoCondiciones(dataBase);
		}

		[HttpGet]
		[Route("condiciones/{id}")]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerator<ResponseBase<CondicionesVariables>>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get(long id)
		{
			var datos = await _BO.Get(id);
			return StatusCode(datos.codigo, datos);
		}

		[HttpGet]
		[Route("condiciones/plan/{idPlan}")]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CondicionesVariables>>>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetByIdPlan(long idPlan)
		{
			var datos = await _BO.GetByIdPlan(idPlan);
			return StatusCode(datos.codigo, datos);
		}

		[HttpGet]
		[Route("condiciones")]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerator<ResponseBase<List<CondicionesVariables>>>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetAll()
		{
			var datos = await _BO.GetAll();
			return StatusCode(datos.codigo, datos);
		}

		[HttpPost]
		[Route("condiciones/crear")]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerator<ResponseBase<CondicionesVariables>>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Set([FromBody] CondicionesVariables objeto)
		{
			var datos = await _BO.Set(objeto, Transaction.Insert);
			return StatusCode(datos.codigo, datos);
		}

		[HttpPut]
		[Route("condiciones/actualizar")]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerator<ResponseBase<CondicionesVariables>>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Update([FromBody] CondicionesVariables objeto)
		{
			objeto.estado = true;
			var datos = await _BO.Set(objeto, Transaction.Update);
			return StatusCode(datos.codigo, datos);
		}

		[HttpPut]
		[Route("condiciones/activar/{id}")]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerator<ResponseBase<CondicionesVariables>>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Active(long id)
		{
			var datos = await SetAction(id, Transaction.Update, true);
			return StatusCode(datos.codigo, datos);
		}

		[HttpPut]
		[Route("condiciones/inactivar/{id}")]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerator<ResponseBase<CondicionesVariables>>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Inactive(long id)
		{
			var datos = await SetAction(id, Transaction.Update, false);
			return StatusCode(datos.codigo, datos);
		}

		[HttpDelete]
		[Route("condiciones/eliminar/{id}")]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerator<ResponseBase<bool>>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Delete(long id)
		{
			var datos = await SetAction(id, Transaction.Delete, false);
			var response = new ResponseBase<bool>();
			if (datos.codigo == 200)
			{
				response.codigo = 200;
				response.datos = true;
				response.mensaje = $"Condicion {id} eliminada";
			}
			else
			{
				response.codigo = 500;
				response.datos = false;
				response.mensaje = $"No se pudo eliminar la condicion: {datos.mensaje}";
			}
			return StatusCode(datos.codigo, datos);
		}

		private async Task<ResponseBase<CondicionesVariables>> SetAction(long id, Transaction transact, bool estado)
		{
			var find = await _BO.Get(id);
			if (find.codigo == (int)HttpStatusCode.OK)
			{
				var objeto = find.datos;
				objeto.estado = estado;
				var datos = await _BO.Set(objeto, transact);
				return datos;
			}
			else
			{
				return new ResponseBase<CondicionesVariables>()
				{
					codigo = 404,
					datos = null,
					mensaje = "la condición a eliminar no se encuentra registrada"
				};
			}
		}
	}
}
