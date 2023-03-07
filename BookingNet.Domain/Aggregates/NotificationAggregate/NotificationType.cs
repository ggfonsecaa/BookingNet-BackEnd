using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.NotificationAggregate
{
    public class NotificationType : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}