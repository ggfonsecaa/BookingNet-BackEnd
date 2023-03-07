using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Aggregates.NotificationAggregate;
using BookingNet.Domain.Aggregates.UserGroupAggregate;
using BookingNet.Domain.Aggregates.UserNotificationAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.UserAggregate
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PassWord { get; set; }
        public int NotificationWayId { get; set; }
        public NotificationWay NotificationWay { get; set; }
        public ICollection<Flow> Flows { get; set; } = new List<Flow>();
        public ICollection<UsersGroups> Groups { get; set; } = new List<UsersGroups>();
        public ICollection<UsersNotifications> Notifications { get; set; } = new List<UsersNotifications>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();


        public User() { }
        public User(string userName, string userEmail, string passWord, int notificationWayId)
        {
            UserName = userName;
            UserEmail = userEmail;
            PassWord = passWord;
            NotificationWayId = notificationWayId;
        }

        public User UpdateUserInfo(User user, string userName = null, string userEmail = null, string passWord = null, int notificationWayId = 0) 
        {
            user.UserName = userName ?? user.UserName;
            user.UserEmail = userEmail ?? user.UserEmail;
            user.PassWord = passWord ?? user.PassWord;
            user.NotificationWayId = notificationWayId == 0 ? 1 : user.NotificationWayId;

            return user;
        }

        public User AddUserGroup(User user, Group userGroup) 
        {
            UsersGroups usersGroups = new()
            {
                User = user,
                Group = userGroup
            };

            user.Groups.Add(usersGroups);

            return user;
        }

        public User AddUserGroups(User user, IEnumerable<Group> userGroups)
        {
            foreach (var group in userGroups) 
            {
                UsersGroups usersGroups = new()
                {
                    User = user,
                    Group = group
                };

                user.Groups.Add(usersGroups);
            }

            return user;
        }

        public User RemoveUserGroup(User user, Group userGroup)
        {
            UsersGroups usersGroups = new()
            {
                User = user,
                Group = userGroup
            };

            user.Groups.Remove(usersGroups);

            return user;
        }

        public User RemoveUserGroups(User user, IEnumerable<Group> userGroups)
        {
            foreach (var group in userGroups)
            {
                UsersGroups usersGroups = new()
                {
                    User = user,
                    Group = group
                };

                user.Groups.Remove(usersGroups);
            }

            return user;
        }
    }
}