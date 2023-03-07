using BookingNet.Domain.Aggregates.GroupAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class GroupMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<Group>().ToTable("Groups");

            /*Definición de propiedades*/
            modelBuilder.Entity<Group>().Property(g => g.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Group>().Property(g => g.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<Group>().Property(g => g.RoleId).IsRequired();
            modelBuilder.Entity<Group>().Property(g => g.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Group>().Ignore(g => g.Role);
            modelBuilder.Entity<Group>().Ignore(g => g.Users);

            /*Definición de indices*/
            modelBuilder.Entity<Group>().HasIndex(g => new { g.Name }).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<Group>().HasKey(g => g.Id);

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<Group>().HasOne(g => g.Role).WithMany(r => r.Groups).HasForeignKey(g => g.RoleId).OnDelete(DeleteBehavior.Restrict);

            /*Inserción de datos predefinidos*/
            modelBuilder.Entity<Group>().HasData(
                    new Group { Id = 1, Name = "Administradores del sistema", RoleId = 1 },
                    new Group { Id = 2, Name = "Usuarios del sistema", RoleId = 2 },
                    new Group { Id = 3, Name = "Clientes", RoleId = 3 });
        }
    }
}