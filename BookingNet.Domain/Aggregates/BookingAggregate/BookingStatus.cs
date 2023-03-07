using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.BookingAggregate
{
    public class BookingStatus : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public ICollection<Flow> Flows { get; set; } = new List<Flow>();
    }
}