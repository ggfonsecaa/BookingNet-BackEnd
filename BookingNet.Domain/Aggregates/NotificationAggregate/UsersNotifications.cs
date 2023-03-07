using BookingNet.Domain.Aggregates.NotificationAggregate;

namespace BookingNet.Domain.Aggregates.UserNotificationAggregate
{
    public partial class UsersNotifications
    {
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
    }
}