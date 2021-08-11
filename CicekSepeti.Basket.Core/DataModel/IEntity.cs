using System;

namespace CicekSepeti.Basket.Core.DataModel
{
    public interface IEntity
    {
    }

    public interface IEntity<out TKey> : IEntity where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
    }
}
