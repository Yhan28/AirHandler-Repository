using AutoMapper;
using AirHandlers.Domain.Entities;
using AirHandlers.GrpcProtos;

public class AirHandlerProfile : Profile
{
    public AirHandlerProfile()
    {
        CreateMap<AirHandlerEntity, AirHandlerProto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.FilterChangeDate, opt => opt.MapFrom(src => src.FilterChangeDate.ToString("o"))) // Formato ISO 8601
            .ForMember(dest => dest.ServedRooms, opt => opt.MapFrom(src => src.ServedRooms)) // Mapeo de la relación one-to-many
            .ForMember(dest => dest.AssociatedRecipes, opt => opt.MapFrom(src => src.AssociatedRecipes)); // Mapeo de la relación many-to-many

        CreateMap<AirHandlerProto, AirHandlerEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignorar si el ID se genera automáticamente
            .ForMember(dest => dest.FilterChangeDate, opt => opt.MapFrom(src => DateTime.Parse(src.FilterChangeDate)))
            .ForMember(dest => dest.ServedRooms, opt => opt.Ignore()) // Ignorar si se maneja de otra forma
            .ForMember(dest => dest.AssociatedRecipes, opt => opt.Ignore()); // Ignorar si se maneja de otra forma
    }
}