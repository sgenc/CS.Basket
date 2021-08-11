using CicekSepeti.Basket.Core;
using CicekSepeti.Basket.Core.DataModel;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Service
{
    public interface IProductService
    {
        Task<bool> CheckStockAvailability(int productId, int quantity);

        Task<Product> AddProduct(ProductApiRequestModel productApiRequestModel);
    }

}
