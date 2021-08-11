using Newtonsoft.Json;
using System.Net;

namespace CicekSepeti.Basket.Core
{
    public class BasketApiResponseModel
    {
        [JsonProperty(PropertyName = "d")]
        public object Data { get; set; }

        [JsonProperty(PropertyName = "sc")]
        public int HttpStatusCode { get; set; }

        [JsonProperty(PropertyName = "em")]
        public string ExceptionMessage { get; set; }
    }
}
