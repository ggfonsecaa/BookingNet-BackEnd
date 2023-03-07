using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.BookingFlowAggregate;
using BookingNet.Domain.Aggregates.BookingInventoryAggregate;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Aggregates.InventoryAggregate;
using BookingNet.Domain.Aggregates.MenuAggregate;
using BookingNet.Domain.Aggregates.NotificationAggregate;
using BookingNet.Domain.Aggregates.ReportAggregate;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Aggregates.RoleMenuAggregate;
using BookingNet.Domain.Aggregates.RoleReportAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Aggregates.UserGroupAggregate;
using BookingNet.Domain.Aggregates.UserNotificationAggregate;
using BookingNet.Infraestructure.Mappings.Entities;
using BookingNet.Infraestructure.Mappings.Entities.Relations;

using Microsoft.EntityFrameworkCore;

namespace BookingNet.Infraestructure.DataContext
{
    public class BookingNetDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingStatus> BookingsStatus { get; set; }
        public DbSet<BookingType> BookingTypes { get; set; }
        public DbSet<BookingsFlows> BookingsFlows { get; set; }
        public DbSet<BookingsInventories> BookingsInventories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationsTypes { get; set; }
        public DbSet<NotificationWay> NotificationsWays { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersGroups> UsersGroups { get; set; }
        public DbSet<UsersNotifications> UsersNotifications { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolesMenus> RolesMenus { get; set; }
        public DbSet<RolesReports> RolesReports { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Flow> Flows { get; set; }

        public BookingNetDbContext(DbContextOptions<BookingNetDbContext> options) : base(options) 
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BookingMapping.LoadMapping(modelBuilder);
            BookingStatusMapping.LoadMapping(modelBuilder);
            BookingTypeMapping.LoadMapping(modelBuilder);
            BookingFlowMapping.LoadMapping(modelBuilder);
            BookingInventoryMapping.LoadMapping(modelBuilder);
            InventoryMapping.LoadMapping(modelBuilder);
            ProductMapping.LoadMapping(modelBuilder);
            NotificationMapping.LoadMapping(modelBuilder);
            NotificationTypeMapping.LoadMapping(modelBuilder);
            NotificationWayMapping.LoadMapping(modelBuilder);
            UserMapping.LoadMapping(modelBuilder);
            UserGroupMapping.LoadMapping(modelBuilder);
            UserNotificationMapping.LoadMapping(modelBuilder);
            GroupMapping.LoadMapping(modelBuilder);
            RoleMapping.LoadMapping(modelBuilder);
            RoleMenuMapping.LoadMapping(modelBuilder);
            RoleReportMapping.LoadMapping(modelBuilder);
            MenuMapping.LoadMapping(modelBuilder);
            ReportMapping.LoadMapping(modelBuilder);
            FlowMapping.LoadMapping(modelBuilder);
        }
    }
}