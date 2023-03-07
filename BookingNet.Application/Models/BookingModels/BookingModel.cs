using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.UserModels;

namespace BookingNet.Application.Models.BookingModels
{
    public class BookingModel
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int Attendants { get; set; }
        public string? Comments { get; set; }
        public IEnumerable<BookingHistoryModel> Flows { get; set; }
        public int Price { get; set; }
        public int BookingTypeId { get; set; }
        public BookingTypeModel BookingType { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
