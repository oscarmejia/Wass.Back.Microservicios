using System;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Enum;

namespace Wass.Back.Programador.Rabbit.Interface
{
    public interface IDALCLectura<T>
    {
        Task<T> Set(T objeto, Transaction transaccion);
    }
}
