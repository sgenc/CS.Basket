using Newtonsoft.Json;
using System;

namespace CicekSepeti.Basket.Core
{
    public class BasketApiRequestModel
    {
        [JsonProperty(PropertyName = "ci")]
        public int CustomerId { get; set; }

        [JsonProperty(PropertyName = "pi")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "q")]
        public int Quantity { get; set; }
    }
}
