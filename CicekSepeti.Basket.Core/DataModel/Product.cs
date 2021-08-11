namespace CicekSepeti.Basket.Core.DataModel
{
    public class Product : MongoDbEntity, IEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
