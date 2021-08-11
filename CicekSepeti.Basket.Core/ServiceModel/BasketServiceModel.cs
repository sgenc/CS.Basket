using System.Collections.Generic;

namespace CicekSepeti.BasketCore
{
    public class BasketServiceModel
    {
        public int CustomerId { get; set; }

        public List<ProductServiceModel> Products { get; set; }
    }
}
