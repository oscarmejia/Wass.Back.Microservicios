using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class CentrosTrabajo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCentroTrabajo { get; set; }
        public long? idSubCentroTrabajo { get; set; }
        public long idSede { get; set; }
        public string nombre { get; set; }
        public string Descripcion { get; set; }
        public bool activo { get; set; }

        [ForeignKey("idSede")]
        [JsonIgnore]
        public Sedes sede { get; set; }

        [ForeignKey("idSubCentroTrabajo")]
        public CentrosTrabajo centrosTrabajo { get; set; }
    }
}
