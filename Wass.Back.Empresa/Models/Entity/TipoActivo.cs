using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class TipoActivo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long idTipoActivo { get; set; }
        public string tipoActivo { get; set; }
    }
}
