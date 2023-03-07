using BookingNet.Domain.Aggregates.BookingAggregate;

namespace BookingNet.Domain.Aggregates.BookingFlowAggregate
{
    public partial class BookingsFlows
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}