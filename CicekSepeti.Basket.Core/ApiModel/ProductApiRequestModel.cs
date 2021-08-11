using Newtonsoft.Json;

namespace CicekSepeti.Basket.Core
{
    public class ProductApiRequestModel
    {
        [JsonProperty(PropertyName = "pi")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "n")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "q")]
        public int Quantity { get; set; }
    }
}