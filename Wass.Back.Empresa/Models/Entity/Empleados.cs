using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Empleados
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idEmpleado { get; set; }
        public int idCargo { get; set; }
        public long idSede { get; set; }
        public int idEstadoEmpleado { get; set; }
        public long idTipoDocumento { get; set; }
        public string numDocumento { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string ConnectionId { get; set; }
        public string celular { get; set; }
        //public bool estadoConversacion { get; set; }
        
        public string estadoChat { get; set; }
        public string urlFotoEmpleado { get; set; }


        [ForeignKey("idSede")]
        public Sedes sede { get; set; }

        [JsonIgnore]
        public List<CuadrillaEmpleados> cuadrillaEmpleados { get; set; }

        public List<Certificacion> ListaCertificados { get; set; }
    }
}
