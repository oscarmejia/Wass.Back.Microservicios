using System;
using System.Collections.Generic;
using Wass.Back.Programador.Models.Entity;
using Wass.Back.Programador.Models.Enum;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
    public class OrdenTrabajoRequest
    {
        public long idOrden { get; set; }
        public long idEmpresa { get; set; }
        public long idSede { get; set; }
        public Servicio idServicio { get; set; }
        public Prioridad prioridad { get; set; }
        public long? aprobador { get; set; }
        public long? programador { get; set; }
        public long? idProveedorAsignado { get; set; }
        public EstadosOrden idEstadoOrden { get; set; }
        public string creador { get; set; }
        public DateTime? fechaProgramacionInicio { get; set; }
        public DateTime? fechaProgramacionCierre { get; set; }
        public string motivoAnulacion { get; set; }
        public DateTime? fechaCierre { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string editor { get; set; }
        public DateTime? fechaEdicion { get; set; }
        public bool eliminada { get; set; }
        public DateTime? fechaLimiteServicio { get; set; }
        public long? variableDecision { get; set; }
        public long? nivelUrgencia { get; set; }
        public bool facturada { get; set; }        
        public long? idCuadrilla { get; set; }
        public DateTime? fechaAtencion { get; set; }
        public List<ActivosRequest> datosActivos { get; set; } = new List<ActivosRequest>();
        public MantenimientoAviso mantenimientoAviso { get; set; } 
        public MantenimientoCorrectivo mantenimientoCorrectivo { get; set; }
        public MantenimientoPreventivo mantenimientoPreventivo { get; set; }
        public MantenimientoRondas mantenimientoRondas { get; set; }
        public List<ArchivosAdjuntosOrdenesTrabajo> ArchivosAdjuntos { get; set; }
        public List<Incidencias> Incidencias { get; set; }
    }

    public class ActivosRequest
    {
        public string idActivo { get; set; }
        public string llave { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; } // flota o equipos
    }

    public class MantenimientoRequest
    {
        public Object mantenimiento { get; set; }
        public DateTime fechaAtencion { get; set; }
        public DateTime fechaCierreOrden { get; set; }
    }
    public class TiempoPromedioRequest
    {
        public List<MantenimientoRequest> mantenimientoRequest { get; set; }
        public double tiempoPromedioTrabajo { get; set; }
    }
}
