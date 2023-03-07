using BookingNet.Domain.Aggregates.UserGroupAggregate;
using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities.Relations
{
    public class UserGroupMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersGroups>().ToTable("UsersGroups");

            /*Definición de propiedades*/
            modelBuilder.Entity<UsersGroups>().Property(ug => ug.UserId).IsRequired();
            modelBuilder.Entity<UsersGroups>().Property(ug => ug.GroupId).IsRequired();
            //modelBuilder.Entity<UsersGroups>().Property(ug => ug.Default).IsRequired().HasDefaultValue(0);
            modelBuilder.Entity<UsersGroups>().Ignore(ug => ug.User);
            modelBuilder.Entity<UsersGroups>().Ignore(ug => ug.Group);

            /*Definición de llaves primarias*/
            modelBuilder.Entity<UsersGroups>().HasKey(t => new { t.UserId, t.GroupId });

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<UsersGroups>().HasOne(ug => ug.User).WithMany(u => u.Groups).HasForeignKey(ug => ug.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UsersGroups>().HasOne(ug => ug.Group).WithMany(g => g.Users).HasForeignKey(ug => ug.GroupId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}