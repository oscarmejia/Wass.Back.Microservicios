using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wass.Back.Empresa.Models.Peticiones.v1.Base;

namespace Wass.Back.Empresa.Kiwi.Interface
{
    public interface IBOLectura<T>
    {
        Task<ResponseBase<T>> GetAsync(long id);
        Task<ResponseBase<List<T>>> GetAllAsync();
    }
}
