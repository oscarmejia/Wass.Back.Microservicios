using System;
namespace Wass.Back.Empresa.Models.Peticiones.v1.ActivoParada
{
    public class ActivosParadaSedeRequest
    {
        public long idSede { get; set; }
        public Guid idActivo { get; set; }
        public double horasParada { get; set; }
    }

    public class ActivosParadaEmpresaRequest
    {
        public long idEmpresa { get; set; }
        public Guid idActivo { get; set; }
        public double horasParada { get; set; }
    }

    public class ActivosParadaActivoRequest
    {
        public Guid idActivo { get; set; }
        public double horasParada { get; set; }
    }

    public class ActivosParadaSedePromedioRequest
    {
        public long idSede { get; set; }
        public Guid idActivo { get; set; }
        public double promedioHorasParada { get; set; }
    }

    public class ActivosParadaSedeDisponibleRequest
    {
        public long idSede { get; set; }
        public Guid idActivo { get; set; }
        public double porcentajeHorasDisponible { get; set; }
    }

    public class ActivosParadaActivoPromedioRequest
    {
        public Guid idActivo { get; set; }
        public double promedioHorasParada { get; set; }
    }

    public class ActivosParadaActivoDisponibleRequest
    {
        public Guid idActivo { get; set; }
        public double porcentajeHorasDisponible { get; set; }
    }

    public class ActivosParadaEmpresaPromedioRequest
    {
        public long idEmpresa { get; set; }
        public Guid idActivo { get; set; }
        public double promedioHorasParada { get; set; }
    }

    public class ActivosParadaEmpresaDisponibleRequest
    {
        public long idEmpresa { get; set; }
        public Guid idActivo { get; set; }
        public double porcentajeHorasDisponible { get; set; }
    }

    public class ActivosParadaRequest
    {
        public long idActivosParada { get; set; }
        public Guid idActivo { get; set; }
        public long idOrden { get; set; }
        public DateTime fechaHoraParada { get; set; }
        public DateTime? fechaHoraReactivacion { get; set; }
        public long tipoOrden { get; set; }
        public long idCuadrilla { get; set; }
        public long idSede { get; set; }
        public long idEmpresa { get; set; }
        public double horasParada { get; set; }
        public Object Activo { get; set; }

    }
}
