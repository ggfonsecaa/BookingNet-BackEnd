using BookingNet.Application.Models.FlowsModels;
using BookingNet.Domain.Aggregates.BookingAggregate;

namespace BookingNet.Application.Models.BookingModels
{
    public class BookingHistoryModel
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int FlowId { get; set; }
        public FlowModel Flow { get; set; }
        public DateTime DateStartFlow { get; set; }
        public DateTime? DateEndFlow { get; set; }
    }
}
