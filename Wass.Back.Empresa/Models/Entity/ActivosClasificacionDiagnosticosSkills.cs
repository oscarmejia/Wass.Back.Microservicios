using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosClasificacionDiagnosticosSkills
    {
		[Key]
		public long idDiagnosticosSkills { get; set; }
		public long idDiagnostico { get; set; }
		public long idSkill { get; set; }
		public bool eliminado { get; set; }

		[ForeignKey("idDiagnostico")]
		[JsonIgnore]
		public ActivosClasificacionDiagnosticos ActivosClasificacionDiagnosticos { get; set; }

		[ForeignKey("idSkill")]
		[JsonIgnore]
		public EmpresaSkills EmpresaSkills { get; set; }
	}
}
