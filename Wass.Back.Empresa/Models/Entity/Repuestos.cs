using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Wass.Back.Empresa.Models.Entity
{
    public class Repuestos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idRepuestos { get; set; }
        public string nombre { get; set; }
        public string codigoBarras { get; set; }
        public long idCategoria { get; set; }
        public long idClasificacion { get; set; }
        public string unidad { get; set; }
        public string especificaciones { get; set; }
        public string urlImagen { get; set; }
        public long tiempoCompra { get; set; }
        public long tiempoEntregaPromedio { get; set; }
        public long numeroMinimoPedido { get; set; }
        public decimal costoUnitario { get; set; }
        public string clasificacionABC { get; set; }
        public long criticidad { get; set; }
        public List<RepuestosAlmacen> RepuestosAlmacen { get; set; }
        public List<RepuestosDiagnostico> RepuestosDiagnostico { get; set; }
    }
}
