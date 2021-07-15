using System;
namespace Wass.Back.Empresa.Models.Peticiones.v1.Base
{
    public class ResponseTransaction
    {

        public bool estado { get; set; }
        public string mensaje { get; set; }
        public ResponseTransaction()
        {
        }
    }
}
