using CicekSepeti.Basket.Core.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Data.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, IEntity, new()
    {
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, int pageIndex, int size);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, int pageIndex, int size);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending);

        IEnumerable<TEntity> FindAll();

        Task<IEnumerable<TEntity>> FindAllAsync();

        IEnumerable<TEntity> FindAll(int pageIndex, int size);

        Task<IEnumerable<TEntity>> FindAllAsync(int pageIndex, int size);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, int pageIndex, int size);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, object>> order, int pageIndex, int size);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending);

        bool Any(Expression<Func<TEntity, bool>> filter);

        TEntity Last();

        TEntity Last(Expression<Func<TEntity, bool>> filter);

        TEntity Last(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order);

        TEntity Last(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isDescending);

        TEntity First();

        Task<TEntity> FirstAsync();

        TEntity First(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter);

        TEntity First(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order);

        TEntity First(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isDescending);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isDescending);

        TEntity GetById(string id);

        Task<TEntity> GetByIdAsync(string id);
    }
}
