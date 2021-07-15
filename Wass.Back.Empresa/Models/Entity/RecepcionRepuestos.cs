using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class RecepcionRepuestos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idRecepcionRepuestos { get; set; }
        public DateTime fechaHora { get; set; }
        public string repuestos { get; set; }
        public long idAlmacen { get; set; }
        public long idUsuario { get; set; }

        [ForeignKey("idAlmacen")]
        [JsonIgnore]
        public Almacen Almacen { get; set; }
    }
}
