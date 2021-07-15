using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wass.Back.Seguridad.Models.Enum;

namespace Wass.Back.Seguridad.Kiwi.Interface
{
    public interface IBOCrud<T>
    {
        Task<T> Get(long id);
        Task<List<T>> GetAll();
        Task<T> Set(T objeto, Transaction transaccion);
    }
}
