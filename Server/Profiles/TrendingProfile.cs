using AutoMapper;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Models.Dtos;

public class TrendingProfile : Profile {
  public TrendingProfile() {
    CreateMap<ShowDto, Trending>().ReverseMap();
  }
}