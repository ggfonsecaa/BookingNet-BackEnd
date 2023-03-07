using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.NotificationAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class NotificationWayMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<NotificationWay>().ToTable("NotificationsWays");

            /*Definición de propiedades*/
            modelBuilder.Entity<NotificationWay>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<NotificationWay>().Property(u => u.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<NotificationWay>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<NotificationWay>().Ignore(u => u.Users);

            /*Definición de indices*/
            modelBuilder.Entity<NotificationWay>().HasIndex(u => u.Name).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<NotificationWay>().HasKey(u => u.Id);

            /*Inserción de datos predefinidos*/
            modelBuilder.Entity<NotificationWay>().HasData(
                    new NotificationWay { Id = 1, Name = "Correo electrónico" },
                    new NotificationWay { Id = 2, Name = "Mensaje de texto" });
        }
    }
}