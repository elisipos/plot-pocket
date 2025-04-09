using System.Text.Json.Serialization;

namespace PlotPocket.Server.Models.Responses;

public class TvShowResponse {
    /*
        TODO: 
        Define the model based on the response from the TvShow endpoints from
        The Movie Database's API. Be sure to use the [JsonPropertyName("<property>")] attribue
        above each of your C# properties to ensure that the JSON maps properly to your objects.
    */

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<TvShow> Results { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}


public class TvShow : ApiMediaItem {
    /*
        TODO: 
        Define the model based on the response from the TvShow endpoints from
        The Movie Database's API. Be sure to use the [JsonPropertyName("<property>")] attribue
        above each of your C# properties to ensure that the JSON maps properly to your objects.
    */

    [JsonPropertyName("adult")]
    public bool Adult { get; set; }

    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; }

    [JsonPropertyName("first_air_date")]
    public string FirstAirDate { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("origin_country")]
    public string[] OriginCountry { get; set; }

    [JsonPropertyName("original_name")]
    public string OriginalName { get; set; }
}