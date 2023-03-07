using BookingNet.Domain.Aggregates.BookingAggregate;

namespace BookingNet.Domain.Aggregates.BookingInventoryAggregate
{
    public partial class BookingsInventories
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}