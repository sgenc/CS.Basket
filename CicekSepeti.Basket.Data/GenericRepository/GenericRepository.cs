using CicekSepeti.Basket.Core.Entity;
using CicekSepeti.Basket.Core.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Data.GenericRepository
{
    public class GenericMongoRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, IEntity, new()
    {
        #region fields

        protected readonly IMongoCollection<TEntity> _mongoCollection;

        #endregion

        #region ctor

        public GenericMongoRepository(IOptions<AppSettings> options)
        {
            if (options.Value != null)
            {
                var client = new MongoClient(options.Value.ConnectionString);
                var database = client.GetDatabase(options.Value.Database);
                _mongoCollection = database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower());
            }
        }

        #endregion

        #region methods

        #region GetById Operations

        /// <summary>
        /// RDMS Gibi id'ye göre kayıt getirme işlemlerini yapar
        /// Senkron ve asenkron metotları region içinde listelenmiştir
        /// </summary>

        public TEntity GetById(string id)
        {
            return _mongoCollection.Find(i => i.Id == id).FirstOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            var result = await _mongoCollection.FindAsync(i => i.Id == id);
            return result.FirstOrDefault();
        }

        #endregion

        #region Find Operations

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return _mongoCollection.Find(filter).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _mongoCollection.Find(filter).ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, int pageIndex, int size)
        {
            return Find(filter, i => i.Id, pageIndex, size);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, int pageIndex, int size)
        {
            return await FindAsync(filter, i => i.Id, pageIndex, size);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size)
        {
            return Find(filter, order, pageIndex, size, true);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size)
        {
            return await FindAsync(filter, order, pageIndex, size, true);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending)
        {
            var query = _mongoCollection.Find(filter).Skip(pageIndex * size).Limit(size);
            return (isDescending ? query.SortByDescending(order) : query.SortBy(order)).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending)
        {
            var query = _mongoCollection.Find(filter).Skip(pageIndex * size).Limit(size);
            return await (isDescending ? query.SortByDescending(order) : query.SortBy(order)).ToListAsync();
        }

        #endregion

        #region FindAll Operations

        public IEnumerable<TEntity> FindAll()
        {
            return _mongoCollection.Find(Builders<TEntity>.Filter.Empty).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await _mongoCollection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();
        }

        public IEnumerable<TEntity> FindAll(int pageIndex, int size)
        {
            return FindAll(i => i.Id, pageIndex, size);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(int pageIndex, int size)
        {
            return await FindAllAsync(i => i.Id, pageIndex, size);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, int pageIndex, int size)
        {
            return FindAll(order, pageIndex, size, true);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, object>> order, int pageIndex, int size)
        {
            return await FindAllAsync(order, pageIndex, size, true);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending)
        {
            var query = _mongoCollection.Find(_ => true).Skip(pageIndex * size).Limit(size);
            return (isDescending ? query.SortByDescending(order) : query.SortBy(order)).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending)
        {
            var query = _mongoCollection.Find(_ => true).Skip(pageIndex * size).Limit(size);
            return await (isDescending ? query.SortByDescending(order) : query.SortBy(order)).ToListAsync();
        }

        #endregion

        #region First Operations

        public TEntity First()
        {
            return FindAll(i => i.Id, 0, 1, false).FirstOrDefault();
        }

        public async Task<TEntity> FirstAsync()
        {
            var query = _mongoCollection.Find(_ => true).Skip(0 * 1).Limit(1);
            return await query.SortBy(i => i.Id).FirstOrDefaultAsync();
        }

        public TEntity First(Expression<Func<TEntity, bool>> filter)
        {
            return First(filter, i => i.Id);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await FirstAsync(filter, i => i.Id);
        }

        public TEntity First(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order)
        {
            return First(filter, order, false);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order)
        {
            return await FirstAsync(filter, order, false);
        }

        public TEntity First(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isDescending)
        {
            return Find(filter, order, 0, 1, isDescending).SingleOrDefault();
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isDescending)
        {
            var query = _mongoCollection.Find(filter).Skip(0 * 1).Limit(1);
            return await (isDescending ? query.SortByDescending(order) : query.SortBy(order)).SingleOrDefaultAsync();
        }

        #endregion

        #region Last Operations

        public TEntity Last()
        {
            return FindAll(i => i.Id, 0, 1, true).FirstOrDefault();
        }

        public TEntity Last(Expression<Func<TEntity, bool>> filter)
        {
            return Last(filter, i => i.Id);
        }

        public TEntity Last(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order)
        {
            return Last(filter, order, false);
        }

        public TEntity Last(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, bool isDescending)
        {
            return First(filter, order, !isDescending);
        }

        #endregion

        #region Any Operations

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return _mongoCollection.AsQueryable().Any(filter);
        }

        #endregion

        #endregion
    }
}
