using BookingNet.Domain.Aggregates.UserNotificationAggregate;
using BookingNet.Domain.Entities;


namespace BookingNet.Domain.Aggregates.NotificationAggregate
{
    public class Notification: BaseEntity, IAggregateRoot
    {
        public string Message { get; set; }
        public DateTime NotificationDate { get; set; }
        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }
        public ICollection<UsersNotifications> Users { get; set; } = new List<UsersNotifications>();
    }
}