using System.Net.Mime;
using AutoMapper;
using Models.Entities;
using Models.Requests;

namespace Services.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ItemCreateUpdateRestDto, Item>();
        
        CreateMap<CourtCreateUpdateRestDto, Court>();
        
        CreateMap<UserInsertRequest, User>();
        CreateMap<UserUpsertRequest, User>();
        
        CreateMap<CountryCreateUpdateRestDto, Country>();
        
        CreateMap<NotificationUpsertRequest, Notification>();
        
        CreateMap<OrderInsertRequest, Order>();

        CreateMap<OrderDetailsUpsertRequest, OrderDetails>();

        CreateMap<ServiceUpsertRequest, Service>();

        CreateMap<ReservationUpsertRequest, Reservation>();
        
        CreateMap<ReviewUpsertRequest, Review>();
        
        CreateMap<ResultCreateUpdateRestDto, Result>();
        
        CreateMap<ImageUpsertRequest, ImageModel>();
        
        CreateMap<NotificationRabbitUpsertDto, NotificationRabbit>();
    }
}