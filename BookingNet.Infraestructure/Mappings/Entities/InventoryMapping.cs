using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.InventoryAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class InventoryMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<Inventory>().ToTable("Inventories");

            /*Definición de propiedades*/
            modelBuilder.Entity<Inventory>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Inventory>().Property(u => u.StockQuantity).IsRequired().HasColumnType("decimal(16, 2)").HasDefaultValue(0);
            modelBuilder.Entity<Inventory>().Property(u => u.ProductId).IsRequired();
            modelBuilder.Entity<Inventory>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Inventory>().Ignore(u => u.Product);
            modelBuilder.Entity<Inventory>().Ignore(r => r.Bookings);

            /*Definición de llaves primarias*/
            modelBuilder.Entity<Inventory>().HasKey(u => u.Id);

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<Inventory>().HasOne(g => g.Product).WithMany(r => r.Inventories).HasForeignKey(g => g.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}