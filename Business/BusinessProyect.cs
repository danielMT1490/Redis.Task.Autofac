using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logic;
using Autofac;
using Repositorio;

namespace Business
{
    public class BusinessProyect<T> : IBusiness<T>
    {
        private  readonly IGet<T> _Get;
        private readonly IAdd<T> _Add;

        public BusinessProyect(IRepository<T> repository)
        {
            _Get = repository;
            _Add = repository;
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                return await _Add.Add(entity);
            }
            catch (FormatException ex)
            {
                throw new BusinessException("Error en la capa Bussines", ex);
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("Error de la capa Dao", ex);
            }   
        }


        public async Task<T> Read()
        {
            try
            {
                return await _Get.Get();
            }
            catch (FormatException ex)
            {

                throw new BusinessException("Error en la capa Bussines",ex);
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("Error de la capa Dao",ex);
            }
        }
    }
}
