using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wass.Back.Empresa.Models.Entity
{
    public class TransferenciasInternasAlmacenes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idTransferenciasInternasAlmacenes { get; set; }
        public long idAlmacenEmisor { get; set; }
        public long idAlmacenReceptor { get; set; }
        public string repuestos { get; set; }
        public DateTime fechaHora { get; set; }
        public long motivoTransferencia { get; set; }
        public string ordenTrabajo { get; set; }
        public long idEmpresa { get; set; }
        public long estado { get; set; }
    }
}
