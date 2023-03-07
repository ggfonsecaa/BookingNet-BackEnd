using BookingNet.Domain.Aggregates.MenuAggregate;

namespace BookingNet.Domain.Aggregates.RoleMenuAggregate
{
    public partial class RolesMenus
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowVisualize { get; set; }
    }
}