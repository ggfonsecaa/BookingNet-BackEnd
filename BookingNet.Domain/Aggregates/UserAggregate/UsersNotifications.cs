using BookingNet.Domain.Aggregates.UserAggregate;

namespace BookingNet.Domain.Aggregates.UserNotificationAggregate
{
    public partial class UsersNotifications
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}