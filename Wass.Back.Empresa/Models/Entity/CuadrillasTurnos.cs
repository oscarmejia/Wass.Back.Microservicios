using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class CuadrillasTurnos
    {
		[Key]
		public long idCuadrillasTurnos { get; set; }
		public long idTurno { get; set; }
		public long idCuadrilla { get; set; }

		[ForeignKey("idCuadrilla")]
		[JsonIgnore]
		public Cuadrillas cuadrillas { get; set; }
	}
}
