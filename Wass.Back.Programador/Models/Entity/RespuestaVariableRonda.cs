using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Programador.Models.Entity
{
    public class RespuestaVariableRonda
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idRespuestaVariableRonda {get; set;}
        public long idRonda { get; set; }
        public long idVariable { get; set; }
        public string variable { get; set; }
        public string respuesta { get; set; }
       
    }
}
