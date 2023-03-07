using BookingNet.Domain.Aggregates.RoleReportAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities.Relations
{
    public class RoleReportMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesReports>().ToTable("RolesReports");

            /*Definición de propiedades*/
            modelBuilder.Entity<RolesReports>().Property(entity => entity.RoleId).IsRequired();
            modelBuilder.Entity<RolesReports>().Property(entity => entity.ReportId).IsRequired();
            modelBuilder.Entity<RolesReports>().Ignore(entity => entity.Role);
            modelBuilder.Entity<RolesReports>().Ignore(entity => entity.Report);

            /*Definición de llaves primarias*/
            modelBuilder.Entity<RolesReports>().HasKey(entity => new { entity.RoleId, entity.ReportId});

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<RolesReports>().HasOne(entity => entity.Role).WithMany(related => related.Reports).HasForeignKey(entity => entity.RoleId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RolesReports>().HasOne(entity => entity.Report).WithMany(related => related.Roles).HasForeignKey(entity => entity.ReportId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}