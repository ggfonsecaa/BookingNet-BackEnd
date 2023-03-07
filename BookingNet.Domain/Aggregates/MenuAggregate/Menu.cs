using BookingNet.Domain.Aggregates.RoleMenuAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.MenuAggregate
{
    public class Menu : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public ICollection<RolesMenus> Roles { get; set; } = new List<RolesMenus>();
    }
}