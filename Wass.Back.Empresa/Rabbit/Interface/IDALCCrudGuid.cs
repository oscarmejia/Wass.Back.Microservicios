using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Enum;

namespace Wass.Back.Empresa.Rabbit.Interface
{
    public interface IDALCCrudGuid<T>
    {
        Task<T> GetAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> SetAsync(T objeto, Transaction transaccion);
    }
}
