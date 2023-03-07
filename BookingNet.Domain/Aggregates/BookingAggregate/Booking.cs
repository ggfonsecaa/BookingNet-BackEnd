using BookingNet.Domain.Aggregates.BookingFlowAggregate;
using BookingNet.Domain.Aggregates.BookingInventoryAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Entities;

namespace BookingNet.Domain.Aggregates.BookingAggregate
{
    public class Booking : BaseEntity, IAggregateRoot
    {
        public DateTime BookingDate { get; set; }
        public int Attendants { get; set; }
        public string? Comments { get; set; }
        public float Price { get; set; }
        public int BookingTypeId { get; set; }
        public BookingType BookingType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<BookingsFlows> Flows { get; set; } = new List<BookingsFlows>();
        public ICollection<BookingsInventories> Inventories { get; set; } = new List<BookingsInventories>();

        public Booking() { }
        public Booking(DateTime bookingDate, int attendants, int price, int bookingTypeId, int userId, string comments = "")
        {
            BookingDate = bookingDate;
            Attendants = attendants;
            Price = price;
            BookingTypeId = bookingTypeId;
            UserId = userId;
            Comments = comments;
        }

        public void UpdateBookingInfo(int price, string comments = null)
        {
            Price = price;
            Comments = comments ?? Comments;
        }

        public void ChangeBookingInfo(DateTime bookingDate, int attendants, int bookingTypeId, string comments = "")
        {
            BookingDate = bookingDate;
            Attendants = attendants;
            BookingTypeId = bookingTypeId;
            Comments = comments ?? Comments;
        }
    }
}