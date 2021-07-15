using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ListasValores
    {
        [Key]
        public long idValor { get; set; }
        public int idLista { get; set; }
        public string valor { get; set; }
        public bool activo { get; set; }

        [ForeignKey("idLista")]
        [JsonIgnore]
        public Listas Lista { get; set; }
    }
}
