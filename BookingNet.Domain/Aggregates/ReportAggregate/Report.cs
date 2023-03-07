using BookingNet.Domain.Aggregates.RoleReportAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.ReportAggregate
{
    public class Report : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public ICollection<RolesReports> Roles { get; set; } = new List<RolesReports>();
    }
}