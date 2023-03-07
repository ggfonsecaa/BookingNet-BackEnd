using BookingNet.Domain.Aggregates.ReportAggregate;

namespace BookingNet.Domain.Aggregates.RoleReportAggregate
{
    public partial class RolesReports
    {
        public int ReportId { get; set; }
        public Report Report { get; set; }
    }
}