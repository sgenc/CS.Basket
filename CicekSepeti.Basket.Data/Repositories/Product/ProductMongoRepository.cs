using CicekSepeti.Basket.Core.DataModel;
using CicekSepeti.Basket.Core.Model;
using Microsoft.Extensions.Options;

namespace CicekSepeti.Basket.Data.Repositories
{
    public class ProductMongoRepository : MongoDbRepositoryBase<Product>, IProductRepository
    {
        public ProductMongoRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
