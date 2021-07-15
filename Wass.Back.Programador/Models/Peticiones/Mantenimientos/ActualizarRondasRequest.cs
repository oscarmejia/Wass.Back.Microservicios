using System;
using System.Collections.Generic;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
	public class ActualizarRondasRequest
	{
		public long idPlan { get; set; }
		public long idGrupo { get; set; }
		public Guid idActivo { get; set; }
		public DateTime fechaUltimoMantenimientoRondas { get; set; }
		public ActualizarRondasOrden datosOrden { get; set; }
		public string observacion { get; set; }
	}

	public class ActualizarRondasVarsRequest
	{
		public long idVariable { get; set; }
		public string valor { get; set; }
		public long idPlan { get; set; }
		public long idGrupo { get; set; }
		public Guid idActivo { get; set; }
		public ActualizarRondasOrden datosOrden { get; set; }
	}

	public class ActualizarRondasOrden
	{
		public long idEmpresa { get; set; }
		public long idSede { get; set; }
		public int prioridad { get; set; }
		public List<detalleActivosRequest> datosActivos { get; set; }
		public DateTime fechaCreacion { get; set; }
	}

	public class RespuestaActivosVariables
	{
		public long idActivoVariable { get; set; }
		public long idClasificacion { get; set; }
		public long idCategorizacion { get; set; }
		public Guid? idActivoFlota { get; set; }
		public Guid? idActivoEquipo { get; set; }
		public string respuesta { get; set; }
	}
	//public class ActualizarRondasVarsRequest
	//{
	//	public long idPlan { get; set; }
	//	public long idGrupo { get; set; }
	//	public Guid idActivo { get; set; }
	//	public long idVariable { get; set; }
	//	public string valor { get; set; }
	//	public DateTime fecha { get; set; }
	//}
}
