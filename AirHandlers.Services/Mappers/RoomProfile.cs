using AutoMapper;
using AirHandlers.Domain.Entities;
using AirHandlers.GrpcProtos;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomProto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

        CreateMap<RoomProto, Room>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignorar si el ID se genera automáticamente
    }
}
