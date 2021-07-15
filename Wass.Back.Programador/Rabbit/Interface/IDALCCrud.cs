using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wass.Back.Programador.Models.Enum;

namespace Wass.Back.Programador.Rabbit.Interface
{
    public interface IDALCCrud<T>
    {
        Task<T> Get(long id);
        Task<List<T>> GetAll();
        Task<T> Set(T objeto, Transaction transaccion);
    }
}
