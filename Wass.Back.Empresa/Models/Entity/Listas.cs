using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Listas
    {
        [Key]
        public int idLista { get; set; }
        public string descripción { get; set; }
        public bool activo { get; set; }

        public List<ListasValores> listasValores { get; set; }
    }
}
