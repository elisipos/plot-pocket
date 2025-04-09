using AutoMapper;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Models.Dtos;

public class TvShowProfile : Profile {
  public TvShowProfile() {
    CreateMap<ShowDto, TvShow>()
      .ForMember(dest => dest.FirstAirDate, opt => opt.MapFrom(src => src.Date))
      .ForMember(dest => dest.Name, opt=> opt.MapFrom(src => src.Title))
      .ReverseMap();
  }
}