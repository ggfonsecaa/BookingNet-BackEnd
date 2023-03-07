using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Aggregates.InventoryAggregate;
using BookingNet.Domain.Aggregates.MenuAggregate;
using BookingNet.Domain.Aggregates.NotificationAggregate;
using BookingNet.Domain.Aggregates.ReportAggregate;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Interfaces;
using BookingNet.Infraestructure.DataContext;

namespace BookingNet.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed = false;
        private readonly BookingNetDbContext _dbContext;
        private IGenericRepository<User> _userRepository { get; set; }
        private IGenericRepository<Group> _groupRepository { get; set; }
        private IGenericRepository<Role> _roleRepository { get; set; }
        private IGenericRepository<Menu> _menuRepository { get; set; }
        private IGenericRepository<Report> _reportRepository { get; set; }
        private IGenericRepository<Flow> _flowRepository { get; set; }
        private IGenericRepository<Inventory> _inventoryRepository { get; set; }
        private IGenericRepository<Product> _productRepository { get; set; }
        private IGenericRepository<Booking> _bookingRepository { get; set; }
        private IGenericRepository<BookingType> _bookingTypeRepository { get; set; }
        private IGenericRepository<BookingStatus> _bookingStatusRepository { get; set; }
        private IGenericRepository<Notification> _notificationRepository { get; set; }
        private IGenericRepository<NotificationType> _notificationTypeRepository { get; set; }
        private IGenericRepository<NotificationWay> _notificationWayRepository { get; set; }

        public IGenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_dbContext);
                }

                return _userRepository;
            }
        }

        public IGenericRepository<Group> GroupRepository
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new GenericRepository<Group>(_dbContext);
                }

                return _groupRepository;
            }
        }

        public IGenericRepository<Role> RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new GenericRepository<Role>(_dbContext);
                }

                return _roleRepository;
            }
        }

        public IGenericRepository<Menu> MenuRepository
        {
            get
            {
                if (_menuRepository == null)
                {
                    _menuRepository = new GenericRepository<Menu>(_dbContext);
                }

                return _menuRepository;
            }
        }

        public IGenericRepository<Report> ReportRepository
        {
            get
            {
                if (_reportRepository == null)
                {
                    _reportRepository = new GenericRepository<Report>(_dbContext);
                }

                return _reportRepository;
            }
        }

        public IGenericRepository<Flow> FlowRepository
        {
            get
            {
                if (_flowRepository == null)
                {
                    _flowRepository = new GenericRepository<Flow>(_dbContext);
                }

                return _flowRepository;
            }
        }

        public IGenericRepository<Inventory> InventoryRepository
        {
            get
            {
                if (_inventoryRepository == null)
                {
                    _inventoryRepository = new GenericRepository<Inventory>(_dbContext);
                }

                return _inventoryRepository;
            }
        }

        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new GenericRepository<Product>(_dbContext);
                }

                return _productRepository;
            }
        }

        public IGenericRepository<Booking> BookingRepository
        {
            get
            {
                if (_bookingRepository == null)
                {
                    _bookingRepository = new GenericRepository<Booking>(_dbContext);
                }

                return _bookingRepository;
            }
        }

        public IGenericRepository<BookingType> BookingTypeRepository
        {
            get
            {
                if (_bookingTypeRepository == null)
                {
                    _bookingTypeRepository = new GenericRepository<BookingType>(_dbContext);
                }

                return _bookingTypeRepository;
            }
        }

        public IGenericRepository<BookingStatus> BookingStatusRepository
        {
            get
            {
                if (_bookingStatusRepository == null)
                {
                    _bookingStatusRepository = new GenericRepository<BookingStatus>(_dbContext);
                }

                return _bookingStatusRepository;
            }
        }

        public IGenericRepository<Notification> NotificationRepository
        {
            get
            {
                if (_notificationRepository == null)
                {
                    _notificationRepository = new GenericRepository<Notification>(_dbContext);
                }

                return _notificationRepository;
            }
        }

        public IGenericRepository<NotificationType> NotificationTypeRepository
        {
            get
            {
                if (_notificationTypeRepository == null)
                {
                    _notificationTypeRepository = new GenericRepository<NotificationType>(_dbContext);
                }

                return _notificationTypeRepository;
            }
        }

        public IGenericRepository<NotificationWay> NotificationWayRepository
        {
            get
            {
                if (_notificationWayRepository == null)
                {
                    _notificationWayRepository = new GenericRepository<NotificationWay>(_dbContext);
                }

                return _notificationWayRepository;
            }
        }

        public UnitOfWork(BookingNetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}