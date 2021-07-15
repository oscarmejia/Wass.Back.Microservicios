using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Herramientas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idHerramienta { get; set; }
        public string nombre { get; set; }
        public long tipo { get; set; }
        public string marca { get; set; }
        public long estado { get; set; }
        public string descripcion { get; set; }
        public string urlImagen { get; set; }
        public long idSede { get; set; }
        public string codigoActivo { get; set; }
        public bool eliminado { get; set; }
    }
}
