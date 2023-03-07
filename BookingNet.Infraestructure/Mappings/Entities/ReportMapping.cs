using BookingNet.Domain.Aggregates.ReportAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class ReportMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<Report>().ToTable("Reports");

            /*Definición de propiedades*/
            modelBuilder.Entity<Report>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Report>().Property(u => u.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Report>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Report>().Ignore(u => u.Roles);

            /*Definición de indices*/
            modelBuilder.Entity<Report>().HasIndex(u => u.Name).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<Report>().HasKey(u => u.Id);
        }
    }
}