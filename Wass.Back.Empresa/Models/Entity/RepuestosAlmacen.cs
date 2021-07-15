using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class RepuestosAlmacen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idRepuestosAlmacen { get; set; }
        public long cantidadActual { get; set; }
        public long cantidadMinima { get; set; }
        public long cantidadOptima { get; set; }
        public long cantidadMaxima { get; set; }
        public long idAlmacen { get; set; }
        public long idRepuestos { get; set; }


        [ForeignKey("idRepuestos")]
        //[JsonIgnore]
        public Repuestos Repuestos { get; set; }

        [ForeignKey("idAlmacen")]
        [JsonIgnore]
        public Almacen Almacen { get; set; }
    }
}
