using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
	public class Agenda
	{
		public Agenda()
		{
			tipoRecurso = 1;
		}

		[Key]
		public long idAgenda { get; set; }
		public long idRecurso { get; set; }
		public long idOrdenTrabajo { get; set; }
		public string titulo { get; set; }
		public string descripcion { get; set; }
		public DateTime fechaInicio { get; set; }
		public DateTime fechaFin { get; set; }
		public string horaInicio { get; set; }
		public string horaFin { get; set; }
		public bool estado { get; set; }
		public int tipoRecurso { get; set; }
	}
}
