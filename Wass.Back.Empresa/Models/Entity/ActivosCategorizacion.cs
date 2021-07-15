using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosCategorizacion
    {
        [Key]
        public long idCategorizacion { get; set; }
        public long idEmpresa { get; set; }
        public string categorizacion { get; set; }
        public bool eliminado { get; set; }
        public DateTime fechaCreacion { get; set; }

        public List<ActivosClasificacion> ListaClasificaciones { get; set; } = new List<ActivosClasificacion>();
    }
}
