using AutoMapper;
using PlotPocket.Server.Models.Responses;
using PlotPocket.Server.Models.Dtos;
using PlotPocket.Server.Models.Entities;

public class ShowProfile : Profile {
  public ShowProfile() {
    CreateMap<ShowDto, Show>()
      .ReverseMap();
  }
}