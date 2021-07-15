using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Enum;

namespace Wass.Back.Programador.Kiwi.Interface
{
    public interface IBOCrudGuid<T>
    {
        Task<T> Get(Guid id);
        Task<List<T>> GetTodas();
        Task<T> Set(T objeto, Transaction transaccion);
    }
}
