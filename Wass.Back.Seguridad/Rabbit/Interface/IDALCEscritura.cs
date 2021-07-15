using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wass.Back.Seguridad.Rabbit.Interface
{
    public interface IDALCEscritura<T>
    {
        Task<T> Get(long id);
        Task<List<T>> GetTodas();
    }
}
