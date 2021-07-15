using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class EmpresaChecks
    {
		[Key]
		public long idEmpresaCheck { get; set; }
		public long idEmpresa { get; set; }
		public long idLista { get; set; }
		public long idValor { get; set; }
		public string detalle { get; set; }
		public bool estado { get; set; }

		[ForeignKey("idEmpresa")]
		[JsonIgnore]
		public Empresas empresas { get; set; }
	}
}
