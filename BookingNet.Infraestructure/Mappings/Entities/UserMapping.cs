using BookingNet.Domain.Aggregates.UserAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class UserMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<User>().ToTable("Users");

            /*Definición de propiedades*/
            modelBuilder.Entity<User>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<User>().Property(u => u.UserEmail).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.PassWord).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.NotificationWayId).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<User>().Ignore(r => r.Groups);
            modelBuilder.Entity<User>().Ignore(r => r.Flows);
            modelBuilder.Entity<User>().Ignore(r => r.Bookings);

            /*Definición de indices*/
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.UserEmail).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<User>().HasOne(g => g.NotificationWay).WithMany(r => r.Users).HasForeignKey(g => g.NotificationWayId).OnDelete(DeleteBehavior.Restrict);

            /*Inserción de datos predefinidos*/
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, UserName = "Admin", UserEmail = "admin@admin.com", PassWord = "", NotificationWayId = 1 });
        }
    }
}