using BookingNet.Domain.Aggregates.InventoryAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class ProductMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<Product>().ToTable("Products");

            /*Definición de propiedades*/
            modelBuilder.Entity<Product>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(u => u.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Product>().Property(u => u.UnitPrice).IsRequired().HasColumnType("decimal(16, 2)").HasDefaultValue(0);
            modelBuilder.Entity<Product>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Product>().Ignore(u => u.Inventories);

            /*Definición de indices*/
            modelBuilder.Entity<Product>().HasIndex(u => u.Name).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<Product>().HasKey(u => u.Id);
        }
    }
}