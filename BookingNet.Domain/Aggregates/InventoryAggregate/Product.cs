using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.InventoryAggregate
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
    }
}