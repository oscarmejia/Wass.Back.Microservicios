using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class RepuestosGrupoPartes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idRepuestosGrupoPartes { get; set; }
        public long idRepuestos { get; set; }
        public long idGrupoPartes { get; set; }
        public bool eliminado { get; set; }
        public long cantidad { get; set; }

        [ForeignKey("idRepuestos")]
        //[JsonIgnore]
        public Repuestos Repuestos { get; set; }
    }
}
