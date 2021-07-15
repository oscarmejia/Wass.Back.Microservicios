using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Enum;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;

namespace Wass.Back.Empresa.Kiwi.Interface
{
    public interface IBOCrudGuid<T>
    {
        Task<ResponseBase<T>> GetAsync(Guid id);
        Task<ResponseBase<List<T>>> GetAllAsync();
        Task<ResponseBase<T>> SetAsync(T objeto, Transaction transaccion);
    }
}
