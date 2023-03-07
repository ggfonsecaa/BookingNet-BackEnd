using BookingNet.Domain.Aggregates.RoleAggregate;

namespace BookingNet.Domain.Aggregates.RoleMenuAggregate
{
    public partial class RolesMenus
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}