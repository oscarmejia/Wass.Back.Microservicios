using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Enum;
using Wass.Back.Programador.Models.Peticiones.Base;

namespace Wass.Back.Programador.Kiwi.Interface
{
    public interface IBOCrud<T>
    {
        Task<ResponseBase<T>> Get(long id);
        Task<ResponseBase<List<T>>> GetAll();
        Task<ResponseBase<T>> Set(T objeto, Transaction transaccion);
    }
}
