using BookingNet.Domain.Aggregates.BookingInventoryAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities.Relations
{
    public class BookingInventoryMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingsInventories>().ToTable("BookingsInventories");

            /*Definición de propiedades*/
            modelBuilder.Entity<BookingsInventories>().Property(entity => entity.BookingId).IsRequired();
            modelBuilder.Entity<BookingsInventories>().Property(entity => entity.InventoryId).IsRequired();
            modelBuilder.Entity<BookingsInventories>().Property(entity => entity.Quantity).IsRequired().HasColumnType("decimal(16, 2)").HasDefaultValue(0);
            modelBuilder.Entity<BookingsInventories>().Ignore(entity => entity.Booking);
            modelBuilder.Entity<BookingsInventories>().Ignore(entity => entity.Inventory);

            /*Definición de llaves primarias*/
            modelBuilder.Entity<BookingsInventories>().HasKey(entity => new { entity.BookingId, entity.InventoryId });

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<BookingsInventories>().HasOne(entity => entity.Booking).WithMany(related => related.Inventories).HasForeignKey(entity => entity.BookingId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BookingsInventories>().HasOne(entity => entity.Inventory).WithMany(related => related.Bookings).HasForeignKey(entity => entity.InventoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}