using AutoMapper;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Models.Dtos;

public class SeasonProfile : Profile {
  public SeasonProfile() {
    CreateMap<SeasonDto, Season>()
      .ReverseMap();
  }
}