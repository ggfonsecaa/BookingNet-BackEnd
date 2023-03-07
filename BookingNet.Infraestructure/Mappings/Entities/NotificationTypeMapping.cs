using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.NotificationAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class NotificationTypeMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<NotificationType>().ToTable("NotificationsTypes");

            /*Definición de propiedades*/
            modelBuilder.Entity<NotificationType>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<NotificationType>().Property(u => u.Name).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            modelBuilder.Entity<NotificationType>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<NotificationType>().Ignore(u => u.Notifications);

            /*Definición de indices*/
            modelBuilder.Entity<NotificationType>().HasIndex(u => u.Name).IsUnique();

            /*Definición de llaves primarias*/
            modelBuilder.Entity<NotificationType>().HasKey(u => u.Id);

            /*Inserción de datos predefinidos*/
            modelBuilder.Entity<NotificationType>().HasData(
                    new NotificationType { Id = 1, Name = "Modificación de reserva" },
                    new NotificationType { Id = 2, Name = "Cambio de estado de reserva" });
        }
    }
}