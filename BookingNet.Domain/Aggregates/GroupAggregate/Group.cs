using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Aggregates.UserGroupAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.GroupAggregate
{
    public class Group : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<UsersGroups> Users { get; set; }

        public Group() { }

        public Group(string name, int roleId = 0)
        {
            Name = name;
            RoleId = roleId;
        }

        public Group UpdateGroupInfo(Group group, string name = null, int role = 0)
        {
            group.Name = name ?? group.Name;
            group.RoleId = role == 0 ? group.RoleId : role;

            return group;
        }
    }
}