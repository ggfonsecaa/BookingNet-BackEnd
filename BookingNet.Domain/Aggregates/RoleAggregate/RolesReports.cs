using BookingNet.Domain.Aggregates.RoleAggregate;

namespace BookingNet.Domain.Aggregates.RoleReportAggregate
{
    public partial class RolesReports
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}