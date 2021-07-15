using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class ActivosEquipos
    {
        [Key]
        public Guid idActivo { get; set; }
        public long idSedeResponsable { get; set; }
        public long idEstado { get; set; }
        public long idCategoria { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarras { get; set; }
        public string urlImgEquipo { get; set; }
        public string Nombre { get; set; }
        public long idClasificacion1 { get; set; }
        public long idClasificacion2 { get; set; }
        public string Referencia { get; set; }
        public string Fabricante { get; set; }
        public long idMarca { get; set; }
        public string Serial { get; set; }
        public string Descripcion { get; set; }
        public int VidaUtil { get; set; }
        public DateTime InicioFuncionamiento { get; set; }
        public DateTime FinalizaFuncionamiento { get; set; }
        public string Responsable { get; set; }
        public string ResponsableEmail { get; set; }
        public long Telefono { get; set; }
        public string otros { get; set; }
        public bool Eliminado { get; set; }
        public long idSkill { get; set; }

        public List<ArchivosAdjuntosActivosEquipos> ArchivosAdjuntos { get; set; }

        public ActivosAdquisicion adquisicion { get; set; }

        public List<ActivosCaracteristicas> caracteristicas { get; set; }

        public ActivosUbicacion ubicacion { get; set; }

        [ForeignKey("idSkill")]
        public Skill skill { get; set; }

        [ForeignKey("idMarca")]
        public Marca marca { get; set; }
    }
}
