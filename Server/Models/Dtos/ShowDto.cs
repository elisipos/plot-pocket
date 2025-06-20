using PlotPocket.Server.Models.Responses;

namespace PlotPocket.Server.Models.Dtos;

public class ShowDto {
  // General Media Item Properties
  public int Id { get; set; }
  public int ShowApiId { get; set; }
  public string Type { get; set; }
  public string Title { get; set; }
  public string? Date { get; set; }
  public string PosterPath { get; set; }


  // Extra Movie Item Properties
  public bool? Adult { get; set; }
  public bool? Video { get; set; }

  public int? Budget { get; set; }
  public string? Homepage { get; set; }
  public int Runtime { get; set; }
  public string? Status { get; set; }
  public string? Tagline { get; set; }
  public string? OriginalTitle { get; set; }
  public string? Overview { get; set; }
  public Genre[]? Genres { get; set; }
  public string? HighResPosterPath { get; set; }

  // Extra TvShow Item Properties
  public string[]? OriginCountry { get; set; }
  public string? OriginalName { get; set; }
  public Person[] CreatedBy { get; set; }
  public bool InProduction { get; set; }
  public string[]? Languages { get; set; }
  public string? LastAirDate { get; set; }
  public Episode LastEpisodeToAir { get; set; }
  public Episode NextEpisodeToAir { get; set; }
  public int? NumberOfEpisodes { get; set; }
  public int? NumberOfSeasons { get; set; }
  public SeasonDto[] Seasons { get; set; }
}