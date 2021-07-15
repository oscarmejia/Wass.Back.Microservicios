using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wass.Back.Empresa.Rabbit.Interface
{
    public interface IDALCLectura<T>
    {
        Task<T> GetAsync(long id);
        Task<List<T>> GetAllAsync();
    }
}
