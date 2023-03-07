using BookingNet.Domain.Aggregates.BookingAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class BookingMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<Booking>().ToTable("Bookings");

            /*Definición de propiedades*/
            modelBuilder.Entity<Booking>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Booking>().Property(u => u.BookingDate).IsRequired().HasColumnType("datetime");
            modelBuilder.Entity<Booking>().Property(u => u.Attendants).IsRequired().HasColumnType("smallint");
            modelBuilder.Entity<Booking>().Property(u => u.Comments).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<Booking>().Property(u => u.Price).IsRequired().HasColumnType("decimal(16, 2)").HasDefaultValue(0);
            modelBuilder.Entity<Booking>().Property(u => u.BookingTypeId).IsRequired();
            modelBuilder.Entity<Booking>().Property(u => u.UserId).IsRequired();
            modelBuilder.Entity<Booking>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Booking>().Ignore(r => r.BookingType);
            modelBuilder.Entity<Booking>().Ignore(r => r.Flows);
            modelBuilder.Entity<Booking>().Ignore(r => r.User);

            /*Definición de llaves primarias*/
            modelBuilder.Entity<Booking>().HasKey(u => u.Id);

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<Booking>().HasOne(g => g.BookingType).WithMany(r => r.Bookings).HasForeignKey(g => g.BookingTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Booking>().HasOne(g => g.User).WithMany(r => r.Bookings).HasForeignKey(g => g.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}