using AutoMapper;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Models.Dtos;

public class MovieProfile : Profile {
  public MovieProfile() {
    CreateMap<ShowDto, Movie>()
      .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.Date))
      .ReverseMap();
  }
}