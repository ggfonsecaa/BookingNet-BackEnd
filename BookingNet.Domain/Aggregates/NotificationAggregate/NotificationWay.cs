using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.NotificationAggregate
{
    public class NotificationWay : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}