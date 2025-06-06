using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PlotPocket.Server.Models.Responses;

public class MovieResponse {

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<Movie> Results { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}

public class Date {

    [JsonPropertyName("maximum")]
    public string Maximum { get; set; }

    [JsonPropertyName("minimum")]
    public string Minimum { get; set; }

}

    // For Movie DETAILS only, will not work with general movie response.
public class Genre {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Movie : ApiMediaItem {
  
    [JsonPropertyName("adult")]
    public bool Adult { get; set; }

    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; }

    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("video")]
    public bool Video { get; set; }

    // Specific Movie Details

    [JsonPropertyName("budget")]
    public int Budget { get; set; }

    [JsonPropertyName("homepage")]
    public string Homepage { get; set; }

    [JsonPropertyName("runtime")]
    public int Runtime { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("tagline")]
    public string Tagline { get; set; }

    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    [JsonPropertyName("origin_country")]
    public string[]? OriginCountry { get; set; }

    [JsonPropertyName("genres")]
    public Genre[] Genres { get; set; }
}