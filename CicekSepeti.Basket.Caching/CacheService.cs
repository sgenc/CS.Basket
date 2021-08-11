using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Caching
{
    public class CacheService<T> : ICacheService<T>
    {
        private readonly IDistributedCache distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task AddAsync(string key, T entity)
        {
           await distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(entity), new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60)));
        }

        public async Task<T> GetAsync(string key)
        {
            var result = await distributedCache.GetStringAsync(key);

            if (String.IsNullOrEmpty(result))
                return default(T);

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task RemoveAsync(string key)
        {
            await distributedCache.RemoveAsync(key);
        }
    }
}