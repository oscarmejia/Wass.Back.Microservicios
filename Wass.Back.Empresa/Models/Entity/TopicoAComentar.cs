using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity

{
    public class TopicoAComentar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long idTopicoAComentar { get; set; }
        public long idEmpresa { get; set; }
        public long idSede { get; set; }
        public long idEmpleado { get; set; }
        public string idTopico { get; set; }
        //TipoTopico : 1 = ActivoEquipo, 2 = ActivoFlota, 3 = Orden de trabajo, 4 = Licitacion 
        public long tipoTopico { get; set; }
        public string urlImagen { get; set; }
        public DateTime fechaHora { get; set; }
        public bool eliminado { get; set; }
        public List<Comentario> comentario { get; set; }
    }
}
