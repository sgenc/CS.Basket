using System.Threading.Tasks;

namespace CicekSepeti.Basket.Caching
{
    public interface ICacheService<T>
    {
        Task<T> GetAsync(string key);

        Task AddAsync(string key, T entity);

        Task RemoveAsync(string key);
    }
}
