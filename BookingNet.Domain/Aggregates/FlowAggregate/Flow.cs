using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.BookingFlowAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.FlowAggregate
{
    public class Flow : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public bool HasPreviousFlow { get; set; }
        public int? FlowId { get; set; }
        public Flow? ParentFlow { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public ICollection<BookingsFlows> Bookings { get; set; } = new List<BookingsFlows>();

        public Flow() { }
        public Flow(string name, int userId, int bookingStatusId, bool hasPreviousFlow = false, int? flowId = null)
        {
            Name = name;
            HasPreviousFlow = hasPreviousFlow;
            FlowId = flowId;
            UserId = userId;
            BookingStatusId = bookingStatusId;
        }

        public void UpdateFlowInfo(string name, bool hasPreviousFlow, int? flowId, int userId)
        {
            Name = name;
            HasPreviousFlow = hasPreviousFlow;
            FlowId = hasPreviousFlow == false ? null : flowId;
            UserId = userId;
        }

        public void ChangeStatus(int bookingStatusId) 
        { 
            BookingStatusId = bookingStatusId;
        }

        public void ChangeUser(int userId)
        {
            UserId = userId;
        }
    }
}