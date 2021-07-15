using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class OrdenEntregaAlmacen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idOrdenEntregaAlmacen { get; set; }
        public string repuestos { get; set; }
        public long idOrdenTrabajo { get; set; }
        public DateTime fechaHora { get; set; }
        public long idAlmacen { get; set; }
        public long idCuadrilla { get; set; }
        public long idSede { get; set; }

        [ForeignKey("idAlmacen")]
        [JsonIgnore]
        public Almacen Almacen { get; set; }
    }
}
