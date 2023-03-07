using BookingNet.Domain.Aggregates.UserAggregate;

namespace BookingNet.Domain.Aggregates.UserGroupAggregate
{
    public partial class UsersGroups
    { 
        public int UserId { get; set; }
        public User User { get; set; }
    }
}