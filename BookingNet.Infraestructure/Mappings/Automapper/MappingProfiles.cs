using AutoMapper;

using BookingNet.Application.Contracts.AuthContracts;
using BookingNet.Application.Models.AuthModels;
using BookingNet.Application.Models.BookingModels;
using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.NotificationModels;
using BookingNet.Application.Models.RoleModels;
using BookingNet.Application.Models.UserModels;
using BookingNet.Application.Services.AuthServices.Login;
using BookingNet.Application.Services.AuthServices.Register;
using BookingNet.Application.Services.UserServices.Queries.Search;
using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.BookingFlowAggregate;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Aggregates.NotificationAggregate;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Aggregates.UserGroupAggregate;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace BookingNet.Infraestructure.Mappings.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        { 
            CreateMap<LoginQuery, LoginModel>().ReverseMap();
            CreateMap<RegisterCommand, RegisterModel>().ReverseMap();

            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<UsersGroupsModel, UsersGroups>().ReverseMap();
            CreateMap<UsersGroupsDetailModel, Group>().ReverseMap();
            CreateMap<GroupRoleModel, Role>().ReverseMap();

            CreateMap<JsonPatchDocument<UserModel>, JsonPatchDocument<User>>().ReverseMap();
            CreateMap<Operation<UserModel>, Operation<User>>().ReverseMap();

            CreateMap<GroupModel, Group>().ReverseMap();
            CreateMap<RoleGroupModel, Role>().ReverseMap();

            CreateMap<JsonPatchDocument<GroupModel>, JsonPatchDocument<Group>>().ReverseMap();
            CreateMap<Operation<GroupModel>, Operation<Group>>().ReverseMap();

            CreateMap<RoleModel, Role>().ReverseMap();

            CreateMap<FlowModel, Flow>().ReverseMap();
            CreateMap<FlowUserModel, User>().ReverseMap();
            CreateMap<FlowParentModel, Flow>().ReverseMap();

            CreateMap<NotificationTypeModel, NotificationType>().ReverseMap();
            CreateMap<NotificationWayModel, NotificationWay>().ReverseMap();

            CreateMap<BookingModel, Booking>().ReverseMap();
            CreateMap<BookingTypeModel, BookingType>().ReverseMap();
            CreateMap<BookingStatusModel, BookingStatus>().ReverseMap();
            CreateMap<BookingHistoryModel, BookingsFlows>().ReverseMap();

            CreateMap<UserSearchQuery, User>().ReverseMap();
        }
    }
}