using BookingNet.Application.Models.NotificationModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Multiple.BookingStatusQuery
{
    public class NotificationWayMultipleReadQuery : IRequest<ErrorOr<IEnumerable<NotificationWayModel>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public NotificationWayMultipleReadQuery() { }
        public NotificationWayMultipleReadQuery(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}