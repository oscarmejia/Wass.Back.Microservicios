using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Wass.Back.Programador.Models.Entity;

namespace Wass.Back.Programador.Models.Peticiones.Mantenimientos
{
    public class ActualizarMantenimientoPreventivoRequest
    {
        public long idPlanMantenimientoPreventivo { get; set; }
        public long idPlan { get; set; }
        public long idGrupo { get; set; }
        public string idActivo { get; set; }
        public DateTime fechaUltimoMantenimientoPreventivo { get; set; }
    }

    public class MantenimientoPreventivoRequest
    {
        public long idMantenimientoPreventivo { get; set; }
        public long idOrden { get; set; }
        public long idPlan { get; set; }
        public long idGrupo { get; set; }
        public DateTime? fechaPropuestaProgramacion { get; set; }
        public bool parada { get; set; }
        public bool eliminado { get; set; }

        public OrdenesTrabajo orden { get; set; }
        public List<string> Acciones { get; set; } = new List<string>();
    }

    public class GruposActivosMantenimientoPreventivoRequest
    {
        public long id { get; set; }
        public detalleActivosRequest Activo { get; set; }
        public long idPlan { get; set; }
        public bool estado { get; set; }
    }
    public class PlanesMantenimientoPreventivoRequest
    {
        public long idPlan { get; set; }
        public long idCategoria { get; set; }
        public long? idClasificacion1 { get; set; }
        public long? idClasificacion2 { get; set; }
        public int prioridad { get; set; }
        public long idEmpresa { get; set; }
        public long idSede { get; set; }
        public string marca { get; set; }
        public bool estado { get; set; }

        [NotMapped]
        public List<GruposActivosMantenimientoPreventivoRequest> GruposActivos { get; set; }

        [NotMapped]
        public List<GruposPartes> GruposPartes { get; set; }
        [NotMapped]
        public List<GruposAcciones> GruposAcciones { get; set; }
    }
    }
