using BookingNet.Domain.Aggregates.BookingAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class BookingTypeMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<BookingType>().ToTable("BookingsTypes");

            /*Definición de propiedades*/
            modelBuilder.Entity<BookingType>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<BookingType>().Property(u => u.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<BookingType>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<BookingType>().Ignore(r => r.Bookings);

            /*Definición de indices*/
            modelBuilder.Entity<BookingType>().HasIndex(u => u.Name).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<BookingType>().HasKey(u => u.Id);

            /*Inserción de datos predefinidos*/
            modelBuilder.Entity<BookingType>().HasData(
                    new BookingType { Id = 1, Name = "Matrimonio" },
                    new BookingType { Id = 2, Name = "Primera comunión" },
                    new BookingType { Id = 3, Name = "Bautismo" },
                    new BookingType { Id = 4, Name = "Quince años" },
                    new BookingType { Id = 5, Name = "Evento particular" },
                    new BookingType { Id = 6, Name = "Evento institucional" });
        }
    }
}