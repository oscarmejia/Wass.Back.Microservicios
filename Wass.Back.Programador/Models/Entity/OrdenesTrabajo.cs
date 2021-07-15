using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;

namespace Wass.Back.Programador.Models.Entity
{
    public class OrdenesTrabajo
    {
        [Key]
        public long idOrden { get; set; }
        public long idEmpresa { get; set; }
        public long idSede { get; set; }
        public int idServicio { get; set; }
        public int prioridad { get; set; }
        public long? aprobador { get; set; }
        public long? programador { get; set; }
        public long? idProveedorAsignado { get; set; }
        public int idEstadoOrden { get; set; }
        public DateTime? fechaProgramacionInicio { get; set; }
        public DateTime? fechaProgramacionCierre { get; set; }
        public string creador { get; set; }
        public string motivoAnulacion { get; set; }
        public DateTime? fechaCierre { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string editor { get; set; }
        public DateTime? fechaEdicion { get; set; }
        public bool eliminada { get; set; }
        public DateTime? fechaLimiteServicio { get; set; }
        public long? variableDecision { get; set; }
        public long? nivelUrgencia { get; set; }
        public long? idCuadrilla { get; set; }
        public DateTime? fechaAtencion { get; set; }
        public bool facturada { get; set; }
        public string datosActivos { get; set; }

        public Licitacion licitacion { get; set; }
        public MantenimientoAviso mantenimientoAviso { get; set; }
        public MantenimientoCorrectivo mantenimientoCorrectivo { get; set; }
        public MantenimientoPreventivo mantenimientoPreventivo { get; set; }
        public MantenimientoRondas mantenimientoRondas { get; set; }
        public List<ArchivosAdjuntosOrdenesTrabajo> ArchivosAdjuntos { get; set; }
        public List<Incidencias> Incidencias { get; set; }
    }
}
