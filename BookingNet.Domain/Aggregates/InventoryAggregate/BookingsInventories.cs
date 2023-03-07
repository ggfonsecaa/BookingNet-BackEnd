using BookingNet.Domain.Aggregates.InventoryAggregate;

namespace BookingNet.Domain.Aggregates.BookingInventoryAggregate
{
    public partial class BookingsInventories
    {
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public float Quantity { get; set; }
    }
}