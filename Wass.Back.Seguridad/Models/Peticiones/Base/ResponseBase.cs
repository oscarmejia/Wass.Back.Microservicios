using System;
namespace Wass.Back.Seguridad.Models.Peticiones.Base
{
    public class ResponseBase<T>
    {
        public int codigo { get; set; } = 200;
        public string mensaje { get; set; } = string.Empty;
        public bool estado { get; set; } = true;
        public T datos { get; set; }
    }
}
