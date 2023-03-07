using BookingNet.Domain.Aggregates.NotificationAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities
{
    public class NotificationMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            /*Definicion de tablas*/
            modelBuilder.Entity<Notification>().ToTable("Notifications");

            /*Definición de propiedades*/
            modelBuilder.Entity<Notification>().Property(u => u.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Notification>().Property(u => u.Message).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<Notification>().Property(u => u.NotificationDate).IsRequired().HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Notification>().Property(u => u.NotificationTypeId).IsRequired();
            modelBuilder.Entity<Notification>().Property(u => u.RowVersion).IsRequired().IsConcurrencyToken().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Notification>().Ignore(r => r.NotificationType);
            modelBuilder.Entity<Notification>().Ignore(r => r.Users);

            /*Definición de llaves primarias*/
            modelBuilder.Entity<Notification>().HasKey(u => u.Id);

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<Notification>().HasOne(g => g.NotificationType).WithMany(r => r.Notifications).HasForeignKey(g => g.NotificationTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}