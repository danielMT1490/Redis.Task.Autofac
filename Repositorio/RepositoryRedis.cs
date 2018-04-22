using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Repositorio
{
    public class RepositoryRedis<T> : IRepository<T>
    {
        private readonly string key;
        private readonly StackExchangeRedisCacheClient redis;

        public RepositoryRedis()
        {
            key = "prueba";
            var redisConfig = new RedisConfiguration()
            {
                AbortOnConnectFail = true,
                KeyPrefix=key,
                Hosts = new RedisHost[] { new RedisHost { Host = "127.0.0.1", Port = 6379 } },
                ConnectTimeout= 4000,
                Database=0
            };
            redis = new StackExchangeRedisCacheClient(new NewtonsoftSerializer(),redisConfig);
          
        }
        public async Task<T> Add(T entity)
        {
            try
            {
                 await redis.AddAsync(key,entity);
                 return await Get();
            }
            catch (Exception ex )
            {

                throw new RepositoryException("Error con redis",ex);
            }
          
        }

        //dentro del parametro task<Modelo> o objeto a devolver por la acion.


        public async Task<T> Get()
        {
            try
            {
                return await redis.GetAsync<T>(key);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error con redis",ex);
                throw;
            }
        }
    }
}
