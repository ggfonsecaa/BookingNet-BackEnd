using BookingNet.Domain.Aggregates.BookingInventoryAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.InventoryAggregate
{
    public class Inventory : BaseEntity, IAggregateRoot
    {
        public float StockQuantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<BookingsInventories> Bookings { get; set; } = new List<BookingsInventories>();
    }
}