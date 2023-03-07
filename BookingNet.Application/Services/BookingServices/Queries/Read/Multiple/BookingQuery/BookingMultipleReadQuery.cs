using BookingNet.Application.Models.BookingModels;
using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.RoleModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Multiple.BookingQuery
{
    public class BookingMultipleReadQuery : IRequest<ErrorOr<IEnumerable<BookingModel>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BookingMultipleReadQuery() { }
        public BookingMultipleReadQuery(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}