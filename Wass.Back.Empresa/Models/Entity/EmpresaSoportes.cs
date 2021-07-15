using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class EmpresaSoportes
    {
        public EmpresaSoportes()
        {
            idSoporte = Guid.NewGuid();
        }

        [Key]
        public Guid idSoporte { get; set; }
        public long idEmpresa { get; set; }
        public int idTipoDocumento { get; set; }
        public string rutaArchivo { get; set; }
        public bool eliminado { get; set; }

        public Guid? idActivosEquipos { get; set; }
        public Guid? idActivosFlotas { get; set; }

        [ForeignKey("idEmpresa")]
        [JsonIgnore]
        public Empresas empresas { get; set; }
    }
}
