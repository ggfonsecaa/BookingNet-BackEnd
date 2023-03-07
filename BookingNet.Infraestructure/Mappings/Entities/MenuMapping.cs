using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.MenuAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class MenuMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<Menu>().ToTable("Menus");

            /*Definición de propiedades*/
            modelBuilder.Entity<Menu>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Menu>().Property(u => u.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Menu>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Menu>().Ignore(r => r.Roles);

            /*Definición de indices*/
            modelBuilder.Entity<Menu>().HasIndex(u => u.Name).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<Menu>().HasKey(u => u.Id);

            /*Inserción de datos predefinidos*/
            //modelBuilder.Entity<Menu>().HasData(
            //        new { Name = "Inicio" },
            //        new { Name = "Servicios" },
            //        new { Name = "Inventarios" },
            //        new { Name = "Comunicaciones" },
            //        new { Name = "Consultas" },
            //        new { Name = "Configuración" }
            //    );
        }
    }
}