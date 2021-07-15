using System;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
	public class Turnos
	{
		[Key]
		public long idTurno { get; set; }
		public long idEmpresa { get; set; }
		public string nombre { get; set; }
		public string descripcion { get; set; }
		public int diaInicial { get; set; }
		public int diaFinal { get; set; }
		public TimeSpan horaInicial { get; set; }
		public TimeSpan horaFinal { get; set; }
		public string creador { get; set; }
		public DateTime fechaCreacion { get; set; }
		public string editor { get; set; }
		public DateTime fechaEdicion { get; set; }
		public bool eliminado { get; set; }
	}
}
