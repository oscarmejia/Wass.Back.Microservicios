using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class Municipios
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMunicipio { get; set; }
        public int idDepto { get; set; }
        public string municipio { get; set; }
        public bool eliminado { get; set; }

        [ForeignKey("idDepto")]
        public Departamentos departamento { get; set; }
    }
}
