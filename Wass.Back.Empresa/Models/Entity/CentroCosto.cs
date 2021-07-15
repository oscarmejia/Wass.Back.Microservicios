using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class CentroCosto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCentroCosto { get; set; }
        public string idCentroCostoPadre { get; set; }
        public long idEmpresa { get; set; }
        public string nombre { get; set; }
        public bool eliminado { get; set; }
        public List<Sedes> Sedes { get; set; }
    }
}
