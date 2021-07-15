using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wass.Back.Empresa.Models.Entity
{
    public class Cuadrillas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idCuadrilla { get; set; }
        public long idSede { get; set; }
        public string nombreA { get; set; }
        public string nombreB { get; set; }
        public string email { get; set; }
        public int estado { get; set; }
        public string zonaAtencion { get; set; }
        public string ubicacionActual { get; set; }
        public string celular { get; set; }
        public int numMiembros { get; set; }

        [ForeignKey("idSede")]
        [JsonIgnore]
        public Sedes sedes { get; set; }

        public List<CuadrillaEmpleados> cuadrillaEmpleados { get; set; }
        public List<CuadrillasTurnos> cuadrillaTurnos { get; set; }
        public List<CuadrillaSkillsEmpresa> cuadrillaSkillsEmpresa { get; set; }

        [NotMapped]
        public List<Turnos> listadoTurnos { get; set; }
    }
}
