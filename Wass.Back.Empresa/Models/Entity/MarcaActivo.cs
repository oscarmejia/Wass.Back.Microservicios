using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class MarcaActivo
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long idMarcaActivo { get; set; }
        public string marcaActivo { get; set; }
    }
}
