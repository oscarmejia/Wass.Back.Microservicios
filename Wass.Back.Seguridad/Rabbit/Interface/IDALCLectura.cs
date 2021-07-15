using System;
using System.Threading.Tasks;
using Wass.Back.Seguridad.Models.Enum;

namespace Wass.Back.Seguridad.Rabbit.Interface
{
    public interface IDALCLectura<T>
    {
        Task<T> Set(T objeto, Transaction transaccion);
    }
}
