using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Cargos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCargo { get; set; }
        public string cargo { get; set; }
    }
}
