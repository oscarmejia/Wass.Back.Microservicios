using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class Departamentos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idDepto { get; set; }
        public int idPais { get; set; }
        public string depto { get; set; }
        public bool eliminado { get; set; }

        [ForeignKey("idPais")]
        public Paises pais { get; set; }
    }
}
