using BookingNet.Domain.Aggregates.RoleMenuAggregate;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.Mappings.Entities.Relations
{
    public class RoleMenuMapping
    {
        public static void LoadMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesMenus>().ToTable("RolesMenus");

            /*Definición de propiedades*/
            modelBuilder.Entity<RolesMenus>().Property(entity => entity.RoleId).IsRequired();
            modelBuilder.Entity<RolesMenus>().Property(entity => entity.MenuId).IsRequired();
            modelBuilder.Entity<RolesMenus>().Property(entity => entity.AllowAdd).HasDefaultValue(false);
            modelBuilder.Entity<RolesMenus>().Property(entity => entity.AllowEdit).HasDefaultValue(false);
            modelBuilder.Entity<RolesMenus>().Property(entity => entity.AllowDelete).HasDefaultValue(false);
            modelBuilder.Entity<RolesMenus>().Property(entity => entity.AllowVisualize).HasDefaultValue(false);
            modelBuilder.Entity<RolesMenus>().Ignore(entity => entity.Role);
            modelBuilder.Entity<RolesMenus>().Ignore(entity => entity.Menu);

            /*Definición de llaves primarias*/
            modelBuilder.Entity<RolesMenus>().HasKey(entity => new { entity.RoleId, entity.MenuId});

            /*Definicion de relacion entre entidades (Llaves foraneas)*/
            modelBuilder.Entity<RolesMenus>().HasOne(entity => entity.Role).WithMany(related => related.Menus).HasForeignKey(entity => entity.RoleId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RolesMenus>().HasOne(entity => entity.Menu).WithMany(related => related.Roles).HasForeignKey(entity => entity.MenuId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}