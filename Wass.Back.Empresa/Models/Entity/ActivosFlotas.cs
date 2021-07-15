using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosFlotas
    {
        [Key]
        public Guid idActivo { get; set; }
        public long idSedeResponsable { get; set; }
        public long idEstado { get; set; }
        public long idCategoria { get; set; }
        public string Codigo { get; set; }
        public string urlImgFlota { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarras { get; set; }
        public long idClasificacion1 { get; set; }
        public long? idClasificacion2 { get; set; }
        public string VIN { get; set; }
        public string Chasis { get; set; }
        public string Licencia { get; set; }
        public long Odometro { get; set; }
        public int Altura { get; set; }
        public int Ancho { get; set; }
        public int Peso { get; set; }
        public int Ejes { get; set; }
        public int Ocupantes { get; set; }
        public long idTipoMotor { get; set; }
        public string UnidadPoder { get; set; }
        public string Rpm { get; set; }
        public int Cilindros { get; set; }
        public string SerialMotor { get; set; }
        public long idTipoCombustiblePrimario { get; set; }
        public long idTipoCombustibleSecundario { get; set; }
        public string Fabricante { get; set; }
        public string Marca { get; set; }
        public string Serial { get; set; }
        public string Descripcion { get; set; }
        public int VidaUtil { get; set; }
        public DateTime InicioFuncionamiento { get; set; }
        public DateTime FinalizaFuncionamiento { get; set; }
        public string CentroCosto { get; set; }
        public string Responsable { get; set; }
        public string ResponsableEmail { get; set; }
        public long Telefono { get; set; }
        public bool Eliminado { get; set; }
        public List<ArchivosAdjuntosActivosFlotas> ArchivosAdjuntos { get; set; }

        public ActivosAdquisicion adquisicion { get; set; }

        public List<ActivosCaracteristicas> caracteristicas { get; set; }

        public ActivosUbicacion ubicacion { get; set; }
    }
}
