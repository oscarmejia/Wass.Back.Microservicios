using System;
using System.Collections.Generic;
using Wass.Back.Empresa.Models.Entity;
namespace Wass.Back.Empresa.Models.Peticiones.v1.Certificacion
{
    public class ResponseCertificacion
    {

        public long idEmpresa { get; set; }
        public List<Entity.Certificacion> certificadosEmpresa { get; set; }
        public List<Entity.Certificacion> certificadosEmpleados { get; set; }
        
    }
}
