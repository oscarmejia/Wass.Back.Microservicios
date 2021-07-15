using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class KardexRepuesto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long idKardexRepuesto { get; set; }
        public string consulta { get; set; }
    }
}
