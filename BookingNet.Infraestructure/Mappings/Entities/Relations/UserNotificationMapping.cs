using BookingNet.Domain.Aggregates.UserNotificationAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities.Relations
{
    public class UserNotificationMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersNotifications>().ToTable("UsersNotifications");

            /*Definición de propiedades*/
            modelBuilder.Entity<UsersNotifications>().Property(un => un.UserId).IsRequired();
            modelBuilder.Entity<UsersNotifications>().Property(un => un.NotificationId).IsRequired();
            modelBuilder.Entity<UsersNotifications>().Ignore(un => un.User);
            modelBuilder.Entity<UsersNotifications>().Ignore(un => un.Notification);

            /*Definición de llaves primarias*/
            modelBuilder.Entity<UsersNotifications>().HasKey(un => new { un.UserId, un.NotificationId });

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<UsersNotifications>().HasOne(un => un.User).WithMany(u => u.Notifications).HasForeignKey(un => un.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UsersNotifications>().HasOne(un => un.Notification).WithMany(n => n.Users).HasForeignKey(un => un.NotificationId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}