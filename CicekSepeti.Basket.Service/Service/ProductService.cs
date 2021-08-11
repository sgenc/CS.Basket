using CicekSepeti.Basket.Core;
using CicekSepeti.Basket.Core.DataModel;
using CicekSepeti.Basket.Data.Repositories;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
           _productRepository = productRepository;
        }

        public async Task<Product> AddProduct(ProductApiRequestModel productApiRequestModel)
        {
            return await _productRepository.AddAsync(new Product()
            {
                Name = productApiRequestModel.Name,
                ProductId = productApiRequestModel.ProductId,
                Quantity = productApiRequestModel.Quantity
            });
        }

        public async Task<bool> CheckStockAvailability(int productId, int quantity)
        {
            var product = await _productRepository.GetAsync(s => s.ProductId == productId);

            if (product != null)
                return product.Quantity >= quantity ? true :  false;

            return false;
        }
    }
}
