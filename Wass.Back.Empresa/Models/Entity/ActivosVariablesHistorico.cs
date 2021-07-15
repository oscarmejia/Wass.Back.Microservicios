using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosVariablesHistorico
    {
        [Key]
        public long idActivoVariableHistorico { get; set; }
        public long idActivoVariable { get; set; }

        [ForeignKey("idActivoVariable")]
        public ActivosVariables ActivoVariable { get; set; }
    }
}
