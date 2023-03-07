using BookingNet.Application.Models.NotificationModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.NotificationServices.Queries.Read.Multiple.NotificationTypeQuery
{
    public class NotificationTypeMultipleReadQuery : IRequest<ErrorOr<IEnumerable<NotificationTypeModel>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public NotificationTypeMultipleReadQuery() { }
        public NotificationTypeMultipleReadQuery(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}