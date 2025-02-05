using AutoMapper;
using AirHandlers.Domain.Entities;
using AirHandlers.GrpcProtos;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeProto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("o"))) // Formato ISO 8601
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString("o"))); // Formato ISO 8601

        CreateMap<RecipeProto, Recipe>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignorar si el ID se genera automáticamente
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.Parse(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTime.Parse(src.EndDate)));
    }
}
