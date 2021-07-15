using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class Recomendaciones
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idRecomendacion { get; set; }

        public long idEmpresaRecomienda { get; set; }

        public long idEmpresaRecomendada { get; set; }

        public string recomendacion { get; set; }

        public DateTime fechaCreacion { get; set; }
    }
}
