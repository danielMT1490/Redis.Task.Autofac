using Common.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public interface IAdd<T>
    {
        Task<T> Add(T entity);
    }
}
