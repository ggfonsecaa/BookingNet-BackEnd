using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Aggregates.RoleMenuAggregate;
using BookingNet.Domain.Aggregates.RoleReportAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.RoleAggregate
{
    public class Role : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<RolesMenus> Menus { get; set; }
        public ICollection<RolesReports> Reports { get; set; }

        public Role() { }
        public Role(string name)
        {
            Name = name;
        }

        public Role UpdateRoleInfo(Role role, string name = null)
        {
            role.Name = name ?? role.Name;

            return role;
        }
    }
}