using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wass.Back.Programador.Kiwi.Interface;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;
using Wass.Back.Programador.Rabbit.Context;
using Wass.Back.Programador.Rabbit.DALC;

namespace Wass.Back.Programador.Kiwi.Bussines
{
	public class BOAgenda : IBOCrud<Agenda>
	{
		public Dictionary<string, string> _endPointsDictinoDictionary { get; set; }
		private readonly DALCAgenda _dalc;
		private readonly string _msg_base;

		public BOAgenda(ProgramadorContext context)
		{
			_dalc = new DALCAgenda(context);
			_msg_base = " agenda";
		}
		public async Task<ResponseBase<List<(int, string)>>> GetTipos()
		{
			try
			{
				var datos = await Task.Run(() => new List<(int, string)>()
				{
					(1, "CUADRILLA"),
					(2, "HERRAMIENTA")
				});

				return new ResponseBase<List<(int, string)>>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = string.Empty,
					datos = datos
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<(int, string)>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Agenda>>> GetAll()
		{
			try
			{
				var datos = await _dalc.GetAll();
				if (datos != null && datos.Count > 0)
				{
					return new ResponseBase<List<Agenda>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<List<Agenda>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = $"No hay agendas registradas.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Agenda>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Agenda>> Get(long id)
		{
			try
			{
				var datos = await _dalc.Get(id);
				if (datos != null)
				{
					return new ResponseBase<Agenda>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<Agenda>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = $"La {_msg_base} no esta disponible.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Agenda>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Turnos>>> GetRecursoTurnos(long idRecurso, int tipo = 1)
		{
			try
			{
				if (tipo == 1)
				{
					var datos = await _dalc.getTurnosRecurso(idRecurso);
					if (datos != null)
					{
						return new ResponseBase<List<Turnos>>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = string.Empty,
							datos = datos
						};
					}
					else
					{
						return new ResponseBase<List<Turnos>>()
						{
							codigo = (int)HttpStatusCode.NotFound,
							estado = true,
							mensaje = $"La {_msg_base} no esta disponible.",
							datos = null
						};
					}
				}
				else
				{
					return new ResponseBase<List<Turnos>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = "Recuerda: Un recurso de tipo 2(Herramienta) no dispone de turnos",
						datos = new List<Turnos>()
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Turnos>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Agenda>>> GetIdRecurso(long idRecurso, DateTime fechaInicio, DateTime fechaFin, bool rango = false)
		{
			try
			{
				var datos = !rango ? await _dalc.GetIdRecurso(idRecurso) : await _dalc.GetIdRecurso(idRecurso, fechaInicio, fechaFin);
				if (datos != null && datos.Count > 0)
				{
					return new ResponseBase<List<Agenda>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<List<Agenda>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = $"El recurso no tiene agendas.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Agenda>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<List<Agenda>>> GetIdOrdenTrabajo(long idOrden)
		{
			try
			{
				var datos = await _dalc.GetIdOrdenTrabajo(idOrden);
				if (datos != null && datos.Count > 0)
				{
					return new ResponseBase<List<Agenda>>()
					{
						codigo = (int)HttpStatusCode.OK,
						estado = true,
						mensaje = string.Empty,
						datos = datos
					};
				}
				else
				{
					return new ResponseBase<List<Agenda>>()
					{
						codigo = (int)HttpStatusCode.NotFound,
						estado = true,
						mensaje = $"La orden de trabajo no tiene agendas.",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<List<Agenda>>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Agenda>> Set(Agenda objeto, Transaction transaccion)
		{
			try
			{
				var validate = await ValidarDisponibilidad(objeto);
				if (validate.estado)
				{
					var data = await _dalc.Set(objeto, transaccion);
					if (data != null)
					{
						return new ResponseBase<Agenda>()
						{
							codigo = (int)HttpStatusCode.OK,
							estado = true,
							mensaje = $"Operación sobre {_msg_base} realizada con exito",
							datos = data
						};
					}
					else
						return new ResponseBase<Agenda>()
						{
							codigo = (int)HttpStatusCode.InternalServerError,
							estado = false,
							mensaje = $"La operación sobre {_msg_base} solicitada no se pudo realizar.",
							datos = data
						};
				}
				else
				{
					return new ResponseBase<Agenda>()
					{
						codigo = (int)HttpStatusCode.BadRequest,
						estado = false,
						mensaje = $"No se puede agendar el recurso: {validate.mensaje}",
						datos = null
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseBase<Agenda>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		public async Task<ResponseBase<Agenda>> CancelAgenda(long idAgenda)
		{
			try
			{
				var data = await _dalc.CancelAgenda(idAgenda);
				return new ResponseBase<Agenda>()
				{
					codigo = (int)HttpStatusCode.OK,
					estado = true,
					mensaje = $"Operación sobre {_msg_base} realizada con exito",
					datos = data
				};
			}
			catch (Exception ex)
			{
				return new ResponseBase<Agenda>()
				{
					codigo = (int)HttpStatusCode.InternalServerError,
					estado = false,
					mensaje = $"Error: {ex.Message}",
					datos = null
				};
			}
		}

		private async Task<(bool estado, string mensaje)> ValidarDisponibilidad(Agenda objeto)
		{
			var fechaInicial = AsignarHoras(objeto.fechaInicio, objeto.horaInicio);
			var fechaFinal = AsignarHoras(objeto.fechaFin, objeto.horaFin);
			var respaldoTurno = new List<(int d1, int d2)>();

			if (objeto.tipoRecurso == 1) //CUADRILLA
			{
				var turnos = await _dalc.getTurnosRecurso(objeto.idRecurso);
				
				foreach (var item in turnos)
				{
					var turnoFechaInicial = AsignarHoras(objeto.fechaInicio, HoursToString(item.horaInicial));
					var turnoFechaFinal = AsignarHoras(objeto.fechaFin, HoursToString(item.horaFinal));

					if (fechaInicial >= turnoFechaInicial && fechaFinal <= turnoFechaFinal)
					{
						respaldoTurno.Add((item.diaInicial, item.diaFinal));
					}
				}
			}

			if (respaldoTurno.Count > 0 || objeto.tipoRecurso == 2) //HERRAMIENTA
			{
				var agendasRecurso = await _dalc.GetIdRecurso(objeto.idRecurso);
				foreach (var item in agendasRecurso)
				{
					var agendaFechaInicial = AsignarHoras(item.fechaInicio, item.horaInicio);
					var agendaFechaFinal = AsignarHoras(item.fechaFin, item.horaFin);

					if ((fechaInicial >= agendaFechaInicial && fechaInicial <= agendaFechaFinal)
						||
						(fechaFinal <= agendaFechaFinal && fechaFinal >= agendaFechaInicial)
						||
						(fechaInicial <= agendaFechaInicial && fechaFinal >= agendaFechaFinal)
						||
						(fechaInicial >= agendaFechaInicial && fechaFinal <= agendaFechaFinal))
					{
						return (false, "Las fechas solicitadas cruzan con agendas ya creadas para el recurso.");
					}
				}

				if (objeto.tipoRecurso == 1)
				{
					var diaInicial = (int)objeto.fechaInicio.DayOfWeek;
					var diaFinal = (int)objeto.fechaFin.DayOfWeek;

					foreach (var item in respaldoTurno)
					{
						if (diaInicial >= item.d1 && diaFinal <= item.d2) return (true, "");
					}

					return (false, "Los días de los turnos asociados no cubren los días de agenda solicitados para el recurso");
				}
				else return (true, "");
			}

			return (false, "No hay un turno asociado al recurso para agendar en las fechas solicitadas.");
		}

		private DateTime AsignarHoras(DateTime fecha, string horas)
		{
			var strFecha = fecha.ToString("yyyy-MM-dd");
			var nfecha = Convert.ToDateTime(strFecha);

			var split = horas.Split(":");

			nfecha = nfecha.AddHours(int.Parse(split[0]));
			nfecha = nfecha.AddMinutes(int.Parse(split[1]));
			nfecha = nfecha.AddSeconds(int.Parse(split[2]));

			return nfecha;
		}

		private string HoursToString(TimeSpan hours)
		{
			var h = hours.Hours.ToString("00");
			var m = hours.Minutes.ToString("00");
			var s = hours.Seconds.ToString("00");

			return $"{h}:{m}:{s}";
		}
	}
}
