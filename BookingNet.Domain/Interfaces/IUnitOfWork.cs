using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Aggregates.InventoryAggregate;
using BookingNet.Domain.Aggregates.MenuAggregate;
using BookingNet.Domain.Aggregates.NotificationAggregate;
using BookingNet.Domain.Aggregates.ReportAggregate;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;

namespace BookingNet.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Group> GroupRepository { get; }
        public IGenericRepository<Role> RoleRepository { get; }
        public IGenericRepository<Menu> MenuRepository { get; }
        public IGenericRepository<Report> ReportRepository { get; }
        public IGenericRepository<Flow> FlowRepository { get; }
        public IGenericRepository<Inventory> InventoryRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Booking> BookingRepository { get; }
        public IGenericRepository<BookingType> BookingTypeRepository { get; }
        public IGenericRepository<BookingStatus> BookingStatusRepository { get; }
        public IGenericRepository<Notification> NotificationRepository { get; }
        public IGenericRepository<NotificationType> NotificationTypeRepository { get; }
        public IGenericRepository<NotificationWay> NotificationWayRepository { get; }
        public void SaveChanges();
        public Task<int> SaveChangesAsync();
    }
}