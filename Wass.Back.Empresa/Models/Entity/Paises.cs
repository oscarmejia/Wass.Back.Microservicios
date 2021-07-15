using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class Paises
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPais { get; set; }
        public string pais { get; set; }
        public bool eliminado { get; set; }
    }
}
