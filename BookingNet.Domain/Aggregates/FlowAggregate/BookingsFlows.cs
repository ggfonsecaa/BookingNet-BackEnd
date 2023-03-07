using BookingNet.Domain.Aggregates.FlowAggregate;

namespace BookingNet.Domain.Aggregates.BookingFlowAggregate
{
    public partial class BookingsFlows
    {
        public int FlowId { get; set; }
        public Flow Flow { get; set; }
        public DateTime DateStartFlow { get; set; }
        public DateTime? DateEndFlow { get; set; }

        public void StartFlow() {
            DateEndFlow = DateTime.Now;
        }
    }
}