using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.BookingAggregate
{
    public class BookingType : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}