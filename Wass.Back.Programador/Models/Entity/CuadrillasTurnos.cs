using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Programador.Models.Entity
{
	public class CuadrillasTurnos
	{
		[Key]
		public long idCuadrillasTurnos { get; set; }
		public long idTurno { get; set; }
		public long idCuadrilla { get; set; }
	}
}
