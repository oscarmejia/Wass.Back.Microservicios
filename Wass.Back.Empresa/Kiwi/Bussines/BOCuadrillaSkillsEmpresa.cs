using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.CuadrillaSkillsEmpresa;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BOCuadrillaSkillsEmpresa
    {
		private readonly DALCCuadrillaSkillsEmpresa _dalc;

		public BOCuadrillaSkillsEmpresa(EmpresaContext context)
		{
			_dalc = new DALCCuadrillaSkillsEmpresa(context);
		}

		public async Task<ResponseBase<CuadrillaSkillsEmpresaRequest>> GetAsync(long idCuadrillaSkillsEmpresa)
		{
			try
			{
				var datos = await _dalc.GetAsync(idCuadrillaSkillsEmpresa);



				if (datos != null)
				{
					var ob = new CuadrillaSkillsEmpresaRequest()
					{
						idCuadrillaSkillsEmpresa = datos.idCuadrillaSkillsEmpresa,
						idCuadrilla = datos.idCuadrilla,
						idSkill = datos.idSkill,
						fechaHora = datos.fechaHora,
						idUsuario = datos.idUsuario,
						skillsSeleccionados = datos != null ? JsonConvert.DeserializeObject<List<string>>(datos.skillsSeleccionados) : new List<string>(),
					};
					return new ResponseBase<CuadrillaSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = ob
					};
				}
				else
				{
					return new ResponseBase<CuadrillaSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "CuadrillaSkillsEmpresa consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<CuadrillaSkillsEmpresaRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<CuadrillaSkillsEmpresaRequest>> GetPorSkillsCuadrillaAsync(long idSkill, long idCuadrilla)
		{
			try
			{
				var datos = await _dalc.GetPorSkillsCuadrillaAsync(idSkill, idCuadrilla);



				if (datos != null)
				{
					var ob = new CuadrillaSkillsEmpresaRequest()
					{
						idCuadrillaSkillsEmpresa = datos.idCuadrillaSkillsEmpresa,
						idCuadrilla = datos.idCuadrilla,
						idSkill = datos.idSkill,
						fechaHora = datos.fechaHora,
						idUsuario = datos.idUsuario,
						skillsSeleccionados = datos != null ? JsonConvert.DeserializeObject<List<string>>(datos.skillsSeleccionados) : new List<string>(),
					};
					return new ResponseBase<CuadrillaSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = ob
					};
				}
				else
				{
					return new ResponseBase<CuadrillaSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "CuadrillaSkillsEmpresa consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<CuadrillaSkillsEmpresaRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		public async Task<ResponseBase<List<CuadrillaSkillsEmpresaRequest>>> GetAllAsync()
		{
			try
			{
				var obj = await _dalc.GetAllAsync();
				var ob = new List<CuadrillaSkillsEmpresaRequest>();
				if (obj != null)
				{
					foreach (var item in obj)
					{
						ob.Add(new CuadrillaSkillsEmpresaRequest()
						{
							idCuadrillaSkillsEmpresa = item.idCuadrillaSkillsEmpresa,
							idCuadrilla = item.idCuadrilla,
							idSkill = item.idSkill,
							fechaHora = item.fechaHora,
							idUsuario = item.idUsuario,
							skillsSeleccionados = item != null ? JsonConvert.DeserializeObject<List<string>>(item.skillsSeleccionados) : new List<string>(),
						});
					}
					if (obj.Count > 0)
						return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = ob
						};
					else
						return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay CuadrillaSkillsEmpresa disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de CuadrillaSkillsEmpresa no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<CuadrillaSkillsEmpresaRequest>>> GetPorCuadrillaAsync(long idCuadrilla)
		{
			try
			{
				var obj = await _dalc.GetPorCuadrillaAsync(idCuadrilla);
				var ob = new List<CuadrillaSkillsEmpresaRequest>();
				if (obj != null)
				{
					foreach (var item in obj)
					{
						ob.Add(new CuadrillaSkillsEmpresaRequest()
						{
							idCuadrillaSkillsEmpresa = item.idCuadrillaSkillsEmpresa,
							idCuadrilla = item.idCuadrilla,
							idSkill = item.idSkill,
							fechaHora = item.fechaHora,
							idUsuario = item.idUsuario,
							skillsSeleccionados = item != null ? JsonConvert.DeserializeObject<List<string>>(item.skillsSeleccionados) : new List<string>(),
						});
					}
					if (obj.Count > 0)
						return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = ob
						};
					else
						return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay CuadrillaSkillsEmpresa disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de CuadrillaSkillsEmpresa no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<CuadrillaSkillsEmpresaRequest>>> GetPorSkillAsync(long idSkill)
		{
			try
			{
				var obj = await _dalc.GetPorSkillAsync(idSkill);
				var ob = new List<CuadrillaSkillsEmpresaRequest>();
				if (obj != null)
				{
					foreach (var item in obj)
					{
						ob.Add(new CuadrillaSkillsEmpresaRequest()
						{
							idCuadrillaSkillsEmpresa = item.idCuadrillaSkillsEmpresa,
							idCuadrilla = item.idCuadrilla,
							idSkill = item.idSkill,
							fechaHora = item.fechaHora,
							idUsuario = item.idUsuario,
							skillsSeleccionados = item != null ? JsonConvert.DeserializeObject<List<string>>(item.skillsSeleccionados) : new List<string>(),
						});
					}
					if (obj.Count > 0)
						return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = ob
						};
					else
						return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay CuadrillaSkillsEmpresa disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de CuadrillaSkillsEmpresa no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<CuadrillaSkillsEmpresaRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}
		public async Task<ResponseBase<CuadrillaSkillsEmpresaRequest>> SetAsync(CuadrillaSkillsEmpresaRequest objeto, Transaction transaccion)
		{
			try
			{
				var ob = new CuadrillaSkillsEmpresa()
				{
					idCuadrillaSkillsEmpresa = objeto.idCuadrillaSkillsEmpresa,
					idCuadrilla = objeto.idCuadrilla,
					idSkill = objeto.idSkill,
					fechaHora = objeto.fechaHora,
					idUsuario = objeto.idUsuario,
					skillsSeleccionados = JsonConvert.SerializeObject(objeto.skillsSeleccionados).ToString(),
				};
				var data = await _dalc.SetAsync(ob, transaccion);

				var respuesta = new CuadrillaSkillsEmpresaRequest()
				{
					idCuadrillaSkillsEmpresa = data.idCuadrillaSkillsEmpresa,
					idCuadrilla = data.idCuadrilla,
					idSkill = data.idSkill,
					fechaHora = data.fechaHora,
					idUsuario = data.idUsuario,
					skillsSeleccionados = data != null ? JsonConvert.DeserializeObject<List<string>>(data.skillsSeleccionados) : new List<string>(),
				};
				if (data != null)
				{
					return new ResponseBase<CuadrillaSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación realizada con exito",
						datos = respuesta
					};
				}
				else
					return new ResponseBase<CuadrillaSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación solicitada no se pudo realizar.",
						datos = null
					};

			}
			catch (Exception ex)
			{
				return new ResponseBase<CuadrillaSkillsEmpresaRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}
	}
}
