using BookingNet.Domain.Aggregates.BookingAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class BookingStatusMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<BookingStatus>().ToTable("BookingsStatus");

            /*Definición de propiedades*/
            modelBuilder.Entity<BookingStatus>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<BookingStatus>().Property(u => u.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<BookingStatus>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<BookingStatus>().Ignore(r => r.Flows);

            /*Definición de indices*/
            modelBuilder.Entity<BookingStatus>().HasIndex(u => u.Name).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<BookingStatus>().HasKey(u => u.Id);

            /*Inserción de datos predefinidos*/
            modelBuilder.Entity<BookingStatus>().HasData(
                    new BookingStatus { Id = 1, Name = "Creada" },
                    new BookingStatus { Id = 2, Name = "Aceptada" },
                    new BookingStatus { Id = 3, Name = "Confirmada" },
                    new BookingStatus { Id = 4, Name = "Pagada" },
                    new BookingStatus { Id = 5, Name = "Aprobada" },
                    new BookingStatus { Id = 6, Name = "Realizada" },
                    new BookingStatus { Id = 7, Name = "Rechazada" });
        }
    }
}