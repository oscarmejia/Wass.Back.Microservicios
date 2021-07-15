using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class MarcaEmpresa
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idMarcaEmpresa { get; set; }
        public long idMarca { get; set; }
        public long idEmpresa { get; set; }
        public bool eliminado { get; set; }
        public DateTime fechaCreacion { get; set; }

        [ForeignKey("idMarca")]
        public Marca Marca { get; set; }

        [ForeignKey("idEmpresa")]
        public Empresas Empresa { get; set; }
    }
}
