using Common.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public interface IGet<T>
    {
        //Debemos declarar la clase Task desde la interface
        Task<T> Get();
    }
}
