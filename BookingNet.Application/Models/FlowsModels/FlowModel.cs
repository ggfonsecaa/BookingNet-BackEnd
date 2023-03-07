using BookingNet.Application.Models.BookingModels;

namespace BookingNet.Application.Models.FlowsModels
{
    public class FlowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasPreviousFlow { get; set; }
        public int FlowId { get; set; }
        public FlowParentModel ParentFlow { get; set; }
        public int UserId { get; set; }
        public FlowUserModel User { get; set; }
        public int BookingStatusId { get; set; }
        public BookingStatusModel BookingStatus { get; set; }
    }
}