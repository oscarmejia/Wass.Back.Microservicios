using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wass.Back.Empresa.Models.Entity;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;
using Wass.Back.Empresa.Models.Peticiones.v1.DiagnosticoSkillsEmpresa;
using Wass.Back.Empresa.Rabbit.Context;
using Wass.Back.Empresa.Rabbit.DALC;
namespace Wass.Back.Empresa.Kiwi.Bussines
{
    public class BODiagnosticoSkillsEmpresa
    {
		private readonly DALCDiagnosticoSkillsEmpresa _dalc;

		public BODiagnosticoSkillsEmpresa(EmpresaContext context)
		{
			_dalc = new DALCDiagnosticoSkillsEmpresa(context);
		}

		public async Task<ResponseBase<DiagnosticoSkillsEmpresaRequest>> GetAsync(long idDiagnosticoSkillsEmpresa)
		{
			try
			{
				var datos = await _dalc.GetAsync(idDiagnosticoSkillsEmpresa);

				if (datos != null)
				{
					var ob = new DiagnosticoSkillsEmpresaRequest()
					{
						idDiagnosticoSkillsEmpresa = datos.idDiagnosticoSkillsEmpresa,
						idDiagnostico = datos.idDiagnostico,
						idSkill = datos.idSkill,
						fechaHora = datos.fechaHora,
						idUsuario = datos.idUsuario,
						skillsSeleccionados = datos != null ? JsonConvert.DeserializeObject<List<string>>(datos.skillsSeleccionados) : new List<string>(),
					};
					return new ResponseBase<DiagnosticoSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = ob
					};
				}
				else
				{
					return new ResponseBase<DiagnosticoSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "DiagnosticoSkillsEmpresa consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<DiagnosticoSkillsEmpresaRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<DiagnosticoSkillsEmpresaRequest>> GetPorSkillDiagnosticoAsync(long idSkill, long idDiagnostico)
		{
			try
			{
				var datos = await _dalc.GetPorSkillDiagnosticoAsync(idSkill, idDiagnostico);

				if (datos != null)
				{
					var ob = new DiagnosticoSkillsEmpresaRequest()
					{
						idDiagnosticoSkillsEmpresa = datos.idDiagnosticoSkillsEmpresa,
						idDiagnostico = datos.idDiagnostico,
						idSkill = datos.idSkill,
						fechaHora = datos.fechaHora,
						idUsuario = datos.idUsuario,
						skillsSeleccionados = datos != null ? JsonConvert.DeserializeObject<List<string>>(datos.skillsSeleccionados) : new List<string>(),
					};
					return new ResponseBase<DiagnosticoSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = ob
					};
				}
				else
				{
					return new ResponseBase<DiagnosticoSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = "DiagnosticoSkillsEmpresa consultado no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<DiagnosticoSkillsEmpresaRequest>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}


		public async Task<ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>> GetAllAsync()
		{
			try
			{
				var obj = await _dalc.GetAllAsync();
				var ob = new List<DiagnosticoSkillsEmpresaRequest>();
				if (obj != null)
				{
					foreach (var item in obj)
					{
						ob.Add(new DiagnosticoSkillsEmpresaRequest()
						{
							idDiagnosticoSkillsEmpresa = item.idDiagnosticoSkillsEmpresa,
							idDiagnostico = item.idDiagnostico,
							idSkill = item.idSkill,
							fechaHora = item.fechaHora,
							idUsuario = item.idUsuario,
							skillsSeleccionados = item != null ? JsonConvert.DeserializeObject<List<string>>(item.skillsSeleccionados) : new List<string>(),
						});
					}
					if (obj.Count > 0)
						return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = ob
						};
					else
						return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay DiagnosticoSkillsEmpresa disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de DiagnosticoSkillsEmpresa no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>> GetPorDiagnosticoAsync(long idDiagnostico)
		{
			try
			{
				var obj = await _dalc.GetPorDiagnosticoAsync(idDiagnostico);
				var ob = new List<DiagnosticoSkillsEmpresaRequest>();
				if (obj != null)
				{
					foreach (var item in obj)
					{
						ob.Add(new DiagnosticoSkillsEmpresaRequest()
						{
							idDiagnosticoSkillsEmpresa = item.idDiagnosticoSkillsEmpresa,
							idDiagnostico = item.idDiagnostico,
							idSkill = item.idSkill,
							fechaHora = item.fechaHora,
							idUsuario = item.idUsuario,
							skillsSeleccionados = item != null ? JsonConvert.DeserializeObject<List<string>>(item.skillsSeleccionados) : new List<string>(),
						});
					}
					if (obj.Count > 0)
						return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = ob
						};
					else
						return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay DiagnosticoSkillsEmpresa disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de DiagnosticoSkillsEmpresa no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>> GetPorSkillAsync(long idSkill)
		{
			try
			{
				var obj = await _dalc.GetPorSkillAsync(idSkill);
				var ob = new List<DiagnosticoSkillsEmpresaRequest>();
				if (obj != null)
				{
					foreach (var item in obj)
					{
						ob.Add(new DiagnosticoSkillsEmpresaRequest()
						{
							idDiagnosticoSkillsEmpresa = item.idDiagnosticoSkillsEmpresa,
							idDiagnostico = item.idDiagnostico,
							idSkill = item.idSkill,
							fechaHora = item.fechaHora,
							idUsuario = item.idUsuario,
							skillsSeleccionados = item != null ? JsonConvert.DeserializeObject<List<string>>(item.skillsSeleccionados) : new List<string>(),
						});
					}
					if (obj.Count > 0)
						return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = ob
						};
					else
						return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = "No hay DiagnosticoSkillsEmpresa disponibles.",
							datos = null
						};
				}
				else
				{
					return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = false,
						mensaje = "La consulta de DiagnosticoSkillsEmpresa no retorno resultados.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<DiagnosticoSkillsEmpresaRequest>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}
		public async Task<ResponseBase<DiagnosticoSkillsEmpresaRequest>> SetAsync(DiagnosticoSkillsEmpresaRequest objeto, Transaction transaccion)
		{
			try
			{
				var ob = new DiagnosticoSkillsEmpresa()
				{
					idDiagnosticoSkillsEmpresa = objeto.idDiagnosticoSkillsEmpresa,
					idDiagnostico = objeto.idDiagnostico,
					idSkill = objeto.idSkill,
					fechaHora = objeto.fechaHora,
					idUsuario = objeto.idUsuario,
					skillsSeleccionados = JsonConvert.SerializeObject(objeto.skillsSeleccionados).ToString(),
				};
				var data = await _dalc.SetAsync(ob, transaccion);

				var respuesta = new DiagnosticoSkillsEmpresaRequest()
				{
					idDiagnosticoSkillsEmpresa = data.idDiagnosticoSkillsEmpresa,
					idDiagnostico = data.idDiagnostico,
					idSkill = data.idSkill,
					fechaHora = data.fechaHora,
					idUsuario = data.idUsuario,
					skillsSeleccionados = data != null ? JsonConvert.DeserializeObject<List<string>>(data.skillsSeleccionados) : new List<string>(),
				};

				if (data != null)
				{
					return new ResponseBase<DiagnosticoSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = $"Operación realizada con exito",
						datos = respuesta
					};
				}
				else
					return new ResponseBase<DiagnosticoSkillsEmpresaRequest>()
					{
						codigo = (int)HttpStatusCode.InternalServerError,
						estado = false,
						mensaje = $"La operación solicitada no se pudo realizar.",
						datos = null
					};

			}
			catch (Exception ex)
			{
				return new ResponseBase<DiagnosticoSkillsEmpresaRequest>()
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
