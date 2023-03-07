using BookingNet.Domain.Aggregates.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using System.Data;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class RoleMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<Role>().ToTable("Roles");

            /*Definición de propiedades*/
            modelBuilder.Entity<Role>().Property(r => r.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Role>().Property(r => r.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Role>().Ignore(r => r.Groups);

            /*Definición de indices*/
            modelBuilder.Entity<Role>().HasIndex(r => new { r.Name }).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<Role>().HasKey(r => r.Id);

            /*Inserción de datos predefinidos*/
            modelBuilder.Entity<Role>().HasData(
                    new Role { Id = 1, Name = "Administradores" },
                    new Role { Id = 2, Name = "Usuarios" },
                    new Role { Id = 3, Name = "Clientes" });
        }
    }
}