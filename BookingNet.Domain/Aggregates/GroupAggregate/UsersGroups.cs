using BookingNet.Domain.Aggregates.GroupAggregate;

namespace BookingNet.Domain.Aggregates.UserGroupAggregate
{
    public partial class UsersGroups
    { 
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}