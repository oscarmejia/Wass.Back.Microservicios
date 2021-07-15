using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Almacen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idAlmacen { get; set; }
        public string nombre { get; set; }
        public bool estado { get; set; }
        public long tipo { get; set; }
        public long idSede { get; set; }
        public long idCuadrilla { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaDesactivacion { get; set; }
        public List<RepuestosAlmacen> RepuestosAlmacen { get; set; }
    }
}
