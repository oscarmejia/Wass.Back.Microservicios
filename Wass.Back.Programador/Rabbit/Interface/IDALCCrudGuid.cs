using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Enum;

namespace Wass.Back.Programador.Rabbit.Interface
{
    public interface IDALCCrudGuid<T>
    {
        Task<T> Get(Guid id);
        Task<List<T>> GetAll();
        Task<T> Set(T objeto, Transaction transaccion);
    }
}
