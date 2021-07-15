using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Marca
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idMarca { get; set; }
        public long? idSubMarca { get; set; }
        public string marca { get; set; }
        public DateTime fechaCreacion { get; set; }
        public bool eliminado { get; set; }

        [ForeignKey("idSubMarca")]
        [JsonIgnore]
        public ActivosClasificacion subMarca { get; set; }
    }
}
